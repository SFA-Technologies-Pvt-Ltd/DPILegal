using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_DesignationMaster : System.Web.UI.Page
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
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void BindGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_DesignationMaster", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdDesignation.DataSource = ds;
                GrdDesignation.DataBind();
            }
            else
            {
                GrdDesignation.DataSource = null;
                GrdDesignation.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_DesignationMaster", new string[] { "DesignationName", "CreatedBy", "CreatedByIP" }
                        , new string[] { txtDeDesignation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["ID"].ToString() != "" && ViewState["ID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_Designationmaster", new string[] { "DesignationName", "LastUpdatedBy", "LastUpdatedByIp", "DesignationID" }
                        , new string[] { txtDeDesignation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["ID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtDeDesignation.Text = "";

                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-warning", "Warning !", ErrMsg);
                        txtDeDesignation.Text = "";
                    }

                }
                BindGrid();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdDesignation.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["ID"] = "";
            if (e.CommandName == "EditDetails")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblDesignationName = (Label)row.FindControl("lblDesignationName");
                Label lblID = (Label)row.FindControl("lblID");

                txtDeDesignation.Text = lblDesignationName.Text;
                btnSave.Text = "Update";
                ViewState["ID"] = e.CommandArgument;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
}