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
            else if (Request.QueryString["Casetype"].ToString() == "PP Case" || Request.QueryString["Casetype"].ToString() == "JD Case" || Request.QueryString["Casetype"].ToString() == "DPI Case" || Request.QueryString["Casetype"].ToString() == "DEO Case" || Request.QueryString["Casetype"].ToString() == "RSK Case" || Request.QueryString["Casetype"].ToString() == "TBC Case")
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
}