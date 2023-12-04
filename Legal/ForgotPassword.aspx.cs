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
	protected void SendWhatsappOTP(string strMobileNo, string strMessage)
    {
        try
        {
            strMobileNo = "91" + strMobileNo.Replace(",", "|91");
            strMobileNo = strMobileNo.Replace(" ", "");
            //string strMessage = strOTP + " is your OTP For Reset Password!";
            string strSignature = " \nकानूनी मामले प्रबंधन पोर्टल,\nस्कूल शिक्षा विभाग, म.प्र. शासन";
            string strUrl = "http://20.193.139.227/msg/public/send-message?api_key=naf7swVOp5t7wsreLQEwhayKQa9TNb&sender=919516879109&number=" + strMobileNo + "&message=" + strMessage + "\n" + strSignature;
            // Create a request object
            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream s = (Stream)response.GetResponseStream();
            StreamReader readStream = new StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

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
                    string RPurl = (url.Contains("localhost") ? "http://localhost:54327/" : "https://dpi.legalmonitoring.in/") + "ResetPassword.aspx?" + ConvertText_SHA512_And_Salt("num=" + ds.Tables[0].Rows[0]["userid"].ToString());

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
						
						 // Send OTP on  Whatsapp

                        string strMobNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        string strWMBody = "Dear " + ds.Tables[0].Rows[0]["Empname"].ToString() + " (" + ds.Tables[0].Rows[0]["UserName"].ToString() + ")\n \nअपना पासवर्ड रीसेट करने के लिए कृपया नीचे दिए गए लिंक पर क्लिक करें। सुरक्षा कारणों से लिंक को किसी अन्य के साथ साझा न करें। \n \n " + RPurl;
                        if (strMobNo != string.Empty || strMobNo != "")
                        {
                            SendWhatsappOTP(strMobNo, strWMBody);
                        }

                        obj.ByTextQuery("insert into tblManageResetPassword(UserId, UserEmail, Isactive,ResetPasswordLink) values(" + ds.Tables[0].Rows[0]["userid"].ToString() + ",'" + ds.Tables[0].Rows[0]["UserEmail"].ToString() + "',1, '" + RPurl.ToString() + "')");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Forget Password Link Successfully Sent on your Registerd Email ID. Do Not Share the link with Others.');", true);
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