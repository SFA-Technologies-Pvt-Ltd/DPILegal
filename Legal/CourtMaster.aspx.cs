using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Legal_CourtMaster : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"];
                ViewState["Office_Id"] = Session["Office_Id"];
                lblMsg.Text = "";
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void BindGrid()
    {
        try
        {
            ds = new DataSet();
            DataSet dst = objdb.ByDataSet("select CourtName,CourtName_ID from tbl_LegalCourtMaster");
            if (dst.Tables.Count > 0)
            {
                grdCourtName.DataSource = dst;
                grdCourtName.DataBind();
            }
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
                    ds = objdb.ByProcedure("USP_CourtMaster", new string[] { "flag", "CourtName", "CreatedBy", "CreatedByIP" }, new string[] {
                        "1",txtCourtName.Text.Trim(),ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["CourtName_ID"] != "" && ViewState["CourtName_ID"] != null)
                {
                    ds = objdb.ByProcedure("USP_CourtMaster", new string[] { "flag", "CourtName", "LastupdatedBy", "LastupdatedByIP", "CourtName_ID" }, new string[] {
                        "2",txtCourtName.Text.Trim(),ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress(), ViewState["CourtName_ID"].ToString() }, "dataset");
                }
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtCourtName.Text = "";

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
    protected void grdCourtName_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                ViewState["CourtName_ID"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblCourtName_ID = (Label)row.FindControl("lblCourtName_ID");

                btnSave.Text = "Update";
                ViewState["CourtName_ID"] = e.CommandArgument;
                txtCourtName.Text = lblCourtName_ID.Text;
            }
            if (e.CommandName == "DeleteDetails")
            {
                int CourtName_ID = Convert.ToInt32(e.CommandArgument);
                objdb.ByTextQuery("delete from tbl_LegalCourtMaster where CourtName_ID=" + CourtName_ID);
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdCourtName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdCourtName.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}