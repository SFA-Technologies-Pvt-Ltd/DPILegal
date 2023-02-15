using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;

public partial class Legal_AddNewCase : System.Web.UI.Page
{
    DataSet ds;
    DataSet dsRecord;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Office_ID"] = Session["Office_ID"];
                    FillDistrict();
                    BindCourtName();
                    BindRespondertype();
                    BindOfficeType();
                    FillCasetype();
                    BindCaseSubject();
                    DtColumn();
                    if (Request.QueryString["Case_ID"] != null)
                    {
                        ViewState["Case_ID"] = objdb.Decrypt(Request.QueryString["Case_ID"].ToString());
                        //  FillCaseDetail();
                        btnClear.Enabled = false;
                    }
                    if (Request.QueryString["ReopenCase_ID"] != null)
                    {
                        ViewState["ReopenCase_ID"] = objdb.Decrypt(Request.QueryString["ReopenCase_ID"].ToString());
                        // FillCaseDetail();
                    }
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    #region Fill Respondent
    protected void BindRespondertype()
    {
        try
        {
            ddlRespondertype.Items.Clear();
            ds = objdb.ByProcedure("USP_Get_ResponderType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlRespondertype.DataTextField = "RespondertypeName";
                ddlRespondertype.DataValueField = "Respondertype_ID";
                ddlRespondertype.DataSource = ds;
                ddlRespondertype.DataBind();
            }
            ddlRespondertype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    # region Datatable
    protected void DtColumn()
    {
        DataTable dtcol = new DataTable();
        if (dtcol.Columns.Count == 0)
        {
            dtcol.Columns.Add("DocName", typeof(string));
            dtcol.Columns.Add("Document", typeof(string));
        }
        ViewState["dtcol"] = dtcol;
    }
    #endregion
    #region Fill CourtName
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear();
            ds = objdb.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = ds;
                ddlCourtType.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill OfficeType
    protected void BindOfficeType()
    {
        ddlOfficetypeName.Items.Clear();
        ds = objdb.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlOfficetypeName.DataTextField = "OfficeType_Name";
            ddlOfficetypeName.DataValueField = "OfficeType_Id";
            ddlOfficetypeName.DataSource = ds;
            ddlOfficetypeName.DataBind();
        }
        ddlOfficetypeName.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion
    #region Fill District as Loaction
    protected void FillDistrict()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Select_District", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CaseStatus
    protected void BindCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            ds = objdb.ByDataSet("SELECT CaseSubject, CaseSubjectID FROM tbl_LegalMstCaseSubject");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataSource = ds;
                ddlCaseSubject.DataBind();
            }
            ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region SubmitButton
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            //string Hearing_Date = txtHearingDate.Text;            
            if (txtCaseNo.Text == "")
            {
                msg += "Enter Case Number.\\n";
            }
            if (ddlCourtType.SelectedIndex < 0)
            {
                msg += "Select Court Type.\\n";
            }
            if (ddlCaseSubject.SelectedIndex < 0)
            {
                msg += "Select Case Subject.\\n";
            }
            if (ddlHighprioritycase.SelectedIndex < 0)
            {
                msg += "Select High Priority Case.\\n";
            }
            if (txtCaseDescription.Text == "")
            {
                msg += "Enter Subject Of Case.\\n";
            }
            //if (FileUpload1.HasFile)
            //{
            //    Document1 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload1.FileName;
            //    FileUpload1.SaveAs(Server.MapPath(Document1));
            //}
            //else if (ViewState["Case_UploadedDoc1"] != null)
            //{
            //    Document1 = ViewState["Case_UploadedDoc1"].ToString();
            //}
            //if (FileUpload2.HasFile)
            //{
            //    Document2 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload2.FileName;
            //    FileUpload2.SaveAs(Server.MapPath(Document2));
            //}
            //else if (ViewState["Case_UploadedDoc2"] != null)
            //{
            //    Document2 = ViewState["Case_UploadedDoc2"].ToString();
            //}
            //if (FileUpload3.HasFile)
            //{
            //    Document3 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload3.FileName;
            //    FileUpload3.SaveAs(Server.MapPath(Document3));
            //}
            //else if (ViewState["Case_UploadedDoc3"] != null)
            //{
            //    Document3 = ViewState["Case_UploadedDoc3"].ToString();
            //}
            ViewState["FileUploadDOC1"] = "";
            ViewState["FileUploadDOC2"] = "";
            ViewState["FileUploadDOC3"] = "";
            int DocFailedCntExt = 0;
            int DocFailedCntSize = 0;
            string strFileName = "";
            string strExtension = "";
            string strTimeStamp = "";
            if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {

                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpload1.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                {
                    DocFailedCntSize += 1;
                }
                else
                {

                    strFileName = FileUpload1.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseDoc/"), strFileName);
                    FileUpload1.SaveAs(path);

                    ViewState["FileUploadDOC1"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }

            }
            else if (FileUpload2.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(FileUpload2.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpload2.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                {
                    DocFailedCntSize += 1;
                }
                else
                {

                    strFileName = FileUpload2.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseDoc/"), strFileName);
                    FileUpload2.SaveAs(path);

                    ViewState["FileUploadDOC2"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            else if (FileUpload3.HasFile)
            {
                string fileExt = System.IO.Path.GetExtension(FileUpload3.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpload3.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                {
                    DocFailedCntSize += 1;
                }
                else
                {

                    strFileName = FileUpload3.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseDoc/"), strFileName);
                    FileUpload3.SaveAs(path);

                    ViewState["FileUploadDOC3"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            string errormsg = "";
            if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
            if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

            if (errormsg == "")
            {
                if (msg == "")
                {
                    string Hearing_Date = "";
                    string DateofReceipt = "";
                    string DateofFiling = "";
                    if (txtHearingDate.Text != "")
                    {
                        Hearing_Date = Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        Hearing_Date = "";
                    }
                    if (txtDateOfReceipt.Text != "")
                    {
                        DateofReceipt = Convert.ToDateTime(txtDateOfReceipt.Text, cult).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        DateofReceipt = "";
                    }
                    if (txtDateOfLastHearing.Text != "")
                    {
                        DateofFiling = Convert.ToDateTime(txtDateOfLastHearing.Text, cult).ToString("yyyy/MM/dd");
                    }
                    else
                    {
                        DateofFiling = "";
                    }
                    lblMsg.Text = "";
                    if (btnSubmit.Text == "Save")
                    {
                        ds = objdb.ByProcedure("USP_Legal_InsertLocalCourtCaseRegis", new string[] { "FilingNo", "Petitoner_Name", "CaseSubject", "CourtType_Id", "PetiAdvocateName", "PetiAdvocateMobile", "petiAdvocateEmail_ID", "CaseDetail", "CreatedBy", "CreatedByIP", "DeptAdvocateName", "DeptAdvocateMobileNo", "DeptAdvocateEmailId", "HighPrirtyCaseSts", "OldCaseNo" },
                            new string[] { txtCaseNo.Text.Trim(), txtPetitionerAppName.Text.Trim(), ddlCaseSubject.SelectedValue, ddlCourtType.SelectedValue, txtPetitionerAdvName.Text.Trim(), txtPetitionerAdvMobileNo.Text.Trim(), txtPetitionerAdvEmail.Text.Trim(), txtCaseDescription.Text.Trim(), ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress(), txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), txtDeptAdvocateEmail.Text.Trim(), ddlHighprioritycase.SelectedItem.Text, txtCaseOldRefNo.Text.Trim() }, "dataset");
                    }
                    //else if (btnSubmit.Text == "Update")
                    //{
                    //    if (FileUpload1.HasFile)
                    //    {
                    //        Document1 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload1.FileName;
                    //        FileUpload1.SaveAs(Server.MapPath(Document1));
                    //    }
                    //    else
                    //    {
                    //        Document1 = ViewState["Case_UploadedDoc1"].ToString();
                    //    }
                    //    if (FileUpload2.HasFile)
                    //    {
                    //        Document2 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload2.FileName;
                    //        FileUpload2.SaveAs(Server.MapPath(Document2));
                    //    }
                    //    else
                    //    {
                    //        Document2 = ViewState["Case_UploadedDoc2"].ToString();
                    //    }
                    //    if (FileUpload3.HasFile)
                    //    {
                    //        Document3 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload3.FileName;
                    //        FileUpload3.SaveAs(Server.MapPath(Document3));
                    //    }
                    //    else
                    //    {
                    //        Document3 = ViewState["Case_UploadedDoc3"].ToString();
                    //    }
                    //   
                    //}
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks!", ErrMsg);
                            ClearText();
                        }
                        else
                        {
                            lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ErrMsg);
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                    }
                }
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                //}
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region ClearButton
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();

    }
    #endregion
    #region ClearAllControl
    protected void ClearText()
    {
        txtCaseNo.Text = "";
        txtCaseOldRefNo.Text = "";
        ddlCourtType.ClearSelection();
        ddlCaseSubject.ClearSelection();
        txtDateOfReceipt.Text = "";
        txtDateOfLastHearing.Text = "";
        txtCaseDescription.Text = "";
        txtDepartment.Text = "";
        txtOICMobileNo.Text = "";
        txtOICEmail.Text = "";
        txtDeptAdvocateName.Text = "";
        txtDeptAdvocateMobileNo.Text = "";
        txtDeptAdvocateEmail.Text = "";
        txtPetitionerAppName.Text = "";
        txtPetitionerAppMobileNo.Text = "";
        txtPetitionerAppEmail.Text = "";
        txtPetitionerAppAddress.Text = "";
        txtPetitionerAdvName.Text = "";
        txtPetitionerAdvMobileNo.Text = "";
        txtPetitionerAdvEmail.Text = "";
        txtPetitionerAdvAddress.Text = "";
        txtHearingDate.Text = "";
        HyperLink1.Visible = false;
        HyperLink2.Visible = false;
        HyperLink3.Visible = false;
    }
    #endregion
    #region FillGrid
    //protected void FillCaseDetail()
    //{
    //    try
    //    {

    //        lblMsg.Text = "";
    //        if (ViewState["Case_ID"] != null)
    //        {
    //            dsRecord = null;
    //            dsRecord = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "7", ViewState["Case_ID"].ToString() }, "dataset");
    //            if (dsRecord.Tables[0].Rows.Count > 0)
    //            {
    //                ddloffice.ClearSelection();
    //                ddloffice.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
    //                txtCaseNo.Text = dsRecord.Tables[0].Rows[0]["Case_No"].ToString();
    //                txtCaseOldRefNo.Text = dsRecord.Tables[0].Rows[0]["Case_OldRefNo"].ToString();
    //                ddlCourtType.ClearSelection();
    //                ddlCourtType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_CourtType"].ToString()).Selected = true;
    //                ddlCaseType.ClearSelection();
    //                ddlCaseType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_Type"].ToString()).Selected = true;
    //                txtSubjectOfCase.Text = dsRecord.Tables[0].Rows[0]["Case_SubjectOfCase"].ToString();
    //                txtDepartmentConcerned.Text = dsRecord.Tables[0].Rows[0]["Case_DepartmentConcerned"].ToString();
    //                txtDateOfReceipt.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfReceipt"].ToString();
    //                txtDateOfFiling.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfFiling"].ToString();
    //                txtInterimOrder.Text = dsRecord.Tables[0].Rows[0]["Case_InterimOrder"].ToString();
    //                txtFinalOrder.Text = dsRecord.Tables[0].Rows[0]["Case_FinalOrder"].ToString();
    //                txtClaimAmount.Text = dsRecord.Tables[0].Rows[0]["Case_ClaimAmount"].ToString();
    //                txtCaseDescription.Text = dsRecord.Tables[0].Rows[0]["Case_Description"].ToString();

    //                txtPetitionerAppName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppName"].ToString();
    //                txtPetitionerAppMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppMobileNo"].ToString();
    //                txtPetitionerAppEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppEmail"].ToString();
    //                txtPetitionerAppAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppAddress"].ToString();
    //                txtPetitionerAdvName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvName"].ToString();
    //                txtPetitionerAdvMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvMobileNo"].ToString();
    //                txtPetitionerAdvEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvEmail"].ToString();
    //                txtPetitionerAdvAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvAddress"].ToString();
    //                ddlOIC.ClearSelection();
    //                // FillOICDropdown();
    //                ddlOIC.Items.FindByValue(dsRecord.Tables[0].Rows[0]["OIC_ID"].ToString()).Selected = true;
    //                ddlAdvocate.ClearSelection();
    //                ddlAdvocate.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Advocate_ID"].ToString()).Selected = true;
    //                txtDepartmentName.Text = dsRecord.Tables[0].Rows[0]["Emp_Name"].ToString();
    //                txtOICMobileNo.Text = dsRecord.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
    //                txtOICEmail.Text = dsRecord.Tables[0].Rows[0]["Emp_Email"].ToString();
    //                txtDesignation.Text = dsRecord.Tables[0].Rows[0]["Designation_Name"].ToString();
    //                txtAdvocateName.Text = dsRecord.Tables[0].Rows[0]["Advocate_Name"].ToString();
    //                txtAdvocateMobileNo.Text = dsRecord.Tables[0].Rows[0]["Advocate_MobileNo"].ToString();
    //                txtAdvocateEmail.Text = dsRecord.Tables[0].Rows[0]["Advocate_Email"].ToString();
    //                txtAdvocateAddress.Text = dsRecord.Tables[0].Rows[0]["Advocate_Address"].ToString();
    //                if (dsRecord.Tables[0].Rows[0]["District_ID"].ToString() != "")
    //                {
    //                    ddlDistrict.ClearSelection();
    //                    ddlDistrict.Items.FindByValue(dsRecord.Tables[0].Rows[0]["District_ID"].ToString()).Selected = true;
    //                }
    //                ViewState["Case_UploadedDoc1"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc1"].ToString();
    //                ViewState["Case_UploadedDoc2"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc2"].ToString();
    //                ViewState["Case_UploadedDoc3"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc3"].ToString();
    //                txtHearingDate.Text = dsRecord.Tables[1].Rows[0]["Hearing_Date"].ToString();
    //                ViewState["Count"] = dsRecord.Tables[2].Rows[0]["Count"].ToString();
    //                if (ViewState["Case_UploadedDoc1"].ToString() != "")
    //                {
    //                    HyperLink1.Visible = true;
    //                    HyperLink1.NavigateUrl = ViewState["Case_UploadedDoc1"].ToString();
    //                    HyperLink1.Text = "View";
    //                }
    //                else
    //                {
    //                    HyperLink1.Visible = true;
    //                    HyperLink1.Text = "NA";

    //                }
    //                if (ViewState["Case_UploadedDoc2"].ToString() != "")
    //                {
    //                    HyperLink2.Visible = true;
    //                    HyperLink2.NavigateUrl = ViewState["Case_UploadedDoc2"].ToString();
    //                    HyperLink2.Text = "View";
    //                }
    //                else
    //                {
    //                    HyperLink2.Visible = true;
    //                    HyperLink2.Text = "NA";

    //                }

    //                if (ViewState["Case_UploadedDoc3"].ToString() != "")
    //                {
    //                    HyperLink3.Visible = true;
    //                    HyperLink3.NavigateUrl = ViewState["Case_UploadedDoc3"].ToString();
    //                    HyperLink3.Text = "View";
    //                }
    //                else
    //                {
    //                    HyperLink3.Visible = true;
    //                    HyperLink3.Text = "NA";

    //                }
    //                btnSubmit.Text = "Update";
    //                if (int.Parse(ViewState["Count"].ToString()) > 1)
    //                {
    //                    txtHearingDate.Enabled = false;
    //                }
    //                else
    //                {
    //                    txtHearingDate.Enabled = true;
    //                }

    //            }
    //        }
    //        if (ViewState["ReopenCase_ID"] != null)
    //        {
    //            dsRecord = null;
    //            dsRecord = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "7", ViewState["ReopenCase_ID"].ToString() }, "dataset");
    //            if (dsRecord.Tables[0].Rows.Count > 0)
    //            {
    //                ddloffice.ClearSelection();
    //                ddloffice.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
    //                txtCaseOldRefNo.Text = dsRecord.Tables[0].Rows[0]["Case_No"].ToString();
    //                ddlCourtType.ClearSelection();
    //                ddlCourtType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_CourtType"].ToString()).Selected = true;
    //                ddlCaseType.ClearSelection();
    //                ddlCaseType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_Type"].ToString()).Selected = true;
    //                txtSubjectOfCase.Text = dsRecord.Tables[0].Rows[0]["Case_SubjectOfCase"].ToString();
    //                txtDepartmentConcerned.Text = dsRecord.Tables[0].Rows[0]["Case_DepartmentConcerned"].ToString();
    //                txtDateOfReceipt.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfReceipt"].ToString();
    //                txtDateOfFiling.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfFiling"].ToString();
    //                txtInterimOrder.Text = dsRecord.Tables[0].Rows[0]["Case_InterimOrder"].ToString();
    //                txtFinalOrder.Text = dsRecord.Tables[0].Rows[0]["Case_FinalOrder"].ToString();
    //                txtClaimAmount.Text = dsRecord.Tables[0].Rows[0]["Case_ClaimAmount"].ToString();
    //                txtCaseDescription.Text = dsRecord.Tables[0].Rows[0]["Case_Description"].ToString();

    //                txtPetitionerAppName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppName"].ToString();
    //                txtPetitionerAppMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppMobileNo"].ToString();
    //                txtPetitionerAppEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppEmail"].ToString();
    //                txtPetitionerAppAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppAddress"].ToString();
    //                txtPetitionerAdvName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvName"].ToString();
    //                txtPetitionerAdvMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvMobileNo"].ToString();
    //                txtPetitionerAdvEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvEmail"].ToString();
    //                txtPetitionerAdvAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvAddress"].ToString();
    //                ddlOIC.ClearSelection();
    //                //  FillOICDropdown();
    //                ddlOIC.Items.FindByValue(dsRecord.Tables[0].Rows[0]["OIC_ID"].ToString()).Selected = true;
    //                ddlAdvocate.ClearSelection();
    //                ddlAdvocate.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Advocate_ID"].ToString()).Selected = true;
    //                txtDepartmentName.Text = dsRecord.Tables[0].Rows[0]["Emp_Name"].ToString();
    //                txtOICMobileNo.Text = dsRecord.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
    //                txtOICEmail.Text = dsRecord.Tables[0].Rows[0]["Emp_Email"].ToString();
    //                txtDesignation.Text = dsRecord.Tables[0].Rows[0]["Designation_Name"].ToString();
    //                txtAdvocateName.Text = dsRecord.Tables[0].Rows[0]["Advocate_Name"].ToString();
    //                txtAdvocateMobileNo.Text = dsRecord.Tables[0].Rows[0]["Advocate_MobileNo"].ToString();
    //                txtAdvocateEmail.Text = dsRecord.Tables[0].Rows[0]["Advocate_Email"].ToString();
    //                txtAdvocateAddress.Text = dsRecord.Tables[0].Rows[0]["Advocate_Address"].ToString();
    //                ViewState["Case_UploadedDoc1"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc1"].ToString();
    //                ViewState["Case_UploadedDoc2"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc2"].ToString();
    //                ViewState["Case_UploadedDoc3"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc3"].ToString();
    //                if (ViewState["Case_UploadedDoc1"].ToString() != "")
    //                {
    //                    HyperLink1.Visible = true;
    //                    HyperLink1.Text = "View";
    //                    HyperLink1.NavigateUrl = ViewState["Case_UploadedDoc1"].ToString();
    //                }
    //                else
    //                {
    //                    HyperLink1.Visible = true;
    //                    HyperLink1.Text = "NA";

    //                }
    //                if (ViewState["Case_UploadedDoc2"].ToString() != "")
    //                {
    //                    HyperLink2.Visible = true;
    //                    HyperLink2.NavigateUrl = ViewState["Case_UploadedDoc2"].ToString();
    //                    HyperLink2.Text = "View";
    //                }
    //                else
    //                {
    //                    HyperLink2.Visible = true;
    //                    HyperLink2.Text = "NA";

    //                }

    //                if (ViewState["Case_UploadedDoc3"].ToString() != "")
    //                {
    //                    HyperLink3.Visible = true;
    //                    HyperLink3.Text = "View";
    //                    HyperLink3.NavigateUrl = ViewState["Case_UploadedDoc3"].ToString();
    //                }
    //                else
    //                {
    //                    HyperLink3.Visible = true;
    //                    HyperLink3.Text = "NA";

    //                }

    //            }
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    #endregion
    #region AddResponder
    protected void btnAddresponder_Click(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddRespondent()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region CaseType
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = objdb.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region AddDoc
    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        try
        {


            ViewState["AddNewCaseDoc"] = "";
            int DocFailedCntExt = 0;
            int DocFailedCntSize = 0;
            string strFileName = "";
            string strExtension = "";
            string strTimeStamp = "";
            if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {

                string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpload1.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                {
                    DocFailedCntSize += 1;
                }
                else
                {

                    strFileName = FileUpload1.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseDoc/"), strFileName);
                    FileUpload1.SaveAs(path);

                    ViewState["AddNewCaseDoc"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            string errormsg = "";
            if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
            if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

            if (errormsg == "")
            {
                DataTable dt = ViewState["dtcol"] as DataTable;
                if (dt.Columns.Count > 0)
                {
                    dt.Rows.Add(txtDocName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString());
                    ds.Tables.Add(dt);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
}

