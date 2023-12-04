using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_InterimOrderRpt : System.Web.UI.Page
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
                FillCasetype();
                FillYear();
                FillCourt();
                ViewState["OIC_ID"] = Session["OICMaster_ID"];
                //txtFromDate.Text = "01/01/" + DateTime.Now.Year.ToString();
                //txtFromDate.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
    protected void FillCourt()
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

    protected void FillYear()
    {
        try
        {
            ddlCaseYear.Items.Clear();
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 2000 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year asc");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCaseYear.DataSource = dsCase.Tables[0];
                ddlCaseYear.DataTextField = "year";
                ddlCaseYear.DataValueField = "year";
                ddlCaseYear.DataBind();
            }
            ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void FillCaseNo()
    {
        try
        {
            ddlCaseNo.Items.Clear();
            DataTable dtCN = new DataTable();
            Helper CaseNo = new Helper();
            if (Session["Role_ID"].ToString() == "4")
            {
                dtCN = CaseNo.GetDistrictWiseCaseNo(Session["District_Id"].ToString()) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "2")
            {
                string Division_Id = Session["Division_Id"].ToString();
                dtCN = CaseNo.GetDvisionWiseCaseNo(Division_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                string District_Id = Session["District_Id"].ToString();
                dtCN = CaseNo.GetCourtWiseCaseNo(District_Id) as DataTable;
            }
            else
            {
                if (!string.IsNullOrEmpty(Session["OICMaster_ID"].ToString()))
                {
                    dtCN = CaseNo.GetOICWiseCaseNo(Session["OICMaster_ID"].ToString()) as DataTable;
                }
                else
                {
                    string CourtType_Id = ddlCourtName.SelectedValue;
                    dtCN = CaseNo.GetCaseNoByCourt(CourtType_Id) as DataTable;

                }
            }

            if (dtCN != null && dtCN.Rows.Count > 0)
            {
                ddlCaseNo.DataValueField = "Case_ID";
                ddlCaseNo.DataTextField = "CaseNo";
                ddlCaseNo.DataSource = dtCN;
                ddlCaseNo.DataBind();
            }
            ddlCaseNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #region Fill Case type
    protected void FillCasetype()
    {
        try
        {
            Helper hl = new Helper();
            DataTable dt = hl.GetCasetype() as DataTable;
            if (dt.Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = dt;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string OIC = "";
                lblMsg.Text = "";
                GrdPendingReport.DataSource = null;
                GrdPendingReport.DataBind();
                string FromDate = !string.IsNullOrEmpty(txtFromDate.Text) ? Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd") : "";
                string Todate = !string.IsNullOrEmpty(txttodate.Text) ? Convert.ToDateTime(txttodate.Text, cult).ToString("yyyy/MM/dd") : "";
                if (Session["Role_ID"].ToString() == "4")
                {
                    string District_Id = Session["District_Id"].ToString();
                    ds = obj.ByProcedure("USP_InterimOrderRpt", new string[] { "Casetype_ID", "District_Id", "flag", "Court_Id", "CaseNo", "Fromdate", "Todate" }
                        , new string[] { ddlCasetype.SelectedValue, District_Id, "2", ddlCourtName.SelectedValue, ddlCaseNo.SelectedItem.Text, FromDate, Todate }, "dataset");
                }
                else if (Session["Role_ID"].ToString() == "2")
                {
                    string Division_Id = Session["Division_Id"].ToString();
                    ds = obj.ByProcedure("USP_InterimOrderRpt", new string[] { "Casetype_ID", "Division_Id", "flag", "Court_Id", "CaseNo", "Fromdate", "Todate" }
                       , new string[] { ddlCasetype.SelectedValue, Division_Id, "3", ddlCourtName.SelectedValue, ddlCaseNo.SelectedItem.Text, FromDate, Todate }, "dataset");
                }
                else if (Session["Role_ID"].ToString() == "5")
                {
                    string District_Id = Session["District_Id"].ToString();
                    ds = obj.ByProcedure("USP_InterimOrderRpt", new string[] { "Casetype_ID", "CourtLocation_Id", "flag", "Court_Id", "CaseNo", "Fromdate", "Todate" }
                       , new string[] { ddlCasetype.SelectedValue, District_Id, "4", ddlCourtName.SelectedValue, ddlCaseNo.SelectedItem.Text, FromDate, Todate }, "dataset");
                }
                else
                {
                    if (Session["OICMaster_ID"] != "" && Session["OICMaster_ID"] != null)
                        OIC = Session["OICMaster_ID"].ToString();
                    ds = obj.ByProcedure("USP_InterimOrderRpt", new string[] { "Casetype_ID", "OICMaster_Id", "flag", "Court_Id", "CaseNo", "Fromdate", "Todate" }
                        , new string[] { ddlCasetype.SelectedValue, OIC, "1", ddlCourtName.SelectedValue, ddlCaseNo.SelectedItem.Text, FromDate, Todate }, "dataset");
                }
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GrdPendingReport.DataSource = ds;
                        GrdPendingReport.DataBind();
                        GrdPendingReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GrdPendingReport.UseAccessibleHeader = true;
                    }


                }
                else
                {
                    GrdPendingReport.DataSource = null;
                    GrdPendingReport.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
        finally { ds.Clear(); }
    }
    #endregion
    #region Row Command
    protected void GrdPendingReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
                string page_ID = HttpUtility.UrlEncode(Encrypt("9"));
                string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
                string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));
                Response.Redirect("~/Legal/ViewWPPendingCaseDetail.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + page_ID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    private string Encrypt(string clearText)
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
    protected void ddlCourtName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCaseNo();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}