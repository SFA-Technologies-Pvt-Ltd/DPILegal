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
                    //ViewState["Office_Id"] =  Session["Office_Id"];
                    //ViewState["Emp_Id"] =  Session["Emp_Id"];
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
                    ValidateExcelData(ds);
                    ds.Tables[0].Columns.Add("CourtTypeID", typeof(Int32));
                    ds.Tables[0].Columns.Add("CourtLocation_ID", typeof(Byte));
                    ds.Tables[0].Columns.Add("UniqueNo", typeof(string));
                    ds.Tables[0].Columns.Add("Casetype_ID", typeof(Int32));
                    DataSet dsc, DsCasetype, Dsbulk = new DataSet();
                    // To Update Courttype_Id into dt1.
                    dsc = obj.ByDataSet("select T.CourtTypeName,T.CourtType_ID,D.District_Name,D.District_ID from tbl_LegalCourtType T inner join Mst_District D on T.District_ID = D.District_ID");
                    DataTable dt2 = dsc.Tables[0];
                    // To Update Casetype_ID into Dt1.
                    DsCasetype = obj.ByDataSet("select Casetype_ID, Casetype_Name from tbl_Legal_Casetype");
                    DataTable dtCtype = DsCasetype.Tables[0];

                    // To Check Datatype Of DataTable Column Below Line.
                    //var type = dt1.Columns["CourtTypeID"].DataType;
                    //var type1 = dt1.Columns["CourtLocation_ID"].DataType;
                    //var type2 = dt1.Columns["UniqueNo"].DataType;
                    //var qtype = dt1.Columns["CaseYear"].DataType;
                    //var etype1 = dt1.Columns["Casetype"].DataType;
                    //var ettype2 = dt1.Columns["Caseno"].DataType;
                    //var ettype2 = dt1.Columns["Casetype_ID"].DataType;

                    dt2.AsEnumerable().ToList().ForEach(m => // To Update dt1 From Court_type master.
                    {
                        dt1.AsEnumerable().Where(r => m.Field<String>("CourtTypeName").Trim() == r.Field<String>("Court").Trim()).ToList().ForEach(s =>
                        {
                            s.SetField<Int32>("CourtTypeID", m.Field<Int32>("CourtType_ID"));
                            s.SetField<Int32>("CourtLocation_ID", m.Field<Byte>("District_ID"));
                            s.SetField<String>("UniqueNo", Convert.ToString((s.Field<Int32>("CourtTypeID"))) + "_" + Convert.ToString(s.Field<Byte>("CourtLocation_ID")) + "_" + s.Field<Double>("CaseYear").ToString() + "_" + s.Field<String>("Casetype") + "_" + s.Field<Double>("Caseno").ToString()); // Here Create Uniqueno.
                        });
                    });

                    dtCtype.AsEnumerable().ToList().ForEach(m => // To Update dt1 From Casetype master.
                    {
                        dt1.AsEnumerable().Where(r => m.Field<String>("Casetype_Name").Trim() == r.Field<String>("Casetype").Trim()).ToList().ForEach(s =>
                        {
                            s.SetField<Int32>("Casetype_ID", m.Field<Int32>("Casetype_ID"));
                        });
                    });

                    DataView view = new DataView(dt1); // Here Filter Main Detail From Dt Without Duplicacy.
                    DataTable DtMain = view.ToTable(true, "UniqueNo", "FillingNo", "Casetype", "Casetype_ID", "Caseno", "CaseYear", "Court", "Petitioner", "CourtTypeID", "CourtLocation_ID", "Flag", "Status");
                    DataTable dtPetitioner = DtMain.Copy(); // Here Only Petitoner Dtl Filter.
                    dtPetitioner.Columns.Remove("FillingNo"); dtPetitioner.Columns.Remove("Status");
                    dtPetitioner.Columns.Remove("Casetype"); dtPetitioner.Columns.Remove("Flag");
                    dtPetitioner.Columns.Remove("Casetype_ID"); dtPetitioner.Columns.Remove("CourtLocation_ID");
                    dtPetitioner.Columns.Remove("Caseno"); dtPetitioner.Columns.Remove("CourtTypeID");
                    dtPetitioner.Columns.Remove("CaseYear"); dtPetitioner.Columns.Remove("Court");
                    dtPetitioner.AcceptChanges();
                    DtMain.Columns.Remove("Petitioner");
                    DtMain.AcceptChanges();

                    DataView view1 = new DataView(dt1);// Here Filter Respondent BY Unique No.
                    DataTable DtRes = view1.ToTable(true, "UniqueNo", "Respondent", "Department", "Address");
                    DataTable DtDoc = dt1.Copy();
                    DtDoc.Columns.Remove("FillingNo"); DtDoc.Columns.Remove("Casetype"); DtDoc.Columns.Remove("Casetype_ID");
                    DtDoc.Columns.Remove("Caseno"); DtDoc.Columns.Remove("Court"); DtDoc.Columns.Remove("Petitioner");
                    DtDoc.Columns.Remove("PartyName"); DtDoc.Columns.Remove("Department"); DtDoc.Columns.Remove("CaseYear");
                    DtDoc.Columns.Remove("Address"); DtDoc.Columns.Remove("Status"); DtDoc.Columns.Remove("CourtLocation_ID");
                    DtDoc.Columns.Remove("srno"); DtDoc.Columns.Remove("Flag"); DtDoc.Columns.Remove("CourtTypeID");
                    DtDoc.Columns.Remove("Respondent");
                    // Here Reoder Column As per Need.
                    DtDoc.Columns["UniqueNo"].SetOrdinal(0);
                    DtDoc.Columns["DocName"].SetOrdinal(1);
                    DtDoc.Columns["PDFLink"].SetOrdinal(2);
                    DtDoc.AcceptChanges();

                    string ErrorColumnName = "";
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ToString() != "srno" && col.ToString() != "FillingNo" && col.ToString() != "Casetype" && col.ToString() != "Caseno" && col.ToString() != "CaseYear"
                            && col.ToString() != "Court" && col.ToString() != "Petitioner" && col.ToString() != "Respondent" && col.ToString() != "PartyName" && col.ToString() != "Address" &&
                            col.ToString() != "Department" && col.ToString() != "Status" && col.ToString() != "DocName" && col.ToString() != "PDFLink" && col.ToString() != "Flag" &&
                            col.ToString() != "CourtTypeID" && col.ToString() != "CourtLocation_ID" && col.ToString() != "UniqueNo" && col.ToString() != "Casetype_ID")
                        {
                            ErrorColumnName += col.ToString() + ", ";
                        }
                    }
                    if (ErrorColumnName != "")
                    {
                        lblMsg.Text = obj.Alert("fa-exclamation", "alert-info", "Invalid Columns Names ", "Invalid File Format " + ErrorColumnName + "column name missed match.");
                        return;
                    }
                    if (!dt1.Columns.Contains("srno"))
                    {
                        ErrorColumnName += "srno, ";
                    }
                    if (!dt1.Columns.Contains("FillingNo"))
                    {
                        ErrorColumnName += "FillingNo, ";
                    }
                    if (!dt1.Columns.Contains("Casetype"))
                    {
                        ErrorColumnName += "Casetype, ";
                    }
                    if (!dt1.Columns.Contains("Caseno"))
                    {
                        ErrorColumnName += "Caseno, ";
                    }
                    if (!dt1.Columns.Contains("CaseYear"))
                    {
                        ErrorColumnName += "CaseYear, ";
                    }
                    if (!dt1.Columns.Contains("Court"))
                    {
                        ErrorColumnName += "Court, ";
                    }
                    if (!dt1.Columns.Contains("Petitioner"))
                    {
                        ErrorColumnName += "Petitioner, ";
                    }
                    if (!dt1.Columns.Contains("Respondent"))
                    {
                        ErrorColumnName += "Respondent, ";
                    }
                    if (!dt1.Columns.Contains("PartyName"))
                    {
                        ErrorColumnName += "PartyName, ";
                    }
                    if (!dt1.Columns.Contains("Address"))
                    {
                        ErrorColumnName += "Address, ";
                    }
                    if (!dt1.Columns.Contains("Department"))
                    {
                        ErrorColumnName += "Department, ";
                    }
                    if (!dt1.Columns.Contains("Status"))
                    {
                        ErrorColumnName += "Status, ";
                    }
                    if (!dt1.Columns.Contains("DocName"))
                    {
                        ErrorColumnName += "DocName, ";
                    }
                    if (!dt1.Columns.Contains("PDFLink"))
                    {
                        ErrorColumnName += "PDFLink, ";
                    }
                    if (!dt1.Columns.Contains("Flag"))
                    {
                        ErrorColumnName += "Flag, ";
                    }
                    if (!dt1.Columns.Contains("CourtTypeID"))
                    {
                        ErrorColumnName += "CourtTypeID, ";
                    }
                    if (!dt1.Columns.Contains("CourtLocation_ID"))
                    {
                        ErrorColumnName += "CourtLocation_ID, ";
                    }
                    if (!dt1.Columns.Contains("UniqueNo"))
                    {
                        ErrorColumnName += "UniqueNo, ";
                    }
                    if (!dt1.Columns.Contains("Casetype_ID"))
                    {
                        ErrorColumnName += "Casetype_ID, ";
                    }
                    if (ErrorColumnName == "")
                    {
                        // Dsbulk = obj.ByProcedure("USP_BulkInsert", new string[] { "CreatedBy", "CreatedByIP" },
                        //new string[] { Session["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, new string[] { "type_BulkInsertCaseRegistration", "type_BulkInsertPetitioner", "type_BulkInsertRespondentDtl", "type_BulkInsertDocumentDtl" },
                        //new DataTable[] { DtMain, dtPetitioner, DtRes, DtDoc }, "dataset");
                        // if (Dsbulk != null && Dsbulk.Tables[0].Rows.Count > 0)
                        // {
                        //     if (Dsbulk.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        //     {
                        //         DtMain.Clear(); dtPetitioner.Clear(); DtRes.Clear(); DtDoc.Clear(); dt1.Clear();
                        //     }
                        // }
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-exclamation", "alert-info", "Invalid Columns Names ", "Invalid File Format " + ErrorColumnName + "column name not found.");
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

    protected void ValidateExcelData(DataSet ds)
    {
        //connection();
        try
        {
            DataSet newds = ds as DataSet;
            if (newds != null && newds.Tables.Count > 0 && newds.Tables[0].Rows.Count > 0)
            {
                #region check excel data
                int ErrorCount = 0;
                string ErrMsg = "";
                for (int i = 0; i < newds.Tables[0].Rows.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["srno"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Sr. No., ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["srno"].ToString().Trim(), @"^[0-9]+$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Sr. No., ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["FillingNo"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Filling number., ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["FillingNo"].ToString().Trim(), @"^[A-Za-z]+[/]+[0-9]+[/]+[0-9]+[-]+[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Filling number., ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Casetype"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Case type., ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Casetype"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Case type., ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Caseno"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Case No., ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Caseno"].ToString().Trim(), @"^[0-9]+$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Case No., ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["CaseYear"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Case Year, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["CaseYear"].ToString().Trim(), @"^[0-9]+$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Court Name, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Court"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Court Name, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Court"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Case Year, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Petitioner"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Petitioner Name, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Petitioner"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Petitioner Name, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Respondent"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Respondent Name, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Respondent"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Respondent Name, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["PartyName"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Party Name, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["PartyName"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Party Name, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Address"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Address, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Address"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Address, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Department"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Department, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Department"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Department, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Status"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Case Status, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Status"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Case Status, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["DocName"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Document Name, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["DocName"].ToString().Trim(), @"^[0-9a-zA-Z\s.-]+$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Document Name, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["PDFLink"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Document Path, ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["PDFLink"].ToString().Trim(), @"^(http(s):\/\/.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Document Path, ";
                        }
                    }
                    if (string.IsNullOrWhiteSpace(newds.Tables[0].Rows[i]["Flag"].ToString().Trim()))
                    {
                        ErrorCount = 1;
                        ErrMsg += "Enter Flag Title. ";
                    }
                    else
                    {
                        if (!System.Text.RegularExpressions.Regex.IsMatch(newds.Tables[0].Rows[i]["Flag"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                        {
                            ErrorCount = 1;
                            ErrMsg += "Invalid Flag Title. ";
                        }
                    }
                }
                #endregion
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