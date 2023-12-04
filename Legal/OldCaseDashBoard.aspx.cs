using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


public partial class mis_Legal_OldCaseDashBoard : System.Web.UI.Page
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
                    //UpComingHearing();
                    CourtTypeCase();
                    CourtTypeCase1();
                    CourtTypeCase2();
                    CourtTypeCase3();
                   // BindCaseTypeCount();
                    CourtWiseContemptCases();
                  
                   
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
    // thise is not Usable Function.
    protected void BindCaseTypeCount()
    {
        DataSet dsCasecount = new DataSet();
        dsCasecount = objdb.ByProcedure("Sp_OldCasesDashboard", new string[] { }, new string[] { }, "dataset");
        string str = "";

        str += "<table border='1' style='text-align:center;color:darkcyan;font-size:18px;height:573px;width:100%;'><tr style='background-color: #fff;'><td style='font-weight:bold;color: black;width: 87px;'>Case Type</td><td style='font-weight:bold;color: black;word-wrap: break-word'>Pending Cases Since 2000</td></tr>";
        int tCount = 0;
        int tCount1 = 0;
        DataSet dsCasecount2 = new DataSet();
        dsCasecount2 = objdb.ByProcedure("Sp_OrderByDirectionPendingCasesDashboard", new string[] { }, new string[] { }, "dataset");

        for (int i = 0; i < dsCasecount.Tables[1].Rows.Count; i++)
        {
            // tCount += Convert.ToInt32(dsCasecount.Tables[1].Rows[i]["CaseTypeWisePendingCases"]);
            // str += "<tr><td style='font-weight:bold;'>" + dsCasecount.Tables[1].Rows[i]["CaseType"].ToString() + "</td><td style='font-size: 22px;'><a href=\"Pending_Case_Since_2000.aspx?CaseType=" + dsCasecount.Tables[1].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount.Tables[1].Rows[i]["CaseTypeWisePendingCases"].ToString() + "</a></td>";
            // str += "</tr> ";
			tCount += Convert.ToInt32(dsCasecount.Tables[1].Rows[i]["CaseTypeWisePendingCases"]);
            if (!string.IsNullOrEmpty(dsCasecount.Tables[1].Rows[i]["CaseType"].ToString()))
            {
                str += "<tr><td style='font-weight:bold;'>" + dsCasecount.Tables[1].Rows[i]["CaseType"].ToString() + "</td><td style='font-size: 22px;'><a href=\"Pending_Case_Since_2000.aspx?CaseType=" + dsCasecount.Tables[1].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount.Tables[1].Rows[i]["CaseTypeWisePendingCases"].ToString() + "</a></td>";
                str += "</tr> ";
            }
        }
        str += "</table>";
        lblCasetypeCountno.Text = "(PENDENCY : " + tCount.ToString() + " No's)";
        CasetypeCountID1.InnerHtml = str;
        string str2 = "";
        str2 += "<table border='1' style='text-align:center;height:573px;color:darkcyan;font-size:18px;width:100%;'><tr style='background-color: #fff;width:100%;'><td rowspan='2' style='font-weight:bold;color: black;width: 87px;'>Case Type</td><td rowspan='2' style='font-weight:bold;color: black;word-wrap: break-word'>Close Cases Since 2018</td><td rowspan='2' style='font-weight:bold;color: black;'>Order By Direction</td><td colspan='7' style='font-weight:bold;color: black;'>Complainces Status</td></tr><tr style='background-color: #fff;'><td colspan='3' style='font-weight:bold;color: black;'>Yes</td><td colspan='3' style='font-weight:bold;color: black;'>No</td><td colspan='3' style='font-weight:bold;color: black;'>Pending</td></tr>";
        for (int i = 0; i < dsCasecount2.Tables[0].Rows.Count; i++)
        {

            tCount1 += Convert.ToInt32(dsCasecount2.Tables[0].Rows[i]["IsOrderByDirectionCount"]);
            str2 += "<tr><td style='font-weight:bold;'>" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "</td><td style='font-size: 22px;'><a href=\"Order_By_Direction_Pending_Cases.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["DisposeOfCaseSince2018"].ToString() + "</a></td>";
           // if (Convert.ToInt32(dsCasecount2.Tables[0].Rows[i]["IsOrderByDirectionCount"]) > 0)
                str2 += "<td style='width: 52px;'><a href=\"Order_By_Direction_Count_Cases.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["IsOrderByDirectionCount"].ToString() + "</a></td>";
          //  else
               // str2 += "<td style='width: 52px;'>" + dsCasecount2.Tables[0].Rows[i]["IsOrderByDirectionCount"].ToString() + "</td>";

         //   if (Convert.ToInt32(dsCasecount2.Tables[0].Rows[i]["YesCount"]) > 0)
                str2 += "<td colspan='3'><a href=\"Order_By_Direction_Count_IsComplainceYes.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["YesCount"].ToString() + "</a></td>";
          //  else
           //     str2 += "<td  style='width: 52px;'>" + dsCasecount2.Tables[0].Rows[i]["YesCount"].ToString() + "</td>";

         //   if (Convert.ToInt32(dsCasecount2.Tables[0].Rows[i]["NOCounts"]) > 0)
                str2 += "<td colspan='3'><a href=\"Order_By_Direction_Count_IsComplainceNo.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["NOCounts"].ToString() + "</a></td>";
          //  else
          //      str2 += "<td  style='width: 52px;'>" + dsCasecount2.Tables[0].Rows[i]["NOCounts"].ToString() + "</td>";
          //  if (Convert.ToInt32(dsCasecount2.Tables[0].Rows[i]["PendingCounts"]) > 0)
                str2 += "<td colspan='3'><a href=\"Order_By_Direction_Count_IsComplaincePending.aspx?CaseType=" + dsCasecount2.Tables[0].Rows[i]["CaseType"].ToString() + "\" target='_blank'>" + dsCasecount2.Tables[0].Rows[i]["PendingCounts"].ToString() + "</a></td>";
          //  else
          //      str2 += "<td>" + dsCasecount2.Tables[0].Rows[i]["PendingCounts"].ToString() + "</td>";

            str2 += "</tr> ";
        }
        str2 += " </table>";
        CasetypeCountID2.InnerHtml =  str2;
        lblOrderByDirectionalCases.Text = "(CASES :" + tCount1.ToString() + " No's)";
        #region
        //try
        //{
        //    DataSet dsCasecount = new DataSet();
        //    dsCasecount = objdb.ByProcedure("Sp_OldCasesDashboard", new string[] { }, new string[] { }, "dataset");
        //    StringBuilder SbCount = new StringBuilder();
        //    SbCount.Append("<script type='text/javascript' src='https://www.gstatic.com/charts/loader.js'></script>");
        //    SbCount.Append("<script type='text/javascript'>");
        //    SbCount.Append(" google.charts.load(");
        //    SbCount.Append("'current', { 'packages': ['corechart'] });");
        //    SbCount.Append("google.charts.setOnLoadCallback(drawChart);");
        //    SbCount.Append("function drawChart()");
        //    SbCount.Append("{");
        //    SbCount.Append("var data = google.visualization.arrayToDataTable([");
        //    SbCount.Append(" ['Court', 'Case No.'],");
        //    for (int i = 0; i < dsCasecount.Tables[1].Rows.Count; i++)
        //    {
        //        SbCount.Append(" ['" + dsCasecount.Tables[1].Rows[i]["CaseType"].ToString() + "', " + dsCasecount.Tables[1].Rows[i]["CaseTypeWisePendingCases"].ToString() + " ],");
        //    }
        //    SbCount.Append("]);");
        //    SbCount.Append("var options = {");
        //    SbCount.Append(" 'title':  'CASE TYPE CASE COUNT.',");
        //    SbCount.Append("colors: ['#4BB160', '#104C9C', '#EC5D92', '#f3b49f','#1fa6ad','#e74c4c','#323a32','#e5e5e5','#ffffff'],");// thise is tempreory coloer shown in chart.
        //    //SbCount.Append("backgroundColor: 'transparent',"); // to remove &change backcolor.
        //    SbCount.Append("chartArea: {");
        //    SbCount.Append("height: '100%',");
        //    SbCount.Append("width: '100%',");
        //    SbCount.Append("top: 12,");
        //    SbCount.Append("left: 12,");
        //    SbCount.Append("right: 12,");
        //    SbCount.Append("bottom: 12");
        //    SbCount.Append("},");
        //    SbCount.Append(" height: 250,");
        //  //  SbCount.Append(" 'is3D': true,pieHole: 0.03,pieSliceTextStyle: {bold:true,fontSize: 12}, "); // Piehole using For Create Circle Into Center.
        //    SbCount.Append(" 'is3D': true,pieSliceTextStyle: {bold:true,fontSize: 12}, ");
        //    SbCount.Append("legend: {");
        //    SbCount.Append("position: 'labeled',");
        //    SbCount.Append("   textStyle: {");
        //    SbCount.Append("fontSize: 13, bold:true");
        //    SbCount.Append("},");
        //    SbCount.Append("labeledValueText: 'none'"); // thise line For Remove Percentage From Legend
        //    SbCount.Append("},");
        //    SbCount.Append("pieSliceText: 'value',"); // thise line For Show value in Chart
        //    SbCount.Append("tooltip: {");
        //    SbCount.Append(" text: 'value'"); // thise line For Remove Percentage From tooltip
        //    SbCount.Append(" }");
        //    SbCount.Append("};");
        //    SbCount.Append("var chart = new google.visualization.BarChart(document.getElementById('piechart'));");
        //    SbCount.Append("chart.draw(data, options);");
        //    SbCount.Append("}");
        //    SbCount.Append("</script>");
        //    SbCount.Append("<div id='piechart' style='width: 500px;'></div>");
        //    CasetypeCountID.InnerHtml = SbCount.ToString();
        //    if (dsCasecount != null && dsCasecount.Tables[1].Rows[0]["CaseTypeWisePendingCases"].ToString() != "")
        //    {
        //        CasetypeCountno.InnerHtml = dsCasecount.Tables[1].Rows[0]["CaseTypeWisePendingCases"].ToString();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        //}
        #endregion
    }

    // CourtWise Case Count.
    protected void CourtTypeCase()
    {
        try
        {
            int CaseCount = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByProcedure("Sp_OldCasesDashboard", new string[] { }, new string[] { }, "dataset");
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
            Sb.Append("colors: ['#008080', '#fcba03', '#30c9d1'],"); // Using To Apply Chart Colors .
            Sb.Append("chartArea: {");
            Sb.Append("height: '100%',");
            Sb.Append("width: '100%',");
            Sb.Append("top: 12,");
            Sb.Append("left: 12,");
            Sb.Append("right:12,");
            Sb.Append("bottom: 12");
            Sb.Append("},");
            Sb.Append(" height: 250,");
            //Sb.Append(" 'is3D': true, pieHole: 0.03, pieSliceTextStyle: {fontSize: 12,bold:true },");// Piehole using For Create Circle Into Center.
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

    protected void CourtTypeCase1()
    {
        try
        {
            int CaseCount1 = 0;
            DataSet dsCase = new DataSet();
           // dsCase = objdb.ByDataSet("Select Court,COUNT(distinct UniqueNo) OrderBydirectCourtWiseCount from tbl_OrderByDirectionPendingCase where IsOrderByDirection='Yes' group by Court");
            dsCase = objdb.ByProcedure("USP_GetOrderByDirCount", new string[] { }, new string[] { }, "dataset");
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
    // NOt Usable.
    protected void CourtTypeCase2()
    {
        try
        {
            int CaseCount1 = 0;
            DataSet dsCase = new DataSet();
            //dsCase = objdb.ByDataSet("Select isNULL((select b.CaseSubject CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectID=a.CaseSubjectId),'Subject Not Applied') CaseSubject," +
            //    "COUNT(distinct UniqueNo) CaseSubjectWiseCount from tbl_OldCaseDetail a  group by a.CaseSubjectId");
            dsCase = objdb.ByProcedure("USP_GetSubjectwiseCount", new string[] { }, new string[] { }, "dataset");
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
            lblCaseCount2.Text = "(PENDING CASES " + CaseCount1.ToString() + " No's)";
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
            sbid2.InnerHtml = Sb1.ToString();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    //ORDER BY DIRECTION COMPLAINES
    protected void CourtTypeCase3()
    {
        try
        {
            int CaseCount1 = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByDataSet("Select case when Compliance_Status  = 1 then 'Yes' when Compliance_Status =2 then 'No' when Compliance_Status = 3 then 'Pending' else Compliance_Status end as IsComplaince,COUNT(distinct UniqueNo ) OrderBydirectComplaincesStatus from tblLegalCaseRegistration where CaseDisposalType_Id='2'   group by Compliance_Status");
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
    //COURT WISE CONTEMPT CASES
    protected void CourtWiseContemptCases()
    {
        try
        {
            int CaseCountCC = 0;
            DataSet dsCase = new DataSet();
            dsCase = objdb.ByProcedure("Sp_OldCasesDashboard", new string[] { }, new string[] { }, "dataset");
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
            Sb.Append("colors: ['#30c9d1', '#008080', '#fcba03'],"); // Using To Apply Chart Colors .
            Sb.Append("chartArea: {");
            Sb.Append("height: '100%',");
            Sb.Append("width: '100%',");
            Sb.Append("top: 12,");
            Sb.Append("left: 12,");
            Sb.Append("right: 12,");
            Sb.Append("bottom: 12");
            Sb.Append("},");
            Sb.Append(" height: 250,");
            //Sb.Append(" 'is3D': true, pieHole: 0.03, pieSliceTextStyle: {fontSize: 12,bold:true },");// Piehole using For Create Circle Into Center.
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
            // spnHearing.InnerHtml = Marquee;
        }
    }

    protected void BIndWACaseCount()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_OldCasesDashboard", new string[] { }, new string[] { }, "dataset");

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
                // thise is old Query
                //ds = objdb.ByDataSet("select distinct UniqueNo, FilingNo, Court, Petitioner, Respondent, HearingDate,(Select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectID = a.CaseSubjectId) CaseSubject,(select OICName from tblOICMaster c where c.OICMaster_ID = a.OICId) OICName from tbl_OldCaseDetail a where a.CaseType = 'CONC'");
                //This is New Query
                Response.Redirect("HighPriCase_ForOldDashb.aspx",false);
				// on date 24/05/23 Comment by bhanu due to unused lines.
                //ds = objdb.ByProcedure("USP_GetHighPriCase_ForOldDashb", new string[]{}, new string[]{},"dataset");
                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    ViewState["dt"] = ds.Tables[0];
                //    GrdHighpriorityCase.DataSource = ds;
                //    GrdHighpriorityCase.DataBind();
                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
                //}
                //else
                //{
                //    GrdHighpriorityCase.DataSource = null;
                //    GrdHighpriorityCase.DataBind();
                //}
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

    protected void btnCourtWise_Click(object sender, EventArgs e)
    {
        try
        { 
            Response.Redirect("OrderByDirectionCase_ForOldDashb.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void btnComplainces_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("OrderByDirectionComplainesCase_ForOldDashb.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}