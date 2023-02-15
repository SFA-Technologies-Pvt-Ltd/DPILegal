using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Legal_Pending_Case_Since_2000 : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["CaseType"]))
                {
                    BindGrid(Request.QueryString["CaseType"]);
                    spnCaseType.InnerHtml = Request.QueryString["CaseType"] + " Case Type Details";
                }
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    //private void GetCourt()
    //{
    //    try
    //    {
    //        dsCase = obj.ByDataSet("select distinct Court from tbl_OldCaseDetail order by Court");
    //        if (dsCase.Tables[0].Rows.Count > 0)
    //        {
    //            ddlCourt.DataSource = dsCase;
    //            ddlCourt.DataTextField = "Court";
    //            ddlCourt.DataValueField = "Court";
    //            ddlCourt.DataBind();
    //            ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //        else
    //        {
    //            ddlYear.DataSource = null;
    //            ddlYear.DataBind();
    //            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }

    //}
    protected void BindGrid(string CaseType)
    {
        try
        {
            dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks,HearingDate,CaseNo from tbl_OldCaseDetail where CaseType='" + Convert.ToString(CaseType) + "' order by HearingDate Desc");
            if (dsCase.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = null;
                ViewState["dt"] = dsCase.Tables[0];
                grdCaseTypeDetail.DataSource = dsCase.Tables[0];
                grdCaseTypeDetail.DataBind();


            }
            else
            {
                grdCaseTypeDetail.DataSource = null;
                grdCaseTypeDetail.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No record found')", true);
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void grdCaseTypeDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdCaseTypeDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdCaseTypeDetail.EditIndex = e.NewEditIndex;
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
        //bindGridData();
    }
    public string convertQuotes(string str)
    {
        return str.Replace("'", "''");
    }

    [Obsolete]
    protected void grdCaseTypeDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        HiddenField hdnUId = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnUId");
        HiddenField hdnCaseNo = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnCaseNo");
        TextBox txtOICMobileNo = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtOICMobileNo");
        TextBox txtRespondentOffice = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondentOffice");
        TextBox txtHearingDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtHearingDate");
        TextBox txtRemarks = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRemarks");
        DropDownList ddlOICName = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlOICName");
        DropDownList ddlCaseSubject = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlCaseSubject");
        TextBox txtRespondent = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondent");
        string strQuery = "update tbl_OldCaseDetail set RespondentOffice='" + convertQuotes(txtRespondentOffice.Text.Trim()) + "',OICId=" + ddlOICName.SelectedItem.Value
            + ",CaseSubjectId=" + ddlCaseSubject.SelectedItem.Value + ",Remarks='" + convertQuotes(txtRemarks.Text.Trim()) + "',OICMobileNo='" + convertQuotes(txtOICMobileNo.Text.Trim());
        if (!string.IsNullOrEmpty(txtHearingDate.Text)) strQuery += "',HearingDate='" + Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd");
        strQuery += "',Respondent='" + convertQuotes(txtRespondent.Text.Trim()) + "' where UniqueNo='" + Convert.ToString(hdnUId.Value) + "'";
        obj.ByTextQuery(strQuery);


        //HiddenField hdnOICId = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnOICId");
        dsCase = obj.ByDataSet("select a.OICName,a.OICEmailID,a.OICMobileNo,b.CirlceName,c.ZoneName,d.OfficeName,e.Designation_Name designation,f.Division_Name from tblOICMaster a " +
            "inner join tblCircleMaster b on b.Circle_ID = a.Circle_ID " +
            "inner join tblZoneMaster c on c.Zone_ID = a.Zone_ID " +
            "inner join tblOfficeMaster d on d.Office_Id = a.Office_ID " +
            "inner join tblDesignationMaster e on e.Designation_ID = a.DesignationID " +
            "inner join tblDivisionMaster f on f.Division_ID = a.Division_ID " +
            "where OICMaster_ID = " + Convert.ToInt32(ddlOICName.SelectedItem.Value));



        if (dsCase.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICEmailID"].ToString()))
        {
            DataTable dt = (DataTable)dsCase.Tables[0];
            EmailContent ObjEC = new EmailContent();
            ObjEC.OIC_Name = dt.Rows[0]["OICName"].ToString();
            ObjEC.OIC_Mobile = dt.Rows[0]["OICMobileNo"].ToString();
            ObjEC.OIC_Email = dt.Rows[0]["OICEmailID"].ToString();
            ObjEC.OIC_Circle = dt.Rows[0]["CirlceName"].ToString();
            ObjEC.OIC_Designation = dsCase.Tables[0].Rows[0]["designation"].ToString();
            ObjEC.OIC_Office = dsCase.Tables[0].Rows[0]["OfficeName"].ToString();
            ObjEC.OIC_Zone = dsCase.Tables[0].Rows[0]["ZoneName"].ToString();
            ObjEC.OIC_Division = dsCase.Tables[0].Rows[0]["Division_Name"].ToString();

            ObjEC.Petitioner = grdCaseTypeDetail.Rows[e.RowIndex].Cells[3].Text;
            ObjEC.respondent = txtRespondent.Text.Trim();
            ObjEC.Case_Number = hdnCaseNo.Value;
            ObjEC.Curr_Date = DateTime.Now.ToString("dd-MM-yyyy");
            ObjEC.Curr_Year = DateTime.Now.Year.ToString();
            ObjEC.Court_Name = grdCaseTypeDetail.Rows[e.RowIndex].Cells[2].Text;
            sendmail(ObjEC, "sfatech.bot@gmail.com");
        }
        //else
        //{
        //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alertMessage()", true);
        //}
        grdCaseTypeDetail.EditIndex = -1;
        BindGrid(Request.QueryString["CaseType"]);
        txtSearch.Text = "";
    }


    private void sendmail(EmailContent ObjEC, string CC)
    {
        try
        {

            string EmailBodyHTMLPath = Server.MapPath("~/HtmlTemplete/Mail_Body_For_OIC.html");

            System.IO.StreamReader objReader;
            //objReader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\intel\\main.html");
            objReader = new StreamReader(EmailBodyHTMLPath);
            string content = objReader.ReadToEnd();
            objReader.Close();
            content = content
               .Replace("{{OIC_Name}}", ObjEC.OIC_Name)
               .Replace("{{Curr_Year}}", ObjEC.Curr_Year)
               .Replace("{{Curr_Date}}", ObjEC.Curr_Date)
               .Replace("{{OIC_Desg}}", ObjEC.OIC_Designation)
               .Replace("{{Petitioner}}", ObjEC.Petitioner)
               .Replace("{{respondent}}", ObjEC.respondent)
               .Replace("{{OIC_Office}}", ObjEC.OIC_Office)
               .Replace("{{Case_Number}}", ObjEC.Case_Number);

            string EmailAttachementHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");

            objReader = new StreamReader(EmailAttachementHTMLPath);
            string Att_content = objReader.ReadToEnd();
            objReader.Close();
            Att_content = Att_content
               .Replace("{{OIC_Name}}", ObjEC.OIC_Name)
               .Replace("{{Curr_Year}}", ObjEC.Curr_Year)
               .Replace("{{Curr_Date}}", ObjEC.Curr_Date)
               .Replace("{{OIC_Desg}}", ObjEC.OIC_Designation)
               .Replace("{{Petitioner}}", ObjEC.Petitioner)
               .Replace("{{respondent}}", ObjEC.respondent)
               .Replace("{{OIC_Office}}", ObjEC.OIC_Office)
               .Replace("{{Case_Number}}", ObjEC.Case_Number)
               .Replace("{{Court_Name}}", ObjEC.Court_Name)
               .Replace("{{Zone_Name_of_OIC}}", ObjEC.OIC_Zone)
               .Replace("{{Circle_Name_of_OIC}}", ObjEC.OIC_Circle)
               .Replace("{{Designation}}", ObjEC.OIC_Designation);


            //  string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage mm = new MailMessage(smtpSection.From, ObjEC.OIC_Email.Trim()))
            {

                #region

                // create the HTML to PDF converter
                HiQPdf.HtmlToPdf htmlToPdfConverter = new HiQPdf.HtmlToPdf();


                // htmlToPdfConverter.BrowserWidth = ;


                //if (textBoxBrowserHeight.Text.Length > 0)
                //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);


                //htmlToPdfConverter.HtmlLoadedTimeout = ;

                // set PDF page size and orientation
                htmlToPdfConverter.Document.PageSize = HiQPdf.PdfPageSize.A4;
                htmlToPdfConverter.Document.PageOrientation = HiQPdf.PdfPageOrientation.Portrait;

                // set the PDF standard used by the document
                htmlToPdfConverter.Document.PdfStandard = HiQPdf.PdfStandard.PdfA;

                // set PDF page margins
                htmlToPdfConverter.Document.Margins = new HiQPdf.PdfMargins(10);

                // set whether to embed the true type font in PDF
                htmlToPdfConverter.Document.FontEmbedding = true;

                // set triggering mode; for WaitTime mode set the wait time before convert
                //switch (dropDownListTriggeringMode.SelectedValue)
                //{
                //    case "Auto":
                htmlToPdfConverter.TriggerMode = HiQPdf.ConversionTriggerMode.Auto;
                //        break;
                //    case "WaitTime":
                //        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.WaitTime;
                //        htmlToPdfConverter.WaitBeforeConvert = int.Parse(textBoxWaitTime.Text);
                //        break;
                //    case "Manual":
                //        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Manual;
                //        break;
                //    default:
                //        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Auto;
                //        break;
                //}

                // set header and footer
                //  SetHeader(htmlToPdfConverter.Document);
                // SetFooter(htmlToPdfConverter.Document);

                // set the document security
                //  htmlToPdfConverter.Document.Security.OpenPassword = textBoxOpenPassword.Text;
                htmlToPdfConverter.Document.Security.AllowPrinting = true;

                // set the permissions password too if an open password was set
                if (htmlToPdfConverter.Document.Security.OpenPassword != null && htmlToPdfConverter.Document.Security.OpenPassword != String.Empty)
                    htmlToPdfConverter.Document.Security.PermissionsPassword = htmlToPdfConverter.Document.Security.OpenPassword + "_admin";

                // convert HTML to PDF
                byte[] pdfBuffer = null;

                //if (radioButtonConvertUrl.Checked)
                //{
                //    // convert URL to a PDF memory buffer
                //    string url = textBoxUrl.Text;

                //    pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
                //}
                //else
                //{
                // convert HTML code
                string htmlCode = Att_content;
                string baseUrl = "";

                // convert HTML code to a PDF memory buffer
                pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
                //}

                // inform the browser about the binary data format
                HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

                // let the browser know how to open the PDF document, attachment or inline, and the file name
                HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
                    false ? "inline" : "attachment", pdfBuffer.Length.ToString()));

                // write the PDF buffer to HTTP response
                HttpContext.Current.Response.BinaryWrite(pdfBuffer);

                // call End() method of HTTP response to stop ASP.NET page processing

                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                #endregion

                mm.Subject = "OIC Successfully Mapped";
                mm.Body = content;
                mm.IsBodyHtml = true;
                mm.CC.Add(CC);
                mm.Attachments.Add(new Attachment(new MemoryStream(pdfBuffer), "OIC(" + ObjEC.OIC_Name + "_Mapped_With_Case_No_)" + ObjEC.Case_Number + ".pdf"));
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

    private void bindGridData()
    {
        if (ViewState["dtsearch"] != null)
        {
            dtCase = (DataTable)ViewState["dtsearch"];
            grdCaseTypeDetail.DataSource = dtCase;
            grdCaseTypeDetail.DataBind();
        }
        else
        {
            dtCase = (DataTable)ViewState["dt"];
            grdCaseTypeDetail.DataSource = dtCase;
            grdCaseTypeDetail.DataBind();
        }
    }

    protected void grdCaseTypeDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCaseTypeDetail.EditIndex = -1;
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
    }



    protected void grdCaseTypeDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int OICId = 0;
        int CaseSubjectId = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) <= 0)
            {
                Label lblOICName = e.Row.FindControl("lblOICName") as Label;
                Label lblOICMobileNo = e.Row.FindControl("lblOICMobileNo") as Label;
                Label lblCaseSubjectId = e.Row.FindControl("lblCaseSubjectId") as Label;
                if (lblOICName.Text != "" && lblOICMobileNo.Text != "")
                {
                    OICId = Convert.ToInt32(lblOICName.Text);
                    dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId + " and Isactive=1");
                    if (dsCase.Tables[0].Rows.Count > 0)
                    {
                        lblOICName.Text = dsCase.Tables[0].Rows[0]["OICName"].ToString();
                        lblOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    }
                }
                if (lblCaseSubjectId.Text != "")
                {
                    CaseSubjectId = Convert.ToInt32(lblCaseSubjectId.Text);
                    dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject where CaseSubjectID=" + CaseSubjectId);
                    if (dsCase.Tables[0].Rows.Count > 0)
                    {
                        lblCaseSubjectId.Text = dsCase.Tables[0].Rows[0]["CaseSubject"].ToString();
                    }
                }
            }
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {

                HiddenField hdnUId = e.Row.FindControl("hdnUId") as HiddenField;
                dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks from tbl_OldCaseDetail where CaseType='" + Convert.ToString(Request.QueryString["CaseType"]) + "' and UniqueNo='" + hdnUId.Value + "'");
                if (dsCase.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICId"].ToString()) && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubjectId"].ToString()))
                {
                    OICId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["OICId"]);
                    CaseSubjectId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubjectId"]);
                }

                //int index = e.Row.RowIndex;
                Label lblOICName = e.Row.FindControl("lblOICName") as Label;


                // TextBox txtOICMobileNo = e.Row.FindControl("txtOICMobileNo") as TextBox;
                //dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                //if (dsCase.Tables[0].Rows.Count > 0)
                //{
                //    txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                //}

                DropDownList ddlOICName = e.Row.FindControl("ddlOICName") as DropDownList;

                dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                if (dsCase.Tables[0].Rows.Count > 0)
                {
                    ddlOICName.DataSource = dsCase.Tables[0];
                    ddlOICName.DataTextField = "OICName";
                    ddlOICName.DataValueField = "OICMaster_ID";
                    ddlOICName.DataBind();
                    ddlOICName.Items.Insert(0, new ListItem("Select", "0"));
                    ddlOICName.Items.FindByValue(OICId.ToString()).Selected = true;
                }
                else
                {
                    ddlOICName.DataSource = null;
                    ddlOICName.DataBind();
                    ddlOICName.Items.Insert(0, new ListItem("Select", "0"));
                }


                DropDownList ddlCaseSubject = (DropDownList)e.Row.FindControl("ddlCaseSubject");
                dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject");
                if (dsCase.Tables[0].Rows.Count > 0)
                {

                    ddlCaseSubject.DataSource = dsCase.Tables[0];
                    ddlCaseSubject.DataTextField = "CaseSubject";
                    ddlCaseSubject.DataValueField = "CaseSubjectID";
                    ddlCaseSubject.DataBind();
                    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                    ddlCaseSubject.Items.FindByValue(CaseSubjectId.ToString()).Selected = true;
                }
                else
                {
                    ddlCaseSubject.DataSource = null;
                    ddlCaseSubject.DataBind();
                    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
    }

    protected void ddlOICName_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        DropDownList ddlOICName = (DropDownList)grow.FindControl("ddlOICName");
        DropDownList ddl_state = (DropDownList)grow.FindControl("ddl_state");
        int OICId = Convert.ToInt32(ddlOICName.SelectedValue);
        // Label lblOICMobileNo = (Label)gvRow.FindControl("lblOICMobileNo");
        TextBox txtOICMobileNo = grow.FindControl("txtOICMobileNo") as TextBox;

        dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId);
        if (dsCase.Tables[0].Rows.Count > 0)
        {
            txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        }
    }

    protected void grdCaseTypeDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCaseTypeDetail.PageIndex = e.NewPageIndex;
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                dsCase = obj.ByDataSet("Select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo," +
                    "CaseSubjectId,Remarks,HearingDate,CaseNo from tbl_OldCaseDetail where FilingNo like '%" + Convert.ToString(txtSearch.Text.Trim()) + "%'  order by HearingDate Desc");
                if (dsCase.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtsearch"] = null;
                    ViewState["dtsearch"] = dsCase.Tables[0];
                    grdCaseTypeDetail.DataSource = dsCase.Tables[0];
                    grdCaseTypeDetail.DataBind();
                }
                else
                {
                    grdCaseTypeDetail.DataSource = null;
                    grdCaseTypeDetail.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No record found')", true);
                }

            }
            catch (Exception ex)
            {

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        ViewState["dtsearch"] = null;
        BindGrid(Request.QueryString["CaseType"]);
        txtSearch.Text = "";
    }
}

public class EmailContent
{
    public string Curr_Year { get; set; }
    public string Curr_Date { get; set; }
    public string OIC_Email { get; set; }
    public string OIC_Name { get; set; }
    public string OIC_Mobile { get; set; }
    public string OIC_Designation { get; set; }
    public string OIC_Office { get; set; }

    public string Court_Name { get; set; }
    public string OIC_Zone { get; set; }
    public string OIC_Circle { get; set; }
    public string OIC_Division { get; set; }
    public string Case_Number { get; set; }
    public string Petitioner { get; set; }
    public string respondent { get; set; }


}