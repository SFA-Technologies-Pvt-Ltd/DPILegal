using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_WAConcludeReport : System.Web.UI.Page
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
                GrdConcludeReport.DataSource = null;
                GrdConcludeReport.DataBind();

                ds = obj.ByProcedure("USP_GetWAConcludeRpt", new string[] { "FromDate", "Todate" }
                    , new string[] { Convert.ToDateTime(txtFromdate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtTodate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrdConcludeReport.DataSource = ds;
                    GrdConcludeReport.DataBind();
                }
                else
                {
                    GrdConcludeReport.DataSource = null;
                    GrdConcludeReport.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdConcludeReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            btnSearch_Click(sender, e);
            GrdConcludeReport.PageIndex = e.NewPageIndex;
            GrdConcludeReport.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
   
}