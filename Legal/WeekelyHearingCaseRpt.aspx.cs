using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_WeekelyHearingCaseRpt : System.Web.UI.Page
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
            }
        }
        else
        {
            Response.Redirect("/Login.aspx", false);
        }
    }

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
    #region Bind Grid
    protected void BindGrid()
    {
        try
        {
            string Curr_Week = "0";
            if (ddlWeek.SelectedItem.Value == "1")
            {
                string OIC_ID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
                ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "Curr_Week", "OICMaster_Id" },
                    new string[] { "7", ddlCaseType.SelectedItem.Value, Curr_Week, OIC_ID }, "dataset");
            }
            else
            {
                string OIC_ID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
                ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "Curr_Week", "OICMaster_Id" },
                    new string[] { "8", ddlCaseType.SelectedItem.Value, Curr_Week, OIC_ID }, "dataset");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdWeekelyWiseCasedtl.DataSource = ds;
                grdWeekelyWiseCasedtl.DataBind();
                if (grdWeekelyWiseCasedtl.Rows.Count > 0)
                {
                    grdWeekelyWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdWeekelyWiseCasedtl.UseAccessibleHeader = true;
                }
            }
            else
            {
                grdWeekelyWiseCasedtl.DataSource = null;
                grdWeekelyWiseCasedtl.DataBind();
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
                string pageID = "6";
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