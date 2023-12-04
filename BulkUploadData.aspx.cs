using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BulkUploadData : System.Web.UI.Page
{
    DataSet dsDPICase = null;
    System.Data.DataTable dtCase = null;
    System.Data.DataSet dsCase = null;
    //AbstApiDBApi objdb = new APIProcedure();
    //CultureInfo cult = new CultureInfo("gu-IN");
    string filepath = "";
    public string constr;
    public SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadExcel("phe-2018-23-closedCC_new2.xls");  //"phe-cc-2000-2023_New_17_Jan_2023.xls"
        }
    }

    protected void btnbulkSave_Click(object sender, EventArgs e)
    {
        //filepath = FU1.FileName;
        
        System.Data.DataTable dtcase = (System.Data.DataTable)ViewState["dt"];
        connection();
        //creating object of SqlBulkCopy  
        SqlBulkCopy objbulk = new SqlBulkCopy(con);
        objbulk.BulkCopyTimeout = 100000;
        //assigning Destination table name  
        objbulk.DestinationTableName = "tbl_OrderByDirectionPendingCase";// "tbl_OldCaseDetail";
        //Mapping Table column  
        objbulk.ColumnMappings.Add("ID", "UniqueNo");
        objbulk.ColumnMappings.Add("Filing No", "FilingNo");
        objbulk.ColumnMappings.Add("Case Type", "CaseType");
        objbulk.ColumnMappings.Add("Case No", "CaseNo");
        objbulk.ColumnMappings.Add("Case Year", "CaseYear");
        objbulk.ColumnMappings.Add("Court", "Court");
        objbulk.ColumnMappings.Add("Petitioner", "Petitioner");
        objbulk.ColumnMappings.Add("Respondent", "Respondent");
        objbulk.ColumnMappings.Add("P/R-No", "P_R_No");
        objbulk.ColumnMappings.Add("Party Name", "PartyName");
        objbulk.ColumnMappings.Add("Address", "Address");
        objbulk.ColumnMappings.Add("Status", "Status");
        //objbulk.ColumnMappings.Add("PDF", "PDF");
        //objbulk.ColumnMappings.Add("Link", "PDFLink");

        //inserting bulk Records into DataBase   
        objbulk.WriteToServer(dtcase);
    }
    public void connection()
    {
        //Stoting connection string   
        constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        con = new SqlConnection(constr);
        con.Open();
    }
    private void LoadExcel(string filename)
    {
        string connStr = "";
        int oItem = 0;

        string fileExtension = Path.GetExtension(filename);
        string filelocation = Server.MapPath("~/Legal/ExcelFile/" + filename); // @"D:\Bhanu\Legal\DPILegal\Legal\ExcelFile\" + filename;

        if (fileExtension == ".xls" || fileExtension == ".XLS")
        {
            connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        }
        else if (fileExtension == ".xlsx" || fileExtension == ".XLSX")
        {
            connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
        }
        System.Data.DataTable dt = new System.Data.DataTable();
        OleDbConnection conn = new OleDbConnection(connStr);
        OleDbCommand cmd = new OleDbCommand();
        cmd.Connection = conn;
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        conn.Open();
        System.Data.DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string sheetName = dtSheet.Rows[0]["table_name"].ToString();
        cmd.CommandText = "select * from [" + sheetName + "]";
        da.SelectCommand = cmd;
        da.Fill(dt);
        conn.Close();
       // dsCase = new DataSet();
      //  dsCase.Tables.Add(dt);
        ViewState["dt"] = dt;
        // DataSet ds = new DataSet();
        // ds.Tables.Add(dt);
       // oItem = dt.Rows.Count;
       // oItem -= 1;
    }
}