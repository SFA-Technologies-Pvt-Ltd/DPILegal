using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_Case_SubSubjectMaster : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"];
                ViewState["Office_Id"] = Session["Office_Id"];
                FillCaseSubject();
                BindGrid();
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx",false);
        }
    }

    protected void BindGrid()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Select_CaseSub_SubjectMaster", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdSub_Subect.DataSource = ds;
                grdSub_Subect.DataBind();
                grdSub_Subect.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSub_Subect.UseAccessibleHeader = true;
            }
            
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("USP_Insert_CaseSub_SubjectMaster", new string[] { "CaseSubSubject", "CaseSubjectID", "CreatedBy", "CreatedByIP", "Office_Id" }, new string[] {
                        txtSubsubject.Text.Trim(),ddlcaseSubject.SelectedValue,ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress(), ViewState["Office_Id"].ToString()}, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["EditID"].ToString() != "" && ViewState["EditID"].ToString() != null)
                {
                    ds = objdb.ByProcedure("USP_Update_CaseSub_SubjectMaster", new string[] { "CaseSubSubject", "CaseSubjectID", "CreatedBy", "CreatedByIP", "Office_Id", "CaseSubSubjId" }, new string[] {
                        txtSubsubject.Text.Trim(),ddlcaseSubject.SelectedValue,ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress(),ViewState["Office_Id"].ToString(),ViewState["EditID"].ToString()}, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        txtSubsubject.Text = "";
                        ddlcaseSubject.ClearSelection();
                        BindGrid();
                        btnSave.Text = "Save";
                        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                    }
                    else
                    {
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #region FillCaseSubject
    protected void FillCaseSubject()
    {
        try
        {
            ddlcaseSubject.Items.Clear();
            ds = objdb.ByDataSet("select CaseSubject, CaseSubjectID from tbl_LegalMstCaseSubject where Isactive = 1");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlcaseSubject.DataValueField = "CaseSubjectID";
                ddlcaseSubject.DataTextField = "CaseSubject";
                ddlcaseSubject.DataSource = ds;
                ddlcaseSubject.DataBind();
            }
            ddlcaseSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }

    }
    #endregion
    protected void grdSub_Subect_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdSub_Subect.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void grdSub_Subect_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                lblMsg.Text = "";
                ViewState["EditID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblCaseSub_Subject = (Label)row.FindControl("lblSub_CaseSubject");
                Label lblCaseSubjectID = (Label)row.FindControl("lblSubjectID");
                btnSave.Text = "Update";
                ViewState["EditID"] = e.CommandArgument;
                grdSub_Subect.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSub_Subect.UseAccessibleHeader = true;
                txtSubsubject.Text = lblCaseSub_Subject.Text;
                ddlcaseSubject.ClearSelection();
                ddlcaseSubject.Items.FindByValue(lblCaseSubjectID.Text).Selected = true;
            }
            if (e.CommandName == "DeleteDetails")
            {
                int CaseSubSubj_Id = Convert.ToInt32(e.CommandArgument);
                objdb.ByTextQuery("delete from tbl_CaseSubSubjectMaster where CaseSubSubj_Id=" + CaseSubSubj_Id);
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}