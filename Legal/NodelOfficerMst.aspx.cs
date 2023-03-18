using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Legal_NodelOfficerMst : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillGrid();
                FillDesignation();
                FillDitrict();
                FillDepartment();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    #region Fill Grid
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_NodelOfficerMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                gridNodelOfficer.DataSource = ds;
                gridNodelOfficer.DataBind();
            }
            else
            {
                gridNodelOfficer.DataSource = null;
                gridNodelOfficer.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill Designation
    protected void FillDesignation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            ds = obj.ByDataSet("select Designation_Id, Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.DataValueField = "Designation_Id";
                ddlDesignation.DataTextField = "Designation_Name";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();
            }
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill District
    protected void FillDitrict()
    {
        try
        {
            ddldivision.Items.Clear();
            ds = obj.ByDataSet("select District_ID, District_Name from  Mst_District");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddldivision.DataTextField = "District_Name";
                ddldivision.DataValueField = "District_ID";
                ddldivision.DataSource = ds;
                ddldivision.DataBind();
            }
            ddldivision.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill Department
    protected void FillDepartment()
    {
        try
        {
            ddlDepartment.Items.Clear();
            ds = obj.ByDataSet("SELECT Dept_ID, Dept_Name FROM tblDepartmentMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDepartment.DataTextField = "Dept_Name";
                ddlDepartment.DataValueField = "Dept_ID";
                ddlDepartment.DataSource = ds;
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Button Insert_update
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_NodelOfficerMaster", new string[] { "Division_ID", "NodelOfficerName", "Designation_ID", "Dept_ID", "NodelOfficerMobileNo", "NodelOfficerEmailID", "Office_ID", "CreatedBy", "CreatedByIP" }
                    , new string[] { ddldivision.SelectedValue, txtNodelOfficerName.Text.Trim(), ddlDesignation.SelectedValue, ddlDepartment.SelectedValue, txtmobileno.Text.Trim(), txtEmailID.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["NodelOfficer_ID"].ToString() != "" && ViewState["NodelOfficer_ID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_NodelOfficerMaster", new string[] { "Division_ID", "NodelOfficerName", "Designation_ID", "Dept_ID", "NodelOfficerMobileNo", "NodelOfficerEmailID", "Office_ID", "LastupdatedBy", "LastupdatedByIP", "NodelOfficer_ID" }
                    , new string[] { ddldivision.SelectedValue, txtNodelOfficerName.Text.Trim(), ddlDesignation.SelectedValue, ddlDepartment.SelectedValue, txtmobileno.Text.Trim(), txtEmailID.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["NodelOfficer_ID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {

                        ddldivision.ClearSelection();
                        ddlDesignation.ClearSelection();
                        ddlDepartment.ClearSelection();
                        txtNodelOfficerName.Text = "";
                        txtmobileno.Text = "";
                        txtEmailID.Text = "";
                        ViewState["NodelOfficer_ID"] = "";
                        FillGrid();
                        btnSave.Text = "Save";
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Good job!', '" + ErrMsg + "', 'success')", true);
                    }
                    else
                    {
                        //lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ds.Tables[0].Rows[0]["ErrMsg"].ToString() + "' , 'warning')", true);
                    //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Warning !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
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
    protected void gridNodelOfficer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetails")
            {
                ViewState["NodelOfficer_ID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDivisionID = (Label)row.FindControl("lblDivisionID");
                Label lblNodelOfficerName = (Label)row.FindControl("lblNodelOfficerName");
                Label lblMobileNo = (Label)row.FindControl("lblMobileNo");
                Label lblDesignationId = (Label)row.FindControl("lblDesignationId");
                Label lblEmailID = (Label)row.FindControl("lblEmailID");
                Label lblDepartmentId = (Label)row.FindControl("lblDepartmentId");


                txtNodelOfficerName.Text = lblNodelOfficerName.Text;
                txtEmailID.Text = lblEmailID.Text;
                txtmobileno.Text = lblMobileNo.Text;
                if (lblDepartmentId.Text != "")
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByValue(lblDepartmentId.Text).Selected = true;
                }
                ddldivision.ClearSelection();
                ddldivision.Items.FindByValue(lblDivisionID.Text).Selected = true;
                ddlDesignation.ClearSelection();
                ddlDesignation.Items.FindByValue(lblDesignationId.Text).Selected = true;
                ViewState["NodelOfficer_ID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteDetails")
            {
                int NodelOfficer_ID = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblNodelOfficerMaster where NodelOfficer_ID=" + NodelOfficer_ID);
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Page Index Changing
    protected void gridNodelOfficer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            gridNodelOfficer.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

}