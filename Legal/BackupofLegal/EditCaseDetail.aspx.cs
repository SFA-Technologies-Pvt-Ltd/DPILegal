using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;
using System.Text;

public partial class Legal_EditCaseDetail : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                //string multiCharString = Request.QueryString.ToString();
                //string[] multiArray = multiCharString.Split(new Char[] { '=', '&' });
                //string CaseID = Decrypt(HttpUtility.UrlDecode(multiArray[1]));
                //string Uniqueno = Decrypt(HttpUtility.UrlDecode(multiArray[3]));
                string CaseID = obj.Decrypt(Request.QueryString["CaseID"].ToString());
                string Uniqueno = obj.Decrypt(Request.QueryString["UniqueNO"].ToString());
                // divReplyDate.Visible = false; divReplyRemark.Visible = false;
                ViewState["ID"] = CaseID;
                ViewState["UniqueNO"] = Uniqueno;
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillOldCaseYear();
                FillYear();
                BindCourtName();
                BindDisposalType();
                FillParty();
                FillCaseSubject();
                FillHODName();
                FillDesignation();
                BindOfficeType();
                CaseDisposeStatus(); // by deafult Case Dispose on NO text.
                FillCasetype();
                FillDepartment();
                //FieldViewOldCaseDtl.Visible = false;
                BindDetails(sender, e);
                GetReturnFileAppeal(sender, e);
                ManagVisiblity();
                //  FillDitrict();
                FillOrderWithDirection();
                FillOldOICDetail();
            }
        }
        else { Response.Redirect("../Login.aspx", false); }
    }
    protected void FillHODName()
    {
        try
        {
            ddlNameHod.ClearSelection();
            DataSet dsHod = obj.ByDataSet("select HOD_ID, HodName from tblHODMaster where HOD_Id in(2,3,4,5,6,7)");
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlNameHod.DataTextField = "HodName";
                ddlNameHod.DataValueField = "HOD_ID";
                ddlNameHod.DataSource = dsHod;
                ddlNameHod.DataBind();
                ddlNameHod.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #region Fill District
    protected void FillDitrict()
    {
        try
        {
            ddlDistrict.Items.Clear();
            DataSet dsd = obj.ByDataSet("select DM.District_ID, District_Name from  Mst_District DM inner join tbl_DistrictCourtMaping_Mst CMM on DM.District_ID=CMM.District_ID " +
             "where CMM.CourtName_ID=" + ddlCourtType.SelectedValue);
            if (dsd != null && dsd.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataSource = dsd;
                ddlDistrict.DataBind();

                ddlOldOICDistrict.DataTextField = "District_Name";
                ddlOldOICDistrict.DataValueField = "District_ID";
                ddlOldOICDistrict.DataSource = dsd;
                ddlOldOICDistrict.DataBind();
            }
            ddlPetitionerPresentDistrict.DataTextField = "District_Name";
            ddlPetitionerPresentDistrict.DataValueField = "District_ID";
            ddlPetitionerPresentDistrict.DataSource = dsd;
            ddlPetitionerPresentDistrict.DataBind();
            ddlPetitionerPresentDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlOldOICDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    private void ManagVisiblity()
    {
        try
        {
            divCaseRelate.Visible = false;
            txtOICDesignation.Enabled = false;
            if (Session["Role_ID"].ToString() == "1")
            {

                ddlCaseSubject.Enabled = true;
                ddlCaseSubSubject.Enabled = true;
                //ddlDepartment.Enabled = true;
                ddlHOD.Enabled = true;
                ddlOicName.Enabled = true;
                txtOicMobileNo.Enabled = true;
                txtOicEmailId.Enabled = true;
                ddlHighprioritycase.Enabled = true;
                ddlParty.Enabled = true;
                divRealtion.Visible = true;
            }
            else if (Session["Role_ID"].ToString() == "3")
            {
                ddlCaseSubject.Enabled = false;
                ddlCaseSubSubject.Enabled = false;
                //ddlDepartment.Enabled = false;
                ddlHOD.Enabled = false;
                ddlOicName.Enabled = false;
                txtOicMobileNo.Enabled = false;
                txtOicEmailId.Enabled = false;
                ddlHighprioritycase.Enabled = false;
                ddlParty.Enabled = false;
                txtPetiName.Enabled = false;
                ddlPetiDesigNation.Enabled = false;
                txtPetiMobileNo.Enabled = false;
                //txtPetiAddRess.Enabled = false;
                ddlPetitionerPresentDistrict.Enabled = false;
                divRealtion.Visible = false;
                ddlDistrict.Enabled = false;
            }
            else if (Session["Role_ID"].ToString() == "4")
            {
                ddlOicName.Enabled = true;
                ddlDistrict.Enabled = false;
                //ddlDepartment.Enabled = false;
                ddlHOD.Enabled = false;
                divCaseRelate.Visible = false;
                divRealtion.Visible = false;
                ddlCaseSubject.Enabled = false;
                ddlCaseSubSubject.Enabled = false;
                ddlParty.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }
    #region Fill CaseDispose Status
    protected void CaseDisposeStatus() // Case Dispose By Default On NO condtiton
    {
        foreach (ListItem item in rdCaseDispose.Items)
        {
            if (item.Text.Contains("No"))
            {
                item.Selected = true;
                break;
            }
            caseDisposeYes.Visible = false;
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            HearingDtl_CaseDispose.Visible = false;
            CimplianceSt_Div.Visible = false;
        }
    }
    #endregion
    #region Fill Case DisposeType
    protected void BindDisposalType()
    {
        try
        {
            ddlDisponsType.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDisponsType.DataTextField = "CaseDisposeType";
                ddlDisponsType.DataValueField = "CaseDisposeType_Id";
                ddlDisponsType.DataSource = ds;
                ddlDisponsType.DataBind();
            }
            ddlDisponsType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill OfficeType
    protected void BindOfficeType()
    {
        try
        {
            ddlResOfficetypeName.Items.Clear();
            ds = obj.ByProcedure("USP_Select_Officetype", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlResOfficetypeName.DataTextField = "OfficeType_Name";
                ddlResOfficetypeName.DataValueField = "OfficeType_Id";
                ddlResOfficetypeName.DataSource = ds;
                ddlResOfficetypeName.DataBind();
            }
            ddlResOfficetypeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Fill OICName
    protected void FillOicName()
    {
        try
        {
            Helper oic = new Helper();
            ddlOicName.Items.Clear();

            //   DataTable dtOic = oic.GetOIC(ddlCourtType.SelectedValue) as DataTable;
            DataSet dtOic = obj.ByDataSet("select OICMaster_ID, OICName, OICMobileNo, OICEmailID from tblOICMaster OM inner join tbl_DistrictCourtMaping_Mst CD on CD.District_ID = OM.District_Id where CD.CourtName_ID = " + ddlCourtType.SelectedValue + "and OM.District_Id=" + ddlDistrict.SelectedValue);

            if (dtOic != null && dtOic.Tables[0].Rows.Count > 0)
            {
                ddlOicName.DataTextField = "OICName";
                ddlOicName.DataValueField = "OICMaster_ID";
                ddlOicName.DataSource = dtOic;
                ddlOicName.DataBind();
            }
            ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        ddlWPRPCaseYear.Items.Clear();
        for (int i = 2000; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
            ddlWPRPCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
        ddlWPRPCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    private void FillDepartment()
    {
        try
        {
            DataSet dsDepart = obj.ByDataSet("select Dept_ID,Dept_Name from tblDepartmentMaster where Isactive=1");
            DataSet dsHOD = obj.ByDataSet("select HOD_Id,HodName from tblHODMaster where Isactive=1");
            if (dsDepart.Tables.Count > 0 && dsDepart.Tables[0].Rows.Count > 0)
            {
                ddlHOD.DataSource = dsHOD.Tables[0];
                ddlHOD.DataTextField = "HodName";
                ddlHOD.DataValueField = "HOD_Id";
                ddlHOD.DataBind();

                ddlResDepartment.DataSource = dsDepart.Tables[0];
                ddlResDepartment.DataTextField = "Dept_Name";
                ddlResDepartment.DataValueField = "Dept_ID";
                ddlResDepartment.DataBind();
                ddlResDepartment.Items.Insert(0, new ListItem("Select", "0"));
                //ddlHOD.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlHOD.DataSource = null;
                ddlHOD.DataBind();
                ddlHOD.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void FillOldCaseYear()
    {
        try
        {
            ddloldCaseYear.Items.Clear();
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 2000 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year asc");
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
    #region Fill CourtName
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear(); ddloldCaseCourt.Items.Clear();
            DataSet dsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = dsCourt;
                ddlCourtType.DataBind();

                ddloldCaseCourt.DataTextField = "CourtTypeName";
                ddloldCaseCourt.DataValueField = "CourtType_ID";
                ddloldCaseCourt.DataSource = dsCourt;
                ddloldCaseCourt.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCaseCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill CaseSubject
    protected void FillCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            DataSet ds_SC = obj.ByDataSet("SELECT CaseSubject, CaseSubjectID FROM tbl_LegalMstCaseSubject");
            if (ds_SC != null && ds_SC.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataSource = ds_SC;
                ddlCaseSubject.DataBind();
            }
            ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region FillParty
    protected void FillParty()
    {
        try
        {
            ddlParty.Items.Clear();
            Helper hlp = new Helper();
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
    #endregion
    #region Fill Designation
    protected void FillDesignation()
    {
        try
        {

            ddlPetiDesigNation.Items.Clear(); ddlResDesig.Items.Clear();
            ds = obj.ByDataSet("SELECT Designation_Id ,Designation_Name FROM tblDesignationMaster ORDER BY Designation_Name ASC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPetiDesigNation.DataTextField = "Designation_Name";
                ddlPetiDesigNation.DataValueField = "Designation_Id";
                ddlPetiDesigNation.DataSource = ds;
                ddlPetiDesigNation.DataBind();

                ddlResDesig.DataTextField = "Designation_Name";
                ddlResDesig.DataValueField = "Designation_Id";
                ddlResDesig.DataSource = ds;
                ddlResDesig.DataBind();

                ddlOICDesignation.DataTextField = "Designation_Name";
                ddlOICDesignation.DataValueField = "Designation_Id";
                ddlOICDesignation.DataSource = ds;
                ddlOICDesignation.DataBind();
            }
            ddlResDesig.Items.Insert(0, new ListItem("Select", "0"));
            ddlPetiDesigNation.Items.Insert(0, new ListItem("Select", "0"));
            ddlOICDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region CaseType
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ddloldCasetype.Items.Clear();
            ds = obj.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
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
    protected void BindDetails(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            ds = obj.ByProcedure("USP_Select_NewCaseRegis", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                lblCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                ViewState["CaseNo"] = lblCaseNo.Text;
                txtCaseDetail.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString();
                //Start Here Add DropDown Bind Methods by bhanu on 09/05/2023
                string PolicyMeter = ds.Tables[0].Rows[0]["PolicyMeterStatus"].ToString();
                if (!string.IsNullOrEmpty(PolicyMeter)) { ddlPolicyMeter.ClearSelection(); ddlPolicyMeter.Items.FindByText(PolicyMeter).Selected = true; }
                //End Here Add DropDown Bind Methods by bhanu on 09/05/2023
                ddlCourtType.ClearSelection();
                BindCourtName();
                if (ddlCourtType.Items.Count > 0)
                    ddlCourtType.Items.FindByValue(ds.Tables[0].Rows[0]["CourtType_Id"].ToString().Trim()).Selected = true; ddlCourtType.Enabled = false;
                if (ds.Tables[0].Rows[0]["Casetype_ID"].ToString() != "") ddlCasetype.ClearSelection(); ddlCasetype.Items.FindByValue(ds.Tables[0].Rows[0]["Casetype_ID"].ToString()).Selected = true; ddlCasetype.Enabled = false;
                FillYear();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseYear"].ToString()))
                {
                    ddlCaseYear.ClearSelection();
                    ddlCaseYear.Items.FindByValue(ds.Tables[0].Rows[0]["CaseYear"].ToString()).Selected = true;
                    ddlCaseYear.Enabled = false;
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CourtLocation_Id"].ToString()))
                {
                    ddlCourtLocation.ClearSelection();
                    ddlCourtType_SelectedIndexChanged(sender, e);
                    ddlCourtLocation.Items.FindByValue(ds.Tables[0].Rows[0]["CourtLocation_Id"].ToString()).Selected = true;
                    ddlCourtLocation.Enabled = false;
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseSubjectID"].ToString()))
                {
                    ddlCaseSubject.ClearSelection();
                    ddlCaseSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubjectID"].ToString().Trim()).Selected = true;
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()))
                {
                    ddlCaseSubject_SelectedIndexChanged(sender, e);
                    ddlCaseSubSubject.ClearSelection();
                    ddlCaseSubSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()).Selected = true;

                }
                ViewState["Department"] = "NA";
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Department_Id"].ToString()))
                {
                    ViewState["Department"] = ds.Tables[0].Rows[0]["Department_Id"].ToString();
                    //ddlDepartment.ClearSelection();
                    //ddlDepartment.Items.FindByValue(ds.Tables[0].Rows[0]["Department_Id"].ToString()).Selected = true;
                }
                for (int i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    foreach (ListItem item in ddlHOD.Items)
                    {
                        if (ds.Tables[8].Rows[i]["Hod_id1"].ToString() == item.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseBelongto_Id"].ToString()))
                {
                    ddlCaseBelongto.ClearSelection();
                    ddlCaseBelongto.Items.FindByValue(ds.Tables[0].Rows[0]["CaseBelongto_Id"].ToString().Trim()).Selected = true;
                    ddlCaseBelogto_SelectedIndexChanged(sender, e);
                }


                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Party_Id"].ToString()))
                {
                    ddlParty.ClearSelection();
                    ddlParty.Items.FindByValue(ds.Tables[0].Rows[0]["Party_Id"].ToString().Trim()).Selected = true;
                }


                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString()))
                {
                    ddlHighprioritycase.ClearSelection();
                    ddlHighprioritycase.Items.FindByText(ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString()).Selected = true;
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseReplyStatus"].ToString()))
                {
                    ddlCaseReply.ClearSelection();
                    ddlCaseReply.Items.FindByValue(ds.Tables[0].Rows[0]["CaseReplyStatus"].ToString()).Selected = true;
                    if (ds.Tables[0].Rows[0]["CaseReplyStatus"].ToString() == "1")
                    {
                        divReplyDate.Visible = true;
                    }
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseReplyDate"].ToString()))
                {
                    txtReplyDate.Text = ds.Tables[0].Rows[0]["CaseReplyDate"].ToString();
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseReplyRemark"].ToString()))
                {
                    txtReplyCaseRemark.Text = ds.Tables[0].Rows[0]["CaseReplyRemark"].ToString();
                    divReplyRemark.Visible = true;
                }
                ddlCaseYear.ClearSelection();
                if (ddlCaseYear.Items.Count > 0) ddlCaseYear.Items.FindByText(ds.Tables[0].Rows[0]["CaseYear"].ToString().Trim()).Selected = true; ddlCaseYear.Enabled = false;

                if (ds.Tables[1].Rows.Count > 0) GrdRespondentDtl.DataSource = ds.Tables[1]; GrdRespondentDtl.DataBind();
                if (ds.Tables[2].Rows.Count > 0) GrdHearingDtl.DataSource = ds.Tables[2]; GrdHearingDtl.DataBind();
                if (ds.Tables[3].Rows.Count > 0) GrdPetiDtl.DataSource = ds.Tables[3]; GrdPetiDtl.DataBind();
                if (ds.Tables[4].Rows.Count > 0) GrdCaseDocument.DataSource = ds.Tables[4]; GrdCaseDocument.DataBind();
                if (ds.Tables[5].Rows.Count > 0) GrdDeptAdvDtl.DataSource = ds.Tables[5]; GrdDeptAdvDtl.DataBind();
                //if (ds.Tables[6].Rows.Count > 0) GrdCaseDispose.DataSource = ds.Tables[6]; GrdCaseDispose.DataBind(); DisposalStatus.Visible = false;
                if (ds.Tables[7].Rows.Count > 0)
                {
                    ddlAskForOldCase.ClearSelection();
                    ddlAskForOldCase.SelectedValue = "1";
                    ddlAskForOldCase_SelectedIndexChanged(this, EventArgs.Empty);
                    GrdOldCaseDtl.DataSource = ds.Tables[7];
                    GrdOldCaseDtl.DataBind();
                }
                else
                {
                    ddlAskForOldCase.ClearSelection();
                    ddlAskForOldCase.SelectedValue = "0";
                    ddlAskForOldCase_SelectedIndexChanged(this, EventArgs.Empty);
                    GrdOldCaseDtl.DataSource = ds.Tables[7];
                    GrdOldCaseDtl.DataBind();
                }
                if (ds.Tables[9].Rows.Count > 0) GrdPetiAdv.DataSource = ds.Tables[9]; GrdPetiAdv.DataBind();

                if (ds.Tables[6].Rows[0]["CaseDisposal_Status"].ToString() != "")
                {
                    // if Case Dispose Once nothing show case Dispose Controls

                    GrdCaseDispose.DataSource = ds.Tables[6]; GrdCaseDispose.DataBind(); DisposalStatus.Visible = false;

                    caseDisposeYes.Visible = false;
                    CimplianceSt_Div.Visible = false;
                    OrderBy1.Visible = false;
                    DivOrderTimeline.Visible = false;
                    OrderSummary_Div.Visible = false;
                    OrderBy2.Visible = false;
                }


                FillDitrict();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["District_ID"].ToString()))
                {
                    string result = ds.Tables[0].Rows[0]["District_ID"].ToString();
                    ddlDistrict.Items.FindByValue(result).Selected = true;
                }
                FillOicName();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["OICMaster_Id"].ToString()))
                {
                    ddlOicName.ClearSelection();
                    ddlOicName.Items.FindByValue(ds.Tables[0].Rows[0]["OICMaster_Id"].ToString().Trim()).Selected = true;
                    ddlOicName_SelectedIndexChanged(sender, e);
                    ddlCaserRelated.Items.FindByValue("1").Selected = true;
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["OICOrderNumber"].ToString()))
                {
                    txtOICcaseNumber.Text = ds.Tables[0].Rows[0]["OICOrderNumber"].ToString();

                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["OICOrderDate"].ToString()))
                {
                    txtOICDate.Text = ds.Tables[0].Rows[0]["OICOrderDate"].ToString();
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["OICOrderDoc"].ToString()))
                {
                    string Link = ds.Tables[0].Rows[0]["OICOrderDoc"].ToString();

                    string lbldoc = "OICOrderDoc/" + Link; ;
                    ViewState["oldOICDoc"] = Link;
                    hyperlinkOICdoc.NavigateUrl = lbldoc;
                    hyperlinkOICdoc.Visible = true; ;
                }
                else
                {
                    ViewState["oldOICDoc"] = "NA";
                    hyperlinkOICdoc.Visible = false;
                }
                // Intirm Order DiV
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IntrimOrderDate"].ToString()))
                {
                    ddlIntrimOrder.ClearSelection();
                    ddlIntrimOrder.Items.FindByValue("1").Selected = true;
                    Div_IntrimOrderYesNO.Visible = true;
                    txtIntirmOrderDate.Text = ds.Tables[0].Rows[0]["IntrimOrderDate"].ToString();
                    btnIntrimOrder.Text = "Update";
                    txtIntrimOrderEnddate.Text = ds.Tables[0].Rows[0]["IntrimOrderEndDate"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IntrimOrderSummary"].ToString() != "") txtIntrimOrderSummary.Text = ds.Tables[0].Rows[0]["IntrimOrderSummary"].ToString();
                if (ds.Tables[0].Rows[0]["IntrimOrderTimeline"].ToString() != "") txtIntrimTimeline.Text = ds.Tables[0].Rows[0]["IntrimOrderTimeline"].ToString();
                if (ds.Tables[0].Rows[0]["IntrimOrderAnyPrevPP"].ToString() != "") txtIntrimPrevPP.Text = ds.Tables[0].Rows[0]["IntrimOrderAnyPrevPP"].ToString();
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    //Petitioner Dtl 1.0
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ViewState["fuOICDocument"] = "";

                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                string OICDocument = "";
                lblMsg.Text = "";
                if (btnUpdate.Text == "Update" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    if (fuOICDocument.HasFile)
                    {
                        string fileExt = System.IO.Path.GetExtension(fuOICDocument.FileName).Substring(1);
                        string[] supportedTypes = { "PDF", "pdf" };
                        if (!supportedTypes.Contains(fileExt))
                        {
                            DocFailedCntExt += 1;
                        }
                        //else if (fuOICDocument.PostedFile.ContentLength > 5120000) // 5 MB = 1024 * 5
                        //{
                        //    DocFailedCntSize += 1;
                        //}
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
                    if (OICDocument == "")
                    {
                        OICDocument = ViewState["oldOICDoc"].ToString();
                    }
                    string errormsg = "";
                    if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                    if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                    if (errormsg == "")
                    {
                        string OICDate = txtOICDate.Text != "" ? Convert.ToDateTime(txtOICDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string ReplyDate = txtReplyDate.Text != "" ? Convert.ToDateTime(txtReplyDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        ds = obj.ByProcedure("USP_Update_CaseRegisDtl",
                             new string[] { "flag",
                                      "OICMaster_Id","OICOrderNumber","OICOrderDate","OICOrderDoc",
                                       "Case_ID","CaseReplyStatus","CaseReplyDate","CaseReplyRemark","UniqueNo", "LastupdatedBy", "LastupdatedByIP" }
                           , new string[] { "1", ddlOicName.SelectedValue,txtOICcaseNumber.Text.Trim(),OICDate,OICDocument,
                            ViewState["ID"].ToString(),ddlCaseReply.SelectedValue,ReplyDate,txtReplyCaseRemark.Text.Trim(),
                            ViewState["UniqueNO"].ToString(),ViewState["Emp_Id"].ToString(),obj.GetLocalIPAddress() }, "dataset");
                    }
                    else
                    {
                        ViewState["fuOICDocument"] = "";
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    }
                }
                if (ds != null)
                {
                    if (ds.Tables[1].Rows[0]["EStatus"].ToString() == "Send")
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["OICEmailID"].ToString()))
                            {
                                string EmailBodyHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
                                System.IO.StreamReader objReader;
                                //objReader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\intel\\main.html");
                                objReader = new StreamReader(EmailBodyHTMLPath);
                                string content = objReader.ReadToEnd();
                                objReader.Close();
                                byte[] pdfBuffer = null;
                                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {

                                    content = content
                                       .Replace("{{OICName}}", ds.Tables[1].Rows[0]["OICName"].ToString())
                                       .Replace("{{CaseNo}}", ds.Tables[1].Rows[0]["FilingNo"].ToString())
                                       .Replace("{{OICOrderNumber}}", ds.Tables[1].Rows[0]["OICOrderNumber"].ToString())
                                       .Replace("{{OICOrderDate}}", ds.Tables[1].Rows[0]["OICOrderDate"].ToString())
                                       .Replace("{{OICDocument}}", ds.Tables[1].Rows[0]["OICOrderDoc"].ToString());
                                    string fileName = ds.Tables[1].Rows[0]["OICOrderDoc"].ToString();
                                    string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
                                    SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                                    using (MailMessage mm = new MailMessage(smtpSection.From, ds.Tables[1].Rows[0]["OICEmailID"].ToString()))
                                    {
                                        mm.Subject = "OIC नियुक्ति आदेश -" + ds.Tables[1].Rows[0]["FilingNo"].ToString();
                                        mm.Body = content;
                                        if (ds.Tables[1].Rows[0]["OICOrderDoc"].ToString() != "")
                                        {
                                            Attachment attachment = new System.Net.Mail.Attachment(Server.MapPath("OICOrderDoc/" + fileName));
                                            mm.Attachments.Add(attachment);

                                        }
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

                                        //obj.ByTextQuery("insert into tblManageResetPassword(UserId, UserEmail, Isactive,ResetPasswordLink) values(" + ds.Tables[0].Rows[0]["userid"].ToString() + ",'" + ds.Tables[0].Rows[0]["UserEmail"].ToString() + "',1, '" + RPurl.ToString() + "')");
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
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            ////lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                            ddlCaseSubject.ClearSelection();
                            ddlCaseSubSubject.ClearSelection();
                            ddlOicName.ClearSelection();
                            ddlParty.ClearSelection();
                            ddlHighprioritycase.ClearSelection();
                            txtCaseDetail.Text = "";
                            BindDetails(sender, e);

                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                            ViewState["oldOICDoc"] = "";
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                            // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCourtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddloldCourtLoca_Id.Items.Clear();
            DataSet dsCourt = obj.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddlCourtType.SelectedValue);
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtLocation.DataTextField = "District_Name";
                ddlCourtLocation.DataValueField = "District_Id";
                ddlCourtLocation.DataSource = dsCourt;
                ddlCourtLocation.DataBind();
            }
            ddlCourtLocation.Items.Insert(0, new ListItem("Select", "0"));
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

            ddlCaseSubSubject.Items.Clear();
            DataSet DsSubs = obj.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            if (DsSubs != null && DsSubs.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubSubject.DataTextField = "CaseSubSubject";
                ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlCaseSubSubject.DataSource = DsSubs;
                ddlCaseSubSubject.DataBind();
            }
            ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsOic = obj.ByDataSet("select OICMaster_ID,OICEmailID,OICMobileNo,OICName,OICM.DesignationID,DM.Designation_Name from tblOICMaster OICM " +
                    "inner join tblDesignationMaster DM on OICM.DesignationID = DM.Designation_Id where OICMaster_Id = " + ddlOicName.SelectedValue);
            if (dsOic.Tables[0].Rows.Count > 0)
            {
                txtOicMobileNo.Text = dsOic.Tables[0].Rows[0]["OICMobileNo"].ToString();
                txtOicEmailId.Text = dsOic.Tables[0].Rows[0]["OICEmailID"].ToString();
                txtOICDesignation.Text = dsOic.Tables[0].Rows[0]["Designation_Name"].ToString();
            }
            else
            {
                txtOicMobileNo.Text = "";
                txtOicEmailId.Text = "";

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    //Petitioner Dtl 1.1
    protected void GrdPetiDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Session["Emp_Id"].ToString() != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are not aligible to edit')", true);
            }
            else
            {


                if (e.CommandName == "EditRecord")
                {
                    ViewState["Petitioner_ID"] = "";
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    Label lblUniqNO = (Label)row.FindControl("lblUniqNO");
                    Label lblPetitionerName = (Label)row.FindControl("lblPetitionerName");
                    Label lblDesignation_Id = (Label)row.FindControl("lblDesignation_Id");
                    Label lblPetitionermobileNo = (Label)row.FindControl("lblPetitionermobileNo");
                    Label lblPresentpostDisID = (Label)row.FindControl("lblPresentpostDisID");
                    Label lblRemark = (Label)row.FindControl("lblRemark");
                    ViewState["Petitioner_ID"] = e.CommandArgument;
                    hdnUniqueNo.Value = !string.IsNullOrEmpty(lblUniqNO.Text) ? lblUniqNO.Text : null;
                    txtPetiName.Text = !string.IsNullOrEmpty(lblPetitionerName.Text) ? lblPetitionerName.Text : null;
                    txtPetiMobileNo.Text = !string.IsNullOrEmpty(lblPetitionermobileNo.Text) ? lblPetitionermobileNo.Text : null;
                    //txtPetiAddRess.Text = !string.IsNullOrEmpty(lblAddress.Text) ? lblAddress.Text : null;
                    txtPetitionerRemark.Text = !string.IsNullOrEmpty(lblRemark.Text) ? lblRemark.Text : null; // Add New label lblRemark by bhanu on 10/05/23
                    if (!string.IsNullOrEmpty(lblPresentpostDisID.Text))
                    {
                        ddlPetitionerPresentDistrict.ClearSelection();
                        ddlPetitionerPresentDistrict.Items.FindByValue(lblPresentpostDisID.Text).Selected = true;
                    }
                    if (!string.IsNullOrEmpty(lblDesignation_Id.Text))
                    {
                        ddlPetiDesigNation.ClearSelection();
                        ddlPetiDesigNation.Items.FindByValue(lblDesignation_Id.Text).Selected = true;
                    }
                    btnPetitioner.Text = "Update";
                }
                if (e.CommandName == "DeleteRecord")
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    DataSet disable = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Petitioner_Id", "LastIsactiveBy", "LastIsactiveByIP" }
                        , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    if (disable != null && disable.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            BindDetails(sender, e);
                        }
                    }
                    else
                        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnPetitioner_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dspeti = new DataSet();
            if (btnPetitioner.Text == "Save")
            {
                dspeti = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Case_Id", "PetitionerName", "Designation_Id", "PetitionerMobileNo", "District_ID", "CreatedBy", "CreatedByIP", "PetitionerRemark" }
                    , new string[] { "1", ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), txtPetiName.Text.Trim(), ddlPetiDesigNation.SelectedValue, txtPetiMobileNo.Text.Trim(), ddlPetitionerPresentDistrict.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), txtPetitionerRemark.Text.Trim() }, "dataset");
            }
            else if (btnPetitioner.Text == "Update")
            {
                dspeti = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Petitioner_Id", "PetitionerName", "Designation_Id", "PetitionerMobileNo", "District_ID", "LastupdatedBy", "LastupdatedByIP", "PetitionerRemark" }
                  , new string[] { "2", ViewState["UniqueNO"].ToString(), ViewState["Petitioner_ID"].ToString(), txtPetiName.Text.Trim(), ddlPetiDesigNation.SelectedValue, txtPetiMobileNo.Text.Trim(), ddlPetitionerPresentDistrict.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), txtPetitionerRemark.Text.Trim() }, "dataset");
            }
            if (dspeti != null && dspeti.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dspeti.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dspeti.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtPetiName.Text = "";
                    ddlPetiDesigNation.ClearSelection();
                    txtPetiMobileNo.Text = "";
                    //txtPetiAddRess.Text = "";
                    btnPetitioner.Text = "Save";
                    txtPetitionerRemark.Text = "";
                    BindDetails(sender, e);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                }
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    //Respondent Dtl 
    protected void ddlResOfficetypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlResOfficeName.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlResOfficetypeName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlResOfficeName.DataTextField = "OfficeName";
                ddlResOfficeName.DataValueField = "Office_Id";
                ddlResOfficeName.DataSource = ds;
                ddlResOfficeName.DataBind();
            }
            ddlResOfficeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdRespondentDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                ViewState["ResponderID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblOfficetype_ID = (Label)row.FindControl("lblOfficetype_ID");
                Label lblOffice_Id = (Label)row.FindControl("lblOffice_Id");
                Label lblDesignation_Id = (Label)row.FindControl("lblDesignation_Id");
                Label lblResName = (Label)row.FindControl("lblRespondentName");
                Label lblResMobileNo = (Label)row.FindControl("lblRespondentNo");
                Label lblDepartent = (Label)row.FindControl("lblDepartment_Id");
                Label lblAddress = (Label)row.FindControl("lblAddress");
                Label lblHod_Id = (Label)row.FindControl("lblHod_Id");
                // Add New Label lblHod_Id by bhanu on 09/05/23 as chnage Req.
                txtResName.Text = !string.IsNullOrEmpty(lblResName.Text) ? lblResName.Text : null;
                txtResMobileNo.Text = !string.IsNullOrEmpty(lblResMobileNo.Text) ? lblResMobileNo.Text : null;
                txtResAddress.Text = !string.IsNullOrEmpty(lblAddress.Text) ? lblAddress.Text : null;
                ViewState["RespondentID"] = e.CommandArgument;
                btnRespondent.Text = "Update";

                if (!string.IsNullOrEmpty(lblDesignation_Id.Text))
                {
                    ddlResDesig.ClearSelection();
                    ddlResDesig.Items.FindByValue(lblDesignation_Id.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblOfficetype_ID.Text))
                {
                    ddlResOfficetypeName.ClearSelection();
                    ddlResOfficetypeName.Items.FindByValue(lblOfficetype_ID.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblDepartent.Text))
                {
                    ddlResDepartment.ClearSelection();
                    ddlResDepartment.Items.FindByValue(lblDepartent.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblHod_Id.Text))
                {
                    ddlResDepartment_SelectedIndexChanged(sender, e);
                    ddlHodName.ClearSelection();
                    ddlHodName.Items.FindByValue(lblHod_Id.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblOffice_Id.Text))
                {
                    ddlResOfficetypeName_SelectedIndexChanged(sender, e);
                    ddlResOfficeName.ClearSelection();
                    ddlResOfficeName.Items.FindByValue(lblOffice_Id.Text).Selected = true;
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "UniqueNo", "Respondent_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void btnRespondent_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsres = new DataSet();
            if (btnRespondent.Text == "Save")
            {   // Add New Column HOD_Id into Insert/Update operation by bhanu on 09/05/23
                dsres = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "HOD_Id", "UniqueNo", "Case_Id", "Officetype_Id", "Office_Id", "Designation_Id", "RespondentName", "RespondentMobileNo", "Address", "Department", "Department_Id", "CreatedBy", "CreatedByIP" }
                    , new string[] { "1", ddlHodName.SelectedValue, ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), ddlResOfficetypeName.SelectedValue, ddlResOfficeName.SelectedValue, ddlResDesig.SelectedValue, txtResName.Text.Trim(), txtResMobileNo.Text.Trim(), txtResAddress.Text.Trim(), ddlResDepartment.SelectedItem.Text, ddlResDepartment.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            else if (btnRespondent.Text == "Update")
            {
                dsres = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "HOD_Id", "UniqueNo", "Respondent_ID", "Officetype_Id", "Office_Id", "Designation_Id", "RespondentName", "RespondentMobileNo", "Address", "Department", "Department_Id", "LastupdatedBy", "LastupdatedByIP" }
                  , new string[] { "2", ddlHodName.SelectedValue, ViewState["UniqueNO"].ToString(), ViewState["RespondentID"].ToString(), ddlResOfficetypeName.SelectedValue, ddlResOfficeName.SelectedValue, ddlResDesig.SelectedValue, txtResName.Text.Trim(), txtResMobileNo.Text.Trim(), txtResAddress.Text.Trim(), ddlResDepartment.SelectedItem.Text, ddlResDepartment.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            if (dsres != null && dsres.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dsres.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dsres.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    txtResName.Text = "";
                    ddlResOfficetypeName.ClearSelection();
                    txtResMobileNo.Text = "";
                    txtResAddress.Text = "";
                    ddlResDepartment.ClearSelection();
                    ddlResDesig.ClearSelection();
                    ddlResOfficeName.ClearSelection();
                    ddlHodName.ClearSelection();
                    btnRespondent.Text = "Save";
                    BindDetails(sender, e);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                }
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    // Dept AdvocatddlDepartment_SelectedIndexChangede Dtl
    protected void btnDeptAdvocate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsdept = new DataSet();
            if (btnDeptAdvocate.Text == "Save")
            {
                dsdept = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "Case_Id", "DeptAdvName", "DeptAdvMobileNo", "CreatedBy", "CreatedByIP" }
                    , new string[] { "1", ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            else if (btnDeptAdvocate.Text == "Update")
            {
                dsdept = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "DeptAdv_Id", "DeptAdvName", "DeptAdvMobileNo", "LastupdatedBy", "LastupdatedByIP" }
                  , new string[] { "2", ViewState["UniqueNO"].ToString(), ViewState["DeptAdv_Id"].ToString(), txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            if (dsdept != null && dsdept.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dsdept.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dsdept.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtDeptAdvocateName.Text = "";
                    txtDeptAdvocateMobileNo.Text = "";
                    btnDeptAdvocate.Text = "Save";
                    BindDetails(sender, e);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                }
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdDeptAdvDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                ViewState["DeptAdv_Id"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblAdvocateName = (Label)row.FindControl("lblAdvocateName");
                Label lblMobileNo = (Label)row.FindControl("lblMobileNo");
                if (lblMobileNo.Text != "")
                    txtDeptAdvocateMobileNo.Text = lblMobileNo.Text;
                if (lblAdvocateName.Text != "")
                    txtDeptAdvocateName.Text = lblAdvocateName.Text;
                ViewState["DeptAdv_Id"] = e.CommandArgument;
                btnDeptAdvocate.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "DeptAdv_Id", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    //Documents Dtl
    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                if (ViewState["AddNewCaseDoc"] == "")
                    ViewState["AddNewCaseDoc"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileCaseDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FileCaseDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    // Ye Code Baad m Chalu krna H
                    // else if (FileCaseDoc.PostedFile.ContentLength > 512000) // 5 MB = 1024 * 5
                    // {
                    // DocFailedCntSize += 1;
                    // }
                    //else
                    //{
                    strFileName = FileCaseDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), strFileName);
                    FileCaseDoc.SaveAs(path);

                    ViewState["AddNewCaseDoc"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                if (errormsg == "")
                {
                    if (btnSaveDoc.Text == "Save")
                    {

                        ds = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "Doc_Name", "Doc_Path", "CreatedBy", "CreatedByIP" }
                            , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), txtDocumentName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSaveDoc.Text == "Update" && ViewState["DocID"].ToString().ToString() != "" && ViewState["DocID"].ToString() != null)
                    {
                        if (ViewState["DocPath"] != ViewState["AddNewCaseDoc"])
                        {
                            string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), ViewState["DocPath"].ToString());
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }

                        ds = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "CaseDoc_ID", "Doc_Name", "Doc_Path", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["DocID"].ToString(), txtDocumentName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            ViewState["AddNewCaseDoc"] = "";
                            BindDetails(sender, e);
                            btnSaveDoc.Text = "Save";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ViewState["AddNewCaseDoc"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
                ErrorLogCls.SendErrorToText(ex);
            }
        }
    }
    protected void GrdCaseDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["DocID"] = "";
            if (e.CommandName == "EditRecord")
            {
                ViewState["DocPath"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDocPath = (Label)row.FindControl("lblDocPath");
                Label lblDocName = (Label)row.FindControl("lblDocName");
                if (lblDocPath.Text != "")
                {
                    ViewState["DocPath"] = lblDocPath.Text;
                    ViewState["AddNewCaseDoc"] = lblDocPath.Text;
                    RfvUploadDoc.Enabled = false;
                }
                if (lblDocName.Text != "")
                    txtDocumentName.Text = lblDocName.Text;
                ViewState["DocID"] = e.CommandArgument;
                btnSaveDoc.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "UniqueNo", "CaseDoc_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    // Hearing Dtl
    protected void btnAddHeairng_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
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
                    //else if (FileHearingDoc.PostedFile.ContentLength > 5120) // 5 KB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{
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
                    // }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {
                    string NextHearingDate = txtNextHearingDate.Text != "" ? Convert.ToDateTime(txtNextHearingDate.Text, cult).ToString("yyyy/MM/dd") : "";

                    if (btnAddHeairng.Text == "Save")
                    {
                        ds = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "NextHearingDate", "HearingDoc", "CreatedBy", "CreatedByIP" }
                            , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), NextHearingDate, ViewState["HearingDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnAddHeairng.Text == "Update" && ViewState["Hearing_Id"].ToString().ToString() != "" && ViewState["Hearing_Id"].ToString() != null)
                    {
                        if (ViewState["EditDocPath"] != ViewState["HearingDoc"])
                        {
                            string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), ViewState["EditDocPath"].ToString());
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                        ds = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "NextHearing_ID", "NextHearingDate", "HearingDoc", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["Hearing_Id"].ToString(), NextHearingDate, ViewState["HearingDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");


                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            txtNextHearingDate.Text = "";
                            ViewState["HearingDoc"] = "";
                            BindDetails(sender, e);
                            btnSaveDoc.Text = "Save";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ViewState["HearingDoc"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogCls.SendErrorToText(ex);
            }
        }
    }
    protected void GrdHearingDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["Hearing_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                ViewState["EditDocPath"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblHearingDate = (Label)row.FindControl("lblHearingDate");
                Label lblHearingDocPath = (Label)row.FindControl("lblHearingDocPath");
                if (lblHearingDocPath.Text != "")
                {
                    ViewState["EditDocPath"] = lblHearingDocPath.Text;
                    ViewState["HearingDoc"] = lblHearingDocPath.Text;
                    //rfvhearingFile.Enabled = false;
                }
                if (lblHearingDate.Text != "")
                    txtNextHearingDate.Text = lblHearingDate.Text;
                ViewState["Hearing_Id"] = e.CommandArgument;
                btnAddHeairng.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "UniqueNo", "NextHearing_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    //Case Dispose
    protected void ddlDisponsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            if (ddlDisponsType.SelectedIndex > 0)
            {
                OrderWithDir_Div.Visible = false;
                OrderBy1.Visible = true;
                OrderBy2.Visible = true;
                HearingDtl_CaseDispose.Visible = true;
                DivOrderTimeline.Visible = true;
                OrderSummary_Div.Visible = true;
                ddlOrderWith.ClearSelection();
                CimplianceSt_Div.Visible = true;
                if (ddlDisponsType.SelectedValue == "2")
                {
                    OrderBy1.Visible = false; OrderBy2.Visible = false; HearingDtl_CaseDispose.Visible = false;
                    DivOrderTimeline.Visible = false; OrderSummary_Div.Visible = false; OrderWithDir_Div.Visible = true;
                    ddlCompliaceSt.ClearSelection(); CimplianceSt_Div.Visible = false; RequiredFieldValidator6.Enabled = true; ddlOrderWith.ClearSelection();
                }
            }
            else
            {
                OrderWithDir_Div.Visible = false; HearingDtl_CaseDispose.Visible = false; OrderBy1.Visible = false; OrderBy2.Visible = false;
                DivOrderTimeline.Visible = false; OrderSummary_Div.Visible = false; CimplianceSt_Div.Visible = false;
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false; ddlOrderWith.ClearSelection();
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void rdCaseDispose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            caseDisposeYes.Visible = false;
            if (rdCaseDispose.SelectedValue == "1")
            {
                caseDisposeYes.Visible = true; OrderWithDir_Div.Visible = false;
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false;
                Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                //divAdditionalReturn.Visible = false;
                btnCaseDispose.Text = "Disposal";
            }
            else if (rdCaseDispose.SelectedValue == "2")
            {
                // divAdditionalReturn.Visible = true;
                caseDisposeYes.Visible = false; CimplianceSt_Div.Visible = false; OrderBy1.Visible = false; HearingDtl_CaseDispose.Visible = false;
                OrderBy2.Visible = false; DivOrderTimeline.Visible = false; ddlDisponsType.ClearSelection(); OrderSummary_Div.Visible = false;
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; OrderWithDir_Div.Visible = false;
                Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
            else
            {
                caseDisposeYes.Visible = false;
                //divAdditionalReturn.Visible = false;
                OrderBy1.Visible = false; OrderBy2.Visible = false; CimplianceSt_Div.Visible = false;
                DivOrderTimeline.Visible = false; ddlDisponsType.ClearSelection();
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; OrderWithDir_Div.Visible = false;
                Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false;
                //txtAdditionalReturn.Text = "";
                HearingDtl_CaseDispose.Visible = false; OrderSummary_Div.Visible = false; btnCaseDispose.Text = "Disposal";
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnCaseDispose_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                lblMsg.Text = "";
                ViewState["DisposeDOC"] = "";
                ViewState["ComplianceDOC"] = "";
                ViewState["RejoinderDOC"] = "";
                ViewState["AdditionalDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FielUpcaseDisposeOrderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FielUpcaseDisposeOrderDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = FielUpcaseDisposeOrderDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    FielUpcaseDisposeOrderDoc.SaveAs(path);

                    ViewState["DisposeDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                if (ComplianceDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(ComplianceDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = ComplianceDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    ComplianceDoc.SaveAs(path);

                    ViewState["ComplianceDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                if (RejoinderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(RejoinderDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = RejoinderDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    RejoinderDoc.SaveAs(path);

                    ViewState["RejoinderDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                if (AdditionalDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(AdditionalDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = AdditionalDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    AdditionalDoc.SaveAs(path);

                    ViewState["AdditionalDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                if (errormsg == "")
                {
                    if (btnCaseDispose.Text == "Disposal")
                    {
                        //if (ViewState["Department"].ToString() == "NA")
                        //{
                        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Fill Case Details', 'info')", true);
                        //}
                        //else
                        //{
                        string DisposalDate = txtCaseDisposeDate.Text != "" ? Convert.ToDateTime(txtCaseDisposeDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string ComplianceDate = txtCompianceDate.Text != "" ? Convert.ToDateTime(txtCompianceDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string RejoinderDate = txtRejoinderDate.Text != "" ? Convert.ToDateTime(txtRejoinderDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string AdditionalDate = txtAdditionalDate.Text != "" ? Convert.ToDateTime(txtAdditionalDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        ds = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "Case_ID", "UniqueNo", "CaseDisposal_Status", "Compliance_Status",
                                "CaseDisposalType_Id", "CaseDisposal_Date", "CaseDisposal_Timeline", "CaseDisposal_Doc", "OrderSummary","ComplianceStatus_ID","ComplianceNo","ComplianceDate",
                                "ComplianceDoc","ComplianceRemark","AnyRejoinder_ID","RejoinderNo","RejoinderDate","RejoinderDoc","RejoinderRemark","AdditionalReturn_ID","AdditionalNo"
                                ,"AdditionalDate","AdditionalDoc","AdditionalRemark","OrderWithDirection_ID","LastupdatedBy", "LastupdatedByIP" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), rdCaseDispose.SelectedItem.Text, ddlCompliaceSt.SelectedValue,
                                    ddlDisponsType.SelectedValue, DisposalDate, txtOrderimpletimeline.Text.Trim(), ViewState["DisposeDOC"].ToString(), txtorderSummary.Text.Trim(),
                                    ddlComlianceYesNo.SelectedValue,txtComplianceNo.Text.Trim(),ComplianceDate, ViewState["ComplianceDOC"].ToString(),txtComplianceRemark.Text.Trim(),
                                    ddlAnyRejoinder.SelectedValue,txtRejoinderNo.Text.Trim(),RejoinderDate,ViewState["RejoinderDOC"].ToString(),txtRejoinderRemark.Text.Trim(),
                                    ddlAdditionalReturn.SelectedValue,txtAdditionalNo.Text.Trim(),AdditionalDate,ViewState["AdditionalDOC"].ToString(),txtAdditionalRemar.Text.Trim(),
                                    ddlOrderWith.SelectedValue,ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                        //}
                    }
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                                txtOrderimpletimeline.Text = "";
                                rdCaseDispose.ClearSelection();
                                ddlDisponsType.ClearSelection();
                                txtCaseDisposeDate.Text = "";
                                ViewState["DisposeDOC"] = "";
                                BindDetails(sender, e);
                                btnCaseDispose.Text = "Disposal";
                                rdCaseDispose_SelectedIndexChanged(sender, e);
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                            }
                            else
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                        }

                    }
                }
                else
                {
                    ViewState["DisposeDOC"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
        }

        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    // Old Case No. Dtl
    protected void btnOldCase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
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
                    // else if (FU1.PostedFile.ContentLength > 512000) // 5 MB = 1024 * 5
                    // {
                    //     DocFailedCntSize += 1;
                    // }
                    // else
                    // {
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
                    // }
                }
                if (FU2.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU2.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    // else if (FU2.PostedFile.ContentLength > 512000) // 5 MB = 1024 * 5
                    // {
                    //     DocFailedCntSize += 1;
                    // }
                    // else
                    // {
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
                    // }
                }
                if (FU3.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU3.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    // else if (FU3.PostedFile.ContentLength > 512000) // 5 MB = 1024 * 5
                    // {
                    //     DocFailedCntSize += 1;
                    // }
                    // else
                    // {
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
                    // }
                }
                if (FU4.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU4.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    // else if (FU4.PostedFile.ContentLength > 512000) // 5 MB = 1024 * 5
                    // {
                    //     DocFailedCntSize += 1;
                    // }
                    // else
                    // {
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
                    // }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                if (errormsg == "")
                {
                    string OrderDate = !string.IsNullOrEmpty(txtOrderDate_OldCase.Text) ? Convert.ToDateTime(txtOrderDate_OldCase.Text, cult).ToString("yyyy/MM/dd") : null;
                    if (btnOldCase.Text == "Save")
                    {
                        if (FU1.HasFile)// Insert data into oldCase Record table
                        {
                            ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag","OrderDate", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink",
                                           "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                             new string[] { "1",OrderDate, ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text,  ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Case Details", ViewState["FU1"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue,
                               ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                        }
                        if (FU2.HasFile)
                        {
                            ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "OrderDate", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                               new string[] { "1", OrderDate, ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Description Of Proceedings", ViewState["FU2"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                        }
                        if (FU3.HasFile)
                        {
                            ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "OrderDate", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                               new string[] { "1", OrderDate, ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Decision", ViewState["FU3"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                        }
                        if (FU4.HasFile)
                        {
                            ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "OrderDate", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                               new string[] { "1", OrderDate, ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Other", ViewState["FU4"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                        }
                        if (ViewState["FU1"] == null && ViewState["FU2"] == null && ViewState["FU3"] == null && ViewState["FU4"] == null)
                        {
                            ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "OrderDate", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "CourtDistLoca_Id", "CourtType_Id", "CreatedBy", "CreatedByIP", "Casetype_Id" },
                                                                            new string[] { "1", OrderDate, ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedValue, ddlCourtType.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ddlCasetype.SelectedValue }, "dataset");
                        }
                    }
                    else if (btnOldCase.Text == "Update" && ViewState["OldCase_Id"] != null)
                    {
                        ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "OrderDate", "Id", "UniqueNo", "oldCaseYear", "CourtType_Id", "Court", "CourtDistLoca_Id", "Casetype_Id", "OldCasetype", "LastupdatedBy", "LastupdatedByIP" }
                            , new string[] { "2", OrderDate, ViewState["OldCase_Id"].ToString(), ViewState["UniqueNO"].ToString(), ddloldCaseYear.SelectedItem.Text, ddloldCaseCourt.SelectedValue, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedValue, ddloldCasetype.SelectedValue, ddloldCasetype.SelectedItem.Text, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            btnOldCase.Text = "Save";
                            txtoldCaseNo.Text = "";
                            ddloldCaseYear.ClearSelection();
                            ddloldCasetype.ClearSelection();
                            ddloldCourtLoca_Id.ClearSelection();
                            ddloldCaseCourt.ClearSelection();
                            txtOrderDate_OldCase.Text = "";
                            Div_Doc1.Visible = true; Div_Doc2.Visible = true; Div_Doc3.Visible = true; Div_Doc4.Visible = true;
                            ViewState["FU1"] = ""; ViewState["FU2"] = ""; ViewState["FU3"] = ""; ViewState["FU4"] = "";
                            BindDetails(sender, e);
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ViewState["FU1"] = ""; ViewState["FU2"] = ""; ViewState["FU3"] = ""; ViewState["FU4"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdOldCaseDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["OldCase_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOldCaseNo = (Label)row.FindControl("lblOldCaseNo");
                Label lblOldCaseYear = (Label)row.FindControl("lblOldCaseYear");
                Label lblOldCasetype_Id = (Label)row.FindControl("lblOldCasetype_Id");
                Label lblOldCourt_Id = (Label)row.FindControl("lblOldCourt_Id");
                Label lblOldCourtLoca_Id = (Label)row.FindControl("lblOldCourtLoca_Id");
                Label lblOldDocName = (Label)row.FindControl("lblOldDocName");
                Label lblOrderDate = (Label)row.FindControl("lblOrderDate");

                ViewState["OldCase_Id"] = e.CommandArgument;
                btnOldCase.Text = "Update";
                txtOrderDate_OldCase.Text = !string.IsNullOrEmpty(lblOrderDate.Text) ? lblOrderDate.Text : null;  // Add New lbl by bhanu on 09/05/2023 as change req.
                txtoldCaseNo.Text = !string.IsNullOrEmpty(lblOldCaseNo.Text) ? lblOldCaseNo.Text : null;
                if (!string.IsNullOrEmpty(lblOldCaseYear.Text)) ddloldCaseYear.ClearSelection(); ddloldCaseYear.Items.FindByValue(lblOldCaseYear.Text).Selected = true;
                if (!string.IsNullOrEmpty(lblOldCourt_Id.Text)) ddloldCaseCourt.ClearSelection(); ddloldCaseCourt.Items.FindByValue(lblOldCourt_Id.Text).Selected = true;
                if (!string.IsNullOrEmpty(lblOldCasetype_Id.Text)) ddloldCasetype.ClearSelection(); ddloldCasetype.Items.FindByValue(lblOldCasetype_Id.Text).Selected = true;
                if (!string.IsNullOrEmpty(lblOldCourtLoca_Id.Text))
                {
                    ddloldCaseCourt_SelectedIndexChanged(sender, e);
                    ddloldCourtLoca_Id.ClearSelection();
                    ddloldCourtLoca_Id.Items.FindByValue(lblOldCourtLoca_Id.Text).Selected = true;
                }
                Div_Doc1.Visible = false; Div_Doc2.Visible = false; Div_Doc3.Visible = false; Div_Doc4.Visible = false;
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "UniqueNo", "Id", "LastisactiveBy", "LastisactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddloldCaseCourt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddloldCourtLoca_Id.Items.Clear();
            DataSet dsCourt = obj.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddloldCaseCourt.SelectedValue);
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddloldCourtLoca_Id.DataTextField = "District_Name";
                ddloldCourtLoca_Id.DataValueField = "District_Id";
                ddloldCourtLoca_Id.DataSource = dsCourt;
                ddloldCourtLoca_Id.DataBind();
            }
            ddloldCourtLoca_Id.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }

    // Petitioner Advocate Dtl
    protected void btnPetiAdvSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnPetiAdvSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "PetiAdv_Name", "PetiAdv_MobileNo", "CreatedBy", "CreatedByIP" }
                        , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), txtPetiAdvocateName.Text.Trim(), txtPetiAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnPetiAdvSave.Text == "Update" && ViewState["PetiAdv_Id"].ToString().ToString() != "" && ViewState["PetiAdv_Id"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "PetiAdv_Id", "PetiAdv_Name", "PetiAdv_MobileNo", "LastupdatedBy", "LastupdatedByIP" }
                        , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["PetiAdv_Id"].ToString(), txtPetiAdvocateName.Text.Trim(), txtPetiAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtPetiAdvocateName.Text = "";
                        txtPetiAdvocateMobileNo.Text = "";
                        ViewState["PetiAdv_Id"] = "";
                        BindDetails(sender, e);
                        btnPetiAdvSave.Text = "Save";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                    }
                    else
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdPetiAdv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["PetiAdv_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblPetiAdvocatename = (Label)row.FindControl("lblPetiAdvocatename");
                Label lblPetiAdvocatMObile = (Label)row.FindControl("lblPetiAdvocatMObile");


                ViewState["PetiAdv_Id"] = e.CommandArgument;
                btnPetiAdvSave.Text = "Update";
                if (lblPetiAdvocatename.Text != "") txtPetiAdvocateName.Text = lblPetiAdvocatename.Text;
                if (lblPetiAdvocatMObile.Text != "") txtPetiAdvocateMobileNo.Text = lblPetiAdvocatMObile.Text;
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "UniqueNo", "PetiAdv_Id", "LastisactiveBy", "LastisactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdCaseDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Link = (HyperLink)e.Row.FindControl("hyperViewLink");
                HyperLink DocWithFolderPath = (HyperLink)e.Row.FindControl("hyperViewDoc");
                Label lbldoc = (Label)e.Row.FindControl("lblDocPath");
                int coul = GrdCaseDocument.Columns.Count;

                string name = lbldoc.Text;
                name.StartsWith("https");
                if (name.StartsWith("https") == true)
                    Link.Visible = true;
                else DocWithFolderPath.Visible = true;

            }
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
                divReplyRemark.Visible = true;
            }
            else
            {
                divReplyRemark.Visible = false;
                divReplyDate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCaserRelated_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeleteCase.ClearSelection();
        if (ddlCaserRelated.SelectedValue == "2")
        {
            divCaseRelate.Visible = true;
        }
        else
        {
            divCaseRelate.Visible = false;
        }
    }
    protected void ddlDeleteCase_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeleteCase.SelectedValue == "1")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ValidatePage()", true);
            ddlDeleteCase.ClearSelection();
        }
        else
        {

        }

    }
    protected void btnYes_Click(object sender, EventArgs e)
    {
        ds = obj.ByProcedure("USP_Update_CaseRegisDtl",
            new string[] { "flag", "Case_ID", "UniqueNo", "LastupdatedBy", "LastupdatedByIP" }
                           , new string[] { "3", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
            {
                Response.Redirect("WPCaseList.aspx");
            }
            else ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
        }

    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlHOD.SelectedValue == "8")
            {
                divOIC.Visible = false;
            }
            else
            {
                divOIC.Visible = true;
            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlAskForOldCase_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtoldCaseNo.Text = "";
            ddloldCaseYear.ClearSelection();
            ddloldCasetype.ClearSelection();
            ddloldCaseCourt.ClearSelection();
            ddloldCourtLoca_Id.ClearSelection();
            if (ddlAskForOldCase.SelectedValue == "1")
            {
                FieldViewOldCaseDtl.Visible = true;
            }
            else
            {
                FieldViewOldCaseDtl.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillOicName();
    }
    // Update Case Detatil Fieldset info.
    protected void btnUpdateCaseDtl_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet Dsupdate = new DataSet();
            string HODId = "";
            foreach (ListItem item in ddlHOD.Items)
            {
                if (item.Selected)
                {
                    HODId += item.Value + ",";
                }
            }
            if (btnUpdateCaseDtl.Text == "Update")
            {
                // add New Column PolicyMeterStatus by bhanu on Date 09/05/2023
                string HinghPriorityStatus = ddlHighprioritycase.SelectedIndex > 0 ? ddlHighprioritycase.SelectedItem.Text.Trim() : null;
                string PolicyMeterStatus = ddlPolicyMeter.SelectedIndex > 0 ? ddlPolicyMeter.SelectedItem.Text.Trim() : null;
                Dsupdate = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "HOD_Id","CaseBelongto_Id", "District_ID", "CaseSubject_Id", "CaseSubSubj_Id",
                "HighPriorityCase_Status", "LastupdatedBy", "LastupdatedByIP", "Case_ID", "UniqueNo","CaseDetail","PolicyMeterStatus" },
                    new string[] { "4", HODId,ddlCaseBelongto.SelectedValue, ddlDistrict.SelectedValue, ddlCaseSubject.SelectedValue,
                    ddlCaseSubSubject.SelectedValue, HinghPriorityStatus,ViewState["Emp_Id"].ToString(),obj.GetLocalIPAddress(),ViewState["ID"].ToString(),ViewState["UniqueNO"].ToString(),txtCaseDetail.Text.Trim(),PolicyMeterStatus}, "dataset");
            }
            if (Dsupdate.Tables.Count > 0)
            {
                if (Dsupdate.Tables[0].Rows.Count > 0)
                {
                    if (Dsupdate.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", Dsupdate.Tables[0].Rows[0]["ErrMsg"].ToString());
                        ddlHOD.ClearSelection(); ddlDistrict.ClearSelection(); ddlCaseSubject.ClearSelection(); ddlCaseBelongto.ClearSelection();
                        ddlCaseSubSubject.ClearSelection(); ddlHighprioritycase.ClearSelection(); txtCaseDetail.Text = "";
                        BindDetails(sender, e);
                    }
                    else lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", Dsupdate.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    // Intirm Order
    protected void btnIntrimOrder_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsintrim = new DataSet();
            string IntrimOrderStartDate = txtIntirmOrderDate.Text.Trim() != "" ? Convert.ToDateTime(txtIntirmOrderDate.Text, cult).ToString("yyyy/MM/dd") : null;
            string IntrimOrderEndDate = txtIntrimOrderEnddate.Text.Trim() != "" ? Convert.ToDateTime(txtIntrimOrderEnddate.Text, cult).ToString("yyyy/MM/dd") : null;
            dsintrim = obj.ByProcedure("USP_InserUpdateIntrimOrder", new string[] { "IntrimOrderStartDate", "IntrimOrderEndDate", "IntrimOrderTimeline", "IntrimOrderSummary", "IntrimOrderAnyPrevPP", "CreatedBy", "CreatedByIP", "Case_ID" },
                new string[] { IntrimOrderStartDate, IntrimOrderEndDate, txtIntrimTimeline.Text.Trim(), txtIntrimOrderSummary.Text.Trim(), txtIntrimPrevPP.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["ID"].ToString() }, "dataset");
            if (dsintrim.Tables[0].Rows.Count > 0)
            {
                if (dsintrim.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", dsintrim.Tables[0].Rows[0]["ErrMsg"].ToString());
                    txtIntirmOrderDate.Text = ""; txtIntrimTimeline.Text = ""; txtIntrimOrderSummary.Text = ""; txtIntrimOrderEnddate.Text = "";
                    btnIntrimOrder.Text = "Save";
                    BindDetails(sender, e);
                }
                else lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", dsintrim.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
            else lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", dsintrim.Tables[0].Rows[0]["ErrMsg"].ToString());
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlIntrimOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlIntrimOrder.SelectedValue == "1") Div_IntrimOrderYesNO.Visible = true;
            else Div_IntrimOrderYesNO.Visible = false;

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlResDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {  // This Event Made by Bhanu on 09/05/23 For Respondent Field.
            ddlHodName.Items.Clear();
            DataSet dsHod = obj.ByDataSet("select HOD_ID, HodName from tblHODMaster where Dept_Id =" + ddlResDepartment.SelectedValue);
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

    protected void ddlCaseBelogto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCaseBelongto.SelectedIndex > 0)
            {
                divDistrictLocation.Visible = true;
            }
            else divDistrictLocation.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlYesOrNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlYesOrNo.SelectedValue == "1")
            {
                divReturnAndReply.Visible = false;
                divReason.Visible = false;
                divBtn.Visible = true;
                divReceiptNo.Visible = true;
                divReceiptDate.Visible = true;
                divActionAppealFrom.Visible = true;
                Div_WPRPFiledOrNot.Visible = true;
                Div_FileMoment.Visible = true;
                ddlWPRPFiledOrNot.ClearSelection();
                RDFileMoment.ClearSelection();
            }
            else if (ddlYesOrNo.SelectedValue == "2")
            {
                divReturnAndReply.Visible = false;
                divReason.Visible = true;
                divBtn.Visible = true;
                divReceiptNo.Visible = false;
                divActionAppealFrom.Visible = false;
                divReceiptDate.Visible = false;
                Div_WPRPFiledOrNot.Visible = true;
                Div_CaseNo.Visible = false;
                div_CaseYear.Visible = false;
                div_WPRPRemark.Visible = false;
                Div_WPRPFileUp.Visible = false;
                Div_FileMoment.Visible = true;
                ddlWPRPFiledOrNot.ClearSelection();
                RDFileMoment.ClearSelection();
            }
            else
            {
                divReason.Visible = false; divReturnAndReply.Visible = false; divBtn.Visible = false; RDFileMoment.ClearSelection();
                divReceiptNo.Visible = false; divReceiptDate.Visible = false; divActionAppealFrom.Visible = false;
                Div_WPRPFiledOrNot.Visible = false; ddlWPRPFiledOrNot.ClearSelection(); Div_FileMoment.Visible = false;
                Div_CaseNo.Visible = false; div_CaseYear.Visible = false; Div_WPRPFileUp.Visible = false; div_WPRPRemark.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAction.SelectedValue == "1")
            {
                divYasOrNo.Visible = true;
                divReturnAndReply.Visible = false; RDFileMoment.ClearSelection();
                divReason.Visible = false; divBtn.Visible = false; OrderBy1.Visible = false; OrderBy2.Visible = false;
                HearingDtl_CaseDispose.Visible = false; DivOrderTimeline.Visible = false; OrderSummary_Div.Visible = false; caseDisposeYes.Visible = false;
                CimplianceSt_Div.Visible = false; divReceiptNo.Visible = false; divReceiptDate.Visible = false; Div_WPRPFiledOrNot.Visible = false;
                Div_CaseNo.Visible = false; div_CaseYear.Visible = false; Div_WPRPFileUp.Visible = false; div_WPRPRemark.Visible = false;
                divActionAppealFrom.Visible = false; div_ComplianceYesOrNo.Visible = false;
                // Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false;div_Compliance_AnyRejoinder.Visible = false;
                //rdCaseDispose.ClearSelection();
            }
            else if (ddlAction.SelectedValue == "2")
            {
                divYasOrNo.Visible = false; div_ComplianceYesOrNo.Visible = true; RDFileMoment.ClearSelection();
                divReturnAndReply.Visible = false; divReason.Visible = false; divBtn.Visible = false;
                divReceiptNo.Visible = false; divReceiptDate.Visible = false; Div_WPRPFiledOrNot.Visible = false; divActionAppealFrom.Visible = false;
                Div_CaseNo.Visible = false; div_CaseYear.Visible = false; Div_WPRPFileUp.Visible = false; div_WPRPRemark.Visible = false;// ddlAnyRejoinder.ClearSelection();div_Compliance_AnyRejoinder.Visible = false;
                //Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false;
                //rdCaseDispose.ClearSelection();
            }
            else
            {
                divYasOrNo.Visible = false;
                divReturnAndReply.Visible = false; div_ComplianceYesOrNo.Visible = false; RDFileMoment.ClearSelection();
                divReason.Visible = false; divBtn.Visible = false; caseDisposeYes.Visible = false; CimplianceSt_Div.Visible = false;
                OrderBy1.Visible = false; OrderBy2.Visible = false; HearingDtl_CaseDispose.Visible = false; DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false; divReceiptNo.Visible = false; divReceiptDate.Visible = false;
                Div_WPRPFiledOrNot.Visible = false; divActionAppealFrom.Visible = false; div_WPRPRemark.Visible = false;
                // Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false;
                Div_CaseNo.Visible = false; div_CaseYear.Visible = false; Div_WPRPFileUp.Visible = false;// div_Compliance_AnyRejoinder.Visible = false;
            }
            ddlYesOrNo.ClearSelection();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ViewState["ActionDoc"] = "";
                ViewState["WPRPDoc"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileUp.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileUp.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    strFileName = FileUp.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-FileMoment-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    FileUp.SaveAs(path);

                    ViewState["ActionDoc"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";

                }
                if (WPRPFileUp.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(WPRPFileUp.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    strFileName = WPRPFileUp.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-FileMoment-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    FileUp.SaveAs(path);
                    ViewState["WPRPDoc"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }
                if (errormsg == "")
                {
                    string ReceiptDate = txtReceiptDate.Text != "" ? Convert.ToDateTime(txtReceiptDate.Text, cult).ToString("yyyy/MM/dd") : "";
                    string DispatchDate = txtDispatchDate.Text != "" ? Convert.ToDateTime(txtDispatchDate.Text, cult).ToString("yyyy/MM/dd") : "";
                    ds = obj.ByProcedure("USP_Insert_DistrictWiseReturnFileAndAppeal", new string[] { "Case_ID", "Action_ID", "ActionYesOrNo_ID", "ReceiptNo", "ReceiptDate", "ActionAppealedFrom",
                                                                                                      "HOD_Id", "DispatchNo", "DispatchDate", "Action_Doc", "DistrictRemark", "Reason",
                                                                                                      "WP_RPFiledOrNot", "Case_No", "CaseYear", "WP_RPDocumnet", "WP_RPRemark","FileMoment_ID", "CreatedBy", "CreatedByIP" }
                                                    , new string[] { ViewState["ID"].ToString(), ddlAction.SelectedValue, ddlYesOrNo.SelectedValue, txtReceiptNo.Text.Trim(), ReceiptDate, txtActionAppealedFrom.Text.Trim(), ddlNameHod.SelectedValue,
                                                                     txtDispatchNo.Text.Trim(), DispatchDate, ViewState["ActionDoc"].ToString(), txtHODRemark.Text.Trim(), txtReason.Text.Trim(),ddlWPRPFiledOrNot.SelectedValue,txtCaseNo.Text.Trim(),
                                                                      ddlWPRPCaseYear.SelectedValue,ViewState["WPRPDoc"].ToString() ,txtWPRPRemark.Text.Trim(),RDFileMoment.SelectedValue,ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                txtReceiptNo.Text = ""; ddlAction.ClearSelection(); ddlYesOrNo.ClearSelection();
                                ddlWPRPFiledOrNot.ClearSelection(); txtCaseNo.Text = ""; ddlWPRPCaseYear.ClearSelection();
                                txtWPRPRemark.Text = ""; txtReceiptDate.Text = ""; txtActionAppealedFrom.Text = "";
                                ddlNameHod.ClearSelection(); txtDispatchNo.Text = ""; txtDispatchDate.Text = ""; RDFileMoment.ClearSelection();
                                txtHODRemark.Text = ""; txtReason.Text = ""; ViewState["ActionDoc"] = ""; ViewState["WPRPDoc"] = "";
                                GetReturnFileAppeal(sender, e);
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                            }
                            else
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                        }
                    }
                }
                else
                {
                    ViewState["ActionDoc"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GetReturnFileAppeal(object sender, EventArgs e)
    {
        try
        {
            ds = obj.ByProcedure("Usp_GetReturnFileAppeal", new string[] { "flag", "Case_ID" }
             , new string[] { "1", ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Action_ID"].ToString()))
                {
                    ddlAction.ClearSelection();
                    ddlAction.Items.FindByValue(ds.Tables[0].Rows[0]["Action_ID"].ToString().Trim()).Selected = true;
                    ddlAction_SelectedIndexChanged(sender, e);
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ActionYesOrNo_ID"].ToString()))
                {
                    ddlYesOrNo.ClearSelection();
                    ddlYesOrNo.Items.FindByValue(ds.Tables[0].Rows[0]["ActionYesOrNo_ID"].ToString().Trim()).Selected = true;
                    ddlYesOrNo_SelectedIndexChanged(sender, e);
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["WP_RPFiledOrNot"].ToString()))
                {
                    ddlWPRPFiledOrNot.ClearSelection();
                    ddlWPRPFiledOrNot.Items.FindByValue(ds.Tables[0].Rows[0]["WP_RPFiledOrNot"].ToString().Trim()).Selected = true;
                    ddlWPRPFiledOrNot_SelectedIndexChanged(sender, e);
                }
                txtCaseNo.Text = ds.Tables[0].Rows[0]["Case_No"].ToString();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["CaseYear"].ToString()))
                {
                    ddlWPRPCaseYear.ClearSelection();
                    ddlWPRPCaseYear.Items.FindByValue(ds.Tables[0].Rows[0]["CaseYear"].ToString().Trim()).Selected = true;
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["WP_RPDocumnet"].ToString()))
                {
                    string LinkWp = ds.Tables[0].Rows[0]["WP_RPDocumnet"].ToString();
                    string lbldoc1 = "DisposalDocs/" + LinkWp;
                    ViewState["WP_RPDocumnet"] = LinkWp;
                    HPWP_RPFile.NavigateUrl = lbldoc1;
                    HPWP_RPFile.Visible = true;
                }
                txtWPRPRemark.Text = ds.Tables[0].Rows[0]["WP_RPRemark"].ToString();
                txtReceiptNo.Text = ds.Tables[0].Rows[0]["ReceiptNo"].ToString();
                txtReceiptDate.Text = ds.Tables[0].Rows[0]["ReceiptDate"].ToString();
                txtActionAppealedFrom.Text = ds.Tables[0].Rows[0]["ActionAppealedFrom"].ToString();
                txtDispatchNo.Text = ds.Tables[0].Rows[0]["DispatchNo"].ToString();
                txtDispatchDate.Text = ds.Tables[0].Rows[0]["DispatchDate"].ToString();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["FileMoment_ID"].ToString()))
                {
                    RDFileMoment.ClearSelection();
                    RDFileMoment.Items.FindByValue(ds.Tables[0].Rows[0]["FileMoment_ID"].ToString().Trim()).Selected = true;
                    RDFileMoment_SelectedIndexChanged(sender, e);
                }


                txtHODRemark.Text = ds.Tables[0].Rows[0]["DistrictRemark"].ToString();

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Reason"].ToString()))
                {
                    txtReason.Text = ds.Tables[0].Rows[0]["Reason"].ToString();
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Action_Doc"].ToString()))
                {
                    string Link = ds.Tables[0].Rows[0]["Action_Doc"].ToString();
                    string lbldoc = "DisposalDocs/" + Link;
                    ViewState["Action_Doc"] = Link;
                    FileHpView.NavigateUrl = lbldoc;
                    FileHpView.Visible = true;

                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["HOD_Id"].ToString()))
                {
                    ddlNameHod.ClearSelection();
                    ddlNameHod.Items.FindByValue(ds.Tables[0].Rows[0]["HOD_Id"].ToString().Trim()).Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlWPRPFiledOrNot_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlWPRPFiledOrNot.SelectedValue == "1")
            {
                Div_CaseNo.Visible = true; div_CaseYear.Visible = true; div_WPRPRemark.Visible = true; Div_WPRPFileUp.Visible = true; rfvCaseNo.Enabled = true;
                rfvCaseYear.Enabled = true; rfvWPRPRemark.Enabled = true;
            }
            else
            {
                Div_CaseNo.Visible = false;
                div_CaseYear.Visible = false;
                div_WPRPRemark.Visible = false;
                Div_WPRPFileUp.Visible = false;
                rfvCaseYear.Enabled = true; rfvWPRPRemark.Enabled = true; rfvCaseNo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void ddlAnyRejoinder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAnyRejoinder.SelectedValue == "1")
            {
                Div_RejoinderNo.Visible = true; Div_RejoinderDate.Visible = true; Div_FileRejoinder.Visible = true; Div_RejoinderRemark.Visible = true;
                divAdditionalReturn.Visible = true;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                rfvRejNo.Enabled = true; rfvRejDate.Enabled = true; rfvRejRemark.Enabled = true;

            }
            else
            {
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; rfvRejNo.Enabled = false; rfvRejDate.Enabled = false; rfvRejRemark.Enabled = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void ddlCompliaceSt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCompliaceSt.SelectedValue == "1")
            {
                Div_CompliancNo.Visible = true; Div_CompliancDate.Visible = true;
                Div_CompliancDoc.Visible = true; Div_ComplianceRemark.Visible = true; div_Compliance_AnyRejoinder.Visible = true;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                rfvComplianceNo.Enabled = true; rfvComplianceDate.Enabled = true; rfvComRemark.Enabled = true;
            }
            else
            {
                Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                rfvComplianceNo.Enabled = false; rfvComplianceDate.Enabled = false; rfvComRemark.Enabled = false;
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; rfvRejNo.Enabled = false; rfvRejDate.Enabled = false; rfvRejRemark.Enabled = false;

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlAdditionalReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdditionalReturn.SelectedValue == "1")
            {
                Div_AdditionalNo.Visible = true; Div_AdditionalDate.Visible = true; Div_AdditionalDoc.Visible = true; Div_AdditionalRemar.Visible = true;
                rfvAdditionalNo.Enabled = true; rfvAdditionalDate.Enabled = true; rfvadditionalRemark.Enabled = true;
            }
            else
            {
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                rfvAdditionalNo.Enabled = false; rfvAdditionalDate.Enabled = false; rfvadditionalRemark.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnBackPage_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Legal/WPCaseList.aspx?CourtId=" + Request.QueryString["CourtId"].ToString() + "&Caseyear=" + Request.QueryString["Caseyear"].ToString() + "&CaseType=" + Request.QueryString["CaseType"].ToString() + "&CaseNo=" + Request.QueryString["CaseNo"].ToString() + "&CaseStatus=" + Request.QueryString["CaseStatus"].ToString(), false);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void RDFileMoment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (RDFileMoment.SelectedValue == "1")
            {
                divReturnAndReply.Visible = true;
                //ddlNameHod.ClearSelection();txtDispatchNo.Text = ""; txtDispatchDate.Text = "";
                //ViewState["ActionDoc"] = ""; txtHODRemark.Text = "";
            }
            else
            {
                divReturnAndReply.Visible = false; ddlNameHod.ClearSelection(); txtDispatchNo.Text = ""; txtDispatchDate.Text = ""; ViewState["ActionDoc"] = "";
                txtHODRemark.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillOrderWithDirection()
    {
        try
        {
            ddlOrderWith.ClearSelection();
            DataSet dsHod = obj.ByDataSet("select OrderWithDirection_ID,OrderWithDirection from tbl_OrderWithDirection");
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlOrderWith.DataTextField = "OrderWithDirection";
                ddlOrderWith.DataValueField = "OrderWithDirection_ID";
                ddlOrderWith.DataSource = dsHod;
                ddlOrderWith.DataBind();
                ddlOrderWith.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void ddlOrderWith_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlOrderWith.SelectedIndex > 0)
            {
                OrderBy1.Visible = true;
                OrderBy2.Visible = true;
                HearingDtl_CaseDispose.Visible = true;
                DivOrderTimeline.Visible = true;
                OrderSummary_Div.Visible = true;
                CimplianceSt_Div.Visible = true;
            }
            else
            {
                CimplianceSt_Div.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                HearingDtl_CaseDispose.Visible = false;
                DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (BtnAdd.Text == "Add")
                {
                    ds = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "CaseNo", "OldOICName", "OldOICDesignation_Id", "OldOICMobileNo", "OldOICEmailID", "OIdOICDistrict", "CreatedBy", "CreatedByIP" },
                                                                new string[] { "5", ViewState["CaseNo"].ToString(), txtOldOicName.Text.Trim(), ddlOICDesignation.SelectedValue, txtOldOICMobileNu.Text.Trim(), txtOldOICEmailID.Text.Trim(), ddlOldOICDistrict.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (BtnAdd.Text == "Update" && ViewState["Case_ID"].ToString() != "" && ViewState["Case_ID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "CaseNo", "OldOICName", "OldOICDesignation_Id", "OldOICMobileNo", "OldOICEmailID", "OIdOICDistrict", "LastupdatedBy", "LastupdatedByIP", "Case_ID" }
                        , new string[] { "6", ViewState["CaseNo"].ToString(), txtOldOicName.Text.Trim(), ddlOICDesignation.SelectedValue, txtOldOICMobileNu.Text.Trim(), txtOldOICEmailID.Text.Trim(), ddlOldOICDistrict.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Case_ID"].ToString() }, "dataset");
                }
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            FillOldOICDetail(); BtnAdd.Text = "Add";
                            txtOldOicName.Text = ""; txtOldOICMobileNu.Text = ""; ddlOldOICDistrict.ClearSelection(); ddlOICDesignation.ClearSelection(); txtOldOICEmailID.Text = "";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void FillOldOICDetail()
    {
        try
        {
            GrdOldOCDetail.DataSource = null;
            GrdOldOCDetail.DataBind();
            ds = obj.ByProcedure("Usp_GetOldOICDetail", new string[] { "CaseNo" }
                                                            , new string[] { ViewState["CaseNo"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdOldOCDetail.DataSource = ds;
                GrdOldOCDetail.DataBind();
                GrdOldOCDetail.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdOldOCDetail.UseAccessibleHeader = true;
            }
            else
            {
                GrdOldOCDetail.DataSource = null;
                GrdOldOCDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void GrdOldOCDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                ViewState["Case_ID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblOldOICNameID = (Label)row.FindControl("lblOldOICNameID");
                Label lblDesignation_Nameid = (Label)row.FindControl("lblDesignation_Nameid");
                Label lblOldOICMobileNo = (Label)row.FindControl("lblOldOICMobileNo");
                Label lblOldOICEmailID = (Label)row.FindControl("lblOldOICEmailID");
                Label lblDistrict_NameID = (Label)row.FindControl("lblDistrict_NameID");
                // Add New Label lblHod_Id by bhanu on 09/05/23 as chnage Req.
                txtOldOicName.Text = !string.IsNullOrEmpty(lblOldOICNameID.Text) ? lblOldOICNameID.Text : null;
                txtOldOICMobileNu.Text = !string.IsNullOrEmpty(lblOldOICMobileNo.Text) ? lblOldOICMobileNo.Text : null;
                txtOldOICEmailID.Text = !string.IsNullOrEmpty(lblOldOICEmailID.Text) ? lblOldOICEmailID.Text : null;
                ViewState["Case_ID"] = e.CommandArgument;
                BtnAdd.Text = "Update";

                if (!string.IsNullOrEmpty(lblDesignation_Nameid.Text))
                {
                    ddlOICDesignation.ClearSelection();
                    ddlOICDesignation.Items.FindByValue(lblDesignation_Nameid.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblDistrict_NameID.Text))
                {
                    ddlOldOICDistrict.ClearSelection();
                    ddlOldOICDistrict.Items.FindByValue(lblDistrict_NameID.Text).Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}
