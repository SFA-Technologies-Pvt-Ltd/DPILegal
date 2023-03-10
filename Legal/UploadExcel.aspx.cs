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
//using Microsoft.Office.Tools.Excel;
//using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using ClosedXML.Excel;






public partial class Legal_UploadExcel : System.Web.UI.Page
{
    System.Data.DataTable dtCase = null;
    System.Data.DataSet dsCase = null;
    string filepath = "";
    public string constr;
    public SqlConnection con;
    DataSet ds = new DataSet();
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_Id"] != null)
            {
                if (!IsPostBack)
                {
                    //LoadExcel();
                }
            }
            else Response.Redirect("../Login.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    public void connection()
    {
        //Stoting connection string   
        constr = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        con = new SqlConnection(constr);
        con.Open();
    }
    private void LoadExcel()
    {
        if (FUExcel.HasFile)
        {

            string FileName = Path.GetFileName(FUExcel.PostedFile.FileName);
            string fileExtension = Path.GetExtension(FUExcel.FileName).ToLower();
            string connStr = "";
            //getting the path of the file 
            string filelocation = Server.MapPath("~/Legal/UploadPetition/" + FileName);
            //saving the file inside the MyFolder of the server  
            FUExcel.SaveAs(filelocation);

            if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                try
                {
                    if (fileExtension == ".xls" || fileExtension == ".XLS")
                    {
                        ViewState["connStr"] = null;
                        //Console.WriteLine("Jet.OLEDB.4.0");
                        connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        ViewState["connStr"] = connStr;
                    }
                    else if (fileExtension == ".xlsx" || fileExtension == ".XLSX")
                    {
                        ViewState["connStr"] = null;
                        connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                        ViewState["connStr"] = connStr;
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
                    da.Fill(ds);

                    conn.Close();
                    DataTable dt1 = ds.Tables[0];

                    ds.Tables[0].Columns.Add("CourtTypeID", typeof(Int32));
                    ds.Tables[0].Columns.Add("District_ID", typeof(Byte));
                    ds.Tables[0].Columns.Add("UniqueNo", typeof(string));
                    ds.Tables[0].Columns.Add("Casetype_ID", typeof(Int32));
                    DataSet dsc, DsCasetype, Dsbulk = new DataSet();
                    // To Update Courttype_Id into dt1.
                    dsc = obj.ByDataSet("select T.CourtTypeName,T.CourtType_ID,D.District_Name,D.District_ID from tbl_LegalCourtType T inner join Mst_District D on T.District_Id = D.District_ID");
                    DataTable dt2 = dsc.Tables[0];
                    // To Update Casetype_ID into Dt1.
                    DsCasetype = obj.ByDataSet("select Casetype_ID, Casetype_Name from tbl_Legal_Casetype");
                    DataTable dtCtype = DsCasetype.Tables[0];

                    // To Check Datatype Of DataTable Column Below Line.
                    //var type = dt1.Columns["CourtTypeID"].DataType;
                    //var type1 = dt1.Columns["District_ID"].DataType;
                    //var type2 = dt1.Columns["UniqueNo"].DataType;
                    //var qtype = dt1.Columns["CASEYEAR"].DataType;
                    //var etype1 = dt1.Columns["CASETYPE"].DataType;
                    //var ettype2 = dt1.Columns["CASENO"].DataType;
                    //var ettype2 = dt1.Columns["Casetype_ID"].DataType;

                    dt2.AsEnumerable().ToList().ForEach(m => // To Update dt1 From Court_type master.
                    {
                        dt1.AsEnumerable().Where(r => m.Field<String>("CourtTypeName").Trim() == r.Field<String>("COURT").Trim()).ToList().ForEach(s =>
                        {
                            s.SetField<Int32>("CourtTypeID", m.Field<Int32>("CourtType_ID"));
                            s.SetField<Int32>("District_ID", m.Field<Byte>("District_ID"));
                            s.SetField<String>("UniqueNo", Convert.ToString((s.Field<Int32>("CourtTypeID"))) + "_" + Convert.ToString(s.Field<Byte>("District_ID")) + "_" + s.Field<Double>("CASEYEAR").ToString() + "_" + s.Field<String>("CASETYPE") + "_" + s.Field<Double>("CASENO").ToString()); // Here Create Uniqueno.
                        });
                    });

                    dtCtype.AsEnumerable().ToList().ForEach(m => // To Update dt1 From Casetype master.
                    {
                        dt1.AsEnumerable().Where(r => m.Field<String>("Casetype_Name").Trim() == r.Field<String>("CASETYPE").Trim()).ToList().ForEach(s =>
                        {
                            s.SetField<Int32>("Casetype_ID", m.Field<Int32>("Casetype_ID"));
                        });
                    });

                    DataView view = new DataView(dt1); // Here Filter Main Detail From Dt Without Duplicacy.
                    DataTable DtMain = view.ToTable(true, "UniqueNo", "FILINGNO", "CASETYPE", "Casetype_ID", "CASENO", "CASEYEAR", "COURT", "PETITIONER", "CourtTypeID", "District_ID", "FLAG", "STATUS");
                    DataTable dtPetitioner = DtMain.Copy(); // Here Only Petitoner Dtl Filter.
                    dtPetitioner.Columns.Remove("FILINGNO"); dtPetitioner.Columns.Remove("STATUS");
                    dtPetitioner.Columns.Remove("CASETYPE"); dtPetitioner.Columns.Remove("FLAG");
                    dtPetitioner.Columns.Remove("Casetype_ID"); dtPetitioner.Columns.Remove("District_ID");
                    dtPetitioner.Columns.Remove("CASENO"); dtPetitioner.Columns.Remove("CourtTypeID");
                    dtPetitioner.Columns.Remove("CASEYEAR"); dtPetitioner.Columns.Remove("COURT");                  
                    dtPetitioner.AcceptChanges();
                    DtMain.Columns.Remove("PETITIONER");
                    DtMain.AcceptChanges();
                    // To Save Data into Main table.
                    //connection();
                    ////creating object of SqlBulkCopy  
                    //SqlBulkCopy objbulk = new SqlBulkCopy(con);
                    //objbulk.BulkCopyTimeout = 100000;
                    ////assigning Destination table name  
                    //objbulk.DestinationTableName = "tbl_OldCaseDetail_Testing";// "tbl_OldCaseDetail";
                    ////Mapping Table column  
                    //objbulk.ColumnMappings.Add("UniqueNo", "UniqueNo");
                    //objbulk.ColumnMappings.Add("FILINGNO", "FilingNo");
                    //objbulk.ColumnMappings.Add("CASETYPE", "CaseType");
                    //objbulk.ColumnMappings.Add("Casetype_ID", "Casetype_ID");
                    //objbulk.ColumnMappings.Add("CASENO", "CaseNo");
                    //objbulk.ColumnMappings.Add("CASEYEAR", "CaseYear");
                    //objbulk.ColumnMappings.Add("COURT", "Court");
                    //objbulk.ColumnMappings.Add("PETITIONER", "Petitioner");
                    //objbulk.ColumnMappings.Add("RESPONDENT", "Respondent");
                    //objbulk.ColumnMappings.Add("PRNO", "P_R_No");
                    //objbulk.ColumnMappings.Add("ADDRESS", "Address");
                    //objbulk.ColumnMappings.Add("PARTYNAME", "PartyName");
                    //objbulk.ColumnMappings.Add("DEPARTMENT", "Department");
                    //objbulk.ColumnMappings.Add("STATUS", "Status");
                    //objbulk.ColumnMappings.Add("PDF", "PDF");
                    //objbulk.ColumnMappings.Add("Link", "PDFLink");
                    //objbulk.ColumnMappings.Add("FLAG", "Flag");
                    //objbulk.ColumnMappings.Add("District_ID", "CourtLocation_Id");
                    //objbulk.ColumnMappings.Add("CourtTypeID", "CourtTypeID");    
                    ////inserting bulk Records into DataBase   
                    //objbulk.WriteToServer(dt1);
                    DataView view1 = new DataView(dt1);// Here Filter Respondent BY Unique No.
                    DataTable DtRes = view1.ToTable(true, "UniqueNo", "RESPONDENT", "DEPARTMENT", "ADDRESS");
                    DataTable DtDoc = dt1.Copy();
                    DtDoc.Columns.Remove("FILINGNO"); DtDoc.Columns.Remove("CASETYPE"); DtDoc.Columns.Remove("Casetype_ID");
                    DtDoc.Columns.Remove("CASENO"); DtDoc.Columns.Remove("COURT"); DtDoc.Columns.Remove("PETITIONER");
                    DtDoc.Columns.Remove("PRNO"); DtDoc.Columns.Remove("PARTYNAME"); DtDoc.Columns.Remove("DEPARTMENT");
                    DtDoc.Columns.Remove("ADDRESS"); DtDoc.Columns.Remove("STATUS"); DtDoc.Columns.Remove("DISTRICTID");
                    DtDoc.Columns.Remove("CASEYEAR"); DtDoc.Columns.Remove("SRNO"); DtDoc.Columns.Remove("FLAG");
                    DtDoc.Columns.Remove("District_ID"); DtDoc.Columns.Remove("CourtTypeID"); DtDoc.Columns.Remove("RESPONDENT");
                    DtDoc.Columns.Remove("CITY");
                    // Here Reoder Column As per Need.
                    DtDoc.Columns["UniqueNo"].SetOrdinal(0);
                    DtDoc.Columns["PDF"].SetOrdinal(1);
                    DtDoc.Columns["Link"].SetOrdinal(2);
                    DtDoc.AcceptChanges();

                    Dsbulk = obj.ByProcedure("USP_BulkInsert", new string[] { "CreatedBy", "CreatedByIP" },
                        new string[] { "1", "1" }, new string[] { "type_BulkInsertCaseRegistration", "type_BulkInsertPetitioner", "type_BulkInsertRespondentDtl", "type_BulkInsertDocumentDtl" },
                        new DataTable[] { DtMain, dtPetitioner, DtRes, DtDoc }, "dataset");
                    if (Dsbulk != null && Dsbulk.Tables[0].Rows.Count > 0)
                    {
                        if (Dsbulk.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            DtMain.Clear(); dtPetitioner.Clear(); DtRes.Clear(); DtDoc.Clear(); dt1.Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogCls.SendErrorToText(ex);
                }
            }
        }
    }


    //protected DataTable ImportExcel(string FilePath)
    //{
    //    //Open the Excel file using ClosedXML.
    //    using (XLWorkbook workBook = new XLWorkbook(FUExcel.PostedFile.InputStream))
    //    {
    //        //Read the first Sheet from Excel file.
    //        IXLWorksheet workSheet = workBook.Worksheet(1);
    //        //Create a new DataTable.
    //        DataTable dt = new DataTable();
    //        //Loop through the Worksheet rows.
    //        bool firstRow = true;
    //        foreach (IXLRow row in workSheet.Rows())
    //        {
    //            //Use the first row to add columns to DataTable.
    //            if (firstRow)
    //            {
    //                foreach (IXLCell cell in row.Cells())
    //                {
    //                    dt.Columns.Add(cell.Value.ToString());
    //                }
    //                firstRow = false;
    //            }
    //            else
    //            {
    //                //Add rows to DataTable.
    //                dt.Rows.Add();
    //                int i = 0;
    //                foreach (IXLCell cell in row.Cells())
    //                {
    //                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
    //                    i++;
    //                }
    //            }
    //        }
    //        return dt;
    //    }
    //}

    protected void ValidateExcelData()
    {
        //connection();
        try
        {
            DataSet newds = (DataSet)ViewState["ds"];
            if (newds != null)
            {
                if (newds.Tables.Count > 0)
                {
                    if (newds.Tables[0].Rows.Count > 0)
                    {
                        if (newds.Tables[0].Columns.Count > 0)
                        {
                            #region check excel data
                            if (newds.Tables[0].Columns["SRNO"].ToString().Trim() == "SRNO")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[0-9]+$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["SRNO"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "SRNO Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["FILINGNO"].ToString().Trim() == "FILINGNO")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[A-Za-z]+[/]+[0-9]+[/]+[0-9]+[-]+[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["FILINGNO"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "FILINGNO Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["CASETYPE"].ToString().Trim() == "CASETYPE")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["CASETYPE"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "CASETYPE Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["CASENO"].ToString().Trim() == "CASENO")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[0-9]+$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["CASENO"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "CASENO Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["CASEYEAR"].ToString().Trim() == "CASEYEAR")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[0-9]+$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["CASEYEAR"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "CASEYEAR Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }

                            if (newds.Tables[0].Columns["COURT"].ToString().Trim() == "COURT")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["COURT"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "COURT Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["CITY"].ToString().Trim() == "CITY")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["CITY"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "CITY Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["PETITIONER"].ToString().Trim() == "PETITIONER")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["PETITIONER"].ToString();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "PETITIONER Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["RESPONDENT"].ToString().Trim() == "RESPONDENT")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["RESPONDENT"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "RESPONDENT Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["PRNO"].ToString().Trim() == "PRNO")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["PRNO"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "PRNO Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["PARTYNAME"].ToString().Trim() == "PARTYNAME")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["PARTYNAME"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "PARTYNAME Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["ADDRESS"].ToString().Trim() == "ADDRESS")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["ADDRESS"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "ADDRESS Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["DEPARTMENT"].ToString().Trim() == "DEPARTMENT")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["DEPARTMENT"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "DEPARTMENT Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["STATUS"].ToString().Trim() == "STATUS")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["STATUS"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "STATUS Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["PDF"].ToString().Trim() == "PDF")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[0-9a-zA-Z\s.-]+$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["PDF"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "PDF Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["Link"].ToString().Trim() == "Link")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^(http(s):\/\/.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["Link"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "Data Check Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "Link Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["DISTRICTID"].ToString().Trim() == "DISTRICTID")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[0-9]+$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["DISTRICTID"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "DISTRICTID Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            if (newds.Tables[0].Columns["FLAG"].ToString().Trim() == "FLAG")
                            {
                                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                                {
                                    string pattern = @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$";
                                    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    string input = newds.Tables[0].Rows[i]["FLAG"].ToString().Trim();
                                    bool isMatch = regex.IsMatch(input);
                                    if (isMatch)
                                    {
                                        if (i == newds.Tables[0].Rows.Count - 1)
                                        {
                                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "thankyou", "DataCheck Success!");
                                        }
                                    }
                                    else
                                    {
                                        i += 1;
                                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Alert", "FLAG Datatype mismatch on Row no.: " + i);
                                        break;
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        try
        {
            LoadExcel();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }


}