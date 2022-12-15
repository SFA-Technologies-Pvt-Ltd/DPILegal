using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Data.Odbc;

public partial class Legal_UploadPetition_ExcelFile : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    string status = "";
    string FileNo = "";
    string ResponDerName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            status = status + " // button clicked";
            if (fileUploadExcel.HasFile)
            {

                string FileName = Path.GetFileName(fileUploadExcel.PostedFile.FileName);


                string Extension = Path.GetExtension(fileUploadExcel.FileName).ToLower();
                //getting the path of the file   

                string path = Server.MapPath("~/Legal/UploadPetition/" + FileName);
                //saving the file inside the MyFolder of the server  
                fileUploadExcel.SaveAs(path);


                if (Extension == ".xls" || Extension == ".xlsx")
                {

                    Import_To_Grid2(path, Extension, "Yes");

                }

                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Invalid File ", "Please convert file into .xls or .xlsx");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    private void Import_To_Grid2(string FilePath, string Extension, string isHDR)
    {

        string ConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        ConnStr = String.Format(ConnStr, FilePath, isHDR);
        OleDbConnection ExcelConn = new OleDbConnection(ConnStr);

        OleDbCommand ExcelCmd = new OleDbCommand();
        OleDbDataAdapter Ada = new OleDbDataAdapter();
        DataTable dt = new DataTable();
        ExcelCmd.Connection = ExcelConn;
        string SheetName = "";


        try
        {
            ExcelConn.Open();
            DataTable dtMain = new DataTable();
            dtMain = ExcelConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            SheetName = dtMain.Rows[0]["Table_Name"].ToString();
            ExcelCmd.CommandText = "SELECT * FROM [" + SheetName + "] order by 1 asc ,2 desc";
            Ada.SelectCommand = ExcelCmd;
            Ada.Fill(dt);
            ExcelConn.Close();

            DataTable DtFiliNo = new DataTable(); // Main table To case file.
            DtFiliNo.Columns.Add("Filing No", typeof(string));
            DtFiliNo.Columns.Add("Petitioner Name", typeof(string));
            DtFiliNo.Columns.Add("Status", typeof(string));

            DataTable DtResponder = new DataTable();  // Responder table of the case.
            DtResponder.Columns.Add("Respondent Name", typeof(string));
            DtResponder.Columns.Add("Responder No", typeof(string));
            DtResponder.Columns.Add("Address", typeof(string));
            DtResponder.Columns.Add("Department", typeof(string));

            // Here Comment Doc Datatable order by mohini maam due to Excel Format.
            //DataTable dtDoc = new DataTable();
            //dtDoc.Columns.Add("Documents", typeof(string));

            int rowCount = dt.Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                if (i == 0) //Step1. for the First time insert data.
                {
                    if (dt.Rows[i]["Filing No"].ToString() != null && dt.Rows[i]["Filing No"].ToString() != "")
                    {
                        DtFiliNo.Rows.Add(dt.Rows[i]["Filing No"].ToString(), dt.Rows[i]["Petitioner Name"].ToString(), dt.Rows[i]["Status"].ToString());
                        DtResponder.Rows.Add(dt.Rows[i]["Respondent Name"].ToString(), dt.Rows[i]["Responder No"].ToString(), dt.Rows[i]["Address"].ToString(), dt.Rows[i]["Department"].ToString());
                        //dtDoc.Rows.Add(dt.Rows[i]["Documents"].ToString()); // Here Comment Doc Datatable order by mohini maam due to Excel Format.
                    }
                }
                else if (FileNo == dt.Rows[i]["Filing No"].ToString()) //Staep2. to Check comman name and file no.(Repeat Data).
                {
                    if (dt.Rows[i]["Filing No"].ToString() != null && dt.Rows[i]["Filing No"].ToString() != "")
                    {
                        if (ResponDerName != dt.Rows[i]["Respondent Name"].ToString())
                        {
                            DtResponder.Rows.Add(dt.Rows[i]["Respondent Name"].ToString(), dt.Rows[i]["Responder No"].ToString(), dt.Rows[i]["Address"].ToString(), dt.Rows[i]["Department"].ToString());
                        }

                        // Here Comment Doc Datatable order by mohini maam due to Excel Format.
                        //if (dt.Rows[i]["Documents"].ToString() != null && dt.Rows[i]["Documents"].ToString() != "")
                        //{
                        //    dtDoc.Rows.Add(dt.Rows[i]["Documents"].ToString());
                        //}
                    }
                }
                else //Step3. If no record same so run these Lines and Insert into datatabse.
                {       //Remove Here  Doc Datatable order by mohini maam due to Excel Format.
                    ds = obj.ByProcedure("USP_LegalCaseRegistration", new string[] { "CreatedBy", "CreatedByIP" }, new string[] { ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }
                        , new string[] { "type_LegalCaseRegistration", "type_LegalCaseResponderDetails" }, new DataTable[] { DtFiliNo, DtResponder }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DtFiliNo.Clear();
                        DtResponder.Clear();
                        //dtDoc.Clear();
                    }
                    if (dt.Rows[i]["Filing No"].ToString() != null && dt.Rows[i]["Filing No"].ToString() != "")
                    {
                        DtFiliNo.Rows.Add(dt.Rows[i]["Filing No"].ToString(), dt.Rows[i]["Petitioner Name"].ToString(), dt.Rows[i]["Status"].ToString());
                        DtResponder.Rows.Add(dt.Rows[i]["Respondent Name"].ToString(), dt.Rows[i]["Responder No"].ToString(), dt.Rows[i]["Address"].ToString(), dt.Rows[i]["Department"].ToString());
                        // dtDoc.Rows.Add(dt.Rows[i]["Documents"].ToString());  // Here Comment Doc Datatable order by mohini maam due to Excel Format.
                    }
                }
                FileNo = dt.Rows[i]["Filing No"].ToString();
                ResponDerName = dt.Rows[i]["Respondent Name"].ToString();

            }
            // thise procedure used when loop end for the last row of loop.
            // Here Comment Doc Datatable order by mohini maam due to Excel Format.
            ds = obj.ByProcedure("USP_LegalCaseRegistration", new string[] { "CreatedBy", "CreatedByIP" }, new string[] { ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }
                         , new string[] { "type_LegalCaseRegistration", "type_LegalCaseResponderDetails" }, new DataTable[] { DtFiliNo, DtResponder }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    DtFiliNo.Clear();
                    DtResponder.Clear();
                   // dtDoc.Clear();
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-warning", "Warning !", ErrMsg);
                }
            }
            else
            {
                lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}