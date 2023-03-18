using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_OfficetypeMaster : System.Web.UI.Page
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
    #region Fill Grid
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_OfficetypeMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdOfficetypeMst.DataSource = ds;
                grdOfficetypeMst.DataBind();
            }
            else
            {
                grdOfficetypeMst.DataSource = null;
                grdOfficetypeMst.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Save Update
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_InsertOfficetypeMaster", new string[] { "OfficeType_Name", "CreatedBy", "CreatedByIP" }
                    , new string[] { txtOfficeTypeName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["OfficeTypeID"].ToString() != "" && ViewState["OfficeTypeID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_UpdateOfficetypeMaster", new string[] { "OfficeType_Name", "LastupdatedBy", "LastupdatedByIP", "OfficeType_Id" }
                    , new string[] { txtOfficeTypeName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["OfficeTypeID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        txtOfficeTypeName.Text = "";
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
    #endregion
    #region Page Index Chaging
    protected void grdOfficetypeMst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdOfficetypeMst.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
    protected void grdOfficetypeMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                lblMsg.Text = "";
                ViewState["OfficeTypeID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOfficetypeName = (Label)row.FindControl("lblOfficetypeName");
                Label lblOfficetypeID = (Label)row.FindControl("lblOfficetypeID");
                txtOfficeTypeName.Text = lblOfficetypeName.Text;
                ViewState["OfficeTypeID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteDetails")
            {
                int OfficeType_Id = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblOfficeTypeMaster where OfficeType_Id=" + OfficeType_Id);
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}