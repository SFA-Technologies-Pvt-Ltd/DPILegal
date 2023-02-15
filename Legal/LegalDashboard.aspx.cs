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
                    BindCaseTypeCount();
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

    protected void BindCaseTypeCount()
    {
        try
        {
            DataSet dsCasecount = new DataSet();
            dsCasecount = objdb.ByProcedure("USP_Legal_GetCasetypeCount_ForGraph", new string[] { }, new string[] { }, "dataset");
            StringBuilder SbCount = new StringBuilder();
            SbCount.Append("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>");
            SbCount.Append("<script type='text/javascript'>");
            SbCount.Append(" google.charts.load(");
            SbCount.Append("'current', { 'packages': ['corechart'] });");
            SbCount.Append("google.charts.setOnLoadCallback(drawChart);");
            SbCount.Append("function drawChart()");
            SbCount.Append("{");
            SbCount.Append("var data = google.visualization.arrayToDataTable([");
            SbCount.Append(" ['Court', 'Case No.'],");
            for (int i = 0; i < dsCasecount.Tables[0].Rows.Count; i++)
            {
                SbCount.Append(" ['" + dsCasecount.Tables[0].Rows[i]["Casetype_Name"].ToString() + "', " + dsCasecount.Tables[0].Rows[i]["CaseID"].ToString() + " ],");
            }
            SbCount.Append("]);");
            SbCount.Append("var options = {");
            SbCount.Append(" 'title':  'CASE TYPE CASE COUNT.',");
            SbCount.Append("colors: ['#4BB160', '#104C9C', '#EC5D92', '#f3b49f'],");// thise is tempreory coloer shown in chart.
            //SbCount.Append("backgroundColor: 'transparent',"); // to remove &change backcolor.
            SbCount.Append("chartArea: {");
            SbCount.Append("height: '100%',");
            SbCount.Append("width: '100%',");
            SbCount.Append("top: 12,");
            SbCount.Append("left: 12,");
            SbCount.Append("right: 12,");
            SbCount.Append("bottom: 12");
            SbCount.Append("},");
            SbCount.Append(" height: 250,");
          //  SbCount.Append(" 'is3D': false,pieHole: 0.03,pieSliceTextStyle: {bold:true,fontSize: 12}, "); // Piehole using For Create Circle Into Center.
            SbCount.Append(" 'is3D': false,pieSliceTextStyle: {bold:true,fontSize: 12}, ");
            SbCount.Append("legend: {");
            SbCount.Append("position: 'labeled',");
            SbCount.Append("   textStyle: {");
            SbCount.Append("fontSize: 13, bold:true");
            SbCount.Append("},");
            SbCount.Append("labeledValueText: 'none'"); // thise line For Remove Percentage From Legend
            SbCount.Append("},");
            SbCount.Append("pieSliceText: 'value',"); // thise line For Show value in Chart
            SbCount.Append("tooltip: {");
            SbCount.Append(" text: 'value'"); // thise line For Remove Percentage From tooltip
            SbCount.Append(" }");
            SbCount.Append("};");
            SbCount.Append("var chart = new google.visualization.PieChart(document.getElementById('piechart'));");
            SbCount.Append("chart.draw(data, options);");
            SbCount.Append("}");
            SbCount.Append("</script>");
            SbCount.Append("<div id='piechart' style='width: 500px;'></div>");
            CasetypeCountID.InnerHtml = SbCount.ToString();
            if (dsCasecount != null && dsCasecount.Tables[1].Rows[0]["TotalCase"].ToString() != "")
            {
                CasetypeCountno.InnerHtml = dsCasecount.Tables[1].Rows[0]["TotalCase"].ToString();
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
            int CaseCount = 0;
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
                CaseCount = CaseCount + Convert.ToInt32(dsCase.Tables[0].Rows[i]["Case_ID"]);
                Sb.Append(" ['" + dsCase.Tables[0].Rows[i]["CourtTypeName"].ToString() + "', " + dsCase.Tables[0].Rows[i]["Case_ID"].ToString() + " ],");
            }
            lblCaseCount.Text = "(TOTAL CASE " + CaseCount.ToString() + " No's)";
            Sb.Append("]);");
            Sb.Append("var options = {");
            Sb.Append(" 'title':  'COURT WISE CASE No.',");
            //Sb.Append("colors: ['#e0440e', '#e6693e', '#ec8f6e', '#f3b49f', '#f6c7b6'],"); // Using To Apply Chart Colors .
            Sb.Append("chartArea: {");
            Sb.Append("height: '100%',");
            Sb.Append("width: '100%',");
            Sb.Append("top: 12,");
            Sb.Append("left: 12,");
            Sb.Append("right: 12,");
            Sb.Append("bottom: 12");
            Sb.Append("},");
            Sb.Append(" height: 250,");
            //Sb.Append(" 'is3D': false, pieHole: 0.03, pieSliceTextStyle: {fontSize: 12,bold:true },");// Piehole using For Create Circle Into Center.
            Sb.Append(" 'is3D': false, pieSliceTextStyle: {fontSize: 12,bold:true },");
            Sb.Append("legend: {");
            Sb.Append("position: 'labeled',");
            Sb.Append("textStyle: {");
            Sb.Append("fontSize: 13, bold:true");
            Sb.Append("}, ");
            Sb.Append("labeledValueText: 'none'"); // thise line For Remove Percentage From Legend
            Sb.Append("},");
            Sb.Append("pieSliceText: 'value',"); // thise line For Show value in Chart
            Sb.Append("tooltip: {");
            Sb.Append(" text: 'value'"); // thise line For Remove Percentage From tooltip
            Sb.Append(" }");
            Sb.Append("};");
            Sb.Append("var chart = new google.visualization.PieChart(document.getElementById('piechartNew'));");
            Sb.Append("chart.draw(data, options);");
            Sb.Append("}");
            Sb.Append("</script>");
            Sb.Append("<div id='piechartNew' style='width: 500px;'></div>");
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
            if (ds.Tables.Count >= 1 && ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["HIghPriorityCase"].ToString() != "")
                {
                    spnhighpriorityCase.InnerHtml = "&nbsp;" + ds.Tables[1].Rows[0]["HIghPriorityCase"].ToString() + " No's";
                }
            }
            else
            {
                spnhighpriorityCase.InnerHtml = "&nbsp;" + "00 No's";
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
                string CaseType = "MP Govt Case"; // Here MP Govt Case
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
                string CaseType = "ENC Case"; // Here ENC Case
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
                string CaseType = "Zone Case"; // Here Zone Case
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
                string CaseType = "Cirlce Case"; // Here Cirlce Case
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
                string CaseType = "Jal Nigam Case"; // Here Jal Nigam Case
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
                string CaseType = "DO Case"; // Here DO Case
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID) + "&Casetype=" + CaseType);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnHighPriorityCase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GrdHighpriorityCase.DataSource = null;
                GrdHighpriorityCase.DataBind();
                ds = objdb.ByProcedure("USP_Legal_GetHighPriorityCase", new string[] { }, new string[] { }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrdHighpriorityCase.DataSource = ds;
                    GrdHighpriorityCase.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}