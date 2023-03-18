using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Legal_ConcludedwpReport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                FillCasetype();
                FillYear();
                ViewState["OIC_ID"] = Session["OICMaster_ID"];
            }
        }
        else
        {
            Response.Redirect("~/Legal/Login.aspx", false);
        }
    }
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year desc");
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            ddlCaseYear.DataSource = dsCase.Tables[0];
            ddlCaseYear.DataTextField = "year";
            ddlCaseYear.DataValueField = "year";
            ddlCaseYear.DataBind();
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    #endregion
    #region Fill Case Type
    protected void FillCasetype()
    {
        try
        {
            Helper hl = new Helper();
            DataTable dt = hl.GetCasetype() as DataTable;
            if (dt.Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = dt;
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
    #region Btn Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = ""; string OIC = "";
                GrdConcludeReport.DataSource = null;
                GrdConcludeReport.DataBind();
                if (Session["OICMaster_ID"] != "" && Session["OICMaster_ID"] != null) OIC = Session["OICMaster_ID"].ToString();
                if (Session["Role_ID"].ToString() == "2")
                {
                    string Division_Id = Session["Division_Id"].ToString();
                    ds = obj.ByProcedure("USP_GetWPConcludeRpt", new string[] { "CaseYear", "Casetype_ID", "Division_ID", "flag" }
                , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, Division_Id, "2" }, "dataset");
                }
                else if (Session["Role_ID"].ToString() == "4")
                {
                    string District_ID = Session["District_Id"].ToString();
                    ds = obj.ByProcedure("USP_GetWPConcludeRpt", new string[] { "CaseYear", "Casetype_ID", "District_ID", "flag" }
                , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, District_ID, "3" }, "dataset");
                }
                else if (Session["Role_ID"].ToString() == "5")
                {
                    string District_ID = Session["District_Id"].ToString();
                    ds = obj.ByProcedure("USP_GetWPConcludeRpt", new string[] { "CaseYear", "Casetype_ID", "CourtLocation_Id", "flag" }
                , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, District_ID, "4" }, "dataset");
                }
                else
                {
                    ds = obj.ByProcedure("USP_GetWPConcludeRpt", new string[] { "CaseYear", "Casetype_ID", "OICMaster_Id", "flag" }
                , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, OIC, "1" }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrdConcludeReport.DataSource = ds;
                    GrdConcludeReport.DataBind();
                    GrdConcludeReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GrdConcludeReport.UseAccessibleHeader = true;
                }
                else
                {
                    GrdConcludeReport.DataSource = null;
                    GrdConcludeReport.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Page Index Changing
    protected void GrdConcludeReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdConcludeReport.PageIndex = e.NewPageIndex;
            btnSearch_Click(sender, e);
            GrdConcludeReport.DataSource = ds;
            GrdConcludeReport.DataBind();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
    protected void GrdConcludeReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                GrdConcludeReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdConcludeReport.UseAccessibleHeader = true;
                string ID = e.CommandArgument.ToString();
                string pageID = "2";
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + Server.UrlEncode(ID) + "&pageID=" + pageID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

}