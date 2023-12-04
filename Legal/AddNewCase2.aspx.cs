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

public partial class Legal_AddNewCase2 : System.Web.UI.Page
{
    DataSet ds;
    DataSet dsRecord;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    Helper hlp = new Helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_ID"] = Session["Emp_ID"];
                ViewState["Office_ID"] = Session["Office_ID"];
                FillYear();
                FillCasetype();
                BindCourtName();
                FillDistrict();
                BindCaseSubject();
                FillDepartment();
                FillParty();
                FillDesignation();
                BindOfficeType();
                PetiDt();
                FillColumn();
                DtColumn();
            }
        }
        else { Response.Redirect("../Login.aspx", false); }
    }
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 2000; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
    }
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
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
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
            ErrorLogCls.SendErrorToText(ex);
        }
    }
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
        }
    }
    #endregion
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
        }
    }
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

                ddlResDepartment.DataSource = dsDepart.Tables[0];
                ddlResDepartment.DataTextField = "Dept_Name";
                ddlResDepartment.DataValueField = "Dept_ID";
                ddlResDepartment.DataBind();
                ddlResDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDepartment.DataSource = null;
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));

                ddlResDepartment.DataSource = null;
                ddlResDepartment.DataBind();
                ddlResDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
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
            ds = objdb.ByProcedure("Usp_Select_CourtDistrictLocation", new string[] { "CourtType_ID" }, new string[] { ddlCourtType.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
				     ddlCourtLocation.DataTextField = "District_Name";
                    ddlCourtLocation.DataValueField = "District_Id";
                    ddlCourtLocation.DataSource = ds.Tables[1];
                    ddlCourtLocation.DataBind();
                if (ddlCourtType.SelectedValue == "2")
                {
                    ds = objdb.ByProcedure("Usp_Select_CourtDistrictLocation", new string[] { }, new string[] { }, "dataset");
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = ds.Tables[2];
                    ddlDistrict.DataBind();
                }
                else
                {
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = ds.Tables[0];
                    ddlDistrict.DataBind();
                }
            }
            ddlCourtLocation.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
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
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
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
            ErrorLogCls.SendErrorToText(ex);
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
    #region  Respondent Column
    protected void FillColumn()
    {
        DataTable dtCol = new DataTable();
        if (dtCol.Columns.Count == 0)
        {
            //dtCol.Columns.Add("RespondenttypeID", typeof(int));
            dtCol.Columns.Add("Dept_ID", typeof(int));
            dtCol.Columns.Add("HOD_ID", typeof(int));
            //dtCol.Columns.Add("Dept_Name", typeof(string));
            dtCol.Columns.Add("HodName", typeof(string));
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
    #region Doc Datatable
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
                //else if (FileUpload10.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                //{
                //    DocFailedCntSize += 1;
                //}
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
        }
    }
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
                        dt.Rows.Add(
                            ddlResDepartment.SelectedValue,
                            ddlHodName.SelectedValue,
                             
                            ddlHodName.SelectedItem.Text,
                            ddlOfficetypeName.SelectedValue,
                            ddlOfficeName.SelectedValue,
                            ddlDesignation.SelectedValue,
                            ddlDesignation.SelectedItem.Text,
                            txtResponderName.Text.Trim(),
                            txtMobileNo.Text.Trim(),
							ddlResDepartment.SelectedItem.Text,
                            txtAddress.Text.Trim(),
                            ddlOfficetypeName.SelectedItem.Text.Trim(),
                            ddlOfficeName.SelectedItem.Text.Trim());
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GrdRespondent.DataSource = dt;
                        GrdRespondent.DataBind();
                        ViewState["dt"] = dt;
                        ddlResDepartment.ClearSelection();
                        ddlHodName.ClearSelection();
                        ddlOfficetypeName.ClearSelection();
                        ddlOfficeName.ClearSelection();
                        ddlDesignation.ClearSelection();
                        ddlOfficetypeName.ClearSelection();
                        txtResponderName.Text = "";
                        txtMobileNo.Text = "";

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


                    if (GrdPetitionerDtl.Rows.Count > 0)
                    {
                        if (GrdRespondent.Rows.Count > 0 )
                        {

                            string errormsg = "";
                            if (errormsg == "")
                            {
                                //dtresponder.Columns.Remove("Dept_Name");
                                dtresponder.Columns.Remove("HodName");
                                string PartyMaster = ddlParty.SelectedIndex > 0 ? ddlParty.SelectedValue : "";
                                string HighPriorityCase = ddlHighprioritycase.SelectedIndex > 0 ? ddlHighprioritycase.SelectedItem.Text : "";
                                string RegDate = txtDateOfCaseReg.Text != "" ? Convert.ToDateTime(txtDateOfCaseReg.Text, cult).ToString("yyyy/MM/dd") : "";
                                string LastHearingDate = txtDateOfLastHearing.Text != "" ? Convert.ToDateTime(txtDateOfLastHearing.Text, cult).ToString("yyyy/MM/dd") : "";
                                // Insert data into Main table
                                ds = objdb.ByProcedure("USP_Insert_NewCaseReg", new string[] {"CaseNo", "Casetype_ID","CasetypeName", "CaseYear", "CourtType_Id", "CourtName", "CourtLocation_Id", "CaseSubject_Id",
                                                                                              "CaseSubSubj_Id", "CaseRegDate", "lastHearingDate", "HighPriorityCase_Status","Department_Id","District_ID", "PetitonerName",
                                                                                               "Designation_Id", "PetitionerMobileNo", "PetitionerAddress", "Office_Id","CaseDetail", "CreatedBy", "CreatedByIP" },
                                                                                new string[] {txtCaseNo.Text.Trim(),ddlCasetype.SelectedValue,ddlCasetype.SelectedItem.Text.Trim(),ddlCaseYear.SelectedItem.Text.Trim(),ddlCourtType.SelectedValue,
                                                                                    ddlCourtType.SelectedItem.Text.Trim(),ddlCourtLocation.SelectedValue,
                                    ddlCaseSubject.SelectedValue, ddlSubSubject.SelectedValue,RegDate,LastHearingDate,HighPriorityCase,ddlDepartment.SelectedValue,ddlDistrict.SelectedValue,txtPetiName.Text.Trim(),ddlPetiDesigNation.SelectedValue,txtPetiMobileNo.Text.Trim(),
                                txtPetiAddRess.Text.Trim(),ViewState["Office_ID"].ToString(),txtCaseDetail.Text.Trim(),ViewState["Emp_ID"].ToString(), objdb.GetLocalIPAddress()},
                                    new string[] { "type_RespondentDtl", "type_DocumentDtl", "type_PetitionerForCaseRegis" }
                                    , new DataTable[] { dtresponder, dtDoc, dtPetitioner }, "dataset");
                            }
                            else
                            {
                                ViewState["fuOICDocument"] = "";
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Add Respondent Detail');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Add  Petitioner Detail');", true);
                    }
                }

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        ClearText();
                        ddlDepartment.ClearSelection(); ddlDistrict.ClearSelection();
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ds.Tables[0].Rows[0]["ErrMsg"].ToString() + "' , 'warning')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();
    }
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

        txtPetiName.Text = "";
        txtPetiMobileNo.Text = "";
        GrdViewDoc.DataSource = null;
        GrdViewDoc.DataBind();
        ddlCasetype.ClearSelection();
        ddlCaseYear.ClearSelection();
        ddlCourtLocation.ClearSelection();
        ddlHighprioritycase.ClearSelection();
        GrdRespondent.DataSource = null;
        GrdRespondent.DataBind();
        GrdPetitionerDtl.DataSource = null;
        GrdPetitionerDtl.DataBind();
        GrdViewDoc.DataSource = null;
        GrdViewDoc.DataBind();
        ddlDepartment.ClearSelection();
        ddlDistrict.ClearSelection();
    }
    protected void ddlResDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {  // This Event Made by Bhanu on 09/05/23 For Respondent Field.
            ddlHodName.Items.Clear();
            DataSet dsHod = objdb.ByDataSet("select HOD_ID, HodName from tblHODMaster where Dept_Id =" + ddlResDepartment.SelectedValue);
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlHodName.DataTextField = "HodName";
                ddlHodName.DataValueField = "HOD_ID";
                ddlHodName.DataSource = dsHod;
                ddlHodName.DataBind();
                ddlHodName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}