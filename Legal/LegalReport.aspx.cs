using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_Legal_LegalReport : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    
                    FillGrid();
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag" }, new string[] { "24" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                int TotalCases = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalCase"));
                int OpenCases = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("OpenCase"));
                int CloseCases = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CloseCase"));
                GridView1.FooterRow.Cells[2].Text = "| TOTAL | ";
                GridView1.FooterRow.Cells[3].Text = "<b>" + TotalCases.ToString() + "</b>";
                GridView1.FooterRow.Cells[4].Text = "<b>" + OpenCases.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + CloseCases.ToString() + "</b>";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void lnktotalcase_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton totalcase = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnktotalcase");
    //        string OfficeID = totalcase.ToolTip.ToString();
    //        string myparam1 = "AllCase";
    //        string QueryString = "AllLegalCasesDetail.aspx?OfficeID=" + OfficeID + "&myparam1=" + myparam1;
    //        Response.Write("<script>");
    //        Response.Write("window.open('" + QueryString + "','_blank')");
    //        Response.Write("</script>");

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void lnkopencase_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton totalcase = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkopencase");
    //        string OfficeID = totalcase.ToolTip.ToString();
    //        string myparam1 = "OpenCase";
    //        string QueryString = "AllLegalCasesDetail.aspx?OfficeID=" + OfficeID + "&myparam1=" + myparam1;
    //        Response.Write("<script>");
    //        Response.Write("window.open('" + QueryString + "','_blank')");
    //        Response.Write("</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    //protected void lnkclosecase_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
    //        LinkButton totalcase = (LinkButton)GridView1.Rows[selRowIndex].FindControl("lnkclosecase");
    //        string OfficeID = totalcase.ToolTip.ToString();
    //        string myparam1 = "CloseCase";
    //        string QueryString = "AllLegalCasesDetail.aspx?OfficeID=" + OfficeID + "&myparam1=" + myparam1;
    //        Response.Write("<script>");
    //        Response.Write("window.open('" + QueryString + "','_blank')");
    //        Response.Write("</script>");

    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

    //    }

    //}

    //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strAllCase = objdb.Encrypt("AllCase");
        string strOpenCase = objdb.Encrypt("OpenCase");
        string strCloseCase = objdb.Encrypt("CloseCase");

        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow gvRow = GridView1.Rows[index];

        if (e.CommandName == "AllCase")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

            string url = "AllLegalCasesDetail.aspx?OfficeID=" + strOfficeID + "&myparam1=" + strAllCase;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        else if (e.CommandName == "OpenCase")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

            string url = "AllLegalCasesDetail.aspx?OfficeID=" + strOfficeID + "&myparam1=" + strOpenCase;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
        else if (e.CommandName == "CloseCase")
        {
            Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
            string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

            string url = "AllLegalCasesDetail.aspx?OfficeID=" + strOfficeID + "&myparam1=" + strCloseCase;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }
    
}