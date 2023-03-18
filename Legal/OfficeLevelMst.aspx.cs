using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Legal_OfficeLevelMst : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure obj = new APIProcedure();
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
            ds = obj.ByProcedure("USP_Select_OfficeLevelMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdOfficelevelMst.DataSource = ds;
                grdOfficelevelMst.DataBind();
            }
            else
            {
                grdOfficelevelMst.DataSource = null;
                grdOfficelevelMst.DataBind();
            }

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
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_InsertOfficeLevelMaster", new string[] { "OfficeLevelName", "CreatedBy", "CreatedByIP" }
                    , new string[] { txtOfficeLevel.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["OfficeLevel_Id"].ToString() != "" && ViewState["OfficeLevel_Id"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_UpdateOfficeLevelMaster", new string[] { "OfficeLevelName", "LastupdatedBy", "LastupdatedByIP", "OfficeLevel_Id" }
                    , new string[] { txtOfficeLevel.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["OfficeLevel_Id"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        txtOfficeLevel.Text = "";
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
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
                FillGrid();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdOfficelevelMst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdOfficelevelMst.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdOfficelevelMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                lblMsg.Text = "";
                ViewState["OfficeLevel_Id"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOfficelevelName = (Label)row.FindControl("lblOfficelevelName");
                Label lblOfficelevelID = (Label)row.FindControl("lblOfficelevelID");
                txtOfficeLevel.Text = lblOfficelevelName.Text;
                ViewState["OfficeLevel_Id"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteDetails")
            {
                int OfficeLevel_Id = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblOfficeLevelMaster where OfficeLevel_Id=" + OfficeLevel_Id);
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}