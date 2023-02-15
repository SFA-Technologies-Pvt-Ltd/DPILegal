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
public partial class Legal_DepartmentWisePSCaseDetails : System.Web.UI.Page
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

                if (!string.IsNullOrEmpty(Request.QueryString["CaseType"]) && !string.IsNullOrEmpty(Request.QueryString["department"]))
                {
                    BindGrid(Request.QueryString["CaseType"], Request.QueryString["department"]);
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
    protected void BindGrid(string CaseType, string department)
    {
        try
        {
            dsCase = obj.ByDataSet("select  distinct UniqueNo,CaseNo,FilingNo,Court,Department,Petitioner,Respondent,CaseType,RespondentOffice,CaseSubjectId,CaseSubSubjectId," +
                "HearingDate, OICId, OICMobileNo,Remarks from tbl_OldCaseDetail " +
                "where Department = '" + department + "' and " +
                "(PartyName like  '%PRINCIPAL SECRETARY%')  and CaseType ='" + Convert.ToString(CaseType) + "' order by HearingDate Desc");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            dsCase = obj.ByDataSet("select  distinct UniqueNo,CaseNo,FilingNo,Court,Department,Petitioner,Respondent,CaseType,RespondentOffice,CaseSubjectId,CaseSubSubjectId," +
           "HearingDate, OICId, OICMobileNo,Remarks from tbl_OldCaseDetail " +
           "where Department = '" + Request.QueryString["department"].ToString() + "' and FilingNo like '%" + Convert.ToString(txtSearch.Text.Trim()) + "%'" +
           " and (PartyName like  '%PRINCIPAL SECRETARY%' or Respondent like '%IAS%')  and CaseType ='" + Convert.ToString(Request.QueryString["CaseType"]) + "' order by HearingDate Desc");
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
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        ViewState["dtsearch"] = null;
        BindGrid(Request.QueryString["CaseType"], Request.QueryString["department"]);
        txtSearch.Text = "";
    }
}

