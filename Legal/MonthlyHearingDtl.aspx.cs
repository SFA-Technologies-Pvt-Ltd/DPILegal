using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_MonthlyHearingDtl : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseType();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx", false);
        }
    }

    #region Fill Year
    protected void FillYear()
    {
        ddlYear.Items.Clear();
        DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year desc");
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = dsCase.Tables[0];
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
        ddlYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    #endregion


    private void GetCaseType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_Legal_Casetype");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = ds.Tables[0];
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseType.DataSource = null;
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void BindGrid()
    {
        try
        {
            ds = new DataSet();
            grdMonthlyHearingdtl.DataSource = null;
            grdMonthlyHearingdtl.DataBind();
            if (Session["Role_ID"].ToString() == "2")// Division
            {
                string Division_ID = Session["Division_Id"].ToString();
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "Division_ID" },
                    new string[] { "2", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, Division_ID }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "4")// District
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "District_ID" },
                   new string[] { "3", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, District_Id }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "5")// Court
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "District_ID" },
                   new string[] { "4", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, District_Id }, "dataset");
            }
            else
            {
                // OIC & Admin login.
                string OICID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
                //ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "OICMaster_Id" },
                //   new string[] { "6", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, OICID }, "dataset");
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "OICMaster_Id" },
                    new string[] { "1", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, OICID }, "dataset");
            }
           
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdMonthlyHearingdtl.DataSource = ds;
                grdMonthlyHearingdtl.DataBind();
                grdMonthlyHearingdtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdMonthlyHearingdtl.UseAccessibleHeader = true;
            }
            else
            {
                grdMonthlyHearingdtl.DataSource = null;
                grdMonthlyHearingdtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            if (Page.IsValid)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdMonthlyHearingdtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        if (e.CommandName == "ViewDtl")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + e.CommandArgument.ToString() + "&pageID=" + 5, false);

        }
    }
    protected void grdMonthlyHearingdtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdMonthlyHearingdtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}