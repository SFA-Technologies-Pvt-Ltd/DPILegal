using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;

public partial class UploadHCFileCaseDetail : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    string filename = "phe-cc-2000-2023_New_17_Jan_2023.xls"; //phe_cc_22_16_Jan_2023_new.xls";//"phe_cc_22_12_Jan_2023_new.xls";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bindgrid(filename);

            if (ViewState["dt"] != null)
            {
                BindCourt();
                BindDropdowns();
            }
        }

    }
    protected void BindCourt()
    {
        try
        {
            dtCase = new DataTable();
            dtCase = (DataTable)ViewState["dt"];
            var res = dtCase.AsEnumerable().Select(i => new { Court = i["Court"] }).ToList();
            ddlCourt.DataSource = res.Distinct();
            // dtCase.Select("select Court").Distinct();
            ddlCourt.DataTextField = "Court";
            ddlCourt.DataValueField = "Court";
            ddlCourt.DataBind();
            ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
        }
    }
    private void Bindgrid(string filename)
    {
        string day, day1 = "";
        string connStr = "";
        int i = 0;
        int oItem = 0;

        //if (file1.HasFile)
        //{
        //  string filename = Path.GetFileName(file1.PostedFile.FileName);
        string fileExtension = Path.GetExtension(filename);
        string filelocation = Server.MapPath("~/Legal/ExcelFile/" + filename); // @"D:\Bhanu\Legal\DPILegal\Legal\ExcelFile\" + filename;
        //Response.Write(filelocation);
        //Response.Write(filename);
        //Response.Write(fileExtension);

        if (fileExtension == ".xls" || fileExtension == ".XLS")
        {
            connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        }
        else if (fileExtension == ".xlsx" || fileExtension == ".XLSX")
        {
            connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
        }
        DataTable dt = new DataTable();
        XmlDocument doc = new XmlDocument();
        OleDbConnection conn = new OleDbConnection(connStr);

        OleDbCommand cmd = new OleDbCommand();
        cmd.Connection = conn;
        OleDbDataAdapter da = new OleDbDataAdapter(cmd);

        conn.Open();
        DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        string sheetName = dtSheet.Rows[0]["table_name"].ToString();
        cmd.CommandText = "select * from [" + sheetName + "]";
        da.SelectCommand = cmd;
        da.Fill(dt);
        conn.Close();


        ////////////BindGrid
        grdExcel.DataSource = dt;
        grdExcel.DataBind();
        grdExcel.PageSize = 15;

        dsCase = new DataSet();
        dsCase.Tables.Add(dt);
        ViewState["dt"] = dt;
        // DataSet ds = new DataSet();
        // ds.Tables.Add(dt);
        oItem = dt.Rows.Count;
        oItem -= 1;

        //XmlTextWriter xmlWrite=new XmlTextWriter("output.xml",);


        XmlDeclaration declaire = doc.CreateXmlDeclaration("1.0", "utf-8", null);
        // -----------------------create root-----------------------------
        XmlElement rootnode = doc.CreateElement("root");
        doc.InsertBefore(declaire, doc.DocumentElement);
        doc.AppendChild(rootnode);

        //Response.Write("Created");
        while (i < oItem)
        {

            day = dt.Rows[i].ItemArray[0].ToString();

            if (day != day1)
            {
                day1 = day;
                //DateTime d= Convert.ToDateTime(dt.Rows[i].ItemArray[4]);

                //string str = Convert.ToDateTime(d.ToString("dd-MMMM-yyyy")).ToString("MMMM-dd-yyyy");                                              

                //string finaldate = str.Replace(d.Year.ToString(), DateTime.Now.ToString("yyyy"));

                XmlElement dobEle = doc.CreateElement("CaseDetailBook");

                // dobEle.SetAttribute("date", finaldate);


                do
                {
                    XmlElement CaseDetail = doc.CreateElement("CaseDetail");

                    //XmlElement Case = doc.CreateElement("Case");
                    XmlElement FilingNo = doc.CreateElement("FilingNo");
                    XmlElement CaseType = doc.CreateElement("CaseType");
                    XmlElement CaseNumber = doc.CreateElement("CaseNumber");
                    XmlElement Year = doc.CreateElement("Year");
                    XmlElement Court = doc.CreateElement("Court");
                    XmlElement Petitioner = doc.CreateElement("Petitioner");
                    XmlElement Respondent = doc.CreateElement("Respondent");
                    XmlElement P_R_No = doc.CreateElement("P_R_No");
                    XmlElement Party_Name = doc.CreateElement("Party_Name");
                    //XmlElement Address = doc.CreateElement("Address");
                    XmlElement Status = doc.CreateElement("Status");
                    XmlElement PDF = doc.CreateElement("PDF");
                    XmlElement PDFLink = doc.CreateElement("PDFLink");

                    // Case.InnerText = dt.Rows[i].ItemArray[0].ToString();
                    FilingNo.InnerText = dt.Rows[i].ItemArray[0].ToString();
                    CaseType.InnerText = dt.Rows[i].ItemArray[1].ToString();
                    CaseNumber.InnerText = dt.Rows[i].ItemArray[2].ToString();
                    Year.InnerText = dt.Rows[i].ItemArray[3].ToString();
                    Court.InnerText = dt.Rows[i].ItemArray[4].ToString();
                    Petitioner.InnerText = dt.Rows[i].ItemArray[5].ToString();
                    Respondent.InnerText = dt.Rows[i].ItemArray[6].ToString();
                    P_R_No.InnerText = dt.Rows[i].ItemArray[7].ToString();
                    Party_Name.InnerText = dt.Rows[i].ItemArray[8].ToString();
                    // Address.InnerText = dt.Rows[i].ItemArray[7].ToString();
                    //Department.InnerText = dt.Rows[i].ItemArray[8].ToString();
                    Status.InnerText = dt.Rows[i].ItemArray[9].ToString();
                    PDF.InnerText = dt.Rows[i].ItemArray[10].ToString();
                    PDFLink.InnerText = dt.Rows[i].ItemArray[11].ToString();

                    // CaseDetail.AppendChild(Case);
                    CaseDetail.AppendChild(FilingNo);
                    CaseDetail.AppendChild(CaseType);
                    CaseDetail.AppendChild(CaseNumber);
                    CaseDetail.AppendChild(Year);
                    CaseDetail.AppendChild(Court);
                    CaseDetail.AppendChild(Petitioner);
                    CaseDetail.AppendChild(Respondent);
                    CaseDetail.AppendChild(P_R_No);
                    CaseDetail.AppendChild(Party_Name);
                    //CaseDetail.AppendChild(Address);
                    //CaseDetail.AppendChild(Department);
                    CaseDetail.AppendChild(Status);
                    CaseDetail.AppendChild(PDF);
                    CaseDetail.AppendChild(PDFLink);


                    dobEle.AppendChild(CaseDetail);
                    i++;
                } while (day1 == dt.Rows[i].ItemArray[0].ToString() && i < oItem);


                doc.DocumentElement.AppendChild(dobEle);

            }


        }

        doc.Save(Server.MapPath("~/Legal/ExcelFile/" + "Output.xml"));
        lblRecord.Text = "Total Record : " + dt.Rows.Count;

        //Response.Write("Created");
        //}
    }
    protected void BindDropdowns()
    {
        try
        {
            dtCase = new DataTable();
            dtCase = (DataTable)ViewState["dt"];

            var result = from customer in dtCase.AsEnumerable()
                             //where customer.Field<string>("Country") == country
                         select new
                         {
                             //AR/28/2022-Principal Seat Jabalpur
                             CaseType = customer.Field<string>("Filing No").ToString().Split('-')[0].Split('/')[0],
                             CaseNo = customer.Field<string>("Filing No").ToString().Split('-')[0].Split('/')[1],
                             Year = customer.Field<string>("Filing No").ToString().Split('-')[0].Split('/')[2]
                         };

            ddlCaseType.DataSource = result.AsEnumerable().Select(c1 => new { CaseType = c1.CaseType }).ToList().Distinct();
            ddlCaseType.DataTextField = "CaseType";
            ddlCaseType.DataValueField = "CaseType";
            ddlCaseType.DataBind();
            ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));

            ddlCaseNo.DataSource = result.AsEnumerable().Select(c2 => new { CaseNo = c2.CaseNo }).ToList().Distinct();
            ddlCaseNo.DataTextField = "CaseNo";
            ddlCaseNo.DataValueField = "CaseNo";
            ddlCaseNo.DataBind();
            ddlCaseNo.Items.Insert(0, new ListItem("Select", "0"));

            ddlYear.DataSource = result.AsEnumerable().Select(c3 => new { Year = c3.Year }).ToList().Distinct();
            ddlYear.DataTextField = "Year";
            ddlYear.DataValueField = "Year";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
        }
    }

    List<string> selectedAddress = new List<string>();

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string Court = "";
        string CaseType = "";
        double CaseNo = 0;
        double Year = 0;
        if (ddlCourt.SelectedItem.Text != "Select")
            Court = ddlCourt.SelectedItem.Text;

        if (ddlCaseNo.SelectedItem.Text != "Select")
            CaseNo = Convert.ToDouble(ddlCaseNo.SelectedItem.Text);

        if (ddlCaseType.SelectedItem.Text != "Select")
            CaseType = ddlCaseType.SelectedItem.Text;

        if (ddlYear.SelectedItem.Text != "Select")
            Year = Convert.ToDouble(ddlYear.SelectedItem.Text);

        dtCase = new DataTable();
        dtCase = (DataTable)ViewState["dt"];
        var results = from ResC in dtCase.AsEnumerable()
                      where (ddlCourt.SelectedIndex > 0 ? ResC.Field<string>("Court").ToLower() == Court.ToLower() : true) &&
                      (ddlCaseNo.SelectedIndex > 0 ? ResC.Field<double>("Case No") == CaseNo : true) &&
                      (ddlCaseType.SelectedIndex > 0 ? ResC.Field<string>("Case Type").ToLower() == CaseType.ToLower() : true) &&
                      (ddlYear.SelectedIndex > 0 ? ResC.Field<double>("Case Year") == Year : true)
                      select ResC;

        if (results.Any())
        {
            dtCase = results.CopyToDataTable();
            ViewState["dt"] = dtCase;
            grdExcel.DataSource = dtCase;
            grdExcel.DataBind();
            lblRecord.Text = "Total Record : " + dtCase.Rows.Count;
        }
        else
        {
            grdExcel.DataSource = null;
            grdExcel.DataBind();
            lblRecord.Text = "Total Record : 0";
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
        }
    }

    protected void grdExcel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dtCase = new DataTable();
        dtCase = (DataTable)ViewState["dt"];
        grdExcel.PageIndex = e.NewPageIndex;
        grdExcel.DataSource = dtCase;
        grdExcel.DataBind();
    }



    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdExcel.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
        dtCase = new DataTable();
        dtCase = (DataTable)ViewState["dt"];

        grdExcel.DataSource = dtCase;
        grdExcel.DataBind();
    }
    string preSrNo = string.Empty;
    string preFNo = string.Empty;
    string preCourt = string.Empty;
    string prePetitioner = string.Empty;
    string preRespondent = string.Empty;
    int firstRow = 0;
    int secondRow = 0;
    int thirdRow = 0;
    //protected void grdExcel_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {

    //        DataRowView drv = ((DataRowView)e.Row.DataItem);

    //        if (preFNo == drv["Filing No"].ToString())
    //        {
    //            if (grdExcel.Rows[firstRow].Cells[1].RowSpan == 0)
    //                grdExcel.Rows[firstRow].Cells[1].RowSpan = 2;
    //            else
    //                grdExcel.Rows[firstRow].Cells[1].RowSpan += 1;

    //            e.Row.Cells.RemoveAt(1);
    //        }
    //        else
    //        {
    //            e.Row.VerticalAlign = VerticalAlign.Middle;
    //            preFNo = drv["Filing No"].ToString();

    //        }

    //        if (preCourt == drv["Court"].ToString())
    //        {
    //            if (grdExcel.Rows[firstRow].Cells[2].RowSpan == 0)
    //                grdExcel.Rows[firstRow].Cells[2].RowSpan = 2;
    //            else
    //                grdExcel.Rows[firstRow].Cells[2].RowSpan += 1;

    //           // e.Row.Cells.RemoveAt(2);
    //        }
    //        else
    //        {
    //            e.Row.VerticalAlign = VerticalAlign.Middle;
    //            preCourt = drv["Court"].ToString();

    //        }

    //        if (prePetitioner == drv["Petitioner"].ToString())
    //        {
    //            if (grdExcel.Rows[firstRow].Cells[3].RowSpan == 0)
    //                grdExcel.Rows[firstRow].Cells[3].RowSpan = 2;
    //            else
    //                grdExcel.Rows[firstRow].Cells[3].RowSpan += 1;

    //           // e.Row.Cells.RemoveAt(3);
    //        }
    //        else
    //        {
    //            e.Row.VerticalAlign = VerticalAlign.Top;
    //            prePetitioner = drv["Petitioner"].ToString();
    //            firstRow = e.Row.RowIndex;
    //        }

    //        //if (preRespondent == drv["Respondent"].ToString())
    //        //{
    //        //    if (grdExcel.Rows[firstRow].Cells[4].RowSpan == 0)
    //        //        grdExcel.Rows[firstRow].Cells[4].RowSpan = 2;
    //        //    else
    //        //        grdExcel.Rows[firstRow].Cells[4].RowSpan += 1;

    //        //    e.Row.Cells.RemoveAt(4);
    //        //}
    //        //else
    //        //{
    //        //    e.Row.VerticalAlign = VerticalAlign.Top;
    //        //    preRespondent = drv["Respondent"].ToString();
    //        //    firstRow = e.Row.RowIndex;
    //        //}

    //    }
    //}


    protected void grdExcel_DataBound(object sender, EventArgs e)
    {
        #region gridview cell merging

        //for (int i = grdExcel.Rows.Count - 1; i > 0; i--)
        //{
        //    GridViewRow row = grdExcel.Rows[i];
        //    GridViewRow previousRow = grdExcel.Rows[i - 1];
        //    for (int j = 0; j < row.Cells.Count; j++)
        //    {
        //        if (row.Cells[j].Text == previousRow.Cells[j].Text)
        //        {
        //            if (j != 4)
        //            {
        //                if (previousRow.Cells[j].RowSpan == 0)
        //                {
        //                    if (row.Cells[j].RowSpan == 0)
        //                    {
        //                        previousRow.Cells[j].RowSpan += 2;
        //                    }
        //                    else
        //                    {
        //                        previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
        //                    }
        //                    row.Cells[j].Visible = false;
        //                    if (j == 0)
        //                    {
        //                        row.Cells[j].Text = previousRow.Cells[j].RowSpan.ToString();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion


    }
    protected void gridView_PreRender(object sender, EventArgs e)
    {
        MergeRows(grdExcel);
    }
    public void MergeRows(GridView gridView)
    {
        int rowindeCount = 0;
        bool ShowRow = false;
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (i != row.Cells.Count - 1)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
    }

    protected void grdExcel_PreRender(object sender, EventArgs e)
    {
        MergeRows(grdExcel);
    }
}

