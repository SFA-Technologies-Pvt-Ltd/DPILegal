using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_DepartmentMaster : System.Web.UI.Page
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
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    protected void FillGrid()
    {
        try
        {
            GrddeptMaster.DataSource = null;
            GrddeptMaster.DataBind();
            ds = obj.ByDataSet("select Dept_ID, Dept_Name, Office_Id, Isactive from tblDepartmentMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrddeptMaster.DataSource = ds;
                GrddeptMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_DepartmentMaster", new string[] { "Dept_Name", "CreatedBy", "CreatedByIP", "Office_Id","flag" },
                        new string[] { txtDeptName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString(),"1" }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["Dept_Id"] != null && ViewState["Dept_Id"] != "")
                {
                    ds = obj.ByProcedure("USP_Insert_DepartmentMaster", new string[] { "Dept_Name", "LastupdatedBy", "LastupdatedbyIp", "Office_Id", "flag", "Dept_ID" },
                        new string[] { txtDeptName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString(), "2", ViewState["Dept_Id"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtDeptName.Text = "";
                        btnSave.Text = "Save";
                        FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString()); 
        }
    }
    protected void GrddeptMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Dept_Id"] = "";
            if (e.CommandName == "EditDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDeptID = (Label)row.FindControl("lbldeptID");
                Label lblDeptName = (Label)row.FindControl("lblDeptName");

                txtDeptName.Text = lblDeptName.Text;
                btnSave.Text = "Update";
                ViewState["Dept_Id"] = e.CommandArgument;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void GrddeptMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }
}