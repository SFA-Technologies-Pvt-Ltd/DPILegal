using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


public partial class mis_Legal_PSDashboard : System.Web.UI.Page
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
                    HighpriorityPScaseCount();
                    DepartmentWiseCase();
                    CourtTypeCase();
                    BindCaseTypeCount();
                    BindDepartmentWiseCaseTableCount();
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
            int c = 0;
            DataSet dsCasecount = new DataSet();
            dsCasecount = objdb.ByDataSet("select  Count(distinct UniqueNo) CaseID,CaseType Casetype_Name from tbl_OldCaseDetail where PartyName like '%PRINCIPAL SECRETARY%' group by CaseType");
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
                c = c + Convert.ToInt32(dsCasecount.Tables[0].Rows[i]["CaseID"]);
                SbCount.Append(" ['" + dsCasecount.Tables[0].Rows[i]["Casetype_Name"].ToString() + "', " + dsCasecount.Tables[0].Rows[i]["CaseID"].ToString() + " ],");
            }
            CasetypeCountno.InnerHtml = c.ToString();
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



        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void BindDepartmentWiseCaseTableCount()
    {
        try
        {
            string[] departArr = new string[] { "Board of Secondary Education","Commissioner," +
            "State Education Centre","Directorate of Public Instruction (DPI)",
            "Sanskrit Board (Maharshree Patanjali Sanskrit Sansthan), Bhopal",
            "School Education Department",
            "State Institute of Science Education, Jabalpur"
            ,"State Open School (MPSOS), Bhopal","Text Book Corporation, Bhopal" };
            //string[] departArr = new string[] { "Board of Secondary Education", "Directorate of Public Instruction (DPI)", "Sanskrit Board", "State Institute of Science Education", "State Open School - MPSOS", "Text Book Corporation", "Commissioner, State Education Centre" };
            DataSet dsCasecount = new DataSet();

            string str = "";

            str += "<table border='1' style='text-align:center;color:darkcyan;font-size:18px;height:573px;width:100%;'><tr style='background-color: #fff;'><td style='font-weight:bold;color: black;width: 500px;'>Department</td><td style='font-weight:bold;color: black;word-wrap: break-word'>CONC</td><td style='font-weight:bold;color: black;word-wrap: break-word'>WP</td></tr>";
            int tCount = 0;
            for (int i = 0; i < departArr.Length; i++)
            {


                // tCount += Convert.ToInt32(dsCasecount.Tables[0].Rows[i]["CaseCount"]);

                str += "<tr><td style='font-weight:bold;'>" + departArr[i].ToString() + "</td>";
                dsCasecount = objdb.ByDataSet("select  Count(distinct UniqueNo) CaseCount,Department,CaseType from tbl_OldCaseDetail " +
            "where  Department like '%" + departArr[i] + "%' and CaseType='CONC'  and (PartyName like '%PRINCIPAL SECRETARY%') group by Department, CaseType");
                if (dsCasecount.Tables.Count > 0 && dsCasecount.Tables[0].Rows.Count > 0)
                {
                    tCount = tCount + Convert.ToInt32(dsCasecount.Tables[0].Rows[0]["CaseCount"]);
                    str += "<td style='font-size: 22px;'><a href=\"DepartmentWisePSCaseDetails.aspx?CaseType=" + dsCasecount.Tables[0].Rows[0]["CaseType"].ToString() + "&department=" + departArr[i] + "\" target='_blank'>" + dsCasecount.Tables[0].Rows[0]["CaseCount"].ToString() + "</a></td>";
                }
                else
                    str += "<td style='font-weight:bold;'> 0 </td>";

                dsCasecount = objdb.ByDataSet("select  Count(distinct UniqueNo) CaseCount,Department,CaseType from tbl_OldCaseDetail " +
           "where Department  like '%" + departArr[i] + "%' and CaseType='WP' and (PartyName like '%PRINCIPAL SECRETARY%') group by Department, CaseType");
                if (dsCasecount.Tables.Count > 0 && dsCasecount.Tables[0].Rows.Count > 0)
                {
                    tCount = tCount + Convert.ToInt32(dsCasecount.Tables[0].Rows[0]["CaseCount"]);
                    str += "<td style='font-size: 22px;'><a href=\"DepartmentWisePSCaseDetails.aspx?CaseType=" + dsCasecount.Tables[0].Rows[0]["CaseType"].ToString() + "&department=" + departArr[i] + "\" target='_blank'>" + dsCasecount.Tables[0].Rows[0]["CaseCount"].ToString() + "</a></td>";
                }
                else
                    str += "<td style='font-weight:bold;'> 0 </td>";

                str += "</tr> ";
            }
            str += "</table>";
            lblCasetypeCountno.Text = "(PENDING CASES : " + tCount.ToString() + " No's)";
            CasetypeCountID1.InnerHtml = str;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void CourtTypeCase()
    {
        try
        {
            int CaseCount = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByDataSet("select  Count(distinct UniqueNo) Case_ID,Court CourtTypeName from tbl_OldCaseDetail where (PartyName like '%PRINCIPAL SECRETARY%') group by Court");
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

    protected void DepartmentWiseCase()
    {
        try
        {
            int CaseCount = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByDataSet("select  Count(distinct UniqueNo) Case_ID,Department from tbl_OldCaseDetail where PartyName like " +
                "'%PRINCIPAL SECRETARY%' group by Department");
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
                Sb.Append(" ['" + dsCase.Tables[0].Rows[i]["Department"].ToString() + "', " + dsCase.Tables[0].Rows[i]["Case_ID"].ToString() + " ],");
            }
            lblCaseCount2.Text = "(TOTAL CASE " + CaseCount.ToString() + " No's)";
            Sb.Append("]);");
            Sb.Append("var options = {");
            Sb.Append(" 'title':  'Department Wise CASE No.',");
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
            Sb.Append("var chart = new google.visualization.PieChart(document.getElementById('piechartNewA'));");
            Sb.Append("chart.draw(data, options);");
            Sb.Append("}");
            Sb.Append("</script>");
            Sb.Append("<div id='piechartNewA' style='width: 500px;'></div>");
            sbid2.InnerHtml = Sb.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    //protected void UpComingHearing()
    //{
    //    ds = objdb.ByProcedure("USP_GetUpcoming_HearingDate", new string[] { }, new string[] { }, "dataset");
    //    string Marquee = "";
    //    string space = "<span style='color:black; font-weight:bold;font-size:18px;'>,</span>";

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            if (i == 0)
    //            {
    //                Marquee += ds.Tables[0].Rows[i]["HearingDate"].ToString();
    //            }
    //            else
    //            {
    //                Marquee += space + "&nbsp;&nbsp;&nbsp;  " + ds.Tables[0].Rows[i]["HearingDate"].ToString();
    //            }

    //        }
    //        spnHearing.InnerHtml = Marquee;
    //    }
    //}
    protected void HighpriorityPScaseCount()
    {
        try
        {
            ds = objdb.ByDataSet("select Count(distinct UniqueNo) HIghPriorityCase from tbl_OldCaseDetail where PartyName like '%PRINCIPAL SECRETARY%'");
            if (ds.Tables.Count >= 1 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["HIghPriorityCase"].ToString() != "")
                {
                    spnhighpriorityCase.InnerHtml = "&nbsp;" + ds.Tables[0].Rows[0]["HIghPriorityCase"].ToString() + " No's";
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


    protected void btnHighPriorityCase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                GrdHighpriorityCase.DataSource = null;
                GrdHighpriorityCase.DataBind();
                ds = objdb.ByDataSet("select  distinct UniqueNo,CaseNo,FilingNo,Court,Department,Petitioner,Respondent,CaseType,RespondentOffice,CaseSubjectId,CaseSubSubjectId," +
                    "HearingDate, OICId, OICMobileNo from tbl_OldCaseDetail where PartyName like  '%PRINCIPAL SECRETARY%' ");
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