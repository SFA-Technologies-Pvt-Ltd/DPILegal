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

using System.Reflection;


public partial class Legal_UploadExcel : System.Web.UI.Page
{
    System.Data.DataTable dtCase = null;
    System.Data.DataSet dsCase = null;
    string filepath = "";
    string PasswordChx = "SED@1234";
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
                    divFill.Visible = false;
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


    protected DataTable ImportExcel(string FilePath)
    {

        //Open the Excel file using ClosedXML.
        using (XLWorkbook workBook = new XLWorkbook(FUExcel.PostedFile.InputStream))
        {
            //Read the first Sheet from Excel file.
            IXLWorksheet workSheet = workBook.Worksheet(1);

            //Create a new DataTable.
            DataTable dt = new DataTable();

            //Loop through the Worksheet rows.
            bool firstRow = true;
            foreach (IXLRow row in workSheet.Rows())
            {
                //Use the first row to add columns to DataTable.
                if (firstRow)
                {
                    foreach (IXLCell cell in row.Cells())
                    {
                        dt.Columns.Add(cell.Value.ToString());
                    }
                    firstRow = false;
                }
                else
                {
                    //Add rows to DataTable.
                    dt.Rows.Add();
                    int i = 0;
                    foreach (IXLCell cell in row.Cells())
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                        i++;
                    }
                }

            }
            return dt;
        }

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

                    //if (fileExtension == ".xls" || fileExtension == ".XLS")
                    //{
                    //    ViewState["connStr"] = null;
                    //    //Console.WriteLine("Jet.OLEDB.4.0");
                    //    connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    //    ViewState["connStr"] = connStr;
                    //}
                    //else if (fileExtension == ".xlsx" || fileExtension == ".XLSX")
                    //{
                    //    ViewState["connStr"] = null;
                    //    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                    //    ViewState["connStr"] = connStr;
                    //}

                    //System.Data.DataTable dt = new System.Data.DataTable();
                    //OleDbConnection conn = new OleDbConnection(connStr);
                    //OleDbCommand cmd = new OleDbCommand();
                    //cmd.Connection = conn;
                    //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    //conn.Open();

                    //System.Data.DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //string sheetName = dtSheet.Rows[0]["table_name"].ToString();
                    //cmd.CommandText = "select * from [" + sheetName + "]";
                    //da.SelectCommand = cmd;
                    //da.Fill(ds);

                    //conn.Close();
                    string ErrMsg = "";
                    DataTable dt1 = ImportExcel(filelocation);
                    #region check excel data
                    if (dt1.Columns.Count > 0 && dt1.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (!System.Text.RegularExpressions.Regex.IsMatch(dt1.Rows[i]["srno"].ToString().Trim(), @"^[0-9]+$"))
                            {
                                ErrMsg += "Invalid Sr. No. \n";
                            }
                            if (!System.Text.RegularExpressions.Regex.IsMatch(dt1.Rows[i]["FillingNo"].ToString().Trim(), @"^[A-Za-z]+[/]+[0-9]+[/]+[0-9]+[-]+[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                            {
                                ErrMsg += "Invalid Filling number. \n";
                            }
                            if (!System.Text.RegularExpressions.Regex.IsMatch(dt1.Rows[i]["Casetype"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                            {
                                ErrMsg += "Invalid Case type. \n";
                            }
                            if (!System.Text.RegularExpressions.Regex.IsMatch(dt1.Rows[i]["Caseno"].ToString().Trim(), @"^[0-9]+$"))
                            {
                                ErrMsg += "Invalid Case No. \n";
                            }
                            if (!System.Text.RegularExpressions.Regex.IsMatch(dt1.Rows[i]["CaseYear"].ToString().Trim(), @"^[0-9]+$"))
                            {
                                ErrMsg += "Invalid Court Name, \n";
                            }
                            if (!System.Text.RegularExpressions.Regex.IsMatch(dt1.Rows[i]["Court"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                            {
                                ErrMsg += "Invalid Case Year \n";
                            }
                        }

                    }
                    #endregion
                    if (ErrMsg == "")
                    {
                        dt1.Columns.Add("CourtTypeID");
                        dt1.Columns.Add("CourtLocation_ID");
                        dt1.Columns.Add("UniqueNo");
                        dt1.Columns.Add("Casetype_ID");
                        DataSet dsc, DsCasetype = new DataSet();
                        // To Update Courttype_Id into dt1.
                        dsc = obj.ByDataSet("select T.CourtTypeName,T.CourtType_ID,D.District_Name,D.District_ID from tbl_LegalCourtType T inner join Mst_District D on T.District_ID = D.District_ID");
                        DataTable dt2 = dsc.Tables[0];
                        // To Update Casetype_ID into Dt1.
                        DsCasetype = obj.ByDataSet("select Casetype_ID, Casetype_Name from tbl_Legal_Casetype");
                        DataTable dtCtype = DsCasetype.Tables[0];

                        //Example How To Check Datatype Of DataTable Column Below Line.
                        //var type = dt1.Columns["CourtTypeID"].DataType;
                        dt2.AsEnumerable().ToList().ForEach(m => // To Update dt1 From Court_type master.
                        {
                            dt1.AsEnumerable().Where(r => m.Field<String>("CourtTypeName").Trim() == r.Field<String>("Court").Trim()).ToList().ForEach(s =>
                            {
                                s.SetField<String>("CourtTypeID", Convert.ToString(m.Field<Int32>("CourtType_ID")));
                                s.SetField<String>("CourtLocation_ID", Convert.ToString(m.Field<Byte>("District_ID")));
                                s.SetField<String>("UniqueNo", Convert.ToString((s.Field<String>("CourtTypeID"))) + "_" + Convert.ToString(s.Field<String>("CourtLocation_ID")) + "_" + s.Field<String>("CaseYear") + "_" + s.Field<String>("Casetype") + "_" + s.Field<String>("Caseno")); // Here Create Uniqueno.
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
                        DataTable DtParty = view.ToTable(true, "UniqueNo", "FillingNo", "Casetype", "Caseno", "CaseYear", "Court", "Petitioner", "Flag", "Status", "PartyName");

                        DataTable DtOriginal = view.ToTable(true, "UniqueNo", "FillingNo", "Casetype", "Casetype_ID", "Caseno", "CaseYear", "Court", "CourtTypeID", "CourtLocation_ID", "Flag", "Status");
                        DataTable newDt = DtOriginal.Copy();
                        System.Data.DataColumn newColumn = new System.Data.DataColumn("PartyName", typeof(System.String));

                        newDt.Columns.Add(newColumn);
                        for (int i = 0; i < DtOriginal.Rows.Count; i++)
                        {
                            var PRes = (from emp in DtParty.AsEnumerable()
                                        where emp.Field<string>("UniqueNo").Trim() == DtOriginal.Rows[i]["UniqueNo"].ToString()
                                        select new
                                        {
                                            PartyName = emp.Field<string>("PartyName")
                                        }).ToList();
                            if (PRes.Count > 0)
                            {
                                newDt.Rows[i]["PartyName"] = PRes[0].PartyName; // Here Remove Duplicate Record(Means Delete Second Dulpicate Record)
                            }
                            else
                            {
                                newDt.Rows[i]["PartyName"] = null;
                            }
                        }

                        //DataTable DtMain = view.ToTable(true, "UniqueNo", "FillingNo", "Casetype", "Caseno", "CaseYear", "Court", "Petitioner", "Flag", "Status");
                        DataTable dtPetitioner = view.ToTable(true, "UniqueNo", "Petitioner"); // Here Only Petitoner Dtl Filter.
                        //dtPetitioner.Columns.Remove("FillingNo"); dtPetitioner.Columns.Remove("Status");
                        //dtPetitioner.Columns.Remove("Casetype"); dtPetitioner.Columns.Remove("Flag");
                        ////dtPetitioner.Columns.Remove("CourtLocation_ID"); //dtPetitioner.Columns.Remove("Casetype_ID");
                        //dtPetitioner.Columns.Remove("Caseno"); //dtPetitioner.Columns.Remove("CourtTypeID");
                        //dtPetitioner.Columns.Remove("CaseYear"); dtPetitioner.Columns.Remove("Court"); dtPetitioner.Columns.Remove("PartyName");
                        //dtPetitioner.AcceptChanges();
                        ViewState["dtPetitioner"] = dtPetitioner;//It Keep Petitioner Dtl For Save.
                        //newDt.Columns.Remove("Petitioner");
                        // newDt.AcceptChanges();
                        ViewState["DtMain"] = newDt; // It Keep Main Dtl For Save.

                        DataView view1 = new DataView(dt1);// Here Filter Respondent BY Unique No.
                        DataTable DtRes = view1.ToTable(true, "UniqueNo", "Respondent", "Department", "Address");
                        ViewState["DtRes"] = DtRes; // It Keep Respondet Data for Save.
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
                        ViewState["DtDoc"] = DtDoc; // It Keep Case Document For Save.

                        string ErrorColumnName = "";
                        foreach (DataColumn col in dt1.Columns)
                        {
                            if (col.ToString() != "srno" && col.ToString() != "FillingNo" && col.ToString() != "Casetype" && col.ToString() != "Caseno" && col.ToString() != "CaseYear"
                                && col.ToString() != "Court" && col.ToString() != "Petitioner" && col.ToString() != "Respondent" && col.ToString() != "PartyName" && col.ToString() != "Address" &&
                                col.ToString() != "Department" && col.ToString() != "Status" && col.ToString() != "DocName" && col.ToString() != "PDFLink" && col.ToString() != "Flag" &&
                                 col.ToString() != "UniqueNo" && col.ToString() != "CourtTypeID" && col.ToString() != "CourtLocation_ID" && col.ToString() != "Casetype_ID")
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
                            // To Check Exists Record
                            DataSet DsRecord = obj.ByDataSet("select Case_ID, UniqueNo, FilingNo, CourtName,CasetypeName, CaseNo, CaseYear from tblLegalCaseRegistration");
                            DataTable dt_ds = DsRecord.Tables[0];
                            List<ExistRecord> Erobj = new List<ExistRecord>();

                            var Joinresult = (from p in dt1.AsEnumerable()
                                              join t in dt_ds.AsEnumerable()
                                              on p.Field<string>("UniqueNo").Trim() equals t.Field<string>("UniqueNo").Trim()
                                              where p.Field<string>("FillingNo").Trim() == t.Field<string>("FilingNo").Trim()
                                              select new
                                              {     // Exist Record From database.
                                                  UniqueNo = t.Field<string>("UniqueNo"),
                                                  FilingNo = t.Field<string>("FilingNo"),
                                                  CaseNo = t.Field<string>("CaseNo"),
                                                  CaseYear = t.Field<string>("CaseYear"),
                                                  CasetypeName = t.Field<string>("CasetypeName"),
                                                  CourtName = t.Field<string>("CourtName"),
                                                  // Exist Record From Uploaded Excel.
                                                  NewUniqueNo = p.Field<string>("UniqueNo"),
                                                  NewFilingNo = p.Field<string>("FillingNo"),
                                                  NewCaseNo = (p.Field<string>("Caseno")).ToString(),
                                                  NewCaseYear = (p.Field<string>("CaseYear")).ToString(),
                                                  NewCasetypeName = p.Field<string>("Casetype"),
                                                  NewCourtName = p.Field<string>("Court")

                                              }).Distinct().ToList();
                            DataSet dsDoc = null;
                            if (Joinresult.Count > 0)
                            {
                                foreach (var item in Joinresult)
                                {
                                    ExistRecord er = new ExistRecord();
                                    er.UniqueNo = item.UniqueNo;
                                    er.FilingNo = item.FilingNo;
                                    er.CaseNo = item.CaseNo;
                                    er.CaseYear = item.CaseYear;
                                    er.CasetypeName = item.CasetypeName;
                                    er.CourtName = item.CourtName;
                                    // Exist Record From Uploaded Excel.
                                    er.NewUniqueNo = item.NewUniqueNo;
                                    er.NewFilingNo = item.NewFilingNo;
                                    er.NewCaseNo = item.NewCaseNo;
                                    er.NewCaseYear = item.NewCaseYear;
                                    er.NewCasetypeName = item.NewCasetypeName;
                                    er.NewCourtName = item.NewCourtName;
                                    dsDoc = obj.ByDataSet("select COUNT(UniqueNo) as DocCount from tbl_LegalCaseDocDetail Where UniqueNo = '" + item.UniqueNo + "'  GROUP BY UniqueNo,Case_ID");
                                    er.pdfCount = dsDoc.Tables[0].Rows[0]["DocCount"].ToString();
                                    Erobj.Add(er);
                                }
                            }
                            // var item2 = it keep for Group By in Datatable
                            //(from a in dt1.AsEnumerable()
                            // where Erobj.Any(x => a.Field<string>("UniqueNo").Contains(x.NewUniqueNo))
                            // select a.Field<string>("PDFLink")).Distinct().ToList();

                            if (Erobj.Count > 0)
                            {
                                // Existing Record From DB.
                                //ViewState["Erobj"] = Erobj;
                                GrdExistRecord.DataSource = Erobj;
                                GrdExistRecord.DataBind();
                                GrdExistRecord.HeaderRow.TableSection = TableRowSection.TableHeader;
                                GrdExistRecord.UseAccessibleHeader = true;
                                // Existing Record From Excel.
                                GrdNewRecord.DataSource = Erobj;
                                GrdNewRecord.DataBind();
                                GrdNewRecord.HeaderRow.TableSection = TableRowSection.TableHeader;
                                GrdNewRecord.UseAccessibleHeader = true;
                                Field_ExistRecord.Visible = true;
                                Field_NewRecord.Visible = true;
                            }
                            btnSave.Visible = true; // Visible Save Button If Everything Ok.
                            divFill.Visible = false;
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-exclamation", "alert-info", "Invalid Columns Names ", "Invalid File Format " + ErrorColumnName + "column name not found.");
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + ErrMsg + ")", true);
                    }
                    GrdNewRecord.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GrdNewRecord.UseAccessibleHeader = true;
                    GrdExistRecord.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GrdExistRecord.UseAccessibleHeader = true;
                }
                catch (Exception ex)
                {
                    ErrorLogCls.SendErrorToText(ex);
                }
            }
        }
    }

    protected void ValidateExcelData(DataTable dt)
    {
        try
        {
            if (dt.Columns.Count > 0 && dt.Rows.Count > 0)
            {
                #region check excel data
                string ErrMsg = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["srno"].ToString().Trim(), @"^[0-9]+$"))
                    {
                        ErrMsg += "Invalid Sr. No. \n";
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["FillingNo"].ToString().Trim(), @"^[A-Za-z]+[/]+[0-9]+[/]+[0-9]+[-]+[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    {
                        ErrMsg += "Invalid Filling number. \n";
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Casetype"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    {
                        ErrMsg += "Invalid Case type. \n";
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Caseno"].ToString().Trim(), @"^[0-9]+$"))
                    {
                        ErrMsg += "Invalid Case No. \n";
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["CaseYear"].ToString().Trim(), @"^[0-9]+$"))
                    {
                        ErrMsg += "Invalid Court Name, \n";
                    }
                    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Court"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    {
                        ErrMsg += "Invalid Case Year \n";
                    }
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["Petitioner"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Petitioner Name, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Petitioner"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Petitioner Name, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["Respondent"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Respondent Name, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Respondent"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Respondent Name, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["PartyName"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Party Name, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["PartyName"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Party Name, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["Address"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Address, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Address"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Address, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["Department"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Department, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Department"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Department, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["Status"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Case Status, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Status"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Case Status, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["DocName"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Document Name, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["DocName"].ToString().Trim(), @"^[0-9a-zA-Z\s.-]+$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Document Name, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["PDFLink"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Document Path, ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["PDFLink"].ToString().Trim(), @"^(http(s):\/\/.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Document Path, ";
                    //    }
                    //}
                    //if (string.IsNullOrWhiteSpace(dt.Rows[i]["Flag"].ToString().Trim()))
                    //{
                    //    ErrorCount = 1;
                    //    ErrMsg += "Enter Flag Title. ";
                    //}
                    //else
                    //{
                    //    if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Flag"].ToString().Trim(), @"^[a-zA-Z]+(([\s][a-zA-Z])?[a-zA-Z]*)*$"))
                    //    {
                    //        ErrorCount = 1;
                    //        ErrMsg += "Invalid Flag Title. ";
                    //    }
                    //}
                }
                if (ErrMsg != "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + ErrMsg + ")", true);
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

    public class ExistRecord
    {
        public string UniqueNo { get; set; }
        public string FilingNo { get; set; }
        public string CaseNo { get; set; }
        public string CaseYear { get; set; }
        public string CasetypeName { get; set; }
        public string CourtName { get; set; }

        public string NewUniqueNo { get; set; }
        public string NewFilingNo { get; set; }
        public string NewCaseNo { get; set; }
        public string NewCaseYear { get; set; }
        public string NewCasetypeName { get; set; }
        public string NewCourtName { get; set; }
        public string pdfCount { get; set; }


    }
    #region SaveRecord
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet Dsbulk = null;
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    if (GrdExistRecord.Rows.Count > 0) //Delete Record IF Match from DB.
                    {
                        foreach (GridViewRow row in GrdExistRecord.Rows)
                        {
                            Label lblUniqNo = (Label)row.FindControl("lblUniqNo");
                            ds = obj.ByProcedure("USP_Delete_CaseRegisAndItsChild", new string[] { "UniqueNo" },
                            new string[] { lblUniqNo.Text.Trim() }, "dataset");
                        }
                    }
                    DataTable Maindt = ViewState["DtMain"] as DataTable;
                    DataTable DtRespondent = ViewState["DtRes"] as DataTable;
                    DataTable dtPeti = ViewState["dtPetitioner"] as DataTable;
                    DataTable Docdt = ViewState["DtDoc"] as DataTable;

                    Dsbulk = obj.ByProcedure("USP_BulkInsert", new string[] { "CreatedBy", "CreatedByIP" },
                   new string[] { Session["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, new string[] { "type_BulkInsertCaseRegistration", "type_BulkInsertPetitioner", "type_BulkInsertRespondentDtl", "type_BulkInsertDocumentDtl" },
                   new DataTable[] { Maindt, dtPeti, DtRespondent, Docdt }, "dataset");
                    if (Dsbulk != null && Dsbulk.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = Dsbulk.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (Dsbulk.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            ViewState["DtMain"] = ""; ViewState["DtRes"] = ""; ViewState["dtPetitioner"] = ""; ViewState["DtDoc"] = "";
                            Maindt.Clear(); dtPeti.Clear(); DtRespondent.Clear(); Docdt.Clear();
                            lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            btnSave.Visible = false;
                            GrdExistRecord.DataSource = null;
                            GrdExistRecord.DataBind();
                            GrdNewRecord.DataSource = null;
                            GrdNewRecord.DataBind();
                            divFill.Visible = true;
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        }
                    }
                }
                GrdNewRecord.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdNewRecord.UseAccessibleHeader = true;
                GrdExistRecord.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdExistRecord.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Cancel Uploadation
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnCancel.Text == "No")
            {
                lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", "Data Uploadation Cancel SuccessFully.");
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion

    protected void btnPassword_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text == PasswordChx)
        {
            divFill.Visible= true;
            divPassword.Visible = false;
            divNote.Visible = false; 
        }
        else
        {
            divFill.Visible = false;
            divPassword.Visible = true;
            divNote.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid Password')", true);
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtPassword.Text = "";
    }
}


