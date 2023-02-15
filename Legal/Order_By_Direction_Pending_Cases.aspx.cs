using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Legal_Order_By_Drection_Pending_Cases : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {

            if (!IsPostBack)
            {

                bindDropDowns();

                if (!string.IsNullOrEmpty(Request.QueryString["CaseType"]))
                {
                    BindGrid(Request.QueryString["CaseType"]);
                    spnCaseType.InnerHtml = Request.QueryString["CaseType"] + " Case Type Details";
                }
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
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
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }


    //private void GetCourt()
    //{
    //    try
    //    {
    //        dsCase = obj.ByDataSet("select distinct Court from tbl_OrderByDirectionPendingCase order by Court");
    //        if (dsCase.Tables[0].Rows.Count > 0)
    //        {
    //            ddlCourt.DataSource = dsCase;
    //            ddlCourt.DataTextField = "Court";
    //            ddlCourt.DataValueField = "Court";
    //            ddlCourt.DataBind();
    //            ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //        else
    //        {
    //            ddlYear.DataSource = null;
    //            ddlYear.DataBind();
    //            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }

    //}
    protected void BindGrid(string CaseType)
    {
        try
        {
            dsCase = obj.ByDataSet("select distinct UniqueNo," +
                  "CaseType," +
                 " FilingNo, Court, Petitioner, Respondent, RespondentOffice, OICId, OICMobileNo, " +
                "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId=a.CaseSubjectId) CaseSubject,CaseSubjectId," +
                "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id=a.CaseSubSubjectId) CaseSubSubject" +
                ",Remarks,HearingDate,CaseNo,OrderComplianceDate,IsComplaince,CaseSubSubjectId,IsOrderByDirection,RespondentOfficeId from tbl_OrderByDirectionPendingCase a where CaseType='" + Convert.ToString(CaseType) + "'");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = null;
                ViewState["dt"] = dsCase.Tables[0];
                grdCaseTypeDetail.DataSource = dsCase.Tables[0];
                grdCaseTypeDetail.DataBind();
            }
            else
            {
                grdCaseTypeDetail.DataSource = null;
                grdCaseTypeDetail.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No record found')", true);
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void grdCaseTypeDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<script type=\"text/javascript\"  language=\"javascript\">function showMsg(){return confirm(\"This image name already exists, do you want to replace it?\");}</script>", true);
        //LinkButton lnkEdit = (LinkButton)row.FindControl("lnkEdit");
        //lnkEdit.OnClientClick = "return showMsg()";

        //GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent
        //using (GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer)
        //{
        //    string Id = Convert.ToString(e.CommandArgument);
        //    dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId," +
        //        "Remarks,HearingDate,CaseNo,OrderComplianceDate,IsComplaince,CaseSubSubjectId,IsOrderByDirection,RespondentOfficeId from tbl_OrderByDirectionPendingCase where UniqueNo='" + Convert.ToString(Id) + "'");
        //    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        //    {
        //        //Label lblRespondent = (Label)row.FindControl("lblRespondent");
        //        //Label lblIsOrderByDirection = (Label)row.FindControl("lblIsOrderByDirection");
        //        //Label lblRespondentOfficeId = (Label)row.FindControl("lblRespondentOfficeId");
        //        //Label lblCaseSubjectId = (Label)row.FindControl("lblCaseSubjectId");
        //        //Label lblCaseSubSubjectId = (Label)row.FindControl("lblCaseSubSubjectId");
        //        //Label lblOICName = (Label)row.FindControl("lblOICName");

        //        //Label lblOICMobileNo = (Label)row.FindControl("lblOICMobileNo");
        //        //Label lblOrderComplianceDate = (Label)row.FindControl("lblOrderComplianceDate");
        //        //Label lblRemarks = (Label)row.FindControl("lblRemarks");
        //        //Label lblIsComplaince = (Label)row.FindControl("lblIsComplaince");

        //        if (dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString().Trim() != "") ddlIsOrderByDirection.Items.FindByValue(dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString().Trim()).Selected = true; else ddlIsOrderByDirection.Items.FindByValue("0").Selected = true;
        //        txtFilingNo.Text = row.Cells[2].Text;
        //        txtCourt.Text = row.Cells[3].Text;
        //        txtPetitioner.Text = row.Cells[4].Text;
        //        txtRespondent.Text = dsCase.Tables[0].Rows[0]["Respondent"].ToString();
        //        if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["RespondentOfficeId"].ToString().Trim()))
        //            ddlRespondentOffice.Items.FindByValue(dsCase.Tables[0].Rows[0]["RespondentOfficeId"].ToString().Trim()).Selected = true;
        //        else
        //            ddlRespondentOffice.SelectedIndex = 0;
        //        if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubjectId"].ToString().Trim()))
        //            ddlCaseSubject.Items.FindByValue(dsCase.Tables[0].Rows[0]["CaseSubjectId"].ToString().Trim()).Selected = true;
        //        else
        //            ddlCaseSubject.SelectedIndex = 0;

        //        if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICId"].ToString().Trim()))
        //            ddlOICNameOpen.Items.FindByValue(dsCase.Tables[0].Rows[0]["OICId"].ToString().Trim()).Selected = true;
        //        else
        //            ddlOICNameOpen.SelectedIndex = 0;
        //        if (dsCase.Tables[0].Rows[0]["OrderComplianceDate"].ToString().Trim() != "")
        //            txtOrderComplianceDate.Text = dsCase.Tables[0].Rows[0]["OrderComplianceDate"].ToString().Trim();

        //        txtRemarks.Text = dsCase.Tables[0].Rows[0]["Remarks"].ToString().Trim();
        //        if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["IsComplaince"].ToString().Trim()))
        //            ddlIsComplaince.Items.FindByValue(dsCase.Tables[0].Rows[0]["IsComplaince"].ToString().Trim()).Selected = true;
        //        else
        //            ddlIsComplaince.SelectedIndex = 0;
        //        //txtContactName.Text = row.Cells[1].Text;
        //        //txtCompany.Text = row.Cells[2].Text;
        //        if (dsCase.Tables[0].Rows[0]["CaseSubSubjectId"].ToString().Trim() != "")
        //        {
        //            dsCase = obj.ByDataSet("select CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubSubjId=" + Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubSubjectId"].ToString()));
        //            if (dsCase.Tables.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString()))
        //            {
        //                txtCaseSubSubject.Text = dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString();
        //            }
        //        }
        //        if (ddlOICNameOpen.SelectedIndex > 0)
        //        {
        //            int OICID = Convert.ToInt32(ddlOICNameOpen.SelectedItem.Value);
        //            dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICID);
        //            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        //            {
        //                txtOICMobileNoOpen.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        //            }
        //        }
        //    }

        //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MyModal", "alertMessage()", true);
        //}

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(@"<script type='text/javascript'>");
        //sb.Append("$('#EditModal').modal('show');");
        //sb.Append(@"</script>");
        //ScriptManager.RegisterStartupScript(UP1, UP1.GetType(), "modal", sb.ToString(), false);
    }

    protected void grdCaseTypeDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
    }
    public string convertQuotes(string str)
    {
        return str.Replace("'", "''");
    }

    protected void grdCaseTypeDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField hdnUId = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnUId");
        HiddenField hdnCaseNo = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnCaseNo");
        TextBox txtOICMobileNo = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtOICMobileNo");
        TextBox txtCaseSubSubjectId = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtCaseSubSubjectId");
        DropDownList ddlRespondentOffice = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlRespondentOffice");
        // TextBox txtHearingDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtHearingDate");
        TextBox txtOrderComplianceDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtOrderComplianceDate");
        TextBox txtRemarks = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRemarks");
        DropDownList ddlOICName = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlOICName");
        DropDownList ddlCaseSubject = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlCaseSubject");
        DropDownList ddlIsComplaince = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlIsComplaince");
        DropDownList ddlIsOrderByDirection = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlIsOrderByDirection");
        TextBox txtRespondent = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondent");
        string strQuery = "update tbl_OrderByDirectionPendingCase set OICId=" + ddlOICName.SelectedItem.Value +
            "',CaseSubjectId=" + ddlCaseSubject.SelectedItem.Value + ",Remarks='" + convertQuotes(txtRemarks.Text.Trim()) + "',OICMobileNo='" + convertQuotes(txtOICMobileNo.Text.Trim());
        if (ddlRespondentOffice.SelectedIndex > 0) strQuery += "',RespondentOfficeId = " + Convert.ToInt32(ddlRespondentOffice.SelectedItem.Value.Trim());
        //if (!string.IsNullOrEmpty(txtHearingDate.Text)) strQuery += "',HearingDate='" + Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd");
        if (!string.IsNullOrEmpty(txtOrderComplianceDate.Text)) strQuery += ",OrderComplianceDate='" + Convert.ToDateTime(txtOrderComplianceDate.Text, cult).ToString("yyyy/MM/dd");
        if (ddlIsComplaince.SelectedIndex > 0) strQuery += "',IsComplaince='" + ddlIsComplaince.SelectedItem.Text;
        if (ddlIsOrderByDirection.SelectedIndex > 0) strQuery += "',IsOrderByDirection='" + ddlIsOrderByDirection.SelectedItem.Text;
        if (!string.IsNullOrEmpty(txtCaseSubSubjectId.Text)) strQuery += "',CaseSubSubjectId='" + txtCaseSubSubjectId.Text;

        strQuery += "',Respondent='" + convertQuotes(txtRespondent.Text.Trim()) + "' where UniqueNo='" + Convert.ToString(hdnUId.Value) + "'";
        obj.ByTextQuery(strQuery);
        //grdCaseTypeDetail.EditIndex = -1;
        BindGrid(Request.QueryString["CaseType"]);
    }

    private void bindGridData()
    {
        if (ViewState["dtsearch"] != null)
        {
            dtCase = (DataTable)ViewState["dtsearch"];
            grdCaseTypeDetail.DataSource = dtCase;
            grdCaseTypeDetail.DataBind();
        }
        else
        {
            dtCase = (DataTable)ViewState["dt"];
            grdCaseTypeDetail.DataSource = dtCase;
            grdCaseTypeDetail.DataBind();
        }
    }

    protected void grdCaseTypeDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
    }

    protected void grdCaseTypeDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int OICId = 0;
        //int CaseSubjectId = 0;
        //int RespondentOfficeId = 0;
        string RES_Office = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRespondentOffice = (Label)e.Row.FindControl("lblRespondentOfficeId");

            if (!string.IsNullOrEmpty(lblRespondentOffice.Text))
            {

                string[] RespondentOfficeStr = convertQuotes(lblRespondentOffice.Text).Trim().Split(',');

                for (int i = 0; i < RespondentOfficeStr.Length; i++)
                {
                    dsCase = obj.ByDataSet("select Respondent_Office,Respondent_office_Id from  tblRespondentOffice where Respondent_office_Id=" + Convert.ToInt32(RespondentOfficeStr[i]));
                    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
                        RES_Office = RES_Office + dsCase.Tables[0].Rows[0]["Respondent_Office"].ToString() + ",";
                }
            }
            lblRespondentOffice.Text = RES_Office;
            Label lblOICName = (Label)e.Row.FindControl("lblOICName");
            if (lblOICName.Text != "")
            {
                OICId = Convert.ToInt32(lblOICName.Text);
                dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId + " and Isactive=1");
                if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
                    lblOICName.Text = dsCase.Tables[0].Rows[0]["OICName"].ToString();
            }


        }
    }

    protected void ddlOICName_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        DropDownList ddlOICName = (DropDownList)grow.FindControl("ddlOICName");
        DropDownList ddl_state = (DropDownList)grow.FindControl("ddl_state");
        int OICId = Convert.ToInt32(ddlOICName.SelectedValue);
        // Label lblOICMobileNo = (Label)gvRow.FindControl("lblOICMobileNo");
        TextBox txtOICMobileNo = grow.FindControl("txtOICMobileNo") as TextBox;

        dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId);
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        }
    }

    //protected void grdCaseTypeDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdCaseTypeDetail.PageIndex = e.NewPageIndex;
    //    BindGrid(Request.QueryString["CaseType"]);
    //}



    #region MyRegion
    //protected void lnkEdit_Click(object sender, EventArgs e)
    //{
    //    //GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent;
    //    // HiddenField hdnUniqueNo = (HiddenField)row.FindControl("hdnUniqueNo");

    //    //string ResOffID = "";
    //    //string IsOrDir = "";
    //    //string FilingNo = "";
    //    //string Remarks = "";
    //    //string HearingDate = "";
    //    //string CaseNo = "";
    //    //string OrderComplianceDate = "";
    //    //string IsComplaince = "";
    //    //string CaseSubSubjectId = "";
    //    //string IsOrderByDirection = "";
    //    //string RespondentOfficeId = "";
    //    ////findGridData(sender);
    //    //using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
    //    //{
    //    //    HiddenField hdnUniqueNo = (HiddenField)row.FindControl("hdnUniqueNo");
    //    //    dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId," +
    //    //       "Remarks,HearingDate,CaseNo,OrderComplianceDate,IsComplaince,CaseSubSubjectId,IsOrderByDirection,RespondentOfficeId from tbl_OrderByDirectionPendingCase where UniqueNo='" + Convert.ToString(hdnUniqueNo.Value) + "'");
    //    //    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
    //    //    {

    //    //        ResOffID = dsCase.Tables[0].Rows[0]["RespondentOfficeId"].ToString().Trim();
    //    //        IsOrDir = dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString().Trim();


    //    //        //txtFilingNo.Text = row.Cells[2].Text;
    //    //        //txtCourt.Text = row.Cells[3].Text;
    //    //        //txtPetitioner.Text = row.Cells[4].Text;
    //    //        //txtRespondent.Text = dsCase.Tables[0].Rows[0]["Respondent"].ToString();

    //    //        //if (dsCase.Tables[0].Rows[0]["OrderComplianceDate"].ToString().Trim() != "")
    //    //        //    txtOrderComplianceDate.Text = dsCase.Tables[0].Rows[0]["OrderComplianceDate"].ToString().Trim();

    //    //        //txtRemarks.Text = dsCase.Tables[0].Rows[0]["Remarks"].ToString().Trim();


    //    //        //if (dsCase.Tables[0].Rows[0]["CaseSubSubjectId"].ToString().Trim() != "")
    //    //        //{
    //    //        //    dsCase = obj.ByDataSet("select CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubSubjId=" + Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubSubjectId"].ToString()));
    //    //        //    if (dsCase.Tables.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString()))
    //    //        //    {
    //    //        //        txtCaseSubSubject.Text = dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString();
    //    //        //    }
    //    //        //}
    //    //        //if (ddlOICNameOpen.SelectedIndex > 0)
    //    //        //{
    //    //        //    int OICID = Convert.ToInt32(ddlOICNameOpen.SelectedItem.Value);
    //    //        //    dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICID);
    //    //        //    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
    //    //        //    {
    //    //        //        txtOICMobileNoOpen.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
    //    //        //    }
    //    //        //}
    //    //    }
    //    //}

    //    //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //    //sb.Append(@"<script type='text/javascript'>");
    //    //sb.Append("$('#EditModal').modal('show');");
    //    //sb.Append(@"</script>");
    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MyModal", sb.ToString(), false);
    //    //// filldropdown(ResOffID, IsOrDir);
    //} 
    #endregion

    private void filldropdown(string ResOffID, string IsOrDir)
    {
        if (!string.IsNullOrEmpty(ResOffID))
            ddlRespondentOffice.Items.FindByValue(ResOffID).Selected = true;
        else
            ddlRespondentOffice.SelectedIndex = 0;
        if (!string.IsNullOrEmpty(IsOrDir))
            ddlIsOrderByDirection.Items.FindByValue(IsOrDir).Selected = true;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            try
            {
                dsCase = obj.ByDataSet("select distinct UniqueNo,CaseType,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo, " +
              "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId=a.CaseSubjectId) CaseSubject,CaseSubjectId," +
              "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id=a.CaseSubSubjectId) CaseSubSubject" +
              ",Remarks,HearingDate,CaseNo,OrderComplianceDate,IsComplaince,CaseSubSubjectId,IsOrderByDirection,RespondentOfficeId  from tbl_OrderByDirectionPendingCase a " +
               "where CaseType='" + Convert.ToString(Request.QueryString["CaseType"]) + "' and FilingNo like '%" + Convert.ToString(txtSearch.Text.Trim()) + "%'");
                if (dsCase.Tables[0].Rows.Count > 0)
                {
                    ViewState["dtsearch"] = null;
                    ViewState["dtsearch"] = dsCase.Tables[0];
                    grdCaseTypeDetail.DataSource = dsCase.Tables[0];
                    grdCaseTypeDetail.DataBind();
                }
                else
                {
                    grdCaseTypeDetail.DataSource = null;
                    grdCaseTypeDetail.DataBind();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No record found')", true);
                }

            }
            catch (Exception ex)
            {

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        ViewState["dtsearch"] = null;
        BindGrid(Request.QueryString["CaseType"]);
        txtSearch.Text = "";
    }
}

