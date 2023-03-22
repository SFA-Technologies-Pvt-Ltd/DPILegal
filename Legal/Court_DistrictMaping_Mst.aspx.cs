using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Legal_Court_DistrictMaping_Mst : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"];
                ViewState["Office_Id"] = Session["Office_Id"];
                FillCourtName();
                FillDistrictName();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    #region Fill Grid
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("Usp_Get_DistrictCourtMaping_Mst", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                grdCourtDistrictMap.DataSource = ds;
                grdCourtDistrictMap.DataBind();
            }
            grdCourtDistrictMap.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdCourtDistrictMap.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill Court Name
    protected void FillCourtName()
    {
        try
        {
            ds = obj.ByDataSet("select CourtName_ID,CourtName from tbl_LegalCourtMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtName.DataValueField = "CourtName_ID";
                ddlCourtName.DataTextField = "CourtName";
                ddlCourtName.DataSource = ds;
                ddlCourtName.DataBind();
            }
            ddlCourtName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill District Name
    protected void FillDistrictName()
    {
        try
        {
            ds = obj.ByProcedure("USP_SelectCourtAndDistrict", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrictName.DataValueField = "District_ID";
                ddlDistrictName.DataTextField = "District_Name";
                ddlDistrictName.DataSource = ds;
                ddlDistrictName.DataBind();
            }
            ddlDistrictName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Map
    protected void btnMap_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (btnMap.Text == "Map")
                {
                    ds = obj.ByProcedure("USP_DistrictCourtMaping_Mst", new string[] { "CourtName_ID", "District_ID", "CreatedBy", "CreatedByIP", "flag" },
                        new string[] { ddlCourtName.SelectedValue, ddlDistrictName.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), "1" }, "dataset");
                }
                else if (btnMap.Text == "Update" && ViewState["Map_ID"] != null && ViewState["Map_ID"] != "")
                {
                    ds = obj.ByProcedure("USP_DistrictCourtMaping_Mst", new string[] { "CourtName_ID", "District_ID", "LastupdatedBy", "LastupdatedbyIp", "flag", "Map_ID" },
                        new string[] { ddlCourtName.SelectedValue, ddlDistrictName.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), "2", ViewState["Map_ID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        FillGrid();
                        FillDistrictName();
                        ddlCourtName.ClearSelection();
                        ddlDistrictName.ClearSelection();
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "NotOK")
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ddlCourtName.SelectedItem.ToString() + ErrMsg + " ' , 'warning')", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
    protected void grdCourtDistrictMap_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Map_ID"] = "";
            if (e.CommandName == "EditDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblCourtNameID = (Label)row.FindControl("lblCourtNameID");
                Label lblDistrictID = (Label)row.FindControl("lblDistrictID");

                ddlCourtName.ClearSelection();
                ddlCourtName.Items.FindByValue(lblCourtNameID.Text).Selected = true;
                ddlDistrictName.ClearSelection();
                ddlDistrictName.Items.FindByValue(lblDistrictID.Text).Selected = true;
                btnMap.Text = "Update";
                ViewState["Map_ID"] = e.CommandArgument;
            }
            else if (e.CommandName == "DeleteDetails")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                DataSet DsDelete = obj.ByDataSet("delete from tbl_DistrictCourtMaping_Mst where Map_ID =" + e.CommandArgument.ToString());
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Delete Record Successfully', 'success')", true);
                FillGrid();
                FillDistrictName();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}