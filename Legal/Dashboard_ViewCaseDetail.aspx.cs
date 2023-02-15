using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_Dashboard_ViewCaseDetail : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != "" && Session["Office_Id"] != "")
        {
            if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["ID"] = Request.QueryString["ID"].ToString();
                    BindCaseDtl();
                }
            }
            else
            {
                if (Request.QueryString["WPID"] == "WPCase") // When WP Case Dtl Would You fetch.
                {
                    GrdCaseDetail.DataSource = null;
                    GrdCaseDetail.DataBind();
                    ds = obj.ByProcedure("USP_Legal_GetCaseDtlForDasboard", new string[] { }
                          , new string[] { }, "dataset");

                    spnCaseHeading.InnerHtml = "WP Case";
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        CourtAndCaseTypeDiv.Visible = true;
                        OfficeAndReponderDiv.Visible = false;
                        GrdCaseDetail.DataSource = ds;
                        GrdCaseDetail.DataBind();
                    }
                    else
                    {
                        GrdCaseDetail.DataSource = null;
                        GrdCaseDetail.DataBind();
                        CourtAndCaseTypeDiv.Visible = true;
                    }
                }
                else if (Request.QueryString["WAID"] == "WACase") // When WA Case Dtl Would You fetch.
                {
                    GrdCaseDetail.DataSource = null;
                    GrdCaseDetail.DataBind();
                    ds = obj.ByProcedure("USP_Legal_GetWACaseDtlForDasboard", new string[] { }
                       , new string[] { }, "dataset");

                    spnCaseHeading.InnerHtml = "WA Case";
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        CourtAndCaseTypeDiv.Visible = true;
                        OfficeAndReponderDiv.Visible = false;
                        GrdCaseDetail.DataSource = ds;
                        GrdCaseDetail.DataBind();
                    }
                    else
                    {
                        GrdCaseDetail.DataSource = null;
                        GrdCaseDetail.DataBind();
                        CourtAndCaseTypeDiv.Visible = true;
                    }
                }
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void BindCaseDtl()
    {
        try
        {
            // When Court Wise Dtl Would You fetch.
            if (Request.QueryString["Casetype"].ToString() == "Jabalpur Court Case" || Request.QueryString["Casetype"].ToString() == "Indore Court Case" || Request.QueryString["Casetype"].ToString() == "Gwalior Court Case")
            {
                GrdCaseDetail.DataSource = null;
                GrdCaseDetail.DataBind();
                ds = obj.ByProcedure("USP_Legal_GetCaseDtlForDasboard", new string[] { "CourtType_Id" }
                    , new string[] { ViewState["ID"].ToString() }, "dataset");

                spnCaseHeading.InnerHtml = Request.QueryString["Casetype"].ToString();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    CourtAndCaseTypeDiv.Visible = true;
                    OfficeAndReponderDiv.Visible = false;
                    GrdCaseDetail.DataSource = ds;
                    GrdCaseDetail.DataBind();
                }
                else
                {
                    CourtAndCaseTypeDiv.Visible = true;
                    GrdCaseDetail.DataSource = null;
                    GrdCaseDetail.DataBind();
                }
            } // When Responder Wise And Office Wise Dtl Would You fetch.
            else if (Request.QueryString["Casetype"].ToString() == "MP Govt Case" || Request.QueryString["Casetype"].ToString() == "ENC Case" || Request.QueryString["Casetype"].ToString() == "Zone Case" || Request.QueryString["Casetype"].ToString() == "Cirlce Case" || Request.QueryString["Casetype"].ToString() == "Jal Nigam Case" || Request.QueryString["Casetype"].ToString() == "DO Case")
            {
                GrdOfficeAndRespndrbyDtl.DataSource = null;
                GrdOfficeAndRespndrbyDtl.DataBind();
                ds = obj.ByProcedure("USP_Legal_GetResptypCaseDtl_ForDashbord", new string[] { "Respondertype_ID" }
                    , new string[] { ViewState["ID"].ToString() }, "dataset");

                spnOfficeWiseheading.InnerHtml = Request.QueryString["Casetype"].ToString();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    CourtAndCaseTypeDiv.Visible = false;
                    OfficeAndReponderDiv.Visible = true;
                    GrdOfficeAndRespndrbyDtl.DataSource = ds;
                    GrdOfficeAndRespndrbyDtl.DataBind();
                }
                else
                {
                    OfficeAndReponderDiv.Visible = true;
                    GrdOfficeAndRespndrbyDtl.DataSource = null;
                    GrdOfficeAndRespndrbyDtl.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GrdOfficeAndRespndrbyDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if(e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lbltbl = (Label)row.FindControl("lbltbl");
                Label lblCaseID = (Label)row.FindControl("lblCaseID");
                Label lblcasstype = (Label)row.FindControl("lblCaetypeID");
                if(lblcasstype.Text == "1" || lblcasstype.Text == "2")
                {
                    ds = obj.ByProcedure("USP_Legal_ViewAllDtl_RespondeWise", new string[] { "flag", "Casetype_ID", "Case_ID" }
                   , new string[] { "1", lblcasstype.Text, e.CommandArgument.ToString() }, "dataset");
                }
                else if (lblcasstype.Text == "3" || lblcasstype.Text == "4" || lblcasstype.Text == "5")
                {
                    ds = obj.ByProcedure("USP_Legal_ViewAllDtl_RespondeWise", new string[] { "flag", "Casetype_ID", "Case_ID" }
                        , new string[] { "2", lblcasstype.Text, e.CommandArgument.ToString() }, "dataset");
                }


                txtCaseno.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                txtCourtName.Text = ds.Tables[0].Rows[0]["CourtTypeName"].ToString();
                txtRespondertype.Text = ds.Tables[0].Rows[0]["RespondertypeName"].ToString();
                txtRespondentName.Text = ds.Tables[0].Rows[0]["ResponderName"].ToString();
                txtRespondentMobileno.Text = ds.Tables[0].Rows[0]["RespondentNo"].ToString();
                txtRespondentEmailID.Text = ds.Tables[0].Rows[0]["responderEmail"].ToString();
                txtNodalName.Text = ds.Tables[0].Rows[0]["NodalOfficer_Name"].ToString();
                txtNodalMobile.Text = ds.Tables[0].Rows[0]["NodalOfficerMobileNo"].ToString();
                txtNodalEmailID.Text = ds.Tables[0].Rows[0]["NodalOfficerEmailID"].ToString();
                txtOICName.Text = ds.Tables[0].Rows[0]["petiAdvocateName"].ToString();
                txtOICMObile.Text = ds.Tables[0].Rows[0]["petiAdvocateMobile"].ToString();
                txtOICEmail.Text = ds.Tables[0].Rows[0]["PetiAdvocateEmailID"].ToString();
                txtAdvocatename.Text = ds.Tables[0].Rows[0]["DeptAdvocateName"].ToString();
                txtAdvocatemobile.Text = ds.Tables[0].Rows[0]["DeptAdvocateMobileNO"].ToString();
                txtAdvocateEmailID.Text = ds.Tables[0].Rows[0]["DeptAdvocateEmailId"].ToString();
                txtNextHearingDate.Text = ds.Tables[0].Rows[0]["NextHearingDate"].ToString();
                txtPetitionerName.Text = ds.Tables[0].Rows[0]["Petitoner_Name"].ToString();
                txtCasesubject.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString();
                txtCaseDtl.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString();
                txtCasetype.Text = ds.Tables[0].Rows[0]["Casetype_Name"].ToString();

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}