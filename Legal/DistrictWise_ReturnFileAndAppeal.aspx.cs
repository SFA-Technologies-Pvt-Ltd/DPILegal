using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;
using System.Text;

public partial class Legal_DistrictWise_ReturnFileAndAppeal : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!Page.IsPostBack)
            {
                string multiCharString = Request.QueryString.ToString();
                string[] multiArray = multiCharString.Split(new Char[] { '=', '&' });
                string ID = Decrypt(HttpUtility.UrlDecode(multiArray[1]));
                string Uniqueno = Decrypt(HttpUtility.UrlDecode(multiArray[3]));
                ViewState["UniqueNO"] = Uniqueno;
                ViewState["ID"] = ID;
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillHODName();
                BindDisposalType();
                CaseDisposeStatus();
            }
        }
        else
            Response.Redirect("../Login.aspx", false);
    }
    protected void TextClear()
    {
        txtDispatchDate.Text = "";
        ddlHODName.ClearSelection();
        txtDispatchNo.Text = "";
        txtReason.Text = "";
        txtReceiptDate.Text = "";
        txtReceiptNo.Text = "";
        txtActionAppealedFrom.Text = "";
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    protected void FillHODName()
    {
        try
        {
            ddlHODName.ClearSelection();
            DataSet dsHod = obj.ByDataSet("select HOD_ID, HodName from tblHODMaster");
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlHODName.DataTextField = "HodName";
                ddlHODName.DataValueField = "HOD_ID";
                ddlHODName.DataSource = dsHod;
                ddlHODName.DataBind();
                ddlHODName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BindDisposalType()
    {
        try
        {
            ddlDisponsType.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDisponsType.DataTextField = "CaseDisposeType";
                ddlDisponsType.DataValueField = "CaseDisposeType_Id";
                ddlDisponsType.DataSource = ds;
                ddlDisponsType.DataBind();
            }
            ddlDisponsType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void CaseDisposeStatus() // Case Dispose By Default On NO condtiton
    {
        foreach (ListItem item in rdCaseDispose.Items)
        {
            if (item.Text.Contains("No"))
            {
                item.Selected = true;
                break;

            }
            caseDisposeYes.Visible = false;
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            HearingDtl_CaseDispose.Visible = false;
            CimplianceSt_Div.Visible = false;
        }
    }
    protected void ddlYesOrNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlYesOrNo.SelectedValue == "1")
            {
                divReturnAndReply.Visible = true;
                divReason.Visible = false;
                divBtn.Visible = true;
            }
            else if (ddlYesOrNo.SelectedValue == "2")
            {
                divReturnAndReply.Visible = true;
                divReason.Visible = true;
                divBtn.Visible = true;
            }
            else
            {
                divReason.Visible = false;
                divReturnAndReply.Visible = false; divBtn.Visible = false;
            }
            TextClear();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void rdCaseDispose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            caseDisposeYes.Visible = false;
            if (rdCaseDispose.SelectedValue == "1")
            {
                caseDisposeYes.Visible = true;
            }
            else
            {
                caseDisposeYes.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                DivOrderTimeline.Visible = false;
                ddlDisponsType.ClearSelection();
                HearingDtl_CaseDispose.Visible = false;
                OrderSummary_Div.Visible = false;
                CimplianceSt_Div.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlDisponsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            if (ddlDisponsType.SelectedIndex > 0)
            {
                OrderBy1.Visible = true;
                OrderBy2.Visible = true;
                HearingDtl_CaseDispose.Visible = true;
                DivOrderTimeline.Visible = true;
                OrderSummary_Div.Visible = true;
                if (ddlDisponsType.SelectedValue == "2")
                {
                    ddlCompliaceSt.ClearSelection();
                    CimplianceSt_Div.Visible = true;
                    //RequiredFieldValidator6.Enabled = true;
                }
            }
            else
            {
                HearingDtl_CaseDispose.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false; CimplianceSt_Div.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnCaseDispose_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                //    ViewState["DisposeDOC"] = "";
                //    int DocFailedCntExt = 0;
                //    int DocFailedCntSize = 0;
                //    string strFileName = "";
                //    string strExtension = "";
                //    string strTimeStamp = "";
                //    if (FielUpcaseDisposeOrderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                //    {

                //        string fileExt = System.IO.Path.GetExtension(FielUpcaseDisposeOrderDoc.FileName).Substring(1);
                //        string[] supportedTypes = { "PDF", "pdf" };
                //        if (!supportedTypes.Contains(fileExt))
                //        {
                //            DocFailedCntExt += 1;
                //        }
                //        else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                //        {
                //            DocFailedCntSize += 1;
                //        }
                //        else
                //        {

                //            strFileName = FielUpcaseDisposeOrderDoc.FileName.ToString();
                //            strExtension = Path.GetExtension(strFileName);
                //            strTimeStamp = DateTime.Now.ToString();
                //            strTimeStamp = strTimeStamp.Replace("/", "-");
                //            strTimeStamp = strTimeStamp.Replace(" ", "-");
                //            strTimeStamp = strTimeStamp.Replace(":", "-");
                //            string strName = Path.GetFileNameWithoutExtension(strFileName);
                //            strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                //            string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                //            FielUpcaseDisposeOrderDoc.SaveAs(path);

                //            ViewState["DisposeDOC"] = strFileName;
                //            path = "";
                //            strFileName = "";
                //            strName = "";
                //        }
                //    }
                //    string errormsg = "";
                //    if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                //    if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                //    if (errormsg == "")
                //    {
                //        if (btnCaseDispose.Text == "Disposal")
                //        {
                //            string DisposalDate = txtCaseDisposeDate.Text != "" ? Convert.ToDateTime(txtCaseDisposeDate.Text, cult).ToString("yyyy/MM/dd") : "";
                //            ds = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "Case_ID", "UniqueNo", "CaseDisposal_Status", "Compliance_Status", "CaseDisposalType_Id", "CaseDisposal_Date", "CaseDisposal_Timeline", "CaseDisposal_Doc", "OrderSummary", "LastupdatedBy", "LastupdatedByIP" }
                //                , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), rdCaseDispose.SelectedItem.Text, ddlCompliaceSt.SelectedValue, ddlDisponsType.SelectedValue, DisposalDate, txtOrderimpletimeline.Text.Trim(), ViewState["DisposeDOC"].ToString(), txtorderSummary.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                //        }
                //        if (ds != null)
                //        {
                //            if (ds.Tables[0].Rows.Count > 0)
                //            {
                //                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                //                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                //                {
                //                    //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                //                    txtOrderimpletimeline.Text = "";
                //                    rdCaseDispose.ClearSelection();
                //                    ddlDisponsType.ClearSelection();
                //                    txtCaseDisposeDate.Text = "";
                //                    ViewState["DisposeDOC"] = "";
                //                    ///BindDetails(sender, e);
                //                    btnCaseDispose.Text = "Disposal";
                //                    rdCaseDispose_SelectedIndexChanged(sender, e);
                //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                //                }
                //                else
                //                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                //            }

                //        }
                //    }
                //    else
                //    {
                //        ViewState["DisposeDOC"] = "";
                //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                //    }
            }
        }

        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAction.SelectedValue == "1")
            {
                divYasOrNo.Visible = true;
                divReturnAndReply.Visible = false;
                divReason.Visible = false; divBtn.Visible = false; DisposalStatus.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                HearingDtl_CaseDispose.Visible = false;
                DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false;
                caseDisposeYes.Visible = false;
                CimplianceSt_Div.Visible = false;
            }
            else if (ddlAction.SelectedValue == "2")
            {
                divYasOrNo.Visible = true;
                divReturnAndReply.Visible = false;
                divReason.Visible = false; divBtn.Visible = false; DisposalStatus.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                HearingDtl_CaseDispose.Visible = false;
                DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false;
                caseDisposeYes.Visible = false;
                CimplianceSt_Div.Visible = false;

            }
            else if (ddlAction.SelectedValue == "3")
            {
                divYasOrNo.Visible = false;
                divReturnAndReply.Visible = false;
                divReason.Visible = false; divBtn.Visible = false;
                DisposalStatus.Visible = true;
            }
            else
            {
                divYasOrNo.Visible = false;
                divReturnAndReply.Visible = false;
                divReason.Visible = false; divBtn.Visible = false; DisposalStatus.Visible = false;
                caseDisposeYes.Visible = false;
                CimplianceSt_Div.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                HearingDtl_CaseDispose.Visible = false;
                DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false;
            }
            TextClear();
            ddlYesOrNo.ClearSelection();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}