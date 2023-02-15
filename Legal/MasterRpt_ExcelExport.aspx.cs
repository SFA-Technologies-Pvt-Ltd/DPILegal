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
            Response.Redirect("../Login.aspx");
        }
    }

    #region Fill Casetype
    protected void FillCaseType()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = obj.ByProcedure("USP_Legal_Select_CaseType", new string[] {  }
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
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlYear.Items.Clear();
        for (int i = 2016; i <= 2023; i++)
        {
            ddlYear.Items.Add(i.ToString());
        }
        ddlYear.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion
    #region Export Excel
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ds = obj.ByProcedure("USP_GetMasterReportForExcl", new string[] { "Casetype_ID", "CaseYear" },
                new string[] { ddlCasetype.SelectedValue, ddlYear.SelectedItem.Text.Trim() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ExportExcel(ds, ddlYear.SelectedItem.Text.Trim() + "_" + ddlCasetype.SelectedItem.Text.Trim());
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion
    #region ExportExcel Function
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
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", "Error 7: " + ex.Message.ToString());
        }
       
    }
    #endregion
}