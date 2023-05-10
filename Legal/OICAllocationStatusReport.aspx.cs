using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_OICAllocationStatusReport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillCasetype();
                FillCaseNo();
                FillCourt();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }
    protected void FillCourt()
    {
        try
        {
            ddlCourt.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = new DataTable();
            if (Session["Role_ID"].ToString() == "5")
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
                ddlCourt.Enabled = false;
                if (dtCourt != null && dtCourt.Rows.Count > 0)
                {
                    ddlCourt.DataValueField = "CourtType_ID";
                    ddlCourt.DataTextField = "CourtTypeName";
                    ddlCourt.DataSource = dtCourt;
                    ddlCourt.DataBind();
                }
            }
            else
            {
                dtCourt = court.GetCourt() as DataTable;

                ddlCourt.Enabled = true;
                if (dtCourt != null && dtCourt.Rows.Count > 0)
                {
                    ddlCourt.DataValueField = "CourtType_ID";
                    ddlCourt.DataTextField = "CourtTypeName";
                    ddlCourt.DataSource = dtCourt;
                    ddlCourt.DataBind();
                }
                ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
            }


        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillYear()
    {
        try
        {
            ddlCaseYear.Items.Clear();
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 2000 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year asc");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCaseYear.DataSource = dsCase.Tables[0];
                ddlCaseYear.DataTextField = "year";
                ddlCaseYear.DataValueField = "year";
                ddlCaseYear.DataBind();
            }
            ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void FillCaseNo()
    {
        try
        {
            ddlCaseNo.Items.Clear();
            DataTable dtCN = new DataTable();
            Helper CaseNo = new Helper();
            if (Session["Role_ID"].ToString() == "4")
            {
                dtCN = CaseNo.GetDistrictWiseCaseNo(Session["District_Id"].ToString()) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "2")
            {
                string Division_Id = Session["Division_Id"].ToString();
                dtCN = CaseNo.GetDvisionWiseCaseNo(Division_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                string District_Id = Session["District_Id"].ToString();
                dtCN = CaseNo.GetCourtWiseCaseNo(District_Id) as DataTable;
            }
            else
            {
                if (!string.IsNullOrEmpty(Session["OICMaster_ID"].ToString()))
                {
                    dtCN = CaseNo.GetOICWiseCaseNo(Session["OICMaster_ID"].ToString()) as DataTable;
                }
                else
                {
                    dtCN = CaseNo.GetCaseNo() as DataTable;

                }
            }

            if (dtCN != null && dtCN.Rows.Count > 0)
            {
                ddlCaseNo.DataValueField = "Case_ID";
                ddlCaseNo.DataTextField = "CaseNo";
                ddlCaseNo.DataSource = dtCN;
                ddlCaseNo.DataBind();
            }
            ddlCaseNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void FillCasetype()
    {
        try
        {
            Helper HP = new Helper();
            DataTable dt = HP.GetCasetype() as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataSource = dt;
                ddlCaseType.DataBind();
            }
            ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
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

            GrdCaseDetails.DataSource = null;
            GrdCaseDetails.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            string OICMaster_Id = "";
            string District_ID = "";
            
            if (!string.IsNullOrEmpty(Session["District_Id"].ToString()))
            {
                District_ID = Session["District_Id"].ToString();
            }

            if (Session["Role_ID"].ToString() == "4")
            {
                ds = obj.ByProcedure("USP_GetOICAllocatedCaseRegisDetail", new string[] { "Casetype_ID", "CourtType_Id", "CaseNo", "Year", "CaseStatus", "District_ID", "flag", "Status" }
                  , new string[] { ddlCaseType.SelectedValue, ddlCourt.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlCaseYear.SelectedItem.Text, ddlCaseStatus.SelectedItem.Text, District_ID, "2",ddlOICAllocated.SelectedValue }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "2")
            {
                string Division_ID = Session["Division_Id"].ToString();
                ds = obj.ByProcedure("USP_GetOICAllocatedCaseRegisDetail", new string[] { "Casetype_ID", "CourtType_Id", "CaseNo", "Year", "CaseStatus", "Division_ID", "flag", "Status" }
                  , new string[] { ddlCaseType.SelectedValue, ddlCourt.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlCaseYear.SelectedItem.Text, ddlCaseStatus.SelectedItem.Text, Division_ID, "3", ddlOICAllocated.SelectedValue }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByProcedure("USP_GetOICAllocatedCaseRegisDetail", new string[] { "Casetype_ID", "CourtType_Id", "CaseNo", "Year", "CaseStatus", "CourtLocation_Id", "flag", "Status" }
                  , new string[] { ddlCaseType.SelectedValue, ddlCourt.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlCaseYear.SelectedItem.Text, ddlCaseStatus.SelectedItem.Text, District_Id, "4", ddlOICAllocated.SelectedValue }, "dataset");
            }
            else
            {
                if (!string.IsNullOrEmpty(Session["OICMaster_ID"].ToString()))
                {
                    OICMaster_Id = Session["OICMaster_ID"].ToString();
                }
                ds = obj.ByProcedure("USP_GetOICAllocatedCaseRegisDetail", new string[] { "Casetype_ID", "CourtType_Id", "CaseNo", "Year", "CaseStatus", "OICMaster_Id", "flag", "Status" }
                   , new string[] { ddlCaseType.SelectedValue, ddlCourt.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlCaseYear.SelectedItem.Text, ddlCaseStatus.SelectedItem.Text, OICMaster_Id, "1", ddlOICAllocated.SelectedValue }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ddlOICAllocated.SelectedValue == "2")
                {
                    GrdCaseDetails.Visible = true;
                    GrdCaseDetails.DataSource = ds;
                    GrdCaseDetails.DataBind();
                    GridView1.Visible = false;
                    GrdCaseDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GrdCaseDetails.UseAccessibleHeader = true;
                }
                else
                {
                    GridView1.Visible = true;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GrdCaseDetails.Visible = false;
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }

                //GrdCaseDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                //GrdCaseDetails.UseAccessibleHeader = true;
            }
            else { GrdCaseDetails.DataSource = null; GrdCaseDetails.DataBind(); }
        }
        catch (Exception)
        {

            throw;
        }
    }
}