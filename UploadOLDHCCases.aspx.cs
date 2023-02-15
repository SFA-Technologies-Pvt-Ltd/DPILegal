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

public partial class UploadOLDHCCases : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    string filename = "phe-cc-2000-2023_New_17_Jan_2023.xls"; //phe_cc_22_16_Jan_2023_new.xls";//"phe_cc_22_12_Jan_2023_new.xls";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                GetCourt();
                GetCaseType();
                GetCaseYear();
                GetCaseNo();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    private void GetCourt()
    {
        try
        {
            dsCase = obj.ByDataSet("select distinct Court from tbl_OldCaseDetail order by Court");
            if (dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCourt.DataSource = dsCase;
                ddlCourt.DataTextField = "Court";
                ddlCourt.DataValueField = "Court";
                ddlCourt.DataBind();
                ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCourt.DataSource = null;
                ddlCourt.DataBind();
                ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
        }
    }
    private void GetCaseType()
    {
        try
        {
            dsCase = obj.ByDataSet("select Distinct CaseType from tbl_OldCaseDetail order by CaseType");
            if (dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = dsCase;
                ddlCaseType.DataTextField = "CaseType";
                ddlCaseType.DataValueField = "CaseType";
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseType.DataSource = null;
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
        }

    }
    private void GetCaseYear()
    {
        try
        {
            dsCase = obj.ByDataSet("select distinct CaseYEAR from tbl_OldCaseDetail order by caseyear");
            if (dsCase.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = dsCase;
                ddlYear.DataTextField = "CaseYEAR";
                ddlYear.DataValueField = "CaseYEAR";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlYear.DataSource = null;
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
        }

    }
    private void GetCaseNo()
    {
        try
        {
            dsCase = obj.ByDataSet("select distinct CaseNo from tbl_OldCaseDetail order by CaseNo");
            if (dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCaseNo.DataSource = dsCase;
                ddlCaseNo.DataTextField = "CaseNo";
                ddlCaseNo.DataValueField = "CaseNo";
                ddlCaseNo.DataBind();
                ddlCaseNo.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseNo.DataSource = null;
                ddlCaseNo.DataBind();
                ddlCaseNo.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
        }

    }

    private void Bindgrid(string court, string casetype, string caseno, string year)
    {
        dsCase = new DataSet();
        if (Page.IsValid)
        {

            dsCase = obj.ByProcedure("Sp_GetOldCaseData", new string[] { "Court", "CaseNo", "CaseYear", "CaseType" }, new string[] { court, caseno, year, casetype }, "dataset");
            if (dsCase.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = dsCase.Tables[0];
                grdExcel.DataSource = dsCase;
                grdExcel.DataBind();
                //lblRecord.Text = "Total Record : " + dsCase.Tables[0].Rows.Count;
                //grdExcel.PageSize = 15; 
            }
            else
            {
                grdExcel.DataSource = null;
                grdExcel.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alertMessage()", true);
            }
        }
    }


    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    string Court = "";
    //    string CaseType = "";
    //    double CaseNo = 0;
    //    double Year = 0;
    //    if (ddlCourt.SelectedItem.Text != "Select")
    //        Court = ddlCourt.SelectedItem.Text;

    //    if (ddlCaseNo.SelectedItem.Text != "Select")
    //        CaseNo = Convert.ToDouble(ddlCaseNo.SelectedItem.Text);

    //    if (ddlCaseType.SelectedItem.Text != "Select")
    //        CaseType = ddlCaseType.SelectedItem.Text;

    //    if (ddlYear.SelectedItem.Text != "Select")
    //        Year = Convert.ToDouble(ddlYear.SelectedItem.Text);

    //    dtCase = new DataTable();
    //    dtCase = (DataTable)ViewState["dt"];
    //    var results = from ResC in dtCase.AsEnumerable()
    //                  where (ddlCourt.SelectedIndex > 0 ? ResC.Field<string>("Court").ToLower() == Court.ToLower() : true) &&
    //                  (ddlCaseNo.SelectedIndex > 0 ? ResC.Field<double>("Case No") == CaseNo : true) &&
    //                  (ddlCaseType.SelectedIndex > 0 ? ResC.Field<string>("Case Type").ToLower() == CaseType.ToLower() : true) &&
    //                  (ddlYear.SelectedIndex > 0 ? ResC.Field<double>("Case Year") == Year : true)
    //                  select ResC;

    //    if (results.Any())
    //    {
    //        dtCase = results.CopyToDataTable();
    //        ViewState["dt"] = dtCase;
    //        grdExcel.DataSource = dtCase;
    //        grdExcel.DataBind();
    //        lblRecord.Text = "Total Record : " + dtCase.Rows.Count;
    //    }
    //    else
    //    {
    //        grdExcel.DataSource = null;
    //        grdExcel.DataBind();
    //        lblRecord.Text = "Total Record : 0";
    //        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
    //    }
    //}

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

    protected void btnSearch1_Click(object sender, EventArgs e)
    {
        string court = null;
        string casetype = null;
        string caseno = null;
        string year = null;

        if (ddlCourt.SelectedIndex > 0)
            court = ddlCourt.SelectedItem.Text;
        if (ddlCaseType.SelectedIndex > 0)
            casetype = ddlCaseType.SelectedItem.Text;
        if (ddlCaseNo.SelectedIndex > 0)
            caseno = ddlCaseNo.SelectedItem.Text;
        if (ddlYear.SelectedIndex > 0)
            year = ddlYear.SelectedItem.Text;

        if (court != null || casetype != null || caseno != null || year != null)
        {
            Bindgrid(court, casetype, caseno, year);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Please Select Any Filter');", true);
        }
    }
}

