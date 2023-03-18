using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_Mst_District : System.Web.UI.Page
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
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillGrid();
                FillDivisionName();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    #region Fill GridView
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_Mst_District", new string[] { }
                   , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdDistrictMst.DataSource = ds.Tables[0];
                grdDistrictMst.DataBind();
            }
            else
            {
                grdDistrictMst.DataSource = null;
                grdDistrictMst.DataBind();
            }
            grdDistrictMst.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdDistrictMst.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    protected void FillDivisionName()
    {
        try
        {
            ddlDivisionName.Items.Clear();
            ds = obj.ByDataSet("select Division_ID,Division_Name from tblDivisionMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDivisionName.DataTextField = "Division_Name";
                ddlDivisionName.DataValueField = "Division_ID";
                ddlDivisionName.DataSource = ds;
                ddlDivisionName.DataBind();
            }
            ddlDivisionName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_Mst_District", new string[] { "District_Name", "District_NameHin", "CreatedBy", "CreatedByIP", "Division_ID" }
                    , new string[] { txtDistrictName.Text.Trim(), txtDistrictNameHin.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ddlDivisionName.SelectedValue }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["DistrictID"].ToString() != "" && ViewState["DistrictID"].ToString() != null)
                {

                    ds = obj.ByProcedure("USP_Upate_Mst_District", new string[] { "District_Name", "District_NameHin", "LastUpdatedBy", "LastUpdatedByIP", "District_ID", "Division_ID" }
                    , new string[] { txtDistrictName.Text.Trim(), txtDistrictNameHin.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["DistrictID"].ToString(), ddlDivisionName.SelectedValue }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        btnSave.Text = "Save";
                        FillGrid();
                        txtDistrictName.Text = "";
                        txtDistrictNameHin.Text = "";
                        ddlDivisionName.ClearSelection();

                        ViewState["DistrictID"] = "";
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Warning !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }


            }
            grdDistrictMst.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdDistrictMst.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdDistrictMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetails")
            {
                ViewState["DistrictID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDistrict_Name = (Label)row.FindControl("lblDistrict_Name");
                Label lblDistrict_NameHin = (Label)row.FindControl("lblDistrict_NameHin");
                Label lblDivision_ID = (Label)row.FindControl("lblDivision_ID");

                if (lblDivision_ID.Text != "")
                {
                    ddlDivisionName.ClearSelection();
                    ddlDivisionName.Items.FindByValue(lblDivision_ID.Text).Selected = true;
                }
                txtDistrictName.Text = lblDistrict_Name.Text;
                txtDistrictNameHin.Text = lblDistrict_NameHin.Text;

                ViewState["DistrictID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteDetails")
            {
                ViewState["DistrictID"] = "";
                ViewState["DistrictID"] = e.CommandArgument;
                int District_ID = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from Mst_District where District_ID=" + District_ID);
                FillGrid();
            }
            grdDistrictMst.HeaderRow.TableSection = TableRowSection.TableHeader;
            grdDistrictMst.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdDistrictMst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdDistrictMst.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    
}