using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data;
public partial class Legal_HoMaster : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != "" && Session["Office_Id"] != "")
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
            Response.Redirect("../Login.aspx", false);
        }
    }
    #region Fill Office Level
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
    #endregion
    #region Fill Office Type
    protected void FillOfficeType()
    {
        try
        {
            ddlOfficetype.Items.Clear();
            ds = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficetype.DataTextField = "OfficeType_Name";
                ddlOfficetype.DataValueField = "OfficeType_Id";
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
    #endregion
    #region Save Update
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("Usp_InsertHoMaster", new string[] { "HoName", "Office_Id", "CreatedBy", "CreatedByIP", "Officetype_Id", "Officelevel_Id", "HOLocation" }
                    , new string[] { txtHoName.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ddlOfficetype.SelectedValue, ddlOfficeLevel.SelectedValue, txtlocation.Text.Trim() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["Ho_Id"].ToString() != "" && ViewState["Ho_Id"].ToString() != null)
                {
                    ds = obj.ByProcedure("Usp_UpdateHoMaster", new string[] { "HoName", "Office_Id", "LastUpdatedBy", "LastUpdatedByIP", "Ho_Id", "Officetype_Id", "Officelevel_Id", "HOLocation" }
                                                        , new string[] { txtHoName.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Ho_Id"].ToString(), ddlOfficetype.SelectedValue, ddlOfficeLevel.SelectedValue, txtlocation.Text.Trim() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrorMsg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrorMsg);
                        FillGrid();
                        btnSave.Text = "Save";
                        ClearData();
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrorMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Warning !", ds.Tables[0].Rows[0]["Msg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Clear Date
    protected void ClearData()
    {
        txtHoName.Text = "";
        ddlOfficetype.ClearSelection();
        ddlOfficeLevel.ClearSelection();
        txtlocation.Text = "";
        btnSave.Text = "Save";
    }
    #endregion
    #region Fill Grid
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("Usp_SelectHomaster", new string[] { }, new string[] { }, "Dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        {
            try
            {
                lblMsg.Text = "";
                ViewState["Ho_Id"] = "";
                if (e.CommandName == "EditDetails")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label lblHoId = (Label)row.FindControl("lblHoId");
                    Label lblHoName = (Label)row.FindControl("lblHoName");
                    Label lblOfficetype = (Label)row.FindControl("lblofficetype_ID");
                    Label lblofficelevel = (Label)row.FindControl("lblofficelevel_ID");
                    Label lbllocation = (Label)row.FindControl("lbllocation");
                    txtHoName.Text = lblHoName.Text;
                    txtlocation.Text = lbllocation.Text;
                    if (lblofficelevel.Text != "")
                    {
                        ddlOfficeLevel.ClearSelection();
                        ddlOfficeLevel.Items.FindByValue(lblofficelevel.Text).Selected = true;
                    }
                    if (lblOfficetype.Text != "")
                    {
                        ddlOfficetype.ClearSelection();
                        ddlOfficetype.Items.FindByValue(lblOfficetype.Text).Selected = true;
                    }
                    ViewState["Ho_Id"] = e.CommandArgument;
                    btnSave.Text = "Update";
                }
                if (e.CommandName == "DeleteDetails")
                {
                    int Ho_Id = Convert.ToInt32(e.CommandArgument);
                    obj.ByTextQuery("delete from tblHoMaster where Ho_Id=" + Ho_Id);
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                ErrorLogCls.SendErrorToText(ex);
            }
            finally { if (ds != null) { ds.Dispose(); } }
        }
    }
    #endregion
    #region Page Index Changing
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}

