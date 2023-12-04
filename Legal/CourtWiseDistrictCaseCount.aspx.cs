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

public partial class Legal_CourtWiseDistrictCaseCount : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillCourt();
                FillYear();
                FillCasetype();
            }
        }
        else
            Response.Redirect("../Login.aspx", false);
    }

    protected void FillCourt()
    {
        try
        {
            ddlCourt.Items.Clear();
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
            else dtCourt = court.GetCourt() as DataTable;
            if (dtCourt.Rows.Count > 0)
            {
                ddlCourt.DataValueField = "CourtType_ID";
                ddlCourt.DataTextField = "CourtTypeName";
                ddlCourt.DataSource = dtCourt;
                ddlCourt.DataBind();
                ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
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
    protected void FillCasetype()
    {
        try
        {
            Helper HP = new Helper();
            DataTable dt = HP.GetCasetype() as DataTable;
            ddlCasetype.Items.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataTextField = "Casetype_Name";
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet ds1 = new DataSet();
            ds1 = obj.ByProcedure("USP_CourtwiseDistrictCaseCountRpt", new string[] { "CourtType_Id", "CaseYear", "Casetype_ID", "CourtLocation_Id" },
                new string[] { ddlCourt.SelectedValue, ddlCaseYear.SelectedItem.Text, ddlCasetype.SelectedValue, Session["District_Id"].ToString() }, "dataset");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                grdCaseCount.DataSource = ds1;
                grdCaseCount.DataBind();
                if (grdCaseCount.Rows.Count > 0)
                {
                    grdCaseCount.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdCaseCount.UseAccessibleHeader = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void grdCaseCount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblUniqueNo = (Label)row.FindControl("lblUniqueNo");
            string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
            string UniqueNO = HttpUtility.UrlEncode(Encrypt(lblUniqueNo.Text));
            string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
            string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));

            Response.Redirect("~/Legal/DistrictWise_ReturnFileAndAppeal.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + UniqueNO, false);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
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
}