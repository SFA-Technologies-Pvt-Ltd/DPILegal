using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_WAPendingReport : System.Web.UI.Page
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

            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                GrdWAPendingReport.DataSource = null;
                GrdWAPendingReport.DataBind();

                ds = obj.ByProcedure("USP_GetWAPendingCaseRpt", new string[] { "FromDate", "Todate" }
                    , new string[] { Convert.ToDateTime(txtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrdWAPendingReport.DataSource = ds;
                    GrdWAPendingReport.DataBind();
                }
                else
                {
                    GrdWAPendingReport.DataSource = null;
                    GrdWAPendingReport.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdWAPendingReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            dtlCaseDetail.DataSource = null;
            dtlCaseDetail.DataBind();
            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdHearingDtl.DataSource = null;
            GrdHearingDtl.DataBind();

            if (e.CommandName == "ViewDetail")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                ds = obj.ByProcedure("USP_ViewWAPendingCaseFullDtlRpt", new string[] { "WACase_ID" }
                   , new string[] { e.CommandArgument.ToString() }, "dataset");

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewAllDtl.Visible = true;
                    ViewCaseDtl.Visible = true;
                    FieldAllRecordGrid.Visible = false;
                    FieldControl.Visible = false;
                    dtlCaseDetail.DataSource = ds;
                    dtlCaseDetail.DataBind();
                    GrdResponderDtl.DataSource = ds.Tables[1];
                    GrdResponderDtl.DataBind();

                    if (ds.Tables[0].Rows[0]["HearingDtl"].ToString() != "")
                    {
                        FieldHearingDtl.Visible = true;
                        GrdHearingDtl.DataSource = ds;
                        GrdHearingDtl.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnBackToDiv_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnBackToDiv.Text == "Back")
            {
                FieldAllRecordGrid.Visible = true;
                FieldControl.Visible = true;
                FieldHearingDtl.Visible = false;
                ViewAllDtl.Visible = false;
                ViewCaseDtl.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}