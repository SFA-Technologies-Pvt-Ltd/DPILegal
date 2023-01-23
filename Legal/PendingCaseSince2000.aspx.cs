using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_PendingCaseSince2000 : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
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
    //private void GetCourt()
    //{
    //    try
    //    {
    //        dsCase = obj.ByDataSet("select distinct Court from tbl_OldCaseDetail order by Court");
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
            dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks from tbl_OldCaseDetail where CaseType='" + Convert.ToString(CaseType) + "'");
            if (dsCase.Tables[0].Rows.Count > 0)
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
        //BindGrid(Request.QueryString["CaseType"]);
        bindGridData();
    }

    protected void grdCaseTypeDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField hdnUId = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnUId");
        TextBox txtOICMobileNo = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtOICMobileNo");
        TextBox txtRespondentOffice = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondentOffice");
        TextBox txtRemarks = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRemarks");
        DropDownList ddlOICName = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlOICName");
        DropDownList ddlCaseSubject = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlCaseSubject");

        obj.ByTextQuery("update tbl_OldCaseDetail set RespondentOffice='" + txtRespondentOffice.Text + "',OICId=" + ddlOICName.SelectedItem.Value + ",CaseSubjectId=" + ddlCaseSubject.SelectedItem.Value + ",Remarks='" + txtRemarks.Text + "',OICMobileNo='" + txtOICMobileNo.Text + "' where UniqueNo='" + Convert.ToString(hdnUId.Value) + "'");
        grdCaseTypeDetail.EditIndex = -1;
        BindGrid(Request.QueryString["CaseType"]);
    }

    private void bindGridData()
    {
        if (ViewState["dt"] != null)
        {
            dtCase = (DataTable)ViewState["dt"];
            grdCaseTypeDetail.DataSource = dtCase;
            grdCaseTypeDetail.DataBind();
        }
    }

    protected void grdCaseTypeDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCaseTypeDetail.EditIndex = -1;
        bindGridData();
    }



    protected void grdCaseTypeDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int OICId = 0;
        int CaseSubjectId = 0;
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) <= 0)
            {
                Label lblOICName = e.Row.FindControl("lblOICName") as Label;
                Label lblOICMobileNo = e.Row.FindControl("lblOICMobileNo") as Label;
                Label lblCaseSubjectId = e.Row.FindControl("lblCaseSubjectId") as Label;
                if (lblOICName.Text != "" && lblOICMobileNo.Text != "")
                {
                    OICId = Convert.ToInt32(lblOICName.Text);
                    dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId + " and Isactive=1");
                    if (dsCase.Tables[0].Rows.Count > 0)
                    {
                        lblOICName.Text = dsCase.Tables[0].Rows[0]["OICName"].ToString();
                        lblOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    }
                }
                if (lblCaseSubjectId.Text != "")
                {
                    CaseSubjectId = Convert.ToInt32(lblCaseSubjectId.Text);
                    dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject where CaseSubjectID=" + CaseSubjectId);
                    if (dsCase.Tables[0].Rows.Count > 0)
                    {
                        lblCaseSubjectId.Text = dsCase.Tables[0].Rows[0]["CaseSubject"].ToString();
                    }
                }
            }
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
               
                HiddenField hdnUId = e.Row.FindControl("hdnUId") as HiddenField;
                dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks from tbl_OldCaseDetail where CaseType='" + Convert.ToString(Request.QueryString["CaseType"]) + "' and UniqueNo='"+hdnUId.Value+"'");
                if (dsCase.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICId"].ToString()) && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubjectId"].ToString()))
                {
                   OICId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["OICId"]);
                    CaseSubjectId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubjectId"]);
                }

                //int index = e.Row.RowIndex;
                Label lblOICName = e.Row.FindControl("lblOICName") as Label;
                

                TextBox txtOICMobileNo = e.Row.FindControl("txtOICMobileNo") as TextBox;
                dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                if (dsCase.Tables[0].Rows.Count > 0)
                {
                    txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                }

                DropDownList ddlOICName = e.Row.FindControl("ddlOICName") as DropDownList;

                dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                if (dsCase.Tables[0].Rows.Count > 0)
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


                DropDownList ddlCaseSubject = (DropDownList)e.Row.FindControl("ddlCaseSubject");
                dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject");
                if (dsCase.Tables[0].Rows.Count > 0)
                {

                    ddlCaseSubject.DataSource = dsCase.Tables[0];
                    ddlCaseSubject.DataTextField = "CaseSubject";
                    ddlCaseSubject.DataValueField = "CaseSubjectID";
                    ddlCaseSubject.DataBind();
                    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                    ddlCaseSubject.Items.FindByValue(CaseSubjectId.ToString()).Selected = true;
                }
                else
                {
                    ddlCaseSubject.DataSource = null;
                    ddlCaseSubject.DataBind();
                    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                }
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
        if (dsCase.Tables[0].Rows.Count > 0)
        {
            txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        }
    }
}