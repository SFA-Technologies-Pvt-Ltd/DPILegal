using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_DisposeCaseRpt : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseDisposeType();
                GetCaseType();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx");
        }
    }
    #region Get Case Dispose Type
    private void GetCaseDisposeType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_LegalCaseDisposeType ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDisposetype.DataSource = ds.Tables[0];
                ddlDisposetype.DataTextField = "CaseDisposeType";
                ddlDisposetype.DataValueField = "CaseDisposeType_Id";
                ddlDisposetype.DataBind();
                ddlDisposetype.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDisposetype.DataSource = null;
                ddlDisposetype.DataBind();
                ddlDisposetype.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Get Case Type
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
    #endregion
    #region Bing Grid
    protected void BindGrid()
    {
        try
        {
            string OIC = "";
            grdSubjectWiseCasedtl.DataSource = null;
            grdSubjectWiseCasedtl.DataBind();
            string Compliance = ddlCompliaceSt.SelectedIndex > 0 ? ddlCompliaceSt.SelectedItem.Value : null;

            if (Session["OICMaster_ID"] != "")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "Compliance_Status", "OICMaster_Id", "flag" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, Compliance, Session["OICMaster_ID"].ToString(), "1" }, "dataset");
            }
            else
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "Compliance_Status", "flag" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, Compliance, "2" }, "dataset");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdSubjectWiseCasedtl.DataSource = ds;
                grdSubjectWiseCasedtl.DataBind();
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
            }
            else
            {
                grdSubjectWiseCasedtl.DataSource = null;
                grdSubjectWiseCasedtl.DataBind();
            }
         
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
    #endregion
    #region Row Command
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = e.CommandArgument.ToString();
                string pageID = "4";
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + Server.UrlEncode(ID) + "&pageID=" + pageID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Page Index Changing
    protected void grdSubjectWiseCasedtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdSubjectWiseCasedtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region ddlDisposetype Selected Index Changed
    protected void ddlDisposetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlCompliaceSt.ClearSelection();
            if (ddlDisposetype.SelectedIndex == 1)
            {
                
                ComplianceSt_Div.Visible = true;
            }
            else { ComplianceSt_Div.Visible = false; }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}