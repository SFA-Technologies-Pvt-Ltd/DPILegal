using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_WPCaseList : System.Web.UI.Page
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
            
                lblMsg.Text = "";
                GrdCaseDetails.DataSource = null;
                GrdCaseDetails.DataBind();

                ds = obj.ByProcedure("USP_Legal_SelectWP_CaseList", new string[] { "Fromdate", "Todate" }
                    , new string[] { Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrdCaseDetails.DataSource = ds;
                    GrdCaseDetails.DataBind();
                    GrdCaseDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GrdCaseDetails.UseAccessibleHeader = true;
                }
                else
                {
                    GrdCaseDetails.DataSource = null;
                    GrdCaseDetails.DataBind();
                }
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdCaseDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            string ID = e.CommandArgument.ToString();
            Response.Redirect("../Legal/EditWPCases.aspx?ID=" + Server.UrlEncode(ID));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    //protected void GrdCaseDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        GrdCaseDetails.PageIndex = e.NewPageIndex;
    //        btnSearch_Click(sender,  e);
    //        GrdCaseDetails.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
    //    }
    //}
}