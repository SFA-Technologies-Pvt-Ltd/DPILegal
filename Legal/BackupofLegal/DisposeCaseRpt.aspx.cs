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

public partial class Legal_DisposeCaseRpt : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseDisposeType();
                GetCaseType();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx");
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

            if (Session["Role_ID"].ToString() == "3")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "Compliance_Status", "OICMaster_Id", "flag" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, Compliance, Session["OICMaster_ID"].ToString(), "1" }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "1")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "Compliance_Status", "flag" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, Compliance, "2" }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "4")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "Compliance_Status", "flag","District_ID" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, Compliance, "3", Session["District_Id"].ToString() }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "2")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "Compliance_Status", "flag", "Division_ID" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, Compliance, "4", Session["Division_Id"].ToString() }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                ds = obj.ByProcedure("USP_Select_CaseDisposalRpt", new string[] { "Casetype_ID", "CaseDisposeType_Id", "Compliance_Status", "flag", "CourtLocation_Id" },
                    new string[] { ddlCaseType.SelectedItem.Value, ddlDisposetype.SelectedItem.Value, Compliance, "5", Session["District_Id"].ToString() }, "dataset");
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
            if (ddlDisposetype.SelectedIndex == 1)
            {
                
                ComplianceSt_Div.Visible = true;
            }
            else { ComplianceSt_Div.Visible = false; }
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