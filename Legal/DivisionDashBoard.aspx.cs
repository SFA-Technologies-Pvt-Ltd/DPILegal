using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Legal_DivisionDashBoard : System.Web.UI.Page
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
                    int Division_ID = Convert.ToInt16(Session["Division_Id"].ToString());
                    BIndWACaseCount(Division_ID);
                    //UpComingHearing();
                    CourtTypeCase(Division_ID);
                    CourtTypeCase1(Division_ID);
                    CourtTypeCase2(Division_ID);
                    CourtTypeCase3(Division_ID);
                    //BindCaseTypeCount(OicId);
                    CourtWiseContemptCases(Division_ID);

                    divCardHeader.InnerText = Session["Officelocation"].ToString();

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

    //protected void BindCaseTypeCount(int Division_ID)
    //{
    //    DataSet dsCasecount = new DataSet();
    //    dsCasecount = objdb.ByProcedure("Sp_DivisionWiseCasesDashboard", new string[] { "Division_ID" }, new string[] { Convert.ToString(Division_ID) }, "dataset");
    //    string str = "";

    //    str += "<table border='1' style='text-align:center;color:darkcyan;font-size:18px;height:573px;width:100%;'><tr style='background-color: #fff;'><td style='font-weight:bold;color: black;width: 87px;'>Case Type</td><td style='font-weight:bold;color: black;word-wrap: break-word'>Pending Cases Since 2000</td></tr>";
    //    int tCount = 0;
    //    int tCount1 = 0;
    //    DataSet dsCasecount2 = new DataSet();
    //    dsCasecount2 = objdb.ByProcedure("Sp_OrderByDirectionOicWisePendingCasesDashboard", new string[] { }, new string[] { }, "dataset");

    //    for (int i = 0; i < dsCasecount.Tables[1].Rows.Count; i++)
    //    {
    //        tCount += Convert.ToInt32(dsCasecount.Tables[1].Rows[i]["CaseTypeWisePendingCases"]);
    //        str += "<tr><td style='font-weight:bold;'>" + dsCasecount.Tables[1].Rows[i]["CaseType"].ToString() + "</td><td style='font-size: 22px;'><a href=\"OICWise_Pending_Case_Since_2000.aspx?CaseType=" + dsCasecount.Tables[1].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount.Tables[1].Rows[i]["CaseTypeWisePendingCases"].ToString() + "</a></td>";
    //        str += "</tr> ";
    //    }
    //    str += "</table>";

    //    string str2 = "";
    //    str2 += "<table border='1' style='text-align:center;height:573px;color:darkcyan;font-size:18px;width:100%;'><tr style='background-color: #fff;width:100%;'><td rowspan='2' style='font-weight:bold;color: black;width: 87px;'>Case Type</td><td rowspan='2' style='font-weight:bold;color: black;word-wrap: break-word'>Close Cases Since 2018</td><td rowspan='2' style='font-weight:bold;color: black;'>Order By Direction</td><td colspan='7' style='font-weight:bold;color: black;'>Complainces Status</td></tr><tr style='background-color: #fff;'><td colspan='3' style='font-weight:bold;color: black;'>Yes</td><td colspan='3' style='font-weight:bold;color: black;'>No</td><td colspan='3' style='font-weight:bold;color: black;'>Pending</td></tr>";
    //    for (int i = 0; i < dsCasecount2.Tables[0].Rows.Count; i++)
    //    {

    //        tCount1 += Convert.ToInt32(dsCasecount2.Tables[0].Rows[i]["IsOrderByDirectionCount"].ToString());
    //        str2 += "<tr><td style='font-weight:bold;'>" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "</td><td style='font-size: 22px;'><a href=\"OicIWse_Order_By_Direction_Pending_Cases.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["DisposeOfCaseSince2018"].ToString() + "</a></td>";
    //        str2 += "<td style='width: 52px;'><a href=\"Order_By_Direction_Count_Cases.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["IsOrderByDirectionCount"].ToString() + "</a></td>";

    //        str2 += "<td colspan='3'><a href=\"Order_By_Direction_Count_IsComplainceYes.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["YesCount"].ToString() + "</a></td>";

    //        str2 += "<td colspan='3'><a href=\"Order_By_Direction_Count_IsComplainceNo.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["NOCounts"].ToString() + "</a></td>";

    //        str2 += "<td colspan='3'><a href=\"Order_By_Direction_Count_IsComplaincePending.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["PendingCounts"].ToString() + "</a></td>";


    //        str2 += "</tr> ";
    //    }
    //    str2 += " </table>";

    //}

    protected void CourtTypeCase(int Division_ID)
    {
        try
        {
            int CaseCount = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByProcedure("Sp_DivisionWiseCasesDashboard", new string[] { "Division_ID" }, new string[] { Convert.ToString(Division_ID) }, "dataset");
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
                CaseCount = CaseCount + Convert.ToInt32(dsCase.Tables[0].Rows[i]["CourtWisePendingCases"]);
                Sb.Append(" ['" + dsCase.Tables[0].Rows[i]["court"].ToString() + "', " + dsCase.Tables[0].Rows[i]["CourtWisePendingCases"].ToString() + " ],");
            }
            lblCaseCount.Text = "(PENDING CASES " + CaseCount.ToString() + " No's)";
            Sb.Append("]);");
            Sb.Append("var options = {");
            Sb.Append(" 'title':  'COURT WISE CASE No.',");
            Sb.Append("colors: ['#fcba03', '#008080',  '#30c9d1'],"); // Using To Apply Chart Colors .
            Sb.Append("chartArea: {");
            Sb.Append("height: '100%',");
            Sb.Append("width: '100%',");
            Sb.Append("top: 12,");
            Sb.Append("left: 12,");
            Sb.Append("right:12,");
            Sb.Append("bottom: 12");
            Sb.Append("},");
            Sb.Append(" height: 250,");
            Sb.Append(" 'is3D': true, pieSliceTextStyle: {fontSize: 12,bold:true },");
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

    protected void CourtTypeCase1(int Division_ID)
    {
        try
        {
            int CaseCount1 = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByDataSet("Select CourtName as Court,COUNT(distinct UniqueNo) OrderBydirectCourtWiseCount from tblLegalCaseRegistration  CR " +
"inner join Mst_District DM on DM.District_ID=CR.District_ID " +
"inner join tblDivisionMaster DVM on DVM.Division_ID = DM.Division_ID " +
"where CaseDisposalType_Id='2' and CR.District_ID in (select District_ID from Mst_District where Division_ID="+Division_ID+") " +
"and CR.Isactive=1  group by CourtName");
            StringBuilder Sb1 = new StringBuilder();
            Sb1.Append("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>");
            Sb1.Append("<script type='text/javascript'>");
            Sb1.Append(" google.charts.load(");
            Sb1.Append("'current', { 'packages': ['corechart'] });");
            Sb1.Append("google.charts.setOnLoadCallback(drawChart);");
            Sb1.Append("function drawChart()");
            Sb1.Append("{");
            Sb1.Append("var data = google.visualization.arrayToDataTable([");
            Sb1.Append(" ['CourtN', 'CaseNo.'],");
            for (int i = 0; i < dsCase.Tables[0].Rows.Count; i++)
            {
                CaseCount1 = CaseCount1 + Convert.ToInt32(dsCase.Tables[0].Rows[i]["OrderBydirectCourtWiseCount"]);
                Sb1.Append(" ['" + dsCase.Tables[0].Rows[i]["Court"].ToString() + "', " + dsCase.Tables[0].Rows[i]["OrderBydirectCourtWiseCount"].ToString() + " ],");
            }
            lblCaseCount1.Text = "(CASES " + CaseCount1.ToString() + " No's)";
            Sb1.Append("]);");
            Sb1.Append("var options = {");
            Sb1.Append(" 'title':  'COURT WISE CASE No.',");
            Sb1.Append("colors: ['#008080', '#fcba03', '#30c9d1'],"); // Using To Apply Chart Colors .
            Sb1.Append("chartArea: {");
            Sb1.Append("height: '100%',");
            Sb1.Append("width: '100%',");
            Sb1.Append("top: 12,");
            Sb1.Append("left: 12,");
            Sb1.Append("right: 12,");
            Sb1.Append("bottom: 12");
            Sb1.Append("},");
            Sb1.Append(" height: 250,");
            //Sb.Append(" 'is3D': true, pieHole: 0.03, pieSliceTextStyle: {fontSize: 12,bold:true },");// Piehole using For Create Circle Into Center.
            Sb1.Append(" 'is3D': true, pieSliceTextStyle: {fontSize: 12,bold:true },");
            Sb1.Append("legend: {");
            Sb1.Append("position: 'labeled',");
            Sb1.Append("textStyle: {");
            Sb1.Append("fontSize: 13, bold:true");
            Sb1.Append("}, ");
            Sb1.Append("labeledValueText: 'none'"); // thise line For Remove Percentage From Legend
            Sb1.Append("},");
            Sb1.Append("pieSliceText: 'value',"); // thise line For Show value in Chart
            Sb1.Append("tooltip: {");
            Sb1.Append(" text: 'value'"); // thise line For Remove Percentage From tooltip
            Sb1.Append(" }");
            Sb1.Append("};");
            Sb1.Append("var chart = new google.visualization.PieChart(document.getElementById('piechartOrderBydirect'));");
            Sb1.Append("chart.draw(data, options);");
            Sb1.Append("}");
            Sb1.Append("</script>");
            Sb1.Append("<div id='piechartOrderBydirect' style='width:500px;'></div>");
            sbid1.InnerHtml = Sb1.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void CourtTypeCase2(int Division_ID)
    {
        try
        {
            int CaseCount1 = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByDataSet("Select isNULL((select b.CaseSubject CaseSubject from tbl_LegalMstCaseSubject b " +
"where b.CaseSubjectID=a.CaseSubject_Id),'Subject Not Applied') CaseSubject, " +
"COUNT(distinct UniqueNo) CaseSubjectWiseCount from tblLegalCaseRegistration a " +
"inner join Mst_District DM on DM.District_ID=a.District_ID " +
"inner join tblDivisionMaster DVM on DVM.Division_ID = DM.Division_ID " +
"where a.District_ID in (select District_ID from Mst_District where Division_ID="+Division_ID+") " +
"group by a.CaseSubject_Id");
            StringBuilder Sb1 = new StringBuilder();
            Sb1.Append("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>");
            Sb1.Append("<script type='text/javascript'>");
            Sb1.Append(" google.charts.load(");
            Sb1.Append("'current', { 'packages': ['corechart'] });");
            Sb1.Append("google.charts.setOnLoadCallback(drawChart);");
            Sb1.Append("function drawChart()");
            Sb1.Append("{");
            Sb1.Append("var data = google.visualization.arrayToDataTable([");
            Sb1.Append(" ['CourtN', 'CaseNo.'],");
            for (int i = 0; i < dsCase.Tables[0].Rows.Count; i++)
            {
                CaseCount1 = CaseCount1 + Convert.ToInt32(dsCase.Tables[0].Rows[i]["CaseSubjectWiseCount"]);
                Sb1.Append(" ['" + dsCase.Tables[0].Rows[i]["CaseSubject"].ToString() + "', " + dsCase.Tables[0].Rows[i]["CaseSubjectWiseCount"].ToString() + " ],");
            }
            Sb1.Append("]);");
            Sb1.Append("var options = {");
            Sb1.Append(" 'title':  'COURT WISE CASE No.',");
            Sb1.Append("colors: ['#e6e037', '#008080', '#fcba03'],"); // Using To Apply Chart Colors .
            Sb1.Append("chartArea: {");
            Sb1.Append("height: '100%',");
            Sb1.Append("width: '100%',");
            Sb1.Append("top: 12,");
            Sb1.Append("left: 12,");
            Sb1.Append("right: 12,");
            Sb1.Append("bottom: 12");
            Sb1.Append("},");
            Sb1.Append(" height: 250,");
            //Sb.Append(" 'is3D': true, pieHole: 0.03, pieSliceTextStyle: {fontSize: 12,bold:true },");// Piehole using For Create Circle Into Center.
            Sb1.Append(" 'is3D': true, pieSliceTextStyle: {fontSize: 12,bold:true },");
            Sb1.Append("legend: {");
            Sb1.Append("position: 'labeled',");
            Sb1.Append("textStyle: {");
            Sb1.Append("fontSize: 13, bold:true");
            Sb1.Append("}, ");
            Sb1.Append("labeledValueText: 'none'"); // thise line For Remove Percentage From Legend
            Sb1.Append("},");
            Sb1.Append("pieSliceText: 'value',"); // thise line For Show value in Chart
            Sb1.Append("tooltip: {");
            Sb1.Append(" text: 'value'"); // thise line For Remove Percentage From tooltip
            Sb1.Append(" }");
            Sb1.Append("};");
            Sb1.Append("var chart = new google.visualization.PieChart(document.getElementById('piechartCaseSubjectWiseCount'));");
            Sb1.Append("chart.draw(data, options);");
            Sb1.Append("}");
            Sb1.Append("</script>");
            Sb1.Append("<div id='piechartCaseSubjectWiseCount' style='width:500px;'></div>");
            //sbid2.InnerHtml = Sb1.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void CourtTypeCase3(int Division_ID)
    {
        try
        {
            int CaseCount1 = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByDataSet("Select  case when Compliance_Status = 1 then 'Yes' else 'No' end as IsComplaince, " +
"COUNT(distinct UniqueNo ) OrderBydirectComplaincesStatus from tblLegalCaseRegistration CR " +
"inner join Mst_District DM on DM.District_ID=CR.District_ID " +
"inner join tblDivisionMaster DVM on DVM.Division_ID = DM.Division_ID " +
"where CaseDisposalType_Id='2'  and CR.District_ID in (select District_ID from Mst_District where Division_ID="+Division_ID+") " +
"group by Compliance_Status");
            StringBuilder Sb1 = new StringBuilder();
            Sb1.Append("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>");
            Sb1.Append("<script type='text/javascript'>");
            Sb1.Append(" google.charts.load(");
            Sb1.Append("'current', { 'packages': ['corechart'] });");
            Sb1.Append("google.charts.setOnLoadCallback(drawChart);");
            Sb1.Append("function drawChart()");
            Sb1.Append("{");
            Sb1.Append("var data = google.visualization.arrayToDataTable([");
            Sb1.Append(" ['CourtN', 'CaseNo.'],");
            for (int i = 0; i < dsCase.Tables[0].Rows.Count; i++)
            {
                CaseCount1 = CaseCount1 + Convert.ToInt32(dsCase.Tables[0].Rows[i]["OrderBydirectComplaincesStatus"]);
                Sb1.Append(" ['" + dsCase.Tables[0].Rows[i]["IsComplaince"].ToString() + "', " + dsCase.Tables[0].Rows[i]["OrderBydirectComplaincesStatus"].ToString() + " ],");
            }
            lblCaseCount3.Text = "(CASES " + CaseCount1.ToString() + " No's)";
            Sb1.Append("]);");
            Sb1.Append("var options = {");
            Sb1.Append(" 'title':  'COURT WISE CASE No.',");
            Sb1.Append("colors: ['#35de79', '#d63e27', '#e5e823'],"); // Using To Apply Chart Colors .
            Sb1.Append("chartArea: {");
            Sb1.Append("height: '100%',");
            Sb1.Append("width: '100%',");
            Sb1.Append("top: 12,");
            Sb1.Append("left: 12,");
            Sb1.Append("right: 12,");
            Sb1.Append("bottom: 12");
            Sb1.Append("},");
            Sb1.Append(" height: 250,");
            Sb1.Append(" 'is3D': true, pieSliceTextStyle: {fontSize: 12,bold:true },");
            Sb1.Append("legend: {");
            Sb1.Append("position: 'labeled',");
            Sb1.Append("textStyle: {");
            Sb1.Append("fontSize: 13, bold:true");
            Sb1.Append("}, ");
            Sb1.Append("labeledValueText: 'none'"); // thise line For Remove Percentage From Legend
            Sb1.Append("},");
            Sb1.Append("pieSliceText: 'value',"); // thise line For Show value in Chart
            Sb1.Append("tooltip: {");
            Sb1.Append(" text: 'value'"); // thise line For Remove Percentage From tooltip
            Sb1.Append(" }");
            Sb1.Append("};");
            Sb1.Append("var chart = new google.visualization.PieChart(document.getElementById('piechartIsCompalinceStatus'));");
            Sb1.Append("chart.draw(data, options);");
            Sb1.Append("}");
            Sb1.Append("</script>");
            Sb1.Append("<div id='piechartIsCompalinceStatus' style='width:500px;'></div>");
            sbid3.InnerHtml = Sb1.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void CourtWiseContemptCases(int Division_ID)
    {
        try
        {
            int CaseCountCC = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByProcedure("Sp_DivisionWiseCasesDashboard", new string[] { "Division_ID" }, new string[] { Convert.ToString(Division_ID) }, "dataset");
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
            for (int i = 0; i < dsCase.Tables[2].Rows.Count; i++)
            {
                CaseCountCC = CaseCountCC + Convert.ToInt32(dsCase.Tables[2].Rows[i]["CourtWisePendingContemptCases"]);
                Sb.Append(" ['" + dsCase.Tables[2].Rows[i]["court"].ToString() + "', " + dsCase.Tables[2].Rows[i]["CourtWisePendingContemptCases"].ToString() + " ],");
            }
            lblConcCount.Text = "(" + CaseCountCC.ToString() + " No's)";
            Sb.Append("]);");
            Sb.Append("var options = {");
            Sb.Append(" 'title':  'COURT WISE CASE No.',");
            Sb.Append("colors: ['#fcba03', '#008080',  '#30c9d1'],"); // Using To Apply Chart Colors .
            Sb.Append("chartArea: {");
            Sb.Append("height: '100%',");
            Sb.Append("width: '100%',");
            Sb.Append("top: 12,");
            Sb.Append("left: 12,");
            Sb.Append("right: 12,");
            Sb.Append("bottom: 12");
            Sb.Append("},");
            Sb.Append(" height: 250,");
            Sb.Append(" 'is3D': true, pieSliceTextStyle: {fontSize: 12,bold:true },");
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
            Sb.Append("var chart = new google.visualization.PieChart(document.getElementById('piechartNew1'));");
            Sb.Append("chart.draw(data, options);");
            Sb.Append("}");
            Sb.Append("</script>");
            Sb.Append("<div id='piechartNew1' style='width: 500px;'></div>");
            cwcc.InnerHtml = Sb.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    //protected void UpComingHearing(int Division_ID)
    //{
    //    ds = objdb.ByProcedure("USP_GetOICWise_Upcoming_HearingDate", new string[] { "Division_ID" }, new string[] { Convert.ToString(Division_ID) }, "dataset");
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
    //        // spnHearing.InnerHtml = Marquee;
    //    }
    //}

    protected void BIndWACaseCount(int Division_ID)
    {
        try
        {
            ds = objdb.ByProcedure("Sp_DivisionWiseCasesDashboard", new string[] { "Division_ID" }, new string[] { Convert.ToString(Division_ID) }, "dataset");


            if (ds.Tables.Count >= 1 && ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["HighPriorityCase"].ToString() != "")
                {
                    spnhighpriorityCase.InnerHtml = "&nbsp;" + ds.Tables[1].Rows[0]["HighPriorityCase"].ToString() + " No's";
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
                int OicId = Convert.ToInt16(Session["OICMaster_ID"].ToString());

                ds = objdb.ByProcedure("USP_Select_ContCaseForOICDashboard", new string[] { "OICMaster_Id" },
                  new string[] { Convert.ToString(OicId) }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["dt"] = ds.Tables[0];
                    GrdHighpriorityCase.DataSource = ds;
                    GrdHighpriorityCase.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
                }
                else
                {
                    GrdHighpriorityCase.DataSource = null;
                    GrdHighpriorityCase.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void GrdHighpriorityCase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdHighpriorityCase.PageIndex = e.NewPageIndex;
        DataTable dt = (DataTable)ViewState["dt"];
        GrdHighpriorityCase.DataSource = dt;
        GrdHighpriorityCase.DataBind();

    }



    protected void btnOrderByDirectionPendingCases_Click(object sender, EventArgs e)
    {

    }
}