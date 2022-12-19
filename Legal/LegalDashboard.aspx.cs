using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


public partial class mis_Legal_LegalDashboard : System.Web.UI.Page
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
                    BIndWACaseCount();
                    UpComingHearing();
                    CourtTypeCase();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void CourtTypeCase()
    {
        try
        {
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByProcedure("USP_Legal_getCourtCaseCountForgraph", new string[] { }, new string[] { }, "dataset");
            StringBuilder Sb = new StringBuilder();
            Sb.Append("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>");
            Sb.Append("<script type='text/javascript'>");
            Sb.Append(" google.charts.load(");
            Sb.Append("'current', { 'packages': ['corechart'] });");
            Sb.Append("google.charts.setOnLoadCallback(drawChart);");
            Sb.Append("function drawChart()");
            Sb.Append("{");
            Sb.Append("var data = google.visualization.arrayToDataTable([");
            Sb.Append(" ['Court', 'Case No.'],");
            for (int i = 0; i < dsCase.Tables[0].Rows.Count; i++)
            {
                Sb.Append(" ['" + dsCase.Tables[0].Rows[i]["CourtTypeName"].ToString() + "', " + dsCase.Tables[0].Rows[i]["Case_ID"].ToString() + " ],");
            }

            Sb.Append("]);");
            //Sb.Append("var formatShort = new google.visualization.NumberFormat({");
            //Sb.Append("pattern: 'short'");
            //Sb.Append("});");
            //Sb.Append("formatShort.format(data, 1);");
            Sb.Append("var options = {");
            Sb.Append(" 'title':  'COURT WISE CASE No.',");
           
            Sb.Append("pieSliceText: 'value',");
            //Sb.Append("pieSliceText: 'label',");
            Sb.Append(" 'is3D':   false,");
            Sb.Append("tooltip: {");
            Sb.Append(" text: 'value'");

            Sb.Append(" }");
            Sb.Append("};");
            Sb.Append("var chart = new google.visualization.PieChart(document.getElementById('piechart'));");
            Sb.Append("chart.draw(data, options);");
            Sb.Append("}");
            Sb.Append("</script>");
            Sb.Append("<div id='piechart' style='width: 600px; height: 500px;'></div>");
            sbid.InnerHtml = Sb.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void UpComingHearing()
    {
        ds = objdb.ByProcedure("USP_GetUpcoming_HearingDate", new string[] { }, new string[] { }, "dataset");
        string Marquee = "";
        string space = "<span style='color:black; font-weight:bold;font-size:18px;'>,</span>";

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    Marquee += ds.Tables[0].Rows[i]["HearingDate"].ToString();
                }
                else
                {
                    Marquee += space + "&nbsp;&nbsp;&nbsp;  " + ds.Tables[0].Rows[i]["HearingDate"].ToString();
                }

            }
            spnHearing.InnerHtml = Marquee;
        }
    }

    protected void BIndWACaseCount()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Get_WACaseCount", new string[] { }, new string[] { }, "dataset");

            // Indore Court Case Count
            if (ds.Tables[0].Rows[0]["IndoreCase"].ToString() != "")
            {

                lblIndoreCases.Text = ds.Tables[0].Rows[0]["IndoreCase"].ToString() + " No's";
            }
            else { lblIndoreCases.Text = "00 No's"; }
            // Jabalpur Court Case Count
            if (ds.Tables[0].Rows[0]["JabalpurCase"].ToString() != "")
            {
                lblJabalpur.Text = ds.Tables[0].Rows[0]["JabalpurCase"].ToString() + " No's";
            }
            else { lblJabalpur.Text = "00 No's"; }
            // Gwalior Court Case Count
            if (ds.Tables[0].Rows[0]["GwaliorCase"].ToString() != "")
            {
                lblGwalior.Text = ds.Tables[0].Rows[0]["GwaliorCase"].ToString() + " No's";
            }
            else { lblGwalior.Text = "00 No's"; }
            // WA Case Count
            if (ds.Tables[0].Rows[0]["WACaseCount"].ToString() != "")
            {
                lblWACase.Text = ds.Tables[0].Rows[0]["WACaseCount"].ToString() + " No's";
            }
            // Contempt case
            //if (ds.Tables[0].Rows[0]["ContemptCaseCount"].ToString() != "")
            //{
            //    lblConTempt.Text = ds.Tables[0].Rows[0]["ContemptCaseCount"].ToString() + " No's";
            //}
            //else { 
            lblConTempt.Text = "00 No's";
            //}

            // WP Case Count
            if (ds.Tables[0].Rows[0]["WPCaseCount"].ToString() != "")
            {
                lblWPCase.Text = ds.Tables[0].Rows[0]["WPCaseCount"].ToString() + " No's";
            }
            else { lblWPCase.Text = "00 No's"; }
            // PP Case
            if (ds.Tables[0].Rows[0]["PPCase"].ToString() != "")
            {
                lblPPCase.Text = ds.Tables[0].Rows[0]["PPCase"].ToString() + " No's";
            }
            else { lblPPCase.Text = "00 No's"; }

            // DPI Case
            if (ds.Tables[0].Rows[0]["DPICase"].ToString() != "")
            {
                lblDPICase.Text = ds.Tables[0].Rows[0]["DPICase"].ToString() + " No's";
            }
            else { lblDPICase.Text = "00 No's"; }
            // JD Case
            if (ds.Tables[0].Rows[0]["JDCase"].ToString() != "")
            {
                lblJDCases.Text = ds.Tables[0].Rows[0]["JDCase"].ToString() + " No's";
            }
            else { lblJDCases.Text = "00 No's"; }
            // DEO Case
            if (ds.Tables[0].Rows[0]["DEOCase"].ToString() != "")
            {
                lblDEOCases.Text = ds.Tables[0].Rows[0]["DEOCase"].ToString() + " No's";
            }
            else { lblDEOCases.Text = "00 No's"; }
            // RSK Case
            if (ds.Tables[0].Rows[0]["RSKCase"].ToString() != "")
            {
                lblRskCases.Text = ds.Tables[0].Rows[0]["RSKCase"].ToString() + " No's";
            }
            else { lblRskCases.Text = "00 No's"; }
            // TBC Case
            if (ds.Tables[0].Rows[0]["TBCCase"].ToString() != "")
            {
                lblTBCCases.Text = ds.Tables[0].Rows[0]["TBCCase"].ToString() + " No's";
            }
            else { lblTBCCases.Text = "00 No's"; }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJabalpur_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnJabalpur.Text == "View Detail")
            {
                string ID = "2";
                string CaseType = "Jabalpur Court Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnIndoreCases_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnIndoreCases.Text == "View Detail")
            {
                string ID = "3";
                string CaseType = "Indore Court Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnGwalior_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnGwalior.Text == "View Detail")
            {
                string ID = "4";
                string CaseType = "Gwalior Court Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnWPCase_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnWPCase.Text == "View Detail")
            {
                string ID = "WPCase";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?WPID=WPCase");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnWACase_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnWACase.Text == "View Detail")
            {
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?WAID=WACase");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnPPCase_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnPPCase.Text == "View Detail")
            {
                string ID = "1";
                string CaseType = "PP Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnDPICase_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnDPICase.Text == "View Detail")
            {
                string ID = "2";
                string CaseType = "DPI Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJDCases_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnJDCases.Text == "View Detail")
            {
                string ID = "3";
                string CaseType = "JD Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnDEOCases_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnDEOCases.Text == "View Detail")
            {
                string ID = "4";
                string CaseType = "DEO Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnRskCases_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnRskCases.Text == "View Detail")
            {
                string ID = "5";
                string CaseType = "RSK Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnTBCCases_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnTBCCases.Text == "View Detail")
            {
                string ID = "6";
                string CaseType = "TBC Case";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}