using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_OrderByDirectionEdit : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDropDowns();
            string ID = Convert.ToString(Request.QueryString["U"]);
            ViewState["CT"] = Convert.ToString(Request.QueryString["CT"]);
            FindGridData(ID);
        }
    }
    private void bindDropDowns()
    {
        try
        {
            dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlOICNameOpen.DataSource = dsCase.Tables[0];
                ddlOICNameOpen.DataTextField = "OICName";
                ddlOICNameOpen.DataValueField = "OICMaster_ID";
                ddlOICNameOpen.DataBind();
                ddlOICNameOpen.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOICNameOpen.DataSource = null;
                ddlOICNameOpen.DataBind();
                ddlOICNameOpen.Items.Insert(0, new ListItem("Select", "0"));
            }

            dsCase = obj.ByDataSet("select Respondent_Office,Respondent_office_Id from tblRespondentOffice");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlRespondentOffice.DataSource = dsCase.Tables[0];
                ddlRespondentOffice.DataTextField = "Respondent_Office";
                ddlRespondentOffice.DataValueField = "Respondent_office_Id";
                ddlRespondentOffice.DataBind();
                // ddlRespondentOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRespondentOffice.DataSource = null;
                ddlRespondentOffice.DataBind();
                ddlRespondentOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlRespondentOffice.ClearSelection();
            ddlRespondentOffice.SelectedIndex = -1;

            dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {

                ddlCaseSubject.DataSource = dsCase.Tables[0];
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataBind();
                ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseSubject.DataSource = null;
                ddlCaseSubject.DataBind();
                ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
            }

            dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubSubj_Id,CaseSubSubject from tbl_CaseSubSubjectMaster");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {

                ddlCaseSubSubject.DataSource = dsCase.Tables[0];
                ddlCaseSubSubject.DataTextField = "CaseSubSubject";
                ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlCaseSubSubject.DataBind();
                ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseSubSubject.DataSource = null;
                ddlCaseSubSubject.DataBind();
                ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    public string convertQuotes(string str)
    {
        return str.Replace("'", "''");
    }
    private void FindGridData(string Id)
    {
        string OICId = "";
        string Court = "";
        string ResOffID = "";
        string IsOrDir = "";
        string FilingNo = "";
        string Remarks = "";
        string HearingDate = "";
        string CaseNo = "";
        string OrderComplianceDate = "";
        string IsComplaince = "";
        string CaseSubSubjectId = "";
        string CaseSubjectId = "";
        string Petitioner = "";
        string Respondent = "";
        string OICMobileNo = "";
        // string Id = (sender as LinkButton).CommandArgument;
        dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId," +
            "Remarks,HearingDate,CaseNo,OrderComplianceDate,IsComplaince,CaseSubSubjectId,IsOrderByDirection,RespondentOfficeId from tbl_OrderByDirectionPendingCase where UniqueNo='" + Convert.ToString(Id) + "'");
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            Court = dsCase.Tables[0].Rows[0]["Court"].ToString().Trim();
            ResOffID = dsCase.Tables[0].Rows[0]["RespondentOfficeId"].ToString().Trim();
            IsOrDir = dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString().Trim();
            FilingNo = dsCase.Tables[0].Rows[0]["FilingNo"].ToString().Trim();
            Remarks = dsCase.Tables[0].Rows[0]["Remarks"].ToString().Trim();
            HearingDate = dsCase.Tables[0].Rows[0]["HearingDate"].ToString().Trim();
            CaseNo = dsCase.Tables[0].Rows[0]["CaseNo"].ToString().Trim();
            OrderComplianceDate = dsCase.Tables[0].Rows[0]["OrderComplianceDate"].ToString().Trim();
            IsComplaince = dsCase.Tables[0].Rows[0]["IsComplaince"].ToString().Trim();
            CaseSubSubjectId = dsCase.Tables[0].Rows[0]["CaseSubSubjectId"].ToString().Trim();
            Petitioner = dsCase.Tables[0].Rows[0]["Petitioner"].ToString().Trim();
            Respondent = dsCase.Tables[0].Rows[0]["Respondent"].ToString().Trim();
            CaseSubSubjectId = dsCase.Tables[0].Rows[0]["CaseSubSubjectId"].ToString().Trim();
            CaseSubjectId = dsCase.Tables[0].Rows[0]["CaseSubjectId"].ToString().Trim();
            OICId = dsCase.Tables[0].Rows[0]["OICId"].ToString().Trim();
            OICMobileNo = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString().Trim();
            string ROIds = dsCase.Tables[0].Rows[0]["RespondentOffice"].ToString().Trim();
            string[] RespondentOffice = ROIds.Split(',');
            txtFilingNo.Text = FilingNo;
            txtCourt.Text = Court;
            txtPetitioner.Text = Petitioner;
            txtRespondent.Text = Respondent;
            txtRemarks.Text = Remarks;

            if (IsOrDir != "")
                ddlIsOrderByDirection.Items.FindByValue(IsOrDir).Selected = true;
            else
                ddlRespondentOffice.SelectedIndex = -1;

            if (!string.IsNullOrEmpty(ROIds))
            {
                for (int i = 0; i < RespondentOffice.Length; i++)
                {
                    ddlRespondentOffice.Items.FindByValue(RespondentOffice[i]).Selected = true;
                }
            }
            else
            {
                ddlRespondentOffice.SelectedIndex = -1;
            }
            if (!string.IsNullOrEmpty(CaseSubjectId))
                ddlCaseSubject.Items.FindByValue(CaseSubjectId).Selected = true;
            else
                ddlCaseSubject.SelectedIndex = -1;

            if (!string.IsNullOrEmpty(OICId))
                ddlOICNameOpen.Items.FindByValue(OICId).Selected = true;
            else
                ddlOICNameOpen.SelectedIndex = -1;

            if (!string.IsNullOrEmpty(OICMobileNo))
                txtOICMobileNoOpen.Text = OICMobileNo;

            if (!string.IsNullOrEmpty(OrderComplianceDate))
                txtOrderComplianceDate.Text = Convert.ToDateTime(OrderComplianceDate).ToShortDateString();

            if (!string.IsNullOrEmpty(IsComplaince))
                ddlIsComplaince.Items.FindByValue(IsComplaince).Selected = true;
            else
                ddlIsComplaince.SelectedIndex = -1;

            if (!string.IsNullOrEmpty(CaseSubSubjectId))
                ddlCaseSubSubject.Items.FindByValue(CaseSubSubjectId).Selected = true;
            else
                ddlCaseSubSubject.SelectedIndex = -1;
        }
        //if (ddlOICNameOpen.SelectedIndex > 0)
        //{
        //    int OICID = Convert.ToInt32(ddlOICNameOpen.SelectedItem.Value);
        //    dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICID);
        //    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        //    {
        //        txtOICMobileNoOpen.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        //    }
        //}

    }

    protected void ddlOICNameOpen_SelectedIndexChanged(object sender, EventArgs e)
    {
        int OICID = Convert.ToInt32(ddlOICNameOpen.SelectedItem.Value);
        dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICID);
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            txtOICMobileNoOpen.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        var selItems = ddlRespondentOffice.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => x.Value).ToArray();
        var result = String.Join(",", selItems);

        string UId = Convert.ToString(Request.QueryString["U"]);
        string CaseNo = ""; //(HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnCaseNo");
        string OICMobileNo = txtOICMobileNoOpen.Text.Trim();
        int CaseSubSubjectId = ddlCaseSubSubject.SelectedIndex > 0 ? Convert.ToInt32(ddlCaseSubSubject.SelectedItem.Value) : 0;
        int RespondentOfficeId = ddlRespondentOffice.SelectedIndex > 0 ? Convert.ToInt32(ddlRespondentOffice.SelectedItem.Value) : 0;
        // TextBox txtHearingDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtHearingDate");
        string OrderComplianceDate = txtOrderComplianceDate.Text.Trim();
        string Remarks = txtRemarks.Text.Trim();
        int OICID = ddlOICNameOpen.SelectedIndex > 0 ? Convert.ToInt32(ddlOICNameOpen.SelectedItem.Value) : 0;
        int CaseSubjectId = ddlCaseSubject.SelectedIndex > 0 ? Convert.ToInt32(ddlCaseSubject.SelectedItem.Value) : 0;
        string IsComplaince = ddlIsComplaince.SelectedIndex > 0 ? ddlIsComplaince.SelectedItem.Value : "";
        string IsOrderByDirection = ddlIsOrderByDirection.SelectedIndex > 0 ? ddlIsOrderByDirection.SelectedItem.Value : "";
        string Respondent = txtRespondent.Text.Trim();
        string strQuery = "update tbl_OrderByDirectionPendingCase set OICId=" + OICID + ",RespondentOffice='" + result +
            "',CaseSubjectId=" + ddlCaseSubject.SelectedItem.Value + ",Remarks='" + convertQuotes(Remarks) + "',OICMobileNo='" + convertQuotes(OICMobileNo) + "'";
        if (ddlRespondentOffice.SelectedIndex > 0) strQuery += ",RespondentOfficeId = " + RespondentOfficeId;
        //if (!string.IsNullOrEmpty(txtHearingDate.Text)) strQuery += "',HearingDate='" + Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd");
        if (!string.IsNullOrEmpty(OrderComplianceDate)) strQuery += ",OrderComplianceDate='" + Convert.ToDateTime(OrderComplianceDate, cult).ToString("yyyy/MM/dd") + "'";
        if (ddlIsComplaince.SelectedIndex > 0) strQuery += ",IsComplaince='" + IsComplaince + "'";
        if (ddlIsOrderByDirection.SelectedIndex > 0) strQuery += ",IsOrderByDirection='" + IsOrderByDirection + "'";
        if (ddlCaseSubSubject.SelectedIndex > 0) strQuery += ",CaseSubSubjectId=" + CaseSubSubjectId;

        strQuery += ",Respondent='" + convertQuotes(Respondent) + "' where UniqueNo='" + UId + "'";
        obj.ByTextQuery(strQuery);
        Response.Redirect("Order_By_Direction_Pending_Cases.aspx?CaseType=" + Convert.ToString(ViewState["CT"]));
        //string jScript = "<script>window.close();</script>";
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);

    }
    //public string convertQuotes(string str)
    //{
    //    return str.Replace("'", "''");
    //}
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Order_By_Direction_Pending_Cases.aspx?CaseType=" + Convert.ToString(ViewState["CT"]));
        //string jScript = "<script>window.close();</script>";
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "keyClientBlock", jScript);
    }

    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubSubj_Id,CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectId=" + Convert.ToInt32(ddlCaseSubject.SelectedItem.Value));
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {

            ddlCaseSubSubject.DataSource = dsCase.Tables[0];
            ddlCaseSubSubject.DataTextField = "CaseSubSubject";
            ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
            ddlCaseSubSubject.DataBind();
            ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlCaseSubSubject.DataSource = null;
            ddlCaseSubSubject.DataBind();
            ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
}