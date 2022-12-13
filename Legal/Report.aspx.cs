using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class mis_Legal_Report : System.Web.UI.Page
{
    DataSet ds,ds1,ds2,ds3;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    FillDivision();
                    FillOffice();
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
    protected void FillDivision()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_LegalReports", new string[] {"flag" }, new string[] {"0" }, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count >0)
            {
                ddlDivision.DataSource = ds;
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_ID";
                ddlDivision.DataBind();
                ddlDivision.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_LegalReports", new string[] { "flag" }, new string[] { "1" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOffice.DataSource = ds;
                ddlOffice.DataTextField = "Office_Name";
                ddlOffice.DataValueField = "Office_ID";
                ddlOffice.DataBind();
                ddlOffice.Items.Insert(0, new ListItem("All", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if(ddlDivision.SelectedIndex >0)
            {
                ds = objdb.ByProcedure("Sp_LegalReports", new string[] { "flag", "Division_ID" }, new string[] { "2",ddlDivision.SelectedValue.ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlOffice.DataSource = ds;
                    ddlOffice.DataTextField = "Office_Name";
                    ddlOffice.DataValueField = "Office_ID";
                    ddlOffice.DataBind();
                    ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                }
            }
            else
            {
                FillOffice();
            }
           
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        GridView1.DataSource = new string[]{};
        GridView1.DataBind();
        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
        GridView1.UseAccessibleHeader = true;
        try
        {
            ds = objdb.ByProcedure("Sp_LegalReports", new string[] { "flag", "Division_ID", "Office_ID", "Case_CourtType" }, new string[] { "3",ddlDivision.SelectedValue,ddlOffice.SelectedValue,ddlCourtType.SelectedValue }, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
                int CivilCase = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CivilCase"));
                int ConsumerCase = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("ConsumerCase"));
                int CriminalCase = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("CriminalCase"));
                int IncometaxCase = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("IncometaxCase"));
                int GSTCase = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("GSTCase"));
                int ServiceMaster = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("ServiceMaster"));
                GridView1.FooterRow.Cells[2].Text = "<b>| TOTAL |</b> ";
                GridView1.FooterRow.Cells[3].Text = "<b>" + CivilCase.ToString() + "</b>";
                GridView1.FooterRow.Cells[4].Text = "<b>" + ConsumerCase.ToString() + "</b>";
                GridView1.FooterRow.Cells[5].Text = "<b>" + CriminalCase.ToString() + "</b>";
                GridView1.FooterRow.Cells[6].Text = "<b>" + IncometaxCase.ToString() + "</b>";
                GridView1.FooterRow.Cells[7].Text = "<b>" + GSTCase.ToString() + "</b>";
                GridView1.FooterRow.Cells[8].Text = "<b>" + ServiceMaster.ToString() + "</b>";
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
            
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            lblMsg.Text = "";
            string strCivilCase = objdb.Encrypt("CivilCase");
            string strConsumerCase = objdb.Encrypt("ConsumerCase");
            string strCriminalCase = objdb.Encrypt("CriminalCase");
            string strIncometaxCase = objdb.Encrypt("IncometaxCase");
            string strGSTCase = objdb.Encrypt("GSTCase");
            string strServiceMaster = objdb.Encrypt("ServiceMaster");
            string strCourtType = objdb.Encrypt(ddlCourtType.SelectedValue.ToString());

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow gvRow = GridView1.Rows[index];
            if (e.CommandName == "CivilCase")
            {
                Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
                string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

                string url = "CaseDetailReport.aspx?OfficeID=" + strOfficeID + "&casetype=" + strCivilCase + "&courttype=" + strCourtType;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
            else if (e.CommandName == "ConsumerCase")
            {
                Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
                string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

                string url = "CaseDetailReport.aspx?OfficeID=" + strOfficeID + "&casetype=" + strConsumerCase + "&courttype=" + strCourtType;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
            else if (e.CommandName == "CriminalCase")
            {
                Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
                string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

                string url = "CaseDetailReport.aspx?OfficeID=" + strOfficeID + "&casetype=" + strCriminalCase + "&courttype=" + strCourtType;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
            else if (e.CommandName == "IncometaxCase")
            {
                Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
                string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

                string url = "CaseDetailReport.aspx?OfficeID=" + strOfficeID + "&casetype=" + strIncometaxCase + "&courttype=" + strCourtType;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
            else if (e.CommandName == "GSTCase")
            {
                Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
                string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

                string url = "CaseDetailReport.aspx?OfficeID=" + strOfficeID + "&casetype=" + strGSTCase + "&courttype=" + strCourtType;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
            else if (e.CommandName == "ServiceMaster")
            {
                Label lblOfficeID = (Label)gvRow.FindControl("lblOfficeID");
                string strOfficeID = objdb.Encrypt(lblOfficeID.Text);

                string url = "CaseDetailReport.aspx?OfficeID=" + strOfficeID + "&casetype=" + strServiceMaster + "&courttype=" + strCourtType;

                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("', '_blank');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}