using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public partial class Legal_MonthlyHearingDtl : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseType();
                FillYear();
                FillCourt();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }

    #region Fill Year
    protected void FillYear()
    {
        ddlYear.Items.Clear();
        DataSet dsCase = obj.ByDataSet("with yearlist as (select 2023 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year asc");
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

    private void GetCaseType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select Casetype_Name,Casetype_ID from tbl_Legal_Casetype");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = ds.Tables[0];
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataValueField = "Casetype_ID";
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
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void FillCourt() //added by omprakash 30/05/2023
    {
        try
        {
            ddlCourtName.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = new DataTable();
            if (Session["Role_ID"].ToString() == "5")// JD Legal.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "4")// District Office.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;

            }
            else if (Session["Role_ID"].ToString() == "3")// OIC Login
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else dtCourt = court.GetCourt() as DataTable;
            if (dtCourt.Rows.Count > 0)
            {
                ddlCourtName.DataValueField = "CourtType_ID";
                ddlCourtName.DataTextField = "CourtTypeName";
                ddlCourtName.DataSource = dtCourt;
                ddlCourtName.DataBind();
                ddlCourtName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BindGrid()
    {
        try
        {
            ds = new DataSet();
            grdMonthlyHearingdtl.DataSource = null;
            grdMonthlyHearingdtl.DataBind();
            if (Session["Role_ID"].ToString() == "2")// Division
            {
                string Division_ID = Session["Division_Id"].ToString();
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "Division_ID", "CourtType_Id" },
                    new string[] { "2", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, Division_ID, ddlCourtName.SelectedValue }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "4")// District
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "District_ID", "CourtType_Id" },
                   new string[] { "3", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, District_Id, ddlCourtName.SelectedValue }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "5")// Court
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "District_ID", "CourtType_Id" },
                   new string[] { "4", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, District_Id, ddlCourtName.SelectedValue }, "dataset");
            }
            else
            {
                // OIC & Admin login.
                string OICID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
                ds = obj.ByProcedure("USP_MonthlyHearingRpt", new string[] { "flag", "Casetype_ID", "CaseYear", "C_Month", "OICMaster_Id", "CourtType_Id" },
                    new string[] { "1", ddlCaseType.SelectedItem.Value, ddlYear.SelectedItem.Text, ddlMonth.SelectedItem.Text, OICID, ddlCourtName.SelectedValue }, "dataset");
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                grdMonthlyHearingdtl.DataSource = ds;
                grdMonthlyHearingdtl.DataBind();
                grdMonthlyHearingdtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdMonthlyHearingdtl.UseAccessibleHeader = true;
            }
            else
            {
                grdMonthlyHearingdtl.DataSource = null;
                grdMonthlyHearingdtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            if (Page.IsValid)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdMonthlyHearingdtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
                string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));
                string page_ID = HttpUtility.UrlEncode(Encrypt("5"));
                string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + page_ID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdMonthlyHearingdtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdMonthlyHearingdtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

}