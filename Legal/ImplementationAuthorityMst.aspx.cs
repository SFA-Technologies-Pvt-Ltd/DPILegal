using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Legal_ImplementationAuthorityMst : System.Web.UI.Page
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
                FillDesigNation();
                FillLocation();
                BindGrid();
                Officetype();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    #region FillDesignation
    protected void FillDesigNation()
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
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    #endregion
    #region FillLocation
    protected void FillLocation()
    {
        try
        {
            ddlLocation.Items.Clear();
            ds = obj.ByProcedure("USP_Select_District", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLocation.DataTextField = "District_Name";
                ddlLocation.DataValueField = "District_ID";
                ddlLocation.DataSource = ds;
                ddlLocation.DataBind();

            }
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill GridView
    protected void BindGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_ImplementAuthorityMst", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdImpleAuthority.DataSource = ds;
                GrdImpleAuthority.DataBind();
            }
            else
            {
                GrdImpleAuthority.DataSource = null;
                GrdImpleAuthority.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill OfficetypeName
    protected void Officetype()
    {
        try
        {
            ddlOfficetype.ClearSelection();
            ds = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
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
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_ImplementionAuthMst", new string[] { "OfficeType_Id", "Office_Id", "IAuthority_Name", "IAuthority_MobileNo", "IAuthority_EmailID", "UserType_Id", "District_Id", "CreatedBy", "CreatedByIP" }
                        , new string[] { ddlOfficetype.SelectedValue, ddlOfficeName.SelectedValue, txtAuthorityName.Text.Trim(), txtMobileNo.Text.Trim(), txtEmailID.Text.Trim(), ddlDesignation.SelectedValue, ddlLocation.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["AuthorityID"].ToString() != "" && ViewState["AuthorityID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_ImplementAuthorityMst", new string[] { "OfficeType_Id", "Office_Id", "IAuthority_Name", "IAuthority_MobileNo", "IAuthority_EmailID", "UserType_Id", "District_Id", "LastUpdatedby", "LastUpdatedByIP", "IAuthority_ID" }
                        , new string[] { ddlOfficetype.SelectedValue, ddlOfficeName.SelectedValue, txtAuthorityName.Text.Trim(), txtMobileNo.Text.Trim(), txtEmailID.Text.Trim(), ddlDesignation.SelectedValue, ddlLocation.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["AuthorityID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtAuthorityName.Text = "";
                        txtMobileNo.Text = "";
                        txtEmailID.Text = "";
                        ddlDesignation.ClearSelection();
                        ddlLocation.ClearSelection();
                        ddlOfficeName.ClearSelection();
                        ddlOfficetype.ClearSelection();
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                BindGrid();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdImpleAuthority_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdImpleAuthority.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdImpleAuthority_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                lblMsg.Text = "";
                ViewState["AuthorityID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblName = (Label)row.FindControl("lblImpleAuthorityName");
                Label lblMobileNo = (Label)row.FindControl("lblImpleAuthorityMobileNo");
                Label lblEmailID = (Label)row.FindControl("lblImpleAuthorityEmailID");
                Label lblDesignationID = (Label)row.FindControl("lblImpleAuthorityDesig_ID");
                Label lblLocationID = (Label)row.FindControl("lblImpleAuthorityLocation_ID");
                Label lblOfficetypeid = (Label)row.FindControl("lblOfficetypeid");
                Label lblOfficeid = (Label)row.FindControl("lblOfficeid");

                txtAuthorityName.Text = lblName.Text;
                txtEmailID.Text = lblEmailID.Text;
                txtMobileNo.Text = lblMobileNo.Text;
                ddlDesignation.ClearSelection();
                ddlDesignation.Items.FindByValue(lblDesignationID.Text).Selected = true;
                ddlLocation.ClearSelection();
                ddlLocation.Items.FindByValue(lblLocationID.Text).Selected = true;
                btnSave.Text = "Update";
                ViewState["AuthorityID"] = e.CommandArgument; // 

                ddlOfficetype.ClearSelection();
                ddlOfficetype.Items.FindByValue(lblOfficetypeid.Text).Selected = true;
                ddlOfficetype_SelectedIndexChanged(sender, e);
                ddlOfficeName.ClearSelection();
                ddlOfficeName.Items.FindByValue(lblOfficeid.Text).Selected = true;
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlOfficetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlOfficetype.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataValueField = "Office_Id";
                ddlOfficeName.DataTextField = "OfficeName";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
            }
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}