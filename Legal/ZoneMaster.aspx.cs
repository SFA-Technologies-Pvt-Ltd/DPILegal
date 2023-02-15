using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_ZoneMaster : System.Web.UI.Page
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
                FillOfficeType();
                FillOfficeLevel();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void FillOfficeType()
    {
        try
        {
            {
                ddlOfficeType.ClearSelection();
                ds = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOfficeType.DataValueField = "OfficeType_Id";
                    ddlOfficeType.DataTextField = "OfficeType_Name";
                    ddlOfficeType.DataSource = ds;
                    ddlOfficeType.DataBind();
                }
                ddlOfficeType.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void FillOfficeLevel()
    {
        try
        {
            {
                ddlOfficeLevel.ClearSelection();
                ds = obj.ByDataSet("select OfficeLevel_Id, OfficeLevelName from tblOfficeLevelMaster");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOfficeLevel.DataValueField = "OfficeLevel_Id";
                    ddlOfficeLevel.DataTextField = "OfficeLevelName";
                    ddlOfficeLevel.DataSource = ds;
                    ddlOfficeLevel.DataBind();
                }
                ddlOfficeLevel.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }


    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_ZoneMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdZoneMaster.DataSource = ds;
                GrdZoneMaster.DataBind();
            }
            else
            {
                GrdZoneMaster.DataSource = null;
                GrdZoneMaster.DataBind();
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
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_ZoneMaster", new string[] { "ZoneName", "OfficeType_Id", "OfficeLevel_Id", "OfficeLocation", "CreatedBy", "CreatedByIP", "Office_Id" }
                         , new string[] { txtZoneName.Text.Trim(), ddlOfficeType.SelectedValue, ddlOfficeLevel.SelectedValue, txtZoneOfficeLocation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["ZoneID"].ToString() != "" && ViewState["ZoneID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_ZoneMaster", new string[] { "ZoneName", "OfficeType_Id", "OfficeLevel_Id", "OfficeLocation", "LastUpdatedBy", "LastUpdatedByIP", "Office_Id", "Zone_ID" }
                                        , new string[] { txtZoneName.Text.Trim(), ddlOfficeType.SelectedValue, ddlOfficeLevel.SelectedValue, txtZoneOfficeLocation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString(), ViewState["ZoneID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        FillGrid();
                        btnSave.Text = "Save";
                        txtZoneName.Text = "";
                        txtZoneCode.Text = "";
                        ddlOfficeType.ClearSelection();
                        ddlOfficeLevel.ClearSelection();
                        txtZoneOfficeLocation.Text = "";
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
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdZoneMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                lblMsg.Text = "";
                ViewState["ZoneID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblZoneName = (Label)row.FindControl("lblZoneName");
                Label lblZoneCode = (Label)row.FindControl("lblZoneCode");
                Label OfficeType_Id = (Label)row.FindControl("lblofficetypeID");
                Label OfficeLevel_Id = (Label)row.FindControl("lblOfficelevelID");
                Label OfficeLocation = (Label)row.FindControl("lblLocation");
                txtZoneName.Text = lblZoneName.Text;
                if (OfficeType_Id.Text != "")
                {
                    ddlOfficeType.ClearSelection();
                    ddlOfficeType.Items.FindByValue(OfficeType_Id.Text).Selected = true;
                }
                if (OfficeLevel_Id.Text != "")
                {
                    ddlOfficeLevel.ClearSelection();
                    ddlOfficeLevel.Items.FindByValue(OfficeLevel_Id.Text).Selected = true;
                }
                txtZoneOfficeLocation.Text = OfficeLocation.Text;
                ViewState["ZoneID"] = e.CommandArgument.ToString();
                btnSave.Text = "Update";

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void GrdZoneMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdZoneMaster.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}