using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_UserRegistration : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                BIndOfficeType();
                BindUserDetails();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }

    protected void BindUserDetails()
    {
        try
        {
            grdUserDetails.DataSource = null;
            grdUserDetails.DataBind();
            ds = obj.ByProcedure("USP_Select_UserMaster", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdUserDetails.DataSource = ds;
                grdUserDetails.DataBind();
            }
            else
            {
                grdUserDetails.DataSource = ds;
                grdUserDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void BIndOfficeType()
    {
        try
        {
            ddlofficetype.Items.Clear();
            ds = obj.ByDataSet("select OfficeType_Id,OfficeType_Name from tblOfficeTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlofficetype.DataTextField = "OfficeType_Name";
                ddlofficetype.DataValueField = "OfficeType_Id";
                ddlofficetype.DataSource = ds;
                ddlofficetype.DataBind();
            }
            ddlofficetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    public void GetRandomText()
    {
        StringBuilder randomText = new StringBuilder();
        string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz!@#$%&*~";
        Random r = new Random();
        for (int j = 0; j <= 10; j++)
        { randomText.Append(alphabets[r.Next(alphabets.Length)]); }
        ViewState["RandomText"] = obj.Encrypt(randomText.ToString());
    }
    private string ConvertText_SHA512_And_Salt(string Text)
    {
        return obj.SHA512_HASH(Text);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (ViewState["UPAGETOKEN"].ToString() == Session["PAGETOKEN"].ToString())
                {

                    if (btnSave.Text == "Save")
                    {
                        string email = txtUserEmail.Text.Trim();

                        //ds = obj.ByDataSet("select * from tblUserMaster where UserEmail='" + txtUserEmail.Text.Trim() + "'  order by UserId desc");

                        string password = ConvertText_SHA512_And_Salt(txtPassword.Text.Trim());

                        ds = obj.ByProcedure("USP_Insert_UserMaster", new string[] { "EMPName", "UserEmail", "UserName", "UserPassword", "MobileNo", "OfficeType_Id", "Office_Id", "UserType_Id", "CreatedBy", "CreatedByIP" }
                            , new string[] { txtEmpployeeName.Text.Trim(), txtUserEmail.Text.Trim(), txtUserName.Text.Trim(), password, txtMobileNo.Text.Trim(), ddlofficetype.SelectedValue, ddlOfficeName.SelectedValue, ddlUsertype.SelectedValue, "1", obj.GetLocalIPAddress() }, "dataset");

                    }
                    else if (btnSave.Text == "Update" && ViewState["UserId"].ToString() != "" && ViewState["UserId"].ToString() != null)
                    {
                        ds = obj.ByProcedure("USP_Update_UserMaster", new string[] { "EMPName", "Designation_Id", "MobileNo", "OfficeType_Id", "Office_Id", "LastupdatedBy", "LastUpdatedByIP", "UserId" }
                        , new string[] { txtEmpployeeName.Text.Trim(), ddlUsertype.SelectedValue, txtMobileNo.Text.Trim(), ddlofficetype.SelectedValue, ddlOfficeName.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["UserId"].ToString() }, "dataset");
                    }

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            string AdminEmail = Session["UserEmail"].ToString();
                            sendmail(AdminEmail);
                            ddlUsertype.ClearSelection();
                            ddlOfficeName.ClearSelection();
                            txtEmpployeeName.Text = "";
                            ddlofficetype.ClearSelection();
                            txtMobileNo.Text = "";
                            txtUserEmail.Text = "";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            UsrConfirmPass_Div.Visible = true;
                            UsrPassword_Div.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        }
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                    }

                    Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                btnSave.Text = "Save";
                BindUserDetails();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlofficetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlofficetype.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataTextField = "OfficeName";
                ddlOfficeName.DataValueField = "Office_Id";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlUsertype.DataTextField = "UserType_Name";
                ddlUsertype.DataValueField = "UserType_Id";
                ddlUsertype.DataSource = ds.Tables[1];
                ddlUsertype.DataBind();
            }
            ddlUsertype.Items.Insert(0, new ListItem("Select", "0"));
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    private void sendmail(string CC)
    {
        try
        {
            string EmailBodyHTMLPath = Server.MapPath("~/HtmlTemplete/UserCreateEmail.html");
            System.IO.StreamReader objReader;
            //objReader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\intel\\main.html");
            objReader = new StreamReader(EmailBodyHTMLPath);
            string content = objReader.ReadToEnd();
            objReader.Close();
            content = content
               .Replace("{{EmployeeName}}", txtEmpployeeName.Text.Trim())
               .Replace("{{UserName}}", txtUserName.Text.Trim())
               .Replace("{{UserPassword}}", txtPassword.Text.Trim());
            //  string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage mm = new MailMessage(smtpSection.From, txtUserEmail.Text.Trim()))
            {
                #region

                //// create the HTML to PDF converter
                //HiQPdf.HtmlToPdf htmlToPdfConverter = new HiQPdf.HtmlToPdf();


                //// htmlToPdfConverter.BrowserWidth = ;


                ////if (textBoxBrowserHeight.Text.Length > 0)
                ////    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);


                ////htmlToPdfConverter.HtmlLoadedTimeout = ;

                //// set PDF page size and orientation
                //htmlToPdfConverter.Document.PageSize = HiQPdf.PdfPageSize.A4;
                //htmlToPdfConverter.Document.PageOrientation = HiQPdf.PdfPageOrientation.Portrait;

                //// set the PDF standard used by the document
                //htmlToPdfConverter.Document.PdfStandard = HiQPdf.PdfStandard.PdfA;

                //// set PDF page margins
                //htmlToPdfConverter.Document.Margins = new HiQPdf.PdfMargins(10);

                //// set whether to embed the true type font in PDF
                //htmlToPdfConverter.Document.FontEmbedding = true;

                //// set triggering mode; for WaitTime mode set the wait time before convert
                ////switch (dropDownListTriggeringMode.SelectedValue)
                ////{
                ////    case "Auto":
                //htmlToPdfConverter.TriggerMode = HiQPdf.ConversionTriggerMode.Auto;
                ////        break;
                ////    case "WaitTime":
                ////        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.WaitTime;
                ////        htmlToPdfConverter.WaitBeforeConvert = int.Parse(textBoxWaitTime.Text);
                ////        break;
                ////    case "Manual":
                ////        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Manual;
                ////        break;
                ////    default:
                ////        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Auto;
                ////        break;
                ////}

                //// set header and footer
                ////  SetHeader(htmlToPdfConverter.Document);
                //// SetFooter(htmlToPdfConverter.Document);

                //// set the document security
                ////  htmlToPdfConverter.Document.Security.OpenPassword = textBoxOpenPassword.Text;
                //htmlToPdfConverter.Document.Security.AllowPrinting = true;

                //// set the permissions password too if an open password was set
                //if (htmlToPdfConverter.Document.Security.OpenPassword != null && htmlToPdfConverter.Document.Security.OpenPassword != String.Empty)
                //    htmlToPdfConverter.Document.Security.PermissionsPassword = htmlToPdfConverter.Document.Security.OpenPassword + "_admin";

                //// convert HTML to PDF
                //byte[] pdfBuffer = null;

                ////if (radioButtonConvertUrl.Checked)
                ////{
                ////    // convert URL to a PDF memory buffer
                ////    string url = textBoxUrl.Text;

                ////    pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
                ////}
                ////else
                ////{
                //// convert HTML code
                //string htmlCode = Att_content;
                //string baseUrl = "";

                //// convert HTML code to a PDF memory buffer
                //pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
                ////}

                //// inform the browser about the binary data format
                //HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

                //// let the browser know how to open the PDF document, attachment or inline, and the file name
                //HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
                //    false ? "inline" : "attachment", pdfBuffer.Length.ToString()));

                //// write the PDF buffer to HTTP response
                //HttpContext.Current.Response.BinaryWrite(pdfBuffer);

                //// call End() method of HTTP response to stop ASP.NET page processing

                //HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                //HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                #endregion

                mm.Subject = "User Creation Successfully";
                mm.Body = content;
                mm.IsBodyHtml = true;
                mm.CC.Add(CC);
                //////  mm.Attachments.Add(new Attachment(new MemoryStream(pdfBuffer), "OIC(" + ObjEC.OIC_Name + "_Mapped_With_Case_No_)" + ObjEC.Case_Number + ".pdf"));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpSection.Network.Host;
                smtp.EnableSsl = smtpSection.Network.EnableSsl;
                NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                smtp.Credentials = networkCred;
                smtp.Port = smtpSection.Network.Port;
                smtp.Send(mm);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Email sent.');", true);
            }

        }
        catch (Exception ex)
        {
        }
    }

    protected void grdUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetails")
            {
                ViewState["UserId"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOfficetypeId = (Label)row.FindControl("lblOfficetypeId");
                Label lblOfficeId = (Label)row.FindControl("lblOfficeId");
                Label lblDesignationType_ID = (Label)row.FindControl("lblDesignationType_ID");
                Label lblEmployeeNameID = (Label)row.FindControl("lblEmployeeNameID");
                Label lblMobileNoID = (Label)row.FindControl("lblMobileNoID");
                Label lblUserNameID = (Label)row.FindControl("lblUserNameID");
                Label lblEmailID = (Label)row.FindControl("lblEmailID");
                Label lbluserPass = (Label)row.FindControl("lbluserPass");


                ViewState["UserId"] = e.CommandArgument;
                btnSave.Text = "Update";
                txtEmpployeeName.Text = lblEmployeeNameID.Text;
                txtMobileNo.Text = lblMobileNoID.Text;
                txtUserName.Text = lblUserNameID.Text;
                txtUserEmail.Text = lblEmailID.Text;
                txtPassword.Text = lbluserPass.Text;

                ddlofficetype.ClearSelection();
                ddlofficetype.Items.FindByValue(lblOfficetypeId.Text).Selected = true;
                ddlofficetype_SelectedIndexChanged(sender, e);
                ddlOfficeName.ClearSelection();
                //ddlOfficeName.Items.FindByValue(lblOfficeId.Text).Selected = true;
                ddlOfficeName.SelectedValue = lblOfficeId.Text;

                ddlofficetype_SelectedIndexChanged(sender, e);
                ddlUsertype.ClearSelection();
                ddlUsertype.Items.FindByValue(lblDesignationType_ID.Text).Selected = true;
                txtUserEmail.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                rfvEmailID.Enabled = false;
                rfvUserName.Enabled = false;
                rfvPassword.Enabled = false;
                rfvpasswordCon.Enabled = false;
                cvdPasscon.Enabled = false;

                // on Selection Keep Visible False;
                UsrConfirmPass_Div.Visible = false;
                UsrPassword_Div.Visible = false;
               
            }
            if (e.CommandName == "DeleteDetails")
            {
                int UserId = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblUserMaster where UserId=" + UserId);
                BindUserDetails();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}