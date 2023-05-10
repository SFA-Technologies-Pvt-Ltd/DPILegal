using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.OleDb;
using ClosedXML.Excel;
using System.IO;
using System.Text;

public partial class Legal_MasterRpt_ExcelExport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillCaseType();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    #region Fill Casetype
    protected void FillCaseType()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = obj.ByProcedure("USP_Legal_Select_CaseType", new string[] { }
           , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlYear.Items.Clear();
        DataSet dsCase = obj.ByDataSet("with yearlist as (select 2000 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year asc");
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            ddlYear.DataSource = dsCase.Tables[0];
            ddlYear.DataTextField = "year";
            ddlYear.DataValueField = "year";
            ddlYear.DataBind();
        }
        ddlYear.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion
    #region Export Excel
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Role_ID"].ToString() == "1")//Admin
            {
                ds = obj.ByProcedure("USP_GetMasterReportForExcl", new string[] { "Casetype_ID", "CaseYear", "flag" },
                    new string[] { ddlCasetype.SelectedValue, ddlYear.SelectedItem.Text.Trim(), "1" }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "2")// Division
            {
                string Division_ID = Session["Division_Id"].ToString();
                ds = obj.ByProcedure("USP_GetMasterReportForExcl", new string[] { "Casetype_ID", "CaseYear", "Division_ID", "flag" },
                    new string[] { ddlCasetype.SelectedValue, ddlYear.SelectedItem.Text.Trim(), Division_ID, "2" }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "4")// District
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByProcedure("USP_GetMasterReportForExcl", new string[] { "Casetype_ID", "CaseYear", "District_ID", "flag" },
                    new string[] { ddlCasetype.SelectedValue, ddlYear.SelectedItem.Text.Trim(), District_Id, "3" }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "5")// Court
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByProcedure("USP_GetMasterReportForExcl", new string[] { "Casetype_ID", "CaseYear", "District_ID", "flag" },
                    new string[] { ddlCasetype.SelectedValue, ddlYear.SelectedItem.Text.Trim(), District_Id, "4" }, "dataset");
            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = ds.Tables[0];
            }
            if (ViewState["dt"] != null)
            {
                DataTable dtG = (DataTable)ViewState["dt"];
                string fileName = "BonusSheet_Accepted_" + DateTime.Now.ToString() + ".xls";
                //Add Response header
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + "MasterReport" + "_" + DateTime.Now.ToString("dd/MM/yyyyhh_mm_ss") + ".csv");
                System.Type.GetType("System.String");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
              
                int ig = 0;
                try
                {
                    //SqlDataReader dr = command.ExecuteReader();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Sr#"
                        + "\t  CaseNo "
                        + "\t PetitionerName"
                        + "\t PetitionerMobileNo"
                        + "\t Designation"
                        + "\t RepondentName"
                        + "\t RespondentMobileNo"
                        + "\t RespondentDepartment"
                        + "\t RepondentAddress"
                        + "\t NodalOfficerName"
                        + "\t NodalOfficerMobileNo"
                        + "\t petiAdvocateName"
                        + "\t OICNAME"
                        + "\t OICMobile"
                        + "\t CaseSubject"
                        + "\t CaseSubSubject"
                        + "\t PetiAdvocateMobile"
                        + "\t DeptAdvocateName"
                        + "\t DeptAdvocateMobileNO"
                        + "\t NextHearingDate"
                        + "\t HearingDtl"
                        + "\t HighPriorityCase"
                        + "\t CaseStatus"
                        + "\t CaseDisposeType"
                        + "\t CaseDisposeDate"
                        + "\t CaseDisposal_Status"
                        + "\t ImplementDays"
                        + "\t CaseDetail"
                       );
                    Response.Write(sb.ToString() + "\n");
                    Response.Flush();
                    int Sr = 1;
                    foreach (DataRow table in dtG.Rows)
                    {
                        sb = new StringBuilder();
                        ig++;
                        sb.Append(Sr.ToString()
                            + "\t" + (table["CaseNo"].ToString() == "" ? "NA" : table["CaseNo"].ToString())
                       + "\t" + (table["PetitionerName"].ToString() == "" ? "NA" : table["PetitionerName"].ToString())
                       + "\t" + (table["PetitionerMobileNo"].ToString() == "" ? "NA" : table["PetitionerMobileNo"].ToString())
                       + "\t" + (table["Designation"].ToString() == "" ? "NA" : table["Designation"].ToString())
                       + "\t" + (table["RepondentName"].ToString() == "" ? "NA" : table["RepondentName"].ToString())
                       + "\t" + (table["RespondentMobileNo"].ToString() == "" ? "NA" : table["RespondentMobileNo"].ToString())
                       + "\t" + (table["RespondentDepartment"].ToString() == "" ? "NA" : table["RespondentDepartment"].ToString())
                       + "\t" + (table["RepondentAddress"].ToString() == "" ? "NA" : table["RepondentAddress"].ToString())
                       + "\t" + (table["NodalOfficerName"].ToString() == "" ? "NA" : table["NodalOfficerName"].ToString())
                       + "\t" + (table["NodalOfficerMobileNo"].ToString() == "" ? "NA" : table["NodalOfficerMobileNo"].ToString())
                       + "\t" + (table["petiAdvocateName"].ToString() == "" ? "NA" : table["petiAdvocateName"].ToString())

                       + "\t" + (table["OICNAME"].ToString() == "" ? "NA" : table["OICNAME"].ToString())
                       + "\t" + (table["OICMobile"].ToString() == "" ? "NA" : table["OICMobile"].ToString())
                       + "\t" + (table["CaseSubject"].ToString() == "" ? "NA" : table["CaseSubject"].ToString())
                       + "\t" + (table["CaseSubSubject"].ToString() == "" ? "NA" : table["CaseSubSubject"].ToString())
                       + "\t" + (table["PetiAdvocateMobile"].ToString() == "" ? "NA" : table["PetiAdvocateMobile"].ToString())

                       + "\t" + (table["DeptAdvocateName"].ToString() == "" ? "NA" : table["DeptAdvocateName"].ToString())
                       + "\t" + (table["DeptAdvocateMobileNO"].ToString() == "" ? "NA" : table["DeptAdvocateMobileNO"].ToString())
                       + "\t" + (table["NextHearingDate"].ToString() == "" ? "NA" : table["NextHearingDate"].ToString())
                       + "\t" + (table["HearingDtl"].ToString() == "" ? "NA" : table["HearingDtl"].ToString())
                       + "\t" + (table["HighPriorityCase"].ToString() == "" ? "NA" : table["HighPriorityCase"].ToString())
                       + "\t" + (table["CaseStatus"].ToString() == "" ? "NA" : table["CaseStatus"].ToString())
                       + "\t" + (table["CaseDisposeType"].ToString() == "" ? "NA" : table["CaseDisposeType"].ToString())
                       + "\t" + (table["CaseDisposeDate"].ToString() == "" ? "NA" : table["CaseDisposeDate"].ToString())
                       + "\t" + (table["CaseDisposal_Status"].ToString() == "" ? "NA" : table["CaseDisposal_Status"].ToString())
                       + "\t" + (table["ImplementDays"].ToString() == "" ? "NA" : table["ImplementDays"].ToString())
                       + "\t" + (table["CaseDetail"].ToString() == "" ? "NA" : table["CaseDetail"].ToString())
                       );

                        Response.Write(sb.ToString() + "\n");
                        Response.Flush();
                        Sr++;
                    }
                    // }
                    //string a = sb.ToString();
                    // dr.Dispose();
                }
                catch (Exception ex)
                {
                    string aa = ig.ToString();
                    Response.Write(ex.Message);
                }
                finally
                {
                    //command.Connection.Close();
                    //connection.Close();
                }
             
                Response.End();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Record Not Found.')", true);
            }
        }
        catch (Exception ex)
        {
            //ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region ExportExcel Function
    protected void btnAcceptExport_Click(object sender, EventArgs e)
    {
       
    }










    private void ExportExcel(DataSet dsfilewise, string Table_Name)
    {

        try
        {
            lblMsg.Text = "";
            if (dsfilewise != null && dsfilewise.Tables[0].Rows.Count > 0)
            {
                DataTable tbldt = new DataTable();
                tbldt = dsfilewise.Tables[0];
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("PheLegal");
                    var dataTable = dsfilewise.Tables[0];
                    ws.Cell(1, 1).Value = Table_Name;
                    ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    ws.Range(1, 1, 1, tbldt.Columns.Count).Merge().AddToNamed("Titles");
                    var tableWithData = ws.Cell(2, 1).InsertTable(dataTable.AsEnumerable());
                    ws.Columns().AdjustToContents();
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    wb.DefaultShowRowColHeaders.ToString();
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Table_Name + "_" + DateTime.Now.ToString("dd/MM/yyyy hh_mm") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            dsfilewise.Dispose();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
}