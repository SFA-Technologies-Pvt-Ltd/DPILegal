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
using System.Globalization;

public partial class Legal_DisposeCaseRpt : System.Web.UI.Page
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
                GetCaseDisposeType();
                GetCaseType();
                FillCourt();
                FillOrderWithDirection();
            }
        }
        else { Response.Redirect("/Login.aspx");}
    }
    protected void FillOrderWithDirection()
    {
        try
        {
            ddlOrderWith.ClearSelection();
            DataSet dsHod = obj.ByDataSet("select OrderWithDirection_ID,OrderWithDirection from tbl_OrderWithDirection");
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlOrderWith.DataTextField = "OrderWithDirection";
                ddlOrderWith.DataValueField = "OrderWithDirection_ID";
                ddlOrderWith.DataSource = dsHod;
                ddlOrderWith.DataBind();
                ddlOrderWith.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillCourt() //added by omprakash 21/06/2023
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
    #region Get Case Dispose Type
    private void GetCaseDisposeType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_LegalCaseDisposeType ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlDisposetype.DataSource = ds.Tables[0];
                ddlDisposetype.DataTextField = "CaseDisposeType";
                ddlDisposetype.DataValueField = "CaseDisposeType_Id";
                ddlDisposetype.DataBind();
                ddlDisposetype.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlDisposetype.DataSource = null;
                ddlDisposetype.DataBind();
                ddlDisposetype.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    #endregion
    #region Get Case Type
    private void GetCaseType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_Legal_Casetype");
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
    #endregion
    #region Bing Grid
    protected void BindGrid()
    {
        try
        {
            string OIC = "";
            grdSubjectWiseCasedtl.DataSource = null;
            grdSubjectWiseCasedtl.DataBind();
            string Compliance = ddlCompliaceSt.SelectedIndex > 0 ? ddlCompliaceSt.SelectedItem.Value : null;
            string FromDate = !string.IsNullOrEmpty(txtFromDate.Text) ? Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd") : "";
            string Todate = !string.IsNullOrEmpty(txttodate.Text) ? Convert.ToDateTime(txttodate.Text, cult).ToString("yyyy/MM/dd") : "";
            if (Session["Role_ID"].ToString() == "3")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id",  "OICMaster_Id", "flag", "OrderWithDirection_ID", "CourtType_Id", "Fromdate", "Todate" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value,  Session["OICMaster_ID"].ToString(), "1", ddlOrderWith.SelectedValue, ddlCourtName.SelectedValue, FromDate, Todate }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "1")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id","flag" , "OrderWithDirection_ID", "CourtType_Id", "Fromdate", "Todate" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, "2",ddlOrderWith.SelectedValue, ddlCourtName.SelectedValue, FromDate, Todate }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "4")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id","flag", "District_ID", "OrderWithDirection_ID", "CourtType_Id", "Fromdate", "Todate" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value,  "3", Session["District_Id"].ToString(), ddlOrderWith.SelectedValue, ddlCourtName.SelectedValue, FromDate, Todate }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "2")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id",  "flag", "Division_ID", "OrderWithDirection_ID", "CourtType_Id", "Fromdate", "Todate" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value,  "4", Session["Division_Id"].ToString(), ddlOrderWith.SelectedValue, ddlCourtName.SelectedValue, FromDate, Todate }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "flag", "CourtLocation_Id", "OrderWithDirection_ID", "CourtType_Id", "Fromdate", "Todate" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, "5", Session["District_Id"].ToString(),ddlOrderWith.SelectedValue,ddlCourtName.SelectedValue,FromDate,Todate }, "dataset");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdSubjectWiseCasedtl.DataSource = ds;
                grdSubjectWiseCasedtl.DataBind();
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
            }
            else
            {
                grdSubjectWiseCasedtl.DataSource = null;
                grdSubjectWiseCasedtl.DataBind();
            }

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
    #endregion
    #region Row Command
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
                string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));
                string page_ID = HttpUtility.UrlEncode(Encrypt("4"));
                string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + page_ID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Page Index Changing
    protected void grdSubjectWiseCasedtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdSubjectWiseCasedtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region ddlDisposetype Selected Index Changed
    protected void ddlDisposetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlCompliaceSt.ClearSelection();
            ddlOrderWith.ClearSelection();
            if (ddlDisposetype.SelectedIndex == 1)
            {
                rfvOrderWith.Enabled = true;
                OrderWithDir_Div.Visible = true;
                ComplianceSt_Div.Visible = false;
            }
            else { ComplianceSt_Div.Visible = false; OrderWithDir_Div.Visible = false; rfvOrderWith.Enabled = false; }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
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