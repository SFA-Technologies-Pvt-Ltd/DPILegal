using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_zonetocircle : System.Web.UI.Page
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
                FillZone();
                FillGrid();
                FillOfficeLevel();
                //FillOfficeType();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    protected void FillOfficeLevel()
    {
        try
        {
            ddlOfficeLevel.Items.Clear();
            ds = obj.ByDataSet("select OfficeLevel_Id, OfficeLevelName from tblOfficeLevelMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeLevel.DataTextField = "OfficeLevelName";
                ddlOfficeLevel.DataValueField = "OfficeLevel_Id";
                ddlOfficeLevel.DataSource = ds;
                ddlOfficeLevel.DataBind();
            }
            ddlOfficeLevel.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    //protected void FillOfficeType()
    //{
    //    try
    //    {
    //        ddlOfficetype.Items.Clear();
    //        ds = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOfficetype.DataTextField = "OfficeType_Name";
    //            ddlOfficetype.DataValueField = "OfficeType_Id";
    //            ddlOfficetype.DataSource = ds;
    //            ddlOfficetype.DataBind();
    //        }
    //        ddlOfficetype.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrorLogCls.SendErrorToText(ex);
    //    }
    //}

    #region Fill GridView
    protected void FillGrid()
    {
        try
        {

            ds = obj.ByProcedure("USP_Select_CircleMaster", new string[] { "flag" }
                    , new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdZoneCircle.DataSource = ds;
                GrdZoneCircle.DataBind();
            }
            else
            {
                GrdZoneCircle.DataSource = ds;
                GrdZoneCircle.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill Zone
    protected void FillZone()
    {
        try
        {
            ddlzone.Items.Clear();
            ds = obj.ByProcedure("USP_SelectZoneForCircleMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlzone.DataTextField = "ZoneName";
                ddlzone.DataValueField = "Zone_ID";
                ddlzone.DataSource = ds;
                ddlzone.DataBind();
            }
            ddlzone.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Save Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_CircleMaster", new string[] { "Zone_ID", "CirlceName", "CreatedBy", "CreatedByIP", "Office_Id", "Officetype_Id", "Officelevel_Id", "HoLocation" }
                                       , new string[] { ddlzone.SelectedValue, txtCircleName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString(), ddlOfficetype.SelectedValue, ddlOfficeLevel.SelectedValue, txtlocation.Text.Trim() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["CircleID"].ToString() != "" && ViewState["CircleID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_CircleMaster", new string[] { "Zone_ID", "CirlceName", "LastUpdatedBy", "LastUpdatedByIP", "Office_Id", "Circle_ID", "Officetype_Id", "Officelevel_Id", "HoLocation" }
                        , new string[] { ddlzone.SelectedValue, txtCircleName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString(), ViewState["CircleID"].ToString(), ddlOfficetype.SelectedValue, ddlOfficeLevel.SelectedValue, txtlocation.Text.Trim() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        ddlzone.ClearSelection();
                        txtCircleName.Text = "";
                        ddlOfficetype.ClearSelection();
                        ddlOfficeLevel.ClearSelection();
                        txtlocation.Text = "";
                        FillGrid();
                        btnSave.Text = "Save";
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
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region RowCommand Event
    protected void GrdZoneCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetails")
            {
                ViewState["CircleID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblZoneID = (Label)row.FindControl("lblZoneID");
                Label lblCircleName = (Label)row.FindControl("lblCircleName");
                Label lblCircleCode = (Label)row.FindControl("lblCircleCode");
                Label lblOfficetype = (Label)row.FindControl("lblofficetype_ID");
                Label lblofficelevel = (Label)row.FindControl("lblofficelevel_ID");
                Label lbllocation = (Label)row.FindControl("lbllocation");
                txtlocation.Text = lbllocation.Text;
                txtCircleName.Text = lblCircleName.Text;
                if (lblZoneID.Text !="")
                {
                    ddlzone.ClearSelection();
                    ddlzone.Items.FindByValue(lblZoneID.Text).Selected = true;
                }
               
                if (lblofficelevel.Text != "")
                {
                    ddlOfficeLevel.ClearSelection();
                    ddlOfficeLevel.Items.FindByValue(lblofficelevel.Text).Selected = true;
                }
                ddlOfficeLevel_SelectedIndexChanged(sender, e);
                if (lblOfficetype.Text != "")
                {
                    ddlOfficetype.ClearSelection();
                    ddlOfficetype.Items.FindByValue(lblOfficetype.Text).Selected = true;
                }
                txtCircleName.Text = lblCircleName.Text;
                ddlzone.ClearSelection();
                ddlzone.Items.FindByValue(lblZoneID.Text).Selected = true;
                ViewState["CircleID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteDetails")
            {
                int Circle_ID = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblCircleMaster where Circle_ID=" + Circle_ID);
                FillGrid();
            }
        }

        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region PageIndexing Event
    protected void GrdZoneCircle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            GrdZoneCircle.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    protected void ddlOfficeLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficetype.Items.Clear();
            ds = obj.ByProcedure("USP_Select_OfficeTypeName", new string[] { "OfficeLevel_Id" }
                , new string[] { ddlOfficeLevel.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficetype.DataValueField = "OfficeType_Id";
                ddlOfficetype.DataTextField = "OfficeType_Name";
                ddlOfficetype.DataSource = ds;
                ddlOfficetype.DataBind();
            }
            ddlOfficetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}