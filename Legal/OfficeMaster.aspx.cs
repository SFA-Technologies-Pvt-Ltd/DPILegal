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
                FillDivision();
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
            GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrdOfficeMaster.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region FillDropDown
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
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillDivision()
    {
        ddlDivision.Items.Clear();
        ds = obj.ByDataSet("select Division_ID,Division_Name from  tblDivisionMaster where IsActive=1");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_ID";
            ddlDivision.DataSource = ds;
            ddlDivision.DataBind();
        }
        ddlDivision.Items.Insert(0, new ListItem("Select", "0"));

    }
    protected void FillDistrict(string Division_ID)
    {
        ddlDistrict.Items.Clear();
        ds = obj.ByDataSet("select District_ID, District_Name from  Mst_District where Division_ID = " + Division_ID + "");
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataSource = ds;
            ddlDistrict.DataBind();
        }
        ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        GrdOfficeMaster.UseAccessibleHeader = true;
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
                    ds = obj.ByProcedure("USP_Insert_OfficeMaster", new string[] { "OfficeType_Id", "OfficeName", "Officelocation", "Division_Id", "District_Id", "CreatedBy", "CreatedByIP" }
                        , new string[] { ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOfficelocation.Text.Trim(), ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["OfficeID"].ToString() != "" && ViewState["OfficeID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_OfficeMaster", new string[] { "OfficeType_Id", "OfficeName", "Officelocation", "Division_Id", "District_Id", "LastupdatedBy", "LastupdatedByIP", "Office_Id" }
                        , new string[] { ddlOfficeType.SelectedValue, txtOfficeName.Text.Trim(), txtOfficelocation.Text.Trim(), ddlDivision.SelectedValue, ddlDistrict.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["OfficeID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtOfficeName.Text = "";
                        txtOfficelocation.Text = "";
                        ddlOfficeType.ClearSelection();
                        FillGrid();
                        btnSave.Text = "Save";
                        DivDivision.Visible = true;
                        divDistrict.Visible = true;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                    }
                    else
                    {
                        //lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);

                    }

                }

            }
            GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrdOfficeMaster.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Page Index Changing
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
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
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
                Label lblDivision_Id = (Label)row.FindControl("lblDivision_Id");
                Label lblDistrict_Id = (Label)row.FindControl("lblDistrict_Id");

                btnSave.Text = "Update";
                ViewState["OfficeID"] = e.CommandArgument;
                if (lblOfficeTypeID.Text != "")
                {
                    ddlOfficeType.ClearSelection();
                    ddlOfficeType.Items.FindByValue(lblOfficeTypeID.Text).Selected = true;
                    if (lblOfficeTypeID.Text == "1" || lblOfficeTypeID.Text == "2") divDistrict.Visible = false; DivDivision.Visible = false;
                }
                if (lblOficeName.Text != "") txtOfficeName.Text = lblOficeName.Text;
                if (lblOficelocation.Text != "") txtOfficelocation.Text = lblOficelocation.Text;
                if (lblDivision_Id.Text != ""){
                FillDivision();
                ddlDivision.ClearSelection();
                ddlDivision.Items.FindByValue(lblDivision_Id.Text).Selected = true;}
                string Division = lblDivision_Id.Text;
                if (ddlOfficeType.SelectedValue == "4")
                {
                    FillDistrict(Division);
                    ddlDistrict.Items.FindByValue(lblDistrict_Id.Text).Selected = true;
                    divDistrict.Visible = true;
                }
                else
                {
                    divDistrict.Visible = false;
                }

            }
            if (e.CommandName == "DeleteDetails")
            {
                int Office_Id = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblOfficeMaster where Office_Id=" + Office_Id);
                FillGrid();
            }
            GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrdOfficeMaster.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        string Division_ID = ddlDivision.SelectedValue;
        FillDistrict(Division_ID);
        GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        GrdOfficeMaster.UseAccessibleHeader = true;
    }
    protected void ddlOfficeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOfficeType.SelectedValue == "3")
        {
            divDistrict.Visible = false;
            DivDivision.Visible = true;
        }
        else if (ddlOfficeType.SelectedValue == "1")
        {
            DivDivision.Visible = false;
            divDistrict.Visible = false;
            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
        }
        else if (ddlOfficeType.SelectedValue == "2")
        {
            DivDivision.Visible = false;
            divDistrict.Visible = false;
            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
        }
        
        GrdOfficeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        GrdOfficeMaster.UseAccessibleHeader = true;
    }
}