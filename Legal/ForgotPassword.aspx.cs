using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_ForgotPassword : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Request.Cookies.Clear();
        }
    }
    private string ConvertText_SHA512_And_Salt(string Text)
    {
        return obj.SHA512_HASH(Text);
    }
    protected void btnForgotPassword_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtUserEmail.Text.Trim()))
            {
                string EmailBodyHTMLPath = Server.MapPath("~/HtmlTemplete/ResetPasswordEmail.html");
                System.IO.StreamReader objReader;
                //objReader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\intel\\main.html");
                objReader = new StreamReader(EmailBodyHTMLPath);
                string content = objReader.ReadToEnd();
                objReader.Close();
                DataSet ds = new DataSet();
                ds = obj.ByDataSet("select * from tblUserMaster where UserEmail='" + txtUserEmail.Text.Trim() + "'  order by UserId desc");

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    string RPurl = (url.Contains("localhost") ? "http://localhost:52673/" : "https://phe.legalmonitoring.in/") + "ResetPassword.aspx?" + ConvertText_SHA512_And_Salt("num=" + ds.Tables[0].Rows[0]["userid"].ToString());

                    content = content
                       .Replace("{{ResetPasswordLink}}", RPurl)
                       .Replace("{{UserName}}", ds.Tables[0].Rows[0]["UserName"].ToString())
                       .Replace("{{EmployeeName}}", ds.Tables[0].Rows[0]["Empname"].ToString());
                    //  string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
                    SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                    using (MailMessage mm = new MailMessage(smtpSection.From, txtUserEmail.Text.Trim()))
                    {
                        mm.Subject = "Forgot Password";
                        mm.Body = content;
                        mm.IsBodyHtml = true;
                        // mm.CC.Add(CC);
                        //////  mm.Attachments.Add(new Attachment(new MemoryStream(pdfBuffer), "OIC(" + ObjEC.OIC_Name + "_Mapped_With_Case_No_)" + ObjEC.Case_Number + ".pdf"));
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = smtpSection.Network.Host;
                        smtp.EnableSsl = smtpSection.Network.EnableSsl;
                        NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                        smtp.Credentials = networkCred;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Send(mm);

                        obj.ByTextQuery("insert into tblManageResetPassword(UserId, UserEmail, Isactive,ResetPasswordLink) values(" + ds.Tables[0].Rows[0]["userid"].ToString() + ",'" + ds.Tables[0].Rows[0]["UserEmail"].ToString() + "',1, '" + RPurl.ToString() + "')");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Email sent Please Check your mail');", true);
                    }
                }
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Please Enter Valid Email Address');", true);
                //}
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
}