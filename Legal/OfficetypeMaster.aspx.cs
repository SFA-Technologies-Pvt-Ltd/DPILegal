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
            Response.Redirect("../Login.aspx");
        }
    }


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
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
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
                    ds = obj.ByProcedure("USP_InsertOfficetypeMaster", new string[] { "OfficeType_Name", "CreatedBy", "CreatedByIP" }
                    , new string[] { txtOfficeTypeName.Text.Trim(),  ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress()}, "dataset");
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
                        lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
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
                FillGrid();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
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
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

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
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}