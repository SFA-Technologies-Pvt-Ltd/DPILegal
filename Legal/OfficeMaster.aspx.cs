using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Legal_OfficeMaster : System.Web.UI.Page
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
                FillOfficetypeName();
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
        ds = obj.ByProcedure("USP_Select_OfficeMaster", new string[] { }, new string[] { }, "dataset");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            GrdOfficeMaster.DataSource = ds;
            GrdOfficeMaster.DataBind();
        }
        else
        {
            GrdOfficeMaster.DataSource = null;
            GrdOfficeMaster.DataBind();
        }
    }

    protected void FillOfficetypeName()
    {
        try
        {
            ddlOfficeType.Items.Clear();
            ds = obj.ByDataSet("select OfficeType_Id,OfficeType_Name from tblOfficeTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeType.DataTextField = "OfficeType_Name";
                ddlOfficeType.DataValueField = "OfficeType_Id";
                ddlOfficeType.DataSource = ds;
                ddlOfficeType.DataBind();
            }
            ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
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
                    ds = obj.ByProcedure("USP_Insert_OfficeMaster", new string[] { "OfficeType_Id", "OfficeName", "Officelocation", "CreatedBy", "CreatedByIP" }
                        , new string[] { ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOfficelocation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["OfficeID"].ToString() != "" && ViewState["OfficeID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_OfficeMaster", new string[] { "OfficeType_Id", "OfficeName", "Officelocation", "LastupdatedBy", "LastupdatedByIP", "Office_Id" }
                        , new string[] { ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOfficelocation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["OfficeID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtOfficeName.Text = "";
                        txtOfficelocation.Text = "";
                        ddlOfficeType.ClearSelection();
                        FillGrid();
                        btnSave.Text = "Save";
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-warning", "Warning !", ErrMsg);

                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdOfficeMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdOfficeMaster.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdOfficeMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                ViewState["OfficeID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOfficeTypeID = (Label)row.FindControl("lblOfficeTypeID");
                Label lblOficeName = (Label)row.FindControl("lblOficeName");
                Label lblOficelocation = (Label)row.FindControl("lblOficelocation");

                btnSave.Text = "Update";
                ViewState["OfficeID"] = e.CommandArgument;
                ddlOfficeType.ClearSelection();
                ddlOfficeType.Items.FindByValue(lblOfficeTypeID.Text).Selected = true;
                txtOfficeName.Text = lblOficeName.Text;
                txtOfficelocation.Text = lblOficelocation.Text;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
}