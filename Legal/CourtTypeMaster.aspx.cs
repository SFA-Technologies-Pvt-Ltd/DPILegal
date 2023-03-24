using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_CourtTypeMaster : System.Web.UI.Page
{
    DataSet ds = null;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
              if (!IsPostBack)
                {
                    ViewState["Emp_Id"] = Session["Emp_Id"];
                    ViewState["Office_Id"] = Session["Office_Id"];
                    BindGrid();
                    lblMsg.Text = "";
                    FillDistrict();
                    FillCourtName();
                }
        }
        else
        {
            Response.Redirect("~/Login.aspx",false);
        }
    }
    #region FillGrid
    private void BindGrid()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("Sp_CourtType", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdCourtType.DataSource = ds;
                grdCourtType.DataBind();
                grdCourtType.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdCourtType.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
        }
    }
    #endregion
    #region FillDistrict
    protected void FillDistrict()
    {
        try
        {
            ddlCourtlocation.Items.Clear();
            ds = objdb.ByDataSet("select District_ID, District_Name from Mst_District");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtlocation.DataValueField = "District_ID";
                ddlCourtlocation.DataTextField = "District_Name";
                ddlCourtlocation.DataSource = ds;
                ddlCourtlocation.DataBind();
            }
            ddlCourtlocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void FillCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear();
            ds = objdb.ByDataSet("select CourtName,CourtName_ID from tbl_LegalCourtMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataValueField = "CourtName_ID";
                ddlCourtType.DataTextField = "CourtName";
                ddlCourtType.DataSource = ds;
                ddlCourtType.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
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
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Sp_CourtType", new string[] { "flag", "CourtTypeName", "CourtName_ID", "District_Id", "CreatedBy", "CreatedByIP" }, new string[] {
                        "1",ddlCourtType.SelectedItem.Text,ddlCourtType.SelectedValue,ddlCourtlocation.SelectedValue,ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["CourtId"] != "" && ViewState["CourtId"] != null)
                {
                    ds = objdb.ByProcedure("Sp_CourtType", new string[] { "flag", "CourtTypeName", "CourtName_ID", "District_Id", "LastupdatedBy", "LastupdatedByIP", "CourtTypeID" }, new string[] {
                        "3",ddlCourtType.SelectedItem.Text,ddlCourtType.SelectedValue,ddlCourtlocation.SelectedValue,ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress(), ViewState["CourtId"].ToString() }, "dataset");
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    ddlCourtType.ClearSelection();
                    ddlCourtlocation.ClearSelection();
                    BindGrid();
                    btnSave.Text = "Save";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                }
                else
                {
                    //lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                }
            }
           
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void grdCaseSubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMsg.Text = "";
        grdCourtType.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void grdCourtType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                ViewState["CourtId"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                HiddenField lblDistrictID = (HiddenField)row.FindControl("hdnDistrictID");
                Label lblCourtNameID = (Label)row.FindControl("lblCourtNameID");
                Label lblOtherlocation = (Label)row.FindControl("lblOtherlocation");
                btnSave.Text = "Update";
                ViewState["CourtId"] = e.CommandArgument;
                grdCourtType.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdCourtType.UseAccessibleHeader = true;
                if (!string.IsNullOrEmpty(lblCourtNameID.Text))
                {
                    ddlCourtType.ClearSelection();
                    ddlCourtType.Items.FindByValue(lblCourtNameID.Text).Selected = true;
                }
              
                ddlCourtlocation.ClearSelection();
                ddlCourtlocation.Items.FindByValue(lblDistrictID.Value).Selected = true;
            }
            if (e.CommandName == "DeleteDetails")
            {
                int CourtType_ID = Convert.ToInt32(e.CommandArgument);
                objdb.ByTextQuery("delete from tbl_LegalCourtType where CourtType_ID=" + CourtType_ID);
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    //protected void ddlCourtlocation_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlCourtlocation.SelectedItem.Text == "Other")
    //        {
    //            otherDiv.Visible = true;
    //        }
    //        else { otherDiv.Visible = false; }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
    //    }
    //}
}
