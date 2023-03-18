using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_DivisionMaster : System.Web.UI.Page
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
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    #region Fill GridView
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_Mst_tblDivision", new string[] { }
                   , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrddivisionMst.DataSource = ds.Tables[0];
                GrddivisionMst.DataBind();
            }
            else
            {
                GrddivisionMst.DataSource = null;
                GrddivisionMst.DataBind();
            }
            GrddivisionMst.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrddivisionMst.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_Mst_tblDivision", new string[] { "Division_Name", "Division_NameHin", "CreatedBy", "CreatedByIP", "Officetype_Id" }
                    , new string[] { txtDivisionName.Text.Trim(), txtDivisionNameHin.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ddlOfficetype.SelectedValue }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["DivisionID"].ToString() != "" && ViewState["DivisionID"].ToString() != null)
                {

                    ds = obj.ByProcedure("USP_Upate_Mst_tblDivision", new string[] { "Division_Name", "Division_NameHin", "LastUpdatedBy", "LastUpdatedByIP", "Division_ID", "Officetype_Id" }
                    , new string[] { txtDivisionName.Text.Trim(), txtDivisionNameHin.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["DivisionID"].ToString(), ddlOfficetype.SelectedValue }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {

                        btnSave.Text = "Save";
                        FillGrid();
                        txtDivisionName.Text = "";
                        txtDivisionNameHin.Text = "";
                        ddlOfficetype.ClearSelection();

                        ViewState["DivisionID"] = "";
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
            GrddivisionMst.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrddivisionMst.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrddivisionMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetails")
            {
                ViewState["DivisionID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDivisionName = (Label)row.FindControl("lblDivisionName");
                Label lblDivisionNameHin = (Label)row.FindControl("lblDivision_NameHin");
                Label lblOfficetype = (Label)row.FindControl("lblofficetype_ID");

                if (lblOfficetype.Text != "")
                {
                    ddlOfficetype.ClearSelection();
                    ddlOfficetype.Items.FindByValue(lblOfficetype.Text).Selected = true;
                }
                txtDivisionName.Text = lblDivisionName.Text;
                txtDivisionNameHin.Text = lblDivisionNameHin.Text;

                ViewState["DivisionID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
            if (e.CommandName == "DeleteDetails")
            {
                ViewState["DivisionID"] = "";
                ViewState["DivisionID"] = e.CommandArgument;
                int Division_ID = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from Mst_tblDivision where Division_ID=" + Division_ID);
                FillGrid();
            }
            GrddivisionMst.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrddivisionMst.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrddivisionMst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrddivisionMst.PageIndex = e.NewPageIndex;
            FillGrid();
            GrddivisionMst.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrddivisionMst.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}