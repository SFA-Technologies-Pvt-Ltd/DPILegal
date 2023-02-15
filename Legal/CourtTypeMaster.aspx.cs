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
                }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("Sp_CourtType", new string[] { "flag", "CourtTypeName", "District_Id", "CreatedBy", "CreatedByIP", "OtherLocation" }, new string[] {
                        "1",txtCourtType.Text,ddlCourtlocation.SelectedValue,ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress(),txtOther.Text.Trim() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["CourtId"] != "" && ViewState["CourtId"] != null)
                {
                    ds = objdb.ByProcedure("Sp_CourtType", new string[] { "flag", "CourtTypeName", "District_Id", "LastupdatedBy", "LastupdatedByIP", "CourtTypeID","OtherLocation" }, new string[] {
                        "3",txtCourtType.Text,ddlCourtlocation.SelectedValue,ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress(), ViewState["CourtId"].ToString(),txtOther.Text.Trim() }, "dataset");
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtCourtType.Text = "";
                    ddlCourtlocation.ClearSelection();
                    ddlCourtlocation_SelectedIndexChanged(sender, e); 
                    txtOther.Text = "";
                    BindGrid();
                    btnSave.Text = "Save";
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                }
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
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
                Label lblCourName = (Label)row.FindControl("lblCourName");
                Label lblOtherlocation = (Label)row.FindControl("lblOtherlocation");
                btnSave.Text = "Update";
                ViewState["CourtId"] = e.CommandArgument;
                txtCourtType.Text = lblCourName.Text;
                ddlCourtlocation.ClearSelection();
                ddlCourtlocation.Items.FindByValue(lblDistrictID.Value).Selected = true;

                ddlCourtlocation_SelectedIndexChanged(sender, e);
                txtOther.Text = lblOtherlocation.Text;
            }
        }
        catch (Exception ex)
        {          
        }
    }

    protected void ddlCourtlocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCourtlocation.SelectedItem.Text == "Other")
            {
                otherDiv.Visible = true;
            }
            else { otherDiv.Visible = false; }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}
