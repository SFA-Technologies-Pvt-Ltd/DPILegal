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
public partial class Legal_Order_By_Direction_Count_Cases : System.Web.UI.Page
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
            dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks,HearingDate,CaseNo,OrderComplianceDate,IsComplaince,CaseSubSubjectId,IsOrderByDirection from tbl_OrderByDirectionPendingCase where CaseType='" + Convert.ToString(CaseType) + "' and IsOrderByDirection='Yes'");
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

    }

    protected void grdCaseTypeDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdCaseTypeDetail.EditIndex = e.NewEditIndex;
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
        //bindGridData();
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
        //TextBox txtCaseSubSubjectId = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtCaseSubSubjectId");
        //TextBox txtRespondentOffice = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondentOffice");
        // TextBox txtHearingDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtHearingDate");
        TextBox txtOrderComplianceDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtOrderComplianceDate");
        TextBox txtRemarks = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRemarks");
        DropDownList ddlOICName = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlOICName");
        //DropDownList ddlCaseSubject = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlCaseSubject");
        DropDownList ddlIsComplaince = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlIsComplaince");
        //DropDownList ddlIsOrderByDirection = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlIsOrderByDirection");
        // TextBox txtRespondent = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondent");
        string strQuery = "update tbl_OrderByDirectionPendingCase set OICId=" + ddlOICName.SelectedItem.Value
            + ",Remarks='" + convertQuotes(txtRemarks.Text.Trim()) + "',OICMobileNo='" + convertQuotes(txtOICMobileNo.Text.Trim());
        //if (!string.IsNullOrEmpty(txtHearingDate.Text)) strQuery += "',HearingDate='" + Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd");
        if (!string.IsNullOrEmpty(txtOrderComplianceDate.Text)) strQuery += "',OrderComplianceDate='" + Convert.ToDateTime(txtOrderComplianceDate.Text, cult).ToString("yyyy/MM/dd");
        if (ddlIsComplaince.SelectedIndex > 0) strQuery += "',IsComplaince='" + ddlIsComplaince.SelectedItem.Text;
        //  if (ddlIsOrderByDirection.SelectedIndex > 0) strQuery += "',IsOrderByDirection='" + ddlIsOrderByDirection.SelectedItem.Text;
        //  if (!string.IsNullOrEmpty(txtCaseSubSubjectId.Text)) strQuery += "',CaseSubSubjectId='" + txtCaseSubSubjectId.Text;

        // strQuery += "',Respondent='" + convertQuotes(txtRespondent.Text.Trim())
        strQuery += "' where UniqueNo='" + Convert.ToString(hdnUId.Value) + "'";
        obj.ByTextQuery(strQuery);
        grdCaseTypeDetail.EditIndex = -1;
        BindGrid(Request.QueryString["CaseType"]);

        ////HiddenField hdnOICId = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnOICId");
        //dsCase = obj.ByDataSet("select a.OICName,a.OICEmailID,a.OICMobileNo,b.CirlceName,c.ZoneName,d.OfficeName,e.UserType_Name designation,f.Division_Name from tblOICMaster a " +
        //    "inner join tblCircleMaster b on b.Circle_ID = a.Circle_ID " +
        //    "inner join tblZoneMaster c on c.Zone_ID = a.Zone_ID " +
        //    "inner join  tblOfficeMaster  d on d.Office_Id = a.Office_ID " +
        //    "inner join tblLegal_UserTypeMaster e on e.UserType_Id = a.DesignationID " +
        //    "inner join tblDivisionMaster f on f.Division_ID = a.Division_ID " +
        //    "where OICMaster_ID = " + Convert.ToInt32(ddlOICName.SelectedItem.Value));
        //DataTable dt = (DataTable)dsCase.Tables[0];


        //if (dsCase.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICEmailID"].ToString()))
        //{

        //    EmailContent1 ObjEC = new EmailContent1();
        //    ObjEC.OIC_Name = dt.Rows[0]["OICName"].ToString();
        //    ObjEC.OIC_Mobile = dt.Rows[0]["OICMobileNo"].ToString();
        //    ObjEC.OIC_Email = dt.Rows[0]["OICEmailID"].ToString();
        //    ObjEC.OIC_Circle = dt.Rows[0]["CirlceName"].ToString();
        //    ObjEC.OIC_Designation = dsCase.Tables[0].Rows[0]["designation"].ToString();
        //    ObjEC.OIC_Office = dsCase.Tables[0].Rows[0]["OfficeName"].ToString();
        //    ObjEC.OIC_Zone = dsCase.Tables[0].Rows[0]["ZoneName"].ToString();
        //    ObjEC.OIC_Division = dsCase.Tables[0].Rows[0]["Division_Name"].ToString();

        //    ObjEC.Petitioner = grdCaseTypeDetail.Rows[e.RowIndex].Cells[4].Text;
        //    ObjEC.respondent = txtRespondent.Text.Trim();
        //    ObjEC.Case_Number = hdnCaseNo.Value;
        //    ObjEC.Curr_Date = DateTime.Now.ToString("dd-MM-yyyy");
        //    ObjEC.Curr_Year = DateTime.Now.Year.ToString();
        //    ObjEC.Court_Name = grdCaseTypeDetail.Rows[e.RowIndex].Cells[3].Text;
        //    //sendmail(ObjEC, "sfatech.bot@gmail.com");
        //}
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alertMessage()", true);
        //}
    }



    private void bindGridData()
    {
        if (ViewState["dtsearch"] != null)
        {
            dtCase = (DataTable)ViewState["dtsearch"];
            grdCaseTypeDetail.DataSource = dtCase;
            grdCaseTypeDetail.DataBind();
        }
    }

    protected void grdCaseTypeDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCaseTypeDetail.EditIndex = -1;
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
    }

    protected void grdCaseTypeDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int OICId = 0;
        int CaseSubjectId = 0;
        string RES_Office = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblRespondentOffice = (Label)e.Row.FindControl("txtRespondentOffice");

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
            //Label lblOICName1 = (Label)e.Row.FindControl("lblOICName");
            //if (lblOICName1.Text != "")
            //{
            //    OICId = Convert.ToInt32(lblOICName1.Text);
            //    dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId + " and Isactive=1");
            //    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            //        lblOICName1.Text = dsCase.Tables[0].Rows[0]["OICName"].ToString();
            //}

            if ((e.Row.RowState & DataControlRowState.Edit) <= 0)
            {
                Label lblOICName = e.Row.FindControl("lblOICName") as Label;
                Label lblOICMobileNo = e.Row.FindControl("lblOICMobileNo") as Label;
                Label lblCaseSubjectId = e.Row.FindControl("lblCaseSubjectId") as Label;
                Label lblCaseSubSubjectId = e.Row.FindControl("lblCaseSubSubjectId") as Label;

                if (lblOICName.Text != "" && lblOICMobileNo.Text != "" && lblOICName.Text != "0")
                {
                    OICId = Convert.ToInt32(lblOICName.Text);
                    dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId + " and Isactive=1");
                    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
                    {
                        lblOICName.Text = dsCase.Tables[0].Rows[0]["OICName"].ToString();
                        lblOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    }
                }
                if (lblCaseSubjectId.Text != "" && lblCaseSubjectId.Text != "0" && lblCaseSubSubjectId.Text != "0" && lblCaseSubSubjectId.Text != "")
                {
                    CaseSubjectId = Convert.ToInt32(lblCaseSubjectId.Text);
                    dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject where CaseSubjectID=" + CaseSubjectId);

                    DataSet dsCase1 = obj.ByDataSet("select CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubSubj_Id=" + Convert.ToInt32(lblCaseSubSubjectId.Text) + " and CaseSubjectID = " + CaseSubjectId);
                    if (dsCase1.Tables.Count > 0 && dsCase1.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase1.Tables[0].Rows[0]["CaseSubSubject"].ToString()))
                        lblCaseSubSubjectId.Text = dsCase1.Tables[0].Rows[0]["CaseSubSubject"].ToString();

                    if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
                        lblCaseSubjectId.Text = dsCase.Tables[0].Rows[0]["CaseSubject"].ToString();
                }
            }
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {

                HiddenField hdnUId = e.Row.FindControl("hdnUId") as HiddenField;
                dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks,IsComplaince,CaseSubSubjectId,IsOrderByDirection from tbl_OrderByDirectionPendingCase where CaseType='" + Convert.ToString(Request.QueryString["CaseType"]) + "' and UniqueNo='" + hdnUId.Value + "'");
                if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICId"].ToString()) && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubjectId"].ToString()))
                {
                    OICId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["OICId"]);
                    CaseSubjectId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubjectId"]);
                    DropDownList ddlIsComplaince = e.Row.FindControl("ddlIsComplaince") as DropDownList;
                    //DropDownList ddlIsOrderByDirection = e.Row.FindControl("ddlIsOrderByDirection") as DropDownList;
                    if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["IsComplaince"].ToString()))
                        ddlIsComplaince.Items.FindByValue(dsCase.Tables[0].Rows[0]["IsComplaince"].ToString()).Selected = true;
                    //if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString()))
                    //    ddlIsOrderByDirection.Items.FindByValue(dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString()).Selected = true;
                }

                //int index = e.Row.RowIndex;



                // TextBox txtOICMobileNo = e.Row.FindControl("txtOICMobileNo") as TextBox;
                //dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                //if (dsCase.Tables[0].Rows.Count > 0)
                //{
                //    txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                //}

                DropDownList ddlOICName = e.Row.FindControl("ddlOICName") as DropDownList;


                dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
                {
                    ddlOICName.DataSource = dsCase.Tables[0];
                    ddlOICName.DataTextField = "OICName";
                    ddlOICName.DataValueField = "OICMaster_ID";
                    ddlOICName.DataBind();
                    ddlOICName.Items.Insert(0, new ListItem("Select", "0"));
                    ddlOICName.Items.FindByValue(OICId.ToString()).Selected = true;
                }
                else
                {
                    ddlOICName.DataSource = null;
                    ddlOICName.DataBind();
                    ddlOICName.Items.Insert(0, new ListItem("Select", "0"));
                }


                //DropDownList ddlCaseSubject = (DropDownList)e.Row.FindControl("ddlCaseSubject");
                //dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject");
                //if (dsCase.Tables[0].Rows.Count > 0)
                //{

                //    ddlCaseSubject.DataSource = dsCase.Tables[0];
                //    ddlCaseSubject.DataTextField = "CaseSubject";
                //    ddlCaseSubject.DataValueField = "CaseSubjectID";
                //    ddlCaseSubject.DataBind();
                //    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                //    ddlCaseSubject.Items.FindByValue(CaseSubjectId.ToString()).Selected = true;
                //}
                //else
                //{
                //    ddlCaseSubject.DataSource = null;
                //    ddlCaseSubject.DataBind();
                //    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                //}

                ///TextBox txtCaseSubSubjectId = (TextBox)e.Row.FindControl("txtCaseSubSubjectId");
                //dsCase = obj.ByDataSet("select * from tbl_CaseSubSubjectMaster where CaseSubSubjId" + Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubSubjectId"]));
                //txtCaseSubSubjectId.Text = dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString();




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

    protected void grdCaseTypeDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCaseTypeDetail.PageIndex = e.NewPageIndex;
        if (ViewState["dtsearch"] != null)
            bindGridData();
        else
            BindGrid(Request.QueryString["CaseType"]);
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

