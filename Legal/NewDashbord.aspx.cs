using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Legal_NewDashbord : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] !=null)
        {
            if (!IsPostBack)
            {
                FillGraph2();
            }
        }
        else { Response.Redirect("~/Login.aspx"); }
    }

    protected void FillGraph2()

    {
        StringBuilder HTML = new StringBuilder();

        DataSet dsCase = new DataSet();
        //dsCase = obj.ByProcedure("Sp_OldCasesDashboard", new string[] { }, new string[] { }, "dataset");
        DataSet dsCourt = obj.ByProcedure("USP_GetNewDashBoardData", new string[] { "flag" }, new string[] { "1" }, "dataset");

        HTML.Append("<div id='chartdiv' ></div>");
        HTML.Append("<script>");
        HTML.Append("var root = am5.Root.new(\"chartdiv\");");
        HTML.Append("root.setThemes([");
        HTML.Append("am5themes_Animated.new(root)");
        HTML.Append("]);");
        HTML.Append("var container = root.container.children.push(");
        HTML.Append("am5.Container.new(root, {");
        HTML.Append("width: am5.percent(100),");
        HTML.Append("height: am5.percent(100),");
        HTML.Append("layout: root.verticalLayout");
        HTML.Append("})");
        HTML.Append(");");
        HTML.Append("var series = container.children.push(");
        HTML.Append("am5hierarchy.Sunburst.new(root, {");
        HTML.Append("downDepth: 1,");
        HTML.Append("initialDepth: 1,");
        HTML.Append("valueField: \"value\",");
        HTML.Append("categoryField: \"name\",");
        HTML.Append("childDataField: \"children\"");
        HTML.Append("})");
        HTML.Append(");");
        HTML.Append("series.data.setAll([");
        HTML.Append("{name: \"Total Case\",");
        HTML.Append("children: [");


        for (int i = 0; i < dsCourt.Tables[0].Rows.Count; i++)
        {
            HTML.Append("{name: '" + dsCourt.Tables[0].Rows[i]["court"].ToString() + "',");
            HTML.Append("children: [");

            DataSet dsCaseType = obj.ByProcedure("USP_GetNewDashBoardData", new string[] { "flag", "CourtType_Id" }, new string[] { "2", Convert.ToString(dsCourt.Tables[0].Rows[i]["CourtType_ID"]) }, "dataset");

            for (int c = 0; c < dsCaseType.Tables[0].Rows.Count; c++)
            {
                HTML.Append("{name: '" + dsCaseType.Tables[0].Rows[c]["Casetype_Name"].ToString() + "',");
                HTML.Append(" children: [");

                DataSet dsCaseStatus = obj.ByProcedure("USP_GetNewDashBoardData", new string[] { "flag", "Casetype_ID" }, new string[] { "3", Convert.ToString(dsCaseType.Tables[0].Rows[c]["Casetype_ID"]) }, "dataset");

                for (int b = 0; b < dsCaseStatus.Tables[0].Rows.Count; b++)
                {
                    HTML.Append("{name: '" + dsCaseStatus.Tables[0].Rows[b]["CaseStatus"].ToString() + "',");
                    HTML.Append(" children: [");

                    DataSet dsYear = obj.ByProcedure("USP_GetNewDashBoardData", new string[] { "flag", "CourtType_Id", "Casetype_ID", "CaseStatus" }, new string[] { "4", Convert.ToString(dsCourt.Tables[0].Rows[i]["CourtType_ID"]), Convert.ToString(dsCaseType.Tables[0].Rows[c]["Casetype_ID"]), Convert.ToString(dsCaseStatus.Tables[0].Rows[b]["CaseStatus"]) }, "dataset");
                    for (int r = 0; r < dsYear.Tables[0].Rows.Count; r++)
                    {
                        HTML.Append(" {name: '" + dsYear.Tables[0].Rows[r]["CaseYear"].ToString() + "',");
                        // for oic
                        DataSet dsoic = obj.ByProcedure("USP_GetNewDashBoardData", new string[] { "flag", "CourtType_Id", "Casetype_ID", "CaseStatus", "CaseYear" }, new string[] { "5", Convert.ToString(dsCourt.Tables[0].Rows[i]["CourtType_ID"]), Convert.ToString(dsCaseType.Tables[0].Rows[c]["Casetype_ID"]), Convert.ToString(dsCaseStatus.Tables[0].Rows[b]["CaseStatus"]), Convert.ToString(dsYear.Tables[0].Rows[r]["CaseYear"].ToString()) }, "dataset");
                        if (dsoic.Tables[0].Rows.Count > 0)
                        {
                            HTML.Append(" children: [");
                            for (int q = 0; q < dsoic.Tables[0].Rows.Count; q++)
                            {
                                HTML.Append(" {name: '" + dsoic.Tables[0].Rows[q]["OICName"].ToString() + "',");
                                DataSet dsdate = obj.ByProcedure("USP_GetNewDashBoardData", new string[] { "flag", "CourtType_Id", "Casetype_ID", "CaseStatus", "CaseYear", "OICMaster_ID" }, new string[] { "6", Convert.ToString(dsCourt.Tables[0].Rows[i]["CourtType_ID"]), Convert.ToString(dsCaseType.Tables[0].Rows[c]["Casetype_ID"]), Convert.ToString(dsCaseStatus.Tables[0].Rows[b]["CaseStatus"]), Convert.ToString(dsYear.Tables[0].Rows[r]["CaseYear"].ToString()),Convert.ToString(dsoic.Tables[0].Rows[q]["OICMaster_ID"].ToString()) }, "dataset");

                                HTML.Append(" children: [");
                                for (int p = 0; p < dsdate.Tables[0].Rows.Count; p++)
                                {
                                    HTML.Append(" {name: '" + dsdate.Tables[0].Rows[p]["NextHearingDate"].ToString() + "',");
                                    HTML.Append("value:  " + dsdate.Tables[0].Rows[p]["CaseCount"].ToString() + "},");
                                }
                                HTML.Append("]},");
                            }
                            HTML.Append("]},");
                        }
                        else
                        {
                            HTML.Append("value:  " + dsYear.Tables[0].Rows[r]["CaseCount"].ToString() + "},");
                        }
                    }

                    HTML.Append("]},");
                }

                HTML.Append("]},");
            }
            HTML.Append("]},");
        }

        HTML.Append("]}]);");
        HTML.Append("series.set(\"selectedDataItem\", series.dataItems[0]);");
        HTML.Append("container.children.unshift(");
        HTML.Append("am5hierarchy.BreadcrumbBar.new(root, {");
        HTML.Append("series: series");
        HTML.Append("})");
        HTML.Append(");");
        HTML.Append("</script>");

        chart.InnerHtml = HTML.ToString();


    }


}
