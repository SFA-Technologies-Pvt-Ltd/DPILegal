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
                BIndOfficeType();
                BindUserDetails();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillOIC();
                OICName_Div.Visible = false;
                //ddlOICYesOrNot.SelectedIndex = 2;
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

    protected void FillOIC()
    {
        try
        {
            ddlOICList.Items.Clear();
            ds = obj.ByDataSet("select OICMaster_ID, OICName from tblOICMaster where District_Id =" + Session["District_Id"].ToString());
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOICList.DataTextField = "OICName";
                ddlOICList.DataValueField = "OICMaster_ID";
                ddlOICList.DataSource = ds;
                ddlOICList.DataBind();
            }
            ddlOICList.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BindUserDetails()
    {
        try
        {
            grdUserDetails.DataSource = null;
            grdUserDetails.DataBind();
            ds = obj.ByProcedure("USP_Select_UserMaster", new string[] { "CreatedBy" }, new string[] { Session["Emp_Id"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdUserDetails.DataSource = ds;
                grdUserDetails.DataBind();
                grdUserDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdUserDetails.UseAccessibleHeader = true;
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BIndOfficeType()
    {
        try
        {
            ddlofficetype.Items.Clear();
            if (Session["Role_ID"].ToString() == "4")
            {
                ds = obj.ByDataSet("select OfficeType_Id,OfficeType_Name from tblOfficeTypeMaster where OfficeType_Id not in (1,2,3,11)");
                ddlofficetype.Enabled = false;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlofficetype.DataTextField = "OfficeType_Name";
                    ddlofficetype.DataValueField = "OfficeType_Id";
                    ddlofficetype.DataSource = ds;
                    ddlofficetype.DataBind();
                }
                BindOfficeName();
            }
            else
            {
                ds = obj.ByDataSet("select OfficeType_Id,OfficeType_Name from tblOfficeTypeMaster");
                ddlofficetype.Enabled = true;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlofficetype.DataTextField = "OfficeType_Name";
                    ddlofficetype.DataValueField = "OfficeType_Id";
                    ddlofficetype.DataSource = ds;
                    ddlofficetype.DataBind();
                }
                ddlofficetype.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
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
                        string EMPName = "";
                        //ds = obj.ByDataSet("select * from tblUserMaster where UserEmail='" + txtUserEmail.Text.Trim() + "'  order by UserId desc");
                        string password = ConvertText_SHA512_And_Salt(txtPassword.Text.Trim());
                        //string EMPName = ddlOICYesOrNot.SelectedIndex > 0 ? ddlOICList.SelectedItem.Text.Trim() : txtEmpployeeName.Text.Trim();
                        if (ddlOICYesOrNot.SelectedValue == "1")
                        {
                            EMPName = ddlOICYesOrNot.SelectedIndex > 0 ? ddlOICList.SelectedItem.Text.Trim() : txtEmpployeeName.Text.Trim();
                        }
                        else
                        {
                            EMPName = txtEmpployeeName.Text.Trim();
                        }
                        string OIc_ID = ddlOICYesOrNot.SelectedIndex > 0 ? ddlOICList.SelectedValue : null;

                        ds = obj.ByProcedure("USP_Insert_UserMaster", new string[] { "OICMaster_ID", "EMPName", "UserEmail", "UserName", "UserPassword", "MobileNo", "Office_Id", "UserType_Id", "CreatedBy", "CreatedByIP" }
                            , new string[] { OIc_ID, EMPName, txtUserEmail.Text.Trim(), txtUserName.Text.Trim(), password, txtMobileNo.Text.Trim(), ddlOfficeName.SelectedValue, ddlUsertype.SelectedValue, Session["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSave.Text == "Update" && ViewState["UserId"].ToString() != "" && ViewState["UserId"].ToString() != null)
                    {
                        //string EMPName = ddlOICYesOrNot.SelectedIndex > 0 ? ddlOICList.SelectedItem.Text.Trim() : txtEmpployeeName.Text.Trim();
                        //string OIc_ID = ddlOICYesOrNot.SelectedIndex > 0 ? ddlOICList.SelectedValue : null;

                        ds = obj.ByProcedure("USP_Update_UserMaster", new string[] { "OICMaster_ID", "EMPName", "Designation_Id", "MobileNo", "OfficeType_Id", "Office_Id", "LastupdatedBy", "LastUpdatedByIP", "UserId", "UserName", "UserEmail" }
                              , new string[] { ddlOICList.SelectedValue, txtEmpployeeName.Text.Trim(), ddlUsertype.SelectedValue, txtMobileNo.Text.Trim(), ddlofficetype.SelectedValue, ddlOfficeName.SelectedValue, Session["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["UserId"].ToString(), txtUserName.Text.Trim(), txtUserEmail.Text.Trim() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                            string AdminEmail = Session["UserEmail"].ToString();
                            //sendmail(AdminEmail);
                            ddlUsertype.ClearSelection();
                            ddlOfficeName.ClearSelection();
                            txtEmpployeeName.Text = "";
                            ddlofficetype.ClearSelection();
                            ddlOICList.ClearSelection();
                            txtMobileNo.Text = "";
                            txtUserEmail.Text = "";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtUserEmail.Enabled = true;
                            txtMobileNo.Enabled = true;
                            txtUserName.Enabled = true;
                            txtPassword.Enabled = true;
                            txtConfirmPassword.Enabled = true;
                            rfvUserName.Enabled = true;
                            rfvPassword.Enabled = true;
                            rfvpasswordCon.Enabled = true;
                            cvdPasscon.Enabled = true;
                            Div_pass.Visible = true;
                            Div_Confpass.Visible = true;
                            btnSave.Text = "Save";
                            BindUserDetails();
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                            BindUserDetails();
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                        }
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                        BindUserDetails();
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ds.Tables[0].Rows[0]["ErrMsg"].ToString() + "' , 'warning')", true);
                    }


                    Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }

            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlofficetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindOfficeName();
    }
    private void BindOfficeName()
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
            if (Session["Role_ID"].ToString() == "4")
            {
                ddlOfficeName.Items.FindByValue(Session["Office_Id"].ToString()).Selected = true;
                ddlOfficeName.Enabled = false;
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
            ErrorLogCls.SendErrorToText(ex);
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
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void checkOic_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlOICYesOrNot.SelectedValue == "1")
            {
                EmpName_Div.Visible = false;
                OICName_Div.Visible = true;

            }
            else
            {
                EmpName_Div.Visible = true;
                OICName_Div.Visible = false;
                //txtMobileNo.Text = "";
                txtMobileNo.ReadOnly = false;
                ddlOICList.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
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
                Label lblOicID = (Label)row.FindControl("lblOicID");
                Label lblDesignationType_ID = (Label)row.FindControl("lblDesignationType_ID");
                Label lblEmployeeNameID = (Label)row.FindControl("lblEmployeeNameID");
                Label lblMobileNoID = (Label)row.FindControl("lblMobileNoID");
                Label lblUserNameID = (Label)row.FindControl("lblUserNameID");
                Label lblEmailID = (Label)row.FindControl("lblEmailID");
                Label lbluserPass = (Label)row.FindControl("lbluserPass");


                ViewState["UserId"] = e.CommandArgument;
                btnSave.Text = "Update";
                if (lblEmployeeNameID.Text != "") txtEmpployeeName.Text = lblEmployeeNameID.Text;
                if (lblMobileNoID.Text != "") txtMobileNo.Text = lblMobileNoID.Text;
                if (lblUserNameID.Text != "") txtUserName.Text = lblUserNameID.Text;
                if (lblEmailID.Text != "") txtUserEmail.Text = lblEmailID.Text;
                if (lbluserPass.Text != "") txtPassword.Text = lbluserPass.Text;
                if (lblOicID.Text != "0")
                {
                    ddlOICYesOrNot.ClearSelection();
                    ddlOICYesOrNot.Items.FindByValue("1").Selected = true;
                }
                else
                {
                    ddlOICYesOrNot.ClearSelection();
                    ddlOICYesOrNot.Items.FindByValue("2").Selected = true;
                }
                if (!string.IsNullOrEmpty(lblOicID.Text))
                {
                    ddlOICList.ClearSelection();
                    ddlOICList.Items.FindByValue(lblOicID.Text).Selected = true;
                    checkOic_CheckedChanged(sender, e);
                    txtMobileNo.Enabled = false; txtUserEmail.Enabled = false;
                }
                if (!string.IsNullOrEmpty(lblOfficetypeId.Text))
                {
                    ddlofficetype.ClearSelection();
                    ddlofficetype.Items.FindByValue(lblOfficetypeId.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblDesignationType_ID.Text))
                {
                    ddlofficetype_SelectedIndexChanged(sender, e);
                    ddlOfficeName.ClearSelection();
                    //ddlOfficeName.Items.FindByValue(lblOfficeId.Text).Selected = true;
                    ddlOfficeName.SelectedValue = lblOfficeId.Text;
                }
                if (!string.IsNullOrEmpty(lblDesignationType_ID.Text))
                {
                    ddlofficetype_SelectedIndexChanged(sender, e);
                    ddlUsertype.ClearSelection();
                    ddlUsertype.Items.FindByValue(lblDesignationType_ID.Text).Selected = true;
                }

                //txtUserEmail.Enabled = false;
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                rfvEmailID.Enabled = false;
                rfvUserName.Enabled = false;
                rfvPassword.Enabled = false;
                rfvpasswordCon.Enabled = false;
                cvdPasscon.Enabled = false;
                Div_pass.Visible = false;
                Div_Confpass.Visible = false;
            }
            else if (e.CommandName == "DeleteDetails")
            {
                //int UserId = Convert.ToInt32(e.CommandArgument);
                //obj.ByTextQuery("delete from tblUserMaster where UserId=" + UserId);
                //BindUserDetails();
            }
            else if (e.CommandName == "btnDelete")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lIsActive = (Label)row.FindControl("lblIsActive");
                string activeStuts = "1";
                if (lIsActive.Text == "True")
                {
                    activeStuts = "0";
                }
                ds = obj.ByProcedure("USP_tblUserMaster_Update_ISActive", new string[] { "UserId", "LastIsActiveBy", "LastIsActiveByIP", "IsActive" },
                     new string[] { e.CommandArgument.ToString(), Session["Emp_Id"].ToString(), obj.GetLocalIPAddress(), activeStuts }, "dataset");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "ok")
                            {

                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "', 'success')", true);
                                BindUserDetails();
                            }
                            else
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ds.Tables[0].Rows[0]["ErrorMsg"].ToString() + "' , 'warning')", true);
                            }
                        }
                    }
                }
            }
            grdUserDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdUserDetails.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlOICList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet DsOIC = obj.ByDataSet("select OICName, OICEmailID, OICMobileNo from tblOICMaster where OICMaster_ID =" + ddlOICList.SelectedValue);
            if (DsOIC != null && DsOIC.Tables[0].Rows.Count > 0)
            {
                txtUserEmail.Text = DsOIC.Tables[0].Rows[0]["OICEmailID"].ToString();
                txtMobileNo.Text = DsOIC.Tables[0].Rows[0]["OICMobileNo"].ToString();
                txtMobileNo.ReadOnly = true;
                txtUserEmail.ReadOnly = true;
            }
           
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}