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
    Helper hlp = new Helper();
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
                    // FillDistrict();
                    BindCourtName();
                    BindOfficeType();
                    FillCasetype();
                    BindCaseSubject();
                    DtColumn();
                    FillYear();
                    FillColumn();
                    FillDesignation();
                //    BiunOicName();
                    FillParty();
                    FillOldCaseYear();
                    AdvDt();
                    PetiDt();
                    HearingDt();
                    PetiAdvDt();
                    OldCaseDt();
                    FillDepartment();
                    //FillDitrict();
                    divReplyDate.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    //#region Fill District
    //protected void FillDitrict()
    //{
    //    try
    //    {
    //        ddlDistrict.Items.Clear();
    //        ds = objdb.ByDataSet("select District_ID, District_Name from  Mst_District");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDistrict.DataTextField = "District_Name";
    //            ddlDistrict.DataValueField = "District_ID";
    //            ddlDistrict.DataSource = ds;
    //            ddlDistrict.DataBind();
    //        }
    //        ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
    //    }
    //}
    //#endregion
    private void FillDepartment()
    {
        try
        {
            DataSet dsDepart = objdb.ByDataSet("select Dept_ID,Dept_Name from tblDepartmentMaster where Isactive=1");
            if (dsDepart.Tables.Count > 0 && dsDepart.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataSource = dsDepart.Tables[0];
                ddlDepartment.DataTextField = "Dept_Name";
                ddlDepartment.DataValueField = "Dept_ID";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDepartment.DataSource = null;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void FillParty()
    {
        try
        {
            ddlParty.Items.Clear();
            DataTable dtParty = hlp.GetPartyName() as DataTable;
            if (dtParty != null && dtParty.Rows.Count > 0)
            {
                ddlParty.DataValueField = "Party_ID";
                ddlParty.DataTextField = "PartyName";
                ddlParty.DataSource = dtParty;
                ddlParty.DataBind();
            }
            ddlParty.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void PetiDt()
    {
        DataTable dtPeti = new DataTable();
        if (dtPeti.Columns.Count == 0)
        {
            dtPeti.Columns.Add("PetiName", typeof(string));
            dtPeti.Columns.Add("Designation_ID", typeof(int));
            dtPeti.Columns.Add("MobileNo", typeof(string));
            dtPeti.Columns.Add("AddRess", typeof(string));
            dtPeti.Columns.Add("Designation_name", typeof(string));
        }
        ViewState["Dtpeti"] = dtPeti;
    }

    protected void AdvDt()
    {
        DataTable DeptAdvDt = new DataTable();
        if (DeptAdvDt.Columns.Count == 0)
        {
            DeptAdvDt.Columns.Add("Dept_AdvName", typeof(string));
            DeptAdvDt.Columns.Add("Dept_MobileNo", typeof(string));
        }
        ViewState["AdvDt"] = DeptAdvDt;
    }

    protected void PetiAdvDt()
    {
        DataTable PetiAdvdt = new DataTable();
        if (PetiAdvdt.Columns.Count == 0)
        {
            PetiAdvdt.Columns.Add("Peti_AdvName", typeof(string));
            PetiAdvdt.Columns.Add("Peti_AdvMobileNo", typeof(string));
        }
        ViewState["PetiAdvDt"] = PetiAdvdt;
    }

    protected void HearingDt()
    {
        DataTable hearingdt = new DataTable();
        if (hearingdt.Columns.Count == 0)
        {
            hearingdt.Columns.Add("HearingDate", typeof(string));
            hearingdt.Columns.Add("HearingDoc", typeof(string));
        }
        ViewState["hearingdt"] = hearingdt;
    }

    protected void OldCaseDt()
    {
        DataTable OldDt = new DataTable();
        if (OldDt.Columns.Count == 0)
        {
            OldDt.Columns.Add("OldFillingNo", typeof(string));
            OldDt.Columns.Add("OldCaseNo", typeof(string));
            OldDt.Columns.Add("CaseYear", typeof(string));
            OldDt.Columns.Add("Casetype_ID", typeof(int));
            OldDt.Columns.Add("Court_ID", typeof(int));
            OldDt.Columns.Add("CourtLocation_ID", typeof(int));
            OldDt.Columns.Add("DocName", typeof(string));
            OldDt.Columns.Add("Document", typeof(string));
            OldDt.Columns.Add("Casetype", typeof(string));
            OldDt.Columns.Add("Court", typeof(string));
            OldDt.Columns.Add("CourtLocation", typeof(string));
        }
        ViewState["OldDt"] = OldDt;
    }

    #region Fill Designation
    protected void FillDesignation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            ddlPetiDesigNation.Items.Clear();
            ds = objdb.ByDataSet("SELECT Designation_Id ,Designation_Name FROM tblDesignationMaster ORDER BY Designation_Name ASC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataValueField = "Designation_Id";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();

                ddlPetiDesigNation.DataTextField = "Designation_Name";
                ddlPetiDesigNation.DataValueField = "Designation_Id";
                ddlPetiDesigNation.DataSource = ds;
                ddlPetiDesigNation.DataBind();
            }
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
            ddlPetiDesigNation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 2000; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());           
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void FillOldCaseYear()
    {
        try
        {
            ddloldCaseYear.Items.Clear(); 
            DataSet dsCase = objdb.ByDataSet("with yearlist as (select 2000 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year asc");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddloldCaseYear.DataSource = dsCase.Tables[0];
                ddloldCaseYear.DataTextField = "year";
                ddloldCaseYear.DataValueField = "year";
                ddloldCaseYear.DataBind();
                ddloldCaseYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    # region Doc Datatable
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
    #region Fill OICName
    //protected void BiunOicName()
    //{
    //    try
    //    {
    //        Helper oic = new Helper();
    //        ddlOicName.Items.Clear();
    //        DataTable dtOic = oic.GetOICAll() as DataTable;
    //        if (dtOic != null && dtOic.Rows.Count > 0)
    //        {
    //            ddlOicName.DataTextField = "OICName";
    //            ddlOicName.DataValueField = "OICMaster_ID";
    //            ddlOicName.DataSource = dtOic;
    //            ddlOicName.DataBind();
    //        }
    //        ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogCls.SendErrorToText(ex);
    //        //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    #endregion
    #region Fill CourtName
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear(); ddloldCaseCourt.Items.Clear();
            ds = objdb.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = ds;
                ddlCourtType.DataBind();

                ddloldCaseCourt.DataTextField = "CourtTypeName";
                ddloldCaseCourt.DataValueField = "CourtType_ID";
                ddloldCaseCourt.DataSource = ds;
                ddloldCaseCourt.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCaseCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill OfficeType
    protected void BindOfficeType()
    {
        try
        {
            ddlOfficetypeName.Items.Clear();
            ds = objdb.ByProcedure("USP_Select_Officetype", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficetypeName.DataTextField = "OfficeType_Name";
                ddlOfficetypeName.DataValueField = "OfficeType_Id";
                ddlOfficetypeName.DataSource = ds;
                ddlOfficetypeName.DataBind();
            }
            ddlOfficetypeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Fill District as Loaction
    protected void FillDistrict()
    {
        try
        {
            ddlCourtLocation.Items.Clear();
            ds = objdb.ByProcedure("USP_Select_District", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtLocation.DataTextField = "District_Name";
                ddlCourtLocation.DataValueField = "District_ID";
                ddlCourtLocation.DataSource = ds;
                ddlCourtLocation.DataBind();
            }
            ddlCourtLocation.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
            ErrorLogCls.SendErrorToText(ex);
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
        ddlSubSubject.ClearSelection();
        ddlParty.ClearSelection();
        ddlPetiDesigNation.ClearSelection();
        txtPetiAddRess.Text = "";
        ddlCourtType.ClearSelection();
        ddlCaseSubject.ClearSelection();
        txtDateOfCaseReg.Text = "";
        txtDateOfLastHearing.Text = "";
        txtCaseDetail.Text = "";
        txtDepartment.Text = "";
        txtOICMobileNo.Text = "";
        txtDeptAdvocateName.Text = "";
        txtDeptAdvocateMobileNo.Text = "";
        txtPetiName.Text = "";
        txtPetiMobileNo.Text = "";
        txtPetiAdvocateName.Text = "";
        txtPetiAdvocateMobileNo.Text = "";
        txtNextHearingDate.Text = "";
        GrdViewDoc.DataSource = null;
        GrdViewDoc.DataBind();
        ddlCasetype.ClearSelection();
        ddlCaseYear.ClearSelection();
        ddlCourtLocation.ClearSelection();
        ddlHighprioritycase.ClearSelection();
        ddlOicName.ClearSelection();
        GrdRespondent.DataSource = null;
        GrdRespondent.DataBind();
        grdDeptAdvocate.DataSource = null;
        grdDeptAdvocate.DataBind();
        GrdPetitionerDtl.DataSource = null;
        GrdPetitionerDtl.DataBind();
        GrdViewDoc.DataSource = null;
        GrdViewDoc.DataBind();
        grdHeairngDtl.DataSource = null;
        grdHeairngDtl.DataBind();
        GrdOldCaseDtl.DataSource = null;
        GrdOldCaseDtl.DataBind();
        txtoldCaseNo.Text = "";
        ddloldCaseCourt.ClearSelection();
        ddloldCasetype.ClearSelection();
        ddloldCourtLoca_Id.ClearSelection();
        ddloldCaseYear.ClearSelection();
        ddlDepartment.ClearSelection();
        ddlDistrict.ClearSelection();
        txtEmailID.Text = "";
        txtOICcaseNumber.Text = "";
        txtOICDate.Text = "";
        GrdPetiAdv.DataSource = null;
        GrdPetiAdv.DataBind();
        ddlCaseReply.ClearSelection();
        txtReplyDate.Text = "";
        divReplyDate.Visible = false;
        txtReplyCaseRemark.Text = "";
    }
    #endregion

    #region CaseType
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ddloldCasetype.Items.Clear();
            ds = objdb.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
                // For old Case type
                ddloldCasetype.DataTextField = "Casetype_Name";
                ddloldCasetype.DataValueField = "Casetype_ID";
                ddloldCasetype.DataSource = ds;
                ddloldCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion

    #region  Respondent Column
    protected void FillColumn()
    {
        DataTable dtCol = new DataTable();
        if (dtCol.Columns.Count == 0)
        {
            //dtCol.Columns.Add("RespondenttypeID", typeof(int));
            dtCol.Columns.Add("OfficeTypeID", typeof(int));
            dtCol.Columns.Add("OfficeNameId", typeof(int));
            dtCol.Columns.Add("DesignationId", typeof(int));
            dtCol.Columns.Add("DesignationName", typeof(string));
            dtCol.Columns.Add("RespondentName", typeof(string));
            dtCol.Columns.Add("RespondentMobileNo", typeof(string));
            dtCol.Columns.Add("Department", typeof(string));
            dtCol.Columns.Add("Address", typeof(string));
            //dtCol.Columns.Add("RespondenttypeName", typeof(string));
            dtCol.Columns.Add("OfficeTypeName", typeof(string));
            dtCol.Columns.Add("OfficeName", typeof(string));
        }
        ViewState["Responder"] = dtCol;
    }
    #endregion
    #region Bind OfficeName
    protected void ddlOfficetypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName.Items.Clear();
            ds = objdb.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlOfficetypeName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataTextField = "OfficeName";
                ddlOfficeName.DataValueField = "Office_Id";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
            }
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Save Add ResponderDtl
    protected void btnYes_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnYes.Text == "Add")
                {
                    DataTable dt = ViewState["Responder"] as DataTable;
                    if (dt.Columns.Count > 0)
                    {



                        dt.Rows.Add(ddlOfficetypeName.SelectedValue,
                            ddlOfficeName.SelectedValue,
                            ddlDesignation.SelectedValue,
                            ddlDesignation.SelectedItem.Text,
                            txtResponderName.Text.Trim(),
                            txtMobileNo.Text.Trim(),
                            txtDepartment.Text.Trim(),
                            txtAddress.Text.Trim(),
                            ddlOfficetypeName.SelectedItem.Text.Trim(),
                            ddlOfficeName.SelectedItem.Text.Trim());
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdRespondent.DataSource = dt;
                        GrdRespondent.DataBind();
                        ViewState["dt"] = dt;
                        ddlOfficetypeName.ClearSelection();
                        ddlOfficeName.ClearSelection();
                        ddlDesignation.ClearSelection();
                        ddlOfficetypeName.ClearSelection();
                        txtResponderName.Text = "";
                        txtMobileNo.Text = "";
                        txtDepartment.Text = "";
                        txtAddress.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Bind MobileNo&Email
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = objdb.ByDataSet("select * from tblOICMaster where OICMaster_Id = " + ddlOicName.SelectedValue);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtOICMobileNo.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                txtEmailID.Text = ds.Tables[0].Rows[0]["OICEmailID"].ToString();
            }
            else
            {
                txtOICMobileNo.Text = "";
                txtEmailID.Text = "";

            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    #region Add DeptAdv
    protected void btnAdvocate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DataTable dt1 = ViewState["AdvDt"] as DataTable;
                if (dt1.Columns.Count > 0)
                {
                    dt1.Rows.Add(txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim());
                }
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    grdDeptAdvocate.DataSource = dt1;
                    grdDeptAdvocate.DataBind();
                    ViewState["AdvData"] = dt1;
                    txtDeptAdvocateName.Text = ""; txtDeptAdvocateMobileNo.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Add Petitioner
    protected void btnPetitioner_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt2 = ViewState["Dtpeti"] as DataTable;
            if (dt2.Columns.Count > 0)
            {
                dt2.Rows.Add(txtPetiName.Text.Trim(), ddlPetiDesigNation.SelectedValue, txtPetiMobileNo.Text.Trim(), txtPetiAddRess.Text.Trim(), ddlPetiDesigNation.SelectedItem.Text);
            }
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                GrdPetitionerDtl.DataSource = dt2;
                GrdPetitionerDtl.DataBind();
                ViewState["Petitioner"] = dt2;
                txtPetiName.Text = "";
                ddlPetiDesigNation.ClearSelection();
                txtPetiMobileNo.Text = "";
                txtPetiAddRess.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Add Hearing
    protected void btnAddHeairng_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DataTable dt3 = ViewState["hearingdt"] as DataTable;

                ViewState["HearingDoc"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileHearingDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileHearingDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileHearingDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FileHearingDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "Hearing-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), strFileName);
                        FileHearingDoc.SaveAs(path);

                        ViewState["HearingDoc"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Format.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                if (errormsg == "")
                {
                    if (dt3 != null && dt3.Columns.Count > 0)
                    {
                        string NextHearingDate = txtNextHearingDate.Text != "" ? Convert.ToDateTime(txtNextHearingDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        dt3.Rows.Add(NextHearingDate, ViewState["HearingDoc"].ToString());
                    }
                    if (dt3.Rows.Count > 0)
                        txtNextHearingDate.Text = "";
                    grdHeairngDtl.DataSource = dt3;
                    grdHeairngDtl.DataBind();
                    ViewState["NextHearingDtl"] = dt3;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + errormsg + "');", true);
                    if (ViewState["HearingDoc"] != "") { if (File.Exists(ViewState["HearingDoc"].ToString())) { File.Delete(ViewState["HearingDoc"].ToString()); } ViewState["HearingDoc"] = ""; }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Add PetiAdv
    protected void btnPetiAdvSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnPetiAdvSave.Text == "Add")
            {
                DataTable dt5 = ViewState["PetiAdvDt"] as DataTable;
                if (dt5.Columns.Count > 0)
                {
                    dt5.Rows.Add(txtPetiAdvocateName.Text.Trim(), txtPetiAdvocateMobileNo.Text.Trim());
                }
                if (dt5 != null && dt5.Rows.Count > 0)
                {
                    GrdPetiAdv.DataSource = dt5;
                    GrdPetiAdv.DataBind();
                    ViewState["Dt5"] = dt5;
                    txtPetiAdvocateName.Text = "";
                    txtPetiAdvocateMobileNo.Text = "";
                }
            }
        }
        catch (Exception)
        {

            throw;
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
            if (FileUpload10.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
            {

                string fileExt = System.IO.Path.GetExtension(FileUpload10.FileName).Substring(1);
                string[] supportedTypes = { "PDF", "pdf" };
                if (!supportedTypes.Contains(fileExt))
                {
                    DocFailedCntExt += 1;
                }
                else if (FileUpload10.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                {
                    DocFailedCntSize += 1;
                }
                else
                {
                    strFileName = FileUpload10.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), strFileName);
                    FileUpload10.SaveAs(path);

                    ViewState["AddNewCaseDoc"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
            }
            string errormsg = "";
            if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
            if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

            if (errormsg == "")
            {
                DataTable dt = ViewState["dtcol"] as DataTable;
                if (dt.Columns.Count > 0)
                {
                    dt.Rows.Add(txtDocName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString());
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["DocData"] = dt;
                    GrdViewDoc.DataSource = dt;
                    GrdViewDoc.DataBind();
                    txtDocName.Text = "";
                    ViewState["AddNewCaseDoc"] = "";
                }
                else
                {
                    GrdViewDoc.DataSource = null;
                    GrdViewDoc.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                if (ViewState["AddNewCaseDoc"] != "") { if (File.Exists(ViewState["AddNewCaseDoc"].ToString())) { File.Delete(ViewState["AddNewCaseDoc"].ToString()); } ViewState["AddNewCaseDoc"] = ""; }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Add oldCaseDtl
    protected void btnSaveOldCase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                DataTable dt7 = ViewState["OldDt"] as DataTable;
                string OldFillingNo = "";
                ViewState["FU1"] = "";
                ViewState["FU2"] = "";
                ViewState["FU3"] = "";
                ViewState["FU4"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FU1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FU1.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU1.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU1.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU1.SaveAs(path);

                        ViewState["FU1"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU2.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU2.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU2.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU2.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU2.SaveAs(path);

                        ViewState["FU2"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU3.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU3.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU3.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU3.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU3.SaveAs(path);

                        ViewState["FU3"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU4.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU4.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU4.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU4.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "Hearing-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU4.SaveAs(path);

                        ViewState["FU4"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                if (errormsg == "")
                {
                    if (btnSaveOldCase.Text == "Add")
                    {
                        if (dt7.Columns.Count > 0)
                        {
                            OldFillingNo = ddloldCasetype.SelectedItem.Text + "/" + txtoldCaseNo.Text.Trim() + "/" + ddloldCaseYear.SelectedItem.Text + "/" + ddloldCaseCourt.SelectedItem.Text;

                            if (FU1.HasFile)
                            {
                                dt7.Rows.Add(OldFillingNo, txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedValue, ddloldCaseCourt.SelectedValue,
                                   ddloldCourtLoca_Id.SelectedValue, "Case Details", ViewState["FU1"].ToString(), ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedItem.Text);
                            }
                            if (FU2.HasFile)
                            {
                                dt7.Rows.Add(OldFillingNo, txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedValue, ddloldCaseCourt.SelectedValue,
                                   ddloldCourtLoca_Id.SelectedValue, "Description Of Proceedings", ViewState["FU2"].ToString(), ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedItem.Text);
                            }
                            if (FU3.HasFile)
                            {
                                dt7.Rows.Add(OldFillingNo, txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedValue, ddloldCaseCourt.SelectedValue,
                                   ddloldCourtLoca_Id.SelectedValue, "Decision", ViewState["FU3"].ToString(), ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedItem.Text);
                            }
                            if (FU4.HasFile)
                            {
                                dt7.Rows.Add(OldFillingNo, txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedValue, ddloldCaseCourt.SelectedValue,
                                   ddloldCourtLoca_Id.SelectedValue, "Other", ViewState["FU4"].ToString(), ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedItem.Text);
                            }
                            if (ViewState["FU1"].ToString() == "" && ViewState["FU2"].ToString() == "" && ViewState["FU3"].ToString() == "" && ViewState["FU4"].ToString() == "")
                            {
                                //OldDt.Columns.Add("OldFillingNo", typeof(string));
                                //OldDt.Columns.Add("OldCaseNo", typeof(string));
                                //OldDt.Columns.Add("CaseYear", typeof(string));
                                //OldDt.Columns.Add("Casetype_ID", typeof(int));
                                //OldDt.Columns.Add("Court_ID", typeof(int));
                                //OldDt.Columns.Add("CourtLocation_ID", typeof(int));
                                //OldDt.Columns.Add("DocName", typeof(string));
                                //OldDt.Columns.Add("Document", typeof(string));
                                //OldDt.Columns.Add("Casetype", typeof(string));
                                //OldDt.Columns.Add("Court", typeof(string));
                                //OldDt.Columns.Add("CourtLocation", typeof(string));



                                dt7.Rows.Add(OldFillingNo, txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedValue, ddloldCaseCourt.SelectedValue,
                                  ddloldCourtLoca_Id.SelectedValue, "Not Available", "", ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedItem.Text);
                            }
                        }
                    }
                    if (dt7.Rows.Count > 0)
                    {
                        ViewState["FU1"] = ""; ViewState["FU2"] = ""; ViewState["FU3"] = ""; ViewState["FU4"] = "";
                        txtoldCaseNo.Text = "";
                        ddloldCaseYear.ClearSelection();
                        ddloldCasetype.ClearSelection();
                        ddloldCaseCourt.ClearSelection();
                        ddloldCourtLoca_Id.ClearSelection();
                        GrdOldCaseDtl.DataSource = dt7;
                        GrdOldCaseDtl.DataBind();
                        ViewState["dt7"] = dt7;
                        OldFillingNo = "";
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    if (ViewState["FU1"] != "") { if (File.Exists(ViewState["FU1"].ToString())) { File.Delete(ViewState["FU1"].ToString()); } ViewState["FU1"] = ""; }
                    if (ViewState["FU2"] != "") { if (File.Exists(ViewState["FU2"].ToString())) { File.Delete(ViewState["FU2"].ToString()); } ViewState["FU2"] = ""; }
                    if (ViewState["FU3"] != "") { if (File.Exists(ViewState["FU3"].ToString())) { File.Delete(ViewState["FU3"].ToString()); } ViewState["FU3"] = ""; }
                    if (ViewState["FU4"] != "") { if (File.Exists(ViewState["FU4"].ToString())) { File.Delete(ViewState["FU4"].ToString()); } ViewState["FU4"] = ""; }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region SubmitButton
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSubmit.Text == "Save")
                {
                    DataTable dtDoc = ViewState["DocData"] as DataTable;
                    DataTable dtresponder = ViewState["dt"] as DataTable;
                    DataTable dtPetitioner = ViewState["Petitioner"] as DataTable;
                    DataTable dtDeptAdv = ViewState["AdvData"] as DataTable;
                    DataTable Hdt = ViewState["NextHearingDtl"] as DataTable;
                    DataTable dt6 = ViewState["Dt5"] as DataTable;
                    DataTable dt8 = ViewState["dt7"] as DataTable;
                    int DocFailedCntExt = 0;
                    int DocFailedCntSize = 0;
                    string strFileName = "";
                    string strExtension = "";
                    string strTimeStamp = "";
                    string OICDocument = "";
                    if (GrdViewDoc.Rows.Count > 0 && GrdRespondent.Rows.Count > 0)
                    {
                        if (GrdPetitionerDtl.Rows.Count > 0 && grdDeptAdvocate.Rows.Count > 0)
                        {
                            if (fuOICDocument.HasFile)
                            {
                                string fileExt = System.IO.Path.GetExtension(fuOICDocument.FileName).Substring(1);
                                string[] supportedTypes = { "PDF", "pdf" };
                                if (!supportedTypes.Contains(fileExt))
                                {
                                    DocFailedCntExt += 1;
                                }
                                else if (fuOICDocument.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                                {
                                    DocFailedCntSize += 1;
                                }
                                else
                                {
                                    strFileName = fuOICDocument.FileName.ToString();
                                    strExtension = Path.GetExtension(strFileName);
                                    strTimeStamp = DateTime.Now.ToString();
                                    strTimeStamp = strTimeStamp.Replace("/", "-");
                                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                                    strTimeStamp = strTimeStamp.Replace(":", "-");
                                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                                    strFileName = strName + "OICOrderDoc-" + strTimeStamp + strExtension;
                                    string path = Path.Combine(Server.MapPath("~/Legal/OICOrderDoc"), strFileName);
                                    fuOICDocument.SaveAs(path);

                                    OICDocument = strFileName;
                                    path = "";
                                    strFileName = "";
                                    strName = "";
                                }
                            }
                            string errormsg = "";
                            if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                            if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                            if (errormsg == "")
                            {
                                string OICDate = txtOICDate.Text != "" ? Convert.ToDateTime(txtOICDate.Text, cult).ToString("yyyy/MM/dd") : "";
                                string PartyMaster = ddlParty.SelectedIndex > 0 ? ddlParty.SelectedValue : "";
                                string HighPriorityCase = ddlHighprioritycase.SelectedIndex > 0 ? ddlHighprioritycase.SelectedItem.Text : "";
                                string RegDate = txtDateOfCaseReg.Text != "" ? Convert.ToDateTime(txtDateOfCaseReg.Text, cult).ToString("yyyy/MM/dd") : "";
                                string LastHearingDate = txtDateOfLastHearing.Text != "" ? Convert.ToDateTime(txtDateOfLastHearing.Text, cult).ToString("yyyy/MM/dd") : "";

                                string ReplyDate = txtReplyDate.Text != "" ? Convert.ToDateTime(txtReplyDate.Text, cult).ToString("yyyy/MM/dd") : "";
                                // Insert data into Main table
                                ds = objdb.ByProcedure("USP_Insert_NewCaseReg", new string[] {"CaseNo", "Casetype_ID","CasetypeName", "CaseYear", "CourtType_Id", "CourtName",
                                "CourtLocation_Id", "CaseSubject_Id", 
                                "CaseSubSubj_Id", "CaseRegDate", "lastHearingDate", "HighPriorityCase_Status","Department_Id","District_ID", "PetitonerName", "Designation_Id", "PetitionerMobileNo", 
                                "PetitionerAddress", "PetiAdvocateName", "PetiAdvocateMobile", "OICMaster_Id","OICOrderNumber","OICOrderDate","OICOrderDoc", "Party_Id", "DeptAdvocateName", 
                                "DeptAdvocateMobileNo", "Office_Id","CaseDetail","CaseReplyStatus","CaseReplyDate","CaseReplyRemark", "CreatedBy", "CreatedByIP" },
                                    new string[] {txtCaseNo.Text.Trim(),ddlCasetype.SelectedValue,ddlCasetype.SelectedItem.Text.Trim(),ddlCaseYear.SelectedItem.Text.Trim(),ddlCourtType.SelectedValue,ddlCourtType.SelectedItem.Text.Trim(),ddlCourtLocation.SelectedValue,
                                    ddlCaseSubject.SelectedValue,
                                ddlSubSubject.SelectedValue,RegDate,LastHearingDate,HighPriorityCase,ddlDepartment.SelectedValue,ddlDistrict.SelectedValue,txtPetiName.Text.Trim(),ddlPetiDesigNation.SelectedValue,txtPetiMobileNo.Text.Trim(),
                                txtPetiAddRess.Text.Trim(),txtPetiAdvocateName.Text.Trim(),txtPetiAdvocateMobileNo.Text.Trim(),ddlOicName.SelectedValue,txtOICcaseNumber.Text.Trim(),OICDate,OICDocument, PartyMaster,txtDeptAdvocateName.Text.Trim(),
                                txtDeptAdvocateMobileNo.Text.Trim(),ViewState["Office_ID"].ToString(),txtCaseDetail.Text.Trim(),ddlCaseReply.SelectedValue,ReplyDate,txtReplyCaseRemark.Text.Trim(),ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress()},
                                    new string[] { "type_RespondentDtl", "type_DocumentDtl", "type_PetitionerForCaseRegis", "type_DeptAdvForCaseRegis", "type_NextHearingForCaseRegis", "type_PetitiAdvocateForCaseRegis", "type_OldCaseDtlForCaseRegis" }
                                    , new DataTable[] { dtresponder, dtDoc, dtPetitioner, dtDeptAdv, Hdt, dt6, dt8 }, "dataset");
                            }
                            else
                            {
                                ViewState["fuOICDocument"] = "";
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Add Dept Advocate Detail & Petitioner Detail');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Add Document & Respondent Detail');", true);
                    }
                }

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks!", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        ClearText();
                        ddlDepartment.ClearSelection(); ddlDistrict.ClearSelection();
                    }
                    else
                    {
                        //lblMsg.Text = objdb.Alert("fa-check", "alert-warning", "Warning!", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ds.Tables[0].Rows[0]["ErrMsg"].ToString() + "' , 'warning')", true);
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    protected void ddloldCaseCourt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddloldCourtLoca_Id.Items.Clear();
            ds = objdb.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddloldCaseCourt.SelectedValue);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ddloldCourtLoca_Id.DataTextField = "District_Name";
                ddloldCourtLoca_Id.DataValueField = "District_ID";
                ddloldCourtLoca_Id.DataSource = ds;
                ddloldCourtLoca_Id.DataBind();
            }
            ddloldCourtLoca_Id.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCourtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourtLocation.Items.Clear();
            //ds = objdb.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddlCourtType.SelectedValue);
            ds = objdb.ByProcedure("Usp_Select_CourtDistrictLocation", new string[] { "CourtType_ID" }, new string[] { ddlCourtType.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtLocation.DataTextField = "District_Name";
                ddlCourtLocation.DataValueField = "District_Id";
                ddlCourtLocation.DataSource = ds.Tables[1];
                ddlCourtLocation.DataBind();

                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataSource = ds.Tables[0];
                ddlDistrict.DataBind();
            }
            ddlCourtLocation.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCaseReply_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCaseReply.SelectedValue == "1")
            {
                divReplyDate.Visible = true;
            }
            else
            {
                divReplyDate.Visible = false;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlSubSubject.Items.Clear();
            DataSet DsSubs = objdb.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            if (DsSubs != null && DsSubs.Tables[0].Rows.Count > 0)
            {
                ddlSubSubject.DataTextField = "CaseSubSubject";
                ddlSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlSubSubject.DataSource = DsSubs;
                ddlSubSubject.DataBind();
            }
            ddlSubSubject.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
        try
        {
            lblMsg.Text = "";
            ddlOicName.Items.Clear();
            DataSet dsOICbyDistrict = objdb.ByDataSet("select OICMaster_ID, OICName from tblOICMaster where District_Id =" + ddlDistrict.SelectedValue);
            if (dsOICbyDistrict.Tables[0].Rows.Count > 0 && dsOICbyDistrict.Tables.Count > 0)
            {
                ddlOicName.DataTextField = "OICName";
                ddlOicName.DataValueField = "OICMaster_ID";
                ddlOicName.DataSource = dsOICbyDistrict;
                ddlOicName.DataBind();
                ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                txtOICMobileNo.Text = "";
                txtEmailID.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}

