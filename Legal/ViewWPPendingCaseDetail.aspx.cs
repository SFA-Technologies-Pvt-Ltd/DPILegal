using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_ViewWPPendingCaseDetail : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
            {

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["CaseID"]))
                    {
                        ViewState["CaseID"] = Request.QueryString["CaseID"].ToString();
                        BindCaseDetail();
                        txtOICDate.Attributes.Add("readonly", "readonly");
                        if (Request.QueryString["pageID"] == "2" || Request.QueryString["pageID"] == "4")
                        {
                            dvOrderSummary.Visible = true;
                            dvCaseDisposalType.Visible = true;
                            Compilance_Div.Visible = true;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #region Bind Case Detail
    protected void BindCaseDetail()
    {
        try
        {
            // lblMsg.Text = "";

            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdHearingDtl.DataSource = null;
            GrdHearingDtl.DataBind();

            ds = obj.ByProcedure("USP_ViewWPPendingCaseFullDtlRpt", new string[] { "Case_ID" }
                , new string[] { ViewState["CaseID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CaseNo"].ToString() != "") lblCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                if (ds.Tables[0].Rows[0]["CaseYear"].ToString() != "") txtCaseYear.Text = ds.Tables[0].Rows[0]["CaseYear"].ToString();
                if (ds.Tables[0].Rows[0]["CourtTypeName"].ToString() != "") txtCourtType.Text = ds.Tables[0].Rows[0]["CourtTypeName"].ToString();
                if (ds.Tables[0].Rows[0]["District_Name"].ToString() != "") txtCourtLocation.Text = ds.Tables[0].Rows[0]["District_Name"].ToString();
                if (ds.Tables[0].Rows[0]["PartyName"].ToString() != "") txtParty.Text = ds.Tables[0].Rows[0]["PartyName"].ToString();
                if (ds.Tables[0].Rows[0]["Casetype_Name"].ToString() != "") txtCasetype.Text = ds.Tables[0].Rows[0]["Casetype_Name"].ToString();
                if (ds.Tables[0].Rows[0]["CaseSubject"].ToString() != "") txtCaseSubject.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString();
                if (ds.Tables[0].Rows[0]["CaseSubSubject"].ToString() != "") txtCaseSubSubject.Text = ds.Tables[0].Rows[0]["CaseSubSubject"].ToString();
                if (ds.Tables[0].Rows[0]["OICNAME"].ToString() != "") txtOicName.Text = ds.Tables[0].Rows[0]["OICNAME"].ToString();
                if (ds.Tables[0].Rows[0]["OICMobileNO"].ToString() != "") txtOicMobileNo.Text = ds.Tables[0].Rows[0]["OICMobileNO"].ToString();
                if (ds.Tables[0].Rows[0]["OICEmailID"].ToString() != "") txtOicEmailId.Text = ds.Tables[0].Rows[0]["OICEmailID"].ToString();
                if (ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString() != "") txtHighprioritycase.Text = ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString();
                if (ds.Tables[0].Rows[0]["CaseDetail"].ToString() != "") txtCaseDetail.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString();
                if (ds.Tables[0].Rows[0]["CaseStatus"].ToString() != "") txtCaseStatus.Text = ds.Tables[0].Rows[0]["CaseStatus"].ToString();
                if (ds.Tables[0].Rows[0]["CaseDisposeType"].ToString() != "") txtcasedisposaltype.Text = ds.Tables[0].Rows[0]["CaseDisposeType"].ToString();
                if (ds.Tables[0].Rows[0]["OrderSummary"].ToString() != "") txtOrderSummary.Text = ds.Tables[0].Rows[0]["OrderSummary"].ToString();
                if (ds.Tables[0].Rows[0]["Compliance_Status"].ToString() != "") txtComplianceStatus.Text = ds.Tables[0].Rows[0]["Compliance_Status"].ToString();
                if (ds.Tables[0].Rows[0]["OICOrderDate"].ToString() != "")
                {
                    txtOICDate.Text = ds.Tables[0].Rows[0]["OICOrderDate"].ToString();
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["CaseStatus"].ToString() == "Dispose")
                    {
                        divCsaeDisposal.Visible = true;
                        grdCaseDisposal.DataSource = ds.Tables[0]; grdCaseDisposal.DataBind();
                    }
                }
                if (ds.Tables[1].Rows.Count > 0) GrdPetitioner.DataSource = ds.Tables[1]; GrdPetitioner.DataBind();
                if (ds.Tables[2].Rows.Count > 0) GrdPetiAdv.DataSource = ds.Tables[2]; GrdPetiAdv.DataBind();
                if (ds.Tables[3].Rows.Count > 0) GrdDeptAdv.DataSource = ds.Tables[3]; GrdDeptAdv.DataBind();
                if (ds.Tables[4].Rows.Count > 0) GrdResponderDtl.DataSource = ds.Tables[4]; GrdResponderDtl.DataBind();
                if (ds.Tables[5].Rows[0]["CaseDoc_ID"].ToString() != "") GrdDocument.DataSource = ds.Tables[5]; GrdDocument.DataBind();
                if(ds.Tables[6].Rows.Count > 0)
                {
                    if (ds.Tables[6].Rows[0]["NextHearing_ID"].ToString() != "") GrdHearingDtl.DataSource = ds.Tables[6]; GrdHearingDtl.DataBind();

                }
            }
            else
            {
                GrdResponderDtl.DataSource = null;
                GrdResponderDtl.DataBind();
                GrdHearingDtl.DataSource = null;
                GrdHearingDtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
    protected void GrdDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Link = (HyperLink)e.Row.FindControl("hyperLink");
                HyperLink DocWithFolderPath = (HyperLink)e.Row.FindControl("lnkDocPath");
                Label lbldoc = (Label)e.Row.FindControl("lbldoc");

                string name = lbldoc.Text;
                name.StartsWith("https");
                if (name.StartsWith("https") == true)
                    Link.Visible = true;
                else DocWithFolderPath.Visible = true;

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Back
    protected void lbkBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["pageID"] == "1") Response.Redirect("../Legal/pendingwpreport.aspx", false); //Pendig Rpt
            if (Request.QueryString["pageID"] == "2") Response.Redirect("../Legal/ConcludedwpReport.aspx", false);//Concolude Rpt
            if (Request.QueryString["pageID"] == "3") Response.Redirect("../Legal/SubjectWiseCaseDtl.aspx", false);// SubjectWise Case Rpt
            if (Request.QueryString["pageID"] == "4") Response.Redirect("../Legal/disposecaserpt.aspx", false);// Disposal Case Rpt
            if (Request.QueryString["pageID"] == "5") Response.Redirect("../Legal/MonthlyHearingDtl.aspx", false);// Monthly Hearing Case Rpt
            if (Request.QueryString["pageID"] == "6") Response.Redirect("../Legal/WeekelyHearingCaseRpt.aspx", false);// Weekely Hearing Case Rpt
            if (Request.QueryString["pageID"] == "7") Response.Redirect("../Legal/LongPendingCaseRpt.aspx", false);// Long Pendinh Case Rpt
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}