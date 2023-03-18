using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Legal_CategoryWiseCases : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Office_ID"] != null && Session["Emp_ID"] != null)
            {
                ViewState["Office_ID"] = Session["Office_ID"].ToString();
                FillGrid();
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            string msg = "";
            //if (ddlCaseType.SelectedIndex == 0)
            //{
            //    msg += "Select Case Type.";
            //}
            if (msg == "")
            {
                FillGrid();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }


        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            if (ddlCaseType.SelectedIndex > 0 && ddlStatus.SelectedIndex == 0)
            {
                ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_Type" }, new string[] { "18", ddlCaseType.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                else
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
            }
            else if (ddlCaseType.SelectedIndex > 0 && ddlStatus.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_Type", "Case_Status" }, new string[] { "19", ddlCaseType.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                else
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
            }
            else if (ddlStatus.SelectedIndex > 0 && ddlCaseType.SelectedIndex == 0)
            {
                ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_Status" }, new string[] { "21", ddlStatus.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                else
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
            }
            else
            {
                ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag"}, new string[] { "12"}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }
                else
                {
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;
                }

            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string Case_ID = GridView1.SelectedDataKey.Value.ToString();
            Response.Redirect("CaseDetail.aspx?Case_ID=" + objdb.Encrypt(Case_ID));
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}