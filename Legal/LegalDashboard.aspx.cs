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
                    BindDataColoumn();
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
            SbCount.Append(" 'is3D': false,pieHole: 0.03,pieSliceTextStyle: {bold:true,fontSize: 12}, "); // Piehole using For Create Circle Into Center.
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
            Sb.Append(" 'is3D': false, pieHole: 0.03, pieSliceTextStyle: {fontSize: 12,bold:true },");// Piehole using For Create Circle Into Center.
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

    protected void BindDataColoumn()
    {
        DataTable dtcol = new DataTable();
        dtcol.Columns.Add("ID", typeof(int));
        dtcol.Columns.Add("Respondertype", typeof(string));
        dtcol.Columns.Add("CaseType", typeof(string));
        dtcol.Columns.Add("CaseSubject", typeof(string));
        dtcol.Columns.Add("CaseNo", typeof(string));
        dtcol.Columns.Add("CourtName", typeof(string));
        dtcol.Columns.Add("PetitionerName", typeof(string));
        dtcol.Columns.Add("NodalName", typeof(string));
        dtcol.Columns.Add("NodalMobileNo", typeof(string));
        dtcol.Columns.Add("NodalEmailID", typeof(string));
        dtcol.Columns.Add("OICName", typeof(string));
        dtcol.Columns.Add("OICMobileNo", typeof(string));
        dtcol.Columns.Add("OICEmailID", typeof(string));
        dtcol.Columns.Add("NextHearingDate", typeof(string));
        dtcol.Columns.Add("AdvocateName", typeof(string));
        dtcol.Columns.Add("AdvocateMobileNo", typeof(string));
        dtcol.Columns.Add("AdvocateEmailID", typeof(string));
        dtcol.Columns.Add("CaseDetail", typeof(string));

        ViewState["dtCol"] = dtcol;
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

            //// Indore Court Case Count
            //if (ds.Tables[0].Rows[0]["IndoreCase"].ToString() != "")
            //{

            //    lblIndoreCases.Text = ds.Tables[0].Rows[0]["IndoreCase"].ToString() + " No's";
            //}
            //else { lblIndoreCases.Text = "00 No's"; }
            //// Jabalpur Court Case Count
            //if (ds.Tables[0].Rows[0]["JabalpurCase"].ToString() != "")
            //{
            //    lblJabalpur.Text = ds.Tables[0].Rows[0]["JabalpurCase"].ToString() + " No's";
            //}
            //else { lblJabalpur.Text = "00 No's"; }
            //// Gwalior Court Case Count
            //if (ds.Tables[0].Rows[0]["GwaliorCase"].ToString() != "")
            //{
            //    lblGwalior.Text = ds.Tables[0].Rows[0]["GwaliorCase"].ToString() + " No's";
            //}
            //else { lblGwalior.Text = "00 No's"; }
            //// WA Case Count
            //if (ds.Tables[0].Rows[0]["WACaseCount"].ToString() != "")
            //{
            //    lblWACase.Text = ds.Tables[0].Rows[0]["WACaseCount"].ToString() + " No's";
            //}
            // Contempt case
            //if (ds.Tables[0].Rows[0]["ContemptCaseCount"].ToString() != "")
            //{
            //    lblConTempt.Text = ds.Tables[0].Rows[0]["ContemptCaseCount"].ToString() + " No's";
            //}
            //else { 
            //  lblConTempt.Text = "00 No's";
            //}

            // WP Case Count
            //if (ds.Tables[0].Rows[0]["WPCaseCount"].ToString() != "")
            //{
            //    lblWPCase.Text = ds.Tables[0].Rows[0]["WPCaseCount"].ToString() + " No's";
            //}
            //else { lblWPCase.Text = "00 No's"; }
            //// PP Case
            //if (ds.Tables[0].Rows[0]["PPCase"].ToString() != "")
            //{
            //    lblPPCase.Text = ds.Tables[0].Rows[0]["PPCase"].ToString() + " No's";
            //}
            //else { lblPPCase.Text = "00 No's"; }

            //// DPI Case
            //if (ds.Tables[0].Rows[0]["DPICase"].ToString() != "")
            //{
            //    lblDPICase.Text = ds.Tables[0].Rows[0]["DPICase"].ToString() + " No's";
            //}
            //else { lblDPICase.Text = "00 No's"; }
            //// JD Case
            //if (ds.Tables[0].Rows[0]["JDCase"].ToString() != "")
            //{
            //    lblJDCases.Text = ds.Tables[0].Rows[0]["JDCase"].ToString() + " No's";
            //}
            //else { lblJDCases.Text = "00 No's"; }
            //// DEO Case
            //if (ds.Tables[0].Rows[0]["DEOCase"].ToString() != "")
            //{
            //    lblDEOCases.Text = ds.Tables[0].Rows[0]["DEOCase"].ToString() + " No's";
            //}
            //else { lblDEOCases.Text = "00 No's"; }
            //// RSK Case
            //if (ds.Tables[0].Rows[0]["RSKCase"].ToString() != "")
            //{
            //    lblRskCases.Text = ds.Tables[0].Rows[0]["RSKCase"].ToString() + " No's";
            //}
            //else { lblRskCases.Text = "00 No's"; }
            //// TBC Case
            //if (ds.Tables[0].Rows[0]["TBCCase"].ToString() != "")
            //{
            //    lblTBCCases.Text = ds.Tables[0].Rows[0]["TBCCase"].ToString() + " No's";
            //}
            //else { lblTBCCases.Text = "00 No's"; }


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["RespondertypeName"].ToString() == "PP Cases")
                {
                    lblPPCase.Text = ds.Tables[0].Rows[i]["Case_ID"].ToString() + " No's";
                }
                if (ds.Tables[0].Rows[i]["RespondertypeName"].ToString() == "DPI Cases")
                {
                    lblDPICase.Text = ds.Tables[0].Rows[i]["Case_ID"].ToString() + " No's";
                }
                if (ds.Tables[0].Rows[i]["RespondertypeName"].ToString() == "JD Cases")
                {
                    lblJDCases.Text = ds.Tables[0].Rows[i]["Case_ID"].ToString() + " No's";
                }
                if (ds.Tables[0].Rows[i]["RespondertypeName"].ToString() == "DEO Cases")
                {
                    lblDEOCases.Text = ds.Tables[0].Rows[i]["Case_ID"].ToString() + " No's";
                }               
                if (ds.Tables[0].Rows[i]["RespondertypeName"].ToString() == "RSK Cases")
                {
                    lblRskCases.Text = ds.Tables[0].Rows[i]["Case_ID"].ToString() + " No's";
                }               
                if (ds.Tables[0].Rows[i]["RespondertypeName"].ToString() == "TBC Cases")
                {
                    lblTBCCases.Text = ds.Tables[0].Rows[i]["Case_ID"].ToString() + " No's";
                }               
            }
            if (lblPPCase.Text == "")
            { lblPPCase.Text = "00 No's"; }
            if (lblDPICase.Text == "")
            { lblDPICase.Text = "00 No's"; }
            if (lblJDCases.Text == "")
            { lblJDCases.Text = "00 No's"; }
            if (lblDEOCases.Text == "")
            { lblDEOCases.Text = "00 No's"; }
            if (lblRskCases.Text == "")
            { lblRskCases.Text = "00 No's"; }
            if (lblTBCCases.Text == "")
            { lblTBCCases.Text = "00 No's"; }
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
    protected void btnHighPriorityCase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GrdHighpriorityCase.DataSource = null;
                GrdHighpriorityCase.DataBind();

                DataTable dt = (DataTable)ViewState["dtCol"];

                if (dt.Columns.Count > 0)
                {
                    dt.Rows.Add("1", "DPI Case", "Yes", "स्थानांतरण", "Ct001202", "Jabalpur High Court", "Mohan Lal Singh", "Gouri Shanker", "8952232325", "gourishanker46@gmail.com", "Srikant Parte", "7895641563", "Srikantp8955@gmail.com", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"), "Narendra Rao", "6589744512", "Narendrrao8745@gmail.com", "Case In Progress");
                    dt.Rows.Add("2", "PP Case", "Yes", "वेतन वृद्धि", "Ct001995", "Gwalior High Court", "Gurmeet Singh", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Mohan Parte", "8895641563", "Mohantp8955@gmail.com", DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"), "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress");
                    dt.Rows.Add("3", "DPI Case", "Yes", "स्थानांतरण", "Ct001202", "Jabalpur High Court", "Durgesh Kahar", "Gouri Shanker", "8952232325", "gourishanker46@gmail.com", "Srikant Parte", "7895641563", "Srikantp8955@gmail.com", DateTime.Now.AddDays(6).ToString("dd/MM/yyyy"), "Narendra Rao", "6589744512", "Narendrrao8745@gmail.com", "Case In Progress");
                    dt.Rows.Add("4", "PP Case", "Yes", "वेतन वृद्धि", "Ct001995589", "Gwalior High Court", "Sharman Singh", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Mohan Parte", "8895641563", "Mohantp8955@gmail.com", DateTime.Now.AddDays(11).ToString("dd/MM/yyyy"), "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress");
                    dt.Rows.Add("4", "PP Case", "Yes", "पदोन्नती", "Ct0019910010", "Indore High Court", "kailash Verma", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Mohan Parte", "8895641563", "Mohantp8955@gmail.com", DateTime.Now.AddDays(9).ToString("dd/MM/yyyy"), "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress");
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    GrdHighpriorityCase.DataSource = dt;
                    GrdHighpriorityCase.DataBind();
                    dt.Clear();
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