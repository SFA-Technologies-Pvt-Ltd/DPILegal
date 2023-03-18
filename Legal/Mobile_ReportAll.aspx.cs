using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_Mobile_ReportAll : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
      
            if (!Page.IsPostBack)
            {
                
                FillCasetype();
                FillCaseNo();
                FillCourt();
                FillYear();
            }
       
    }

    protected void FillCourt()
    {
        try
        {
            ddlCourt.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = court.GetCourt() as DataTable;
            if (dtCourt != null && dtCourt.Rows.Count > 0)
            {
                ddlCourt.DataValueField = "CourtType_ID";
                ddlCourt.DataTextField = "CourtTypeName";
                ddlCourt.DataSource = dtCourt;
                ddlCourt.DataBind();
            }
            ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
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
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year");
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
            Helper CaseNo = new Helper();
            DataTable dtCN = CaseNo.GetCaseNo() as DataTable;
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

            lblMsg.Text = "";
            //if (ddlCaseType.SelectedIndex > 0 || ddlCaseYear.SelectedIndex > 0 || ddlCourt.SelectedIndex > 0 || ddlCaseNo.SelectedIndex > 0 || ddlCaseStatus.SelectedIndex > 0)
            //{
            GrdCaseDetails.DataSource = null;
            GrdCaseDetails.DataBind();
            //string OICMaster_Id = "";
            //if (!string.IsNullOrEmpty(Session["OICMaster_ID"].ToString()))
            //{
            //    OICMaster_Id = Session["OICMaster_ID"].ToString();
            //}
            ds = obj.ByProcedure("USP_GetCaseRegisDetail", new string[] { "Casetype_ID", "CourtType_Id", "CaseNo", "Year", "CaseStatus" }
                    , new string[] { ddlCaseType.SelectedValue, ddlCourt.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlCaseYear.SelectedItem.Text, ddlCaseStatus.SelectedItem.Text }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdCaseDetails.DataSource = ds;
                GrdCaseDetails.DataBind();
                GrdCaseDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdCaseDetails.UseAccessibleHeader = true;
            }
            else { GrdCaseDetails.DataSource = null; GrdCaseDetails.DataBind(); }
            //}
            //else
            //{
            //    GrdCaseDetails.DataSource = null; GrdCaseDetails.DataBind();
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    protected void GrdCaseDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblUniqueNo = (Label)row.FindControl("lblUniqueNo");
            string UniqueNO = lblUniqueNo.Text;
            string ID = e.CommandArgument.ToString();
            Response.Redirect("../Legal/Mobile_ReportView.aspx?ID=" + Server.UrlEncode(ID) + "&UniqueNO=" + UniqueNO, false);
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}