using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_CaseSubjectMaster : System.Web.UI.Page
{
    DataSet ds = null;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_Id"] != null)
        {
            if (!IsPostBack)
            {
                BindGridCaseSubject();
                lblMsg.Text = "";
                lblRecord.Text = "";
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }
    private void BindGridCaseSubject()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("Sp_CaseSubject", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt = (DataTable)ViewState["dtCol"];
                DataTable dt = ds.Tables[0];
                grdCaseSubject.DataSource = dt;
                grdCaseSubject.DataBind();
            }
        }
        catch (Exception ex)
        {

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
                    ds = objdb.ByProcedure("Sp_CaseSubject", new string[] { "flag", "CaseSubject", "CaseSubjectCode", "CaseSubjectDetail","CreatedBy","CreatedByIP" }, new string[] {
                        "1",txtCaseSubject.Text.Trim(),txtCaseSubjectCode.Text.Trim(),txtCaseSubjectDetail.Text.Trim(),ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress()}, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["EditID"].ToString() != "" && ViewState["EditID"].ToString() != null)
                {
                    ds = objdb.ByProcedure("Sp_CaseSubject", new string[] { "flag", "CaseSubject", "CaseSubjectCode", "CaseSubjectDetail", "ModiFyBy", "ModiFyByIP", "CaseSubjectID" }, new string[] {
                        "4",txtCaseSubject.Text.Trim(),txtCaseSubjectCode.Text.Trim(),txtCaseSubjectDetail.Text.Trim(),ViewState["Emp_Id"].ToString(),objdb.GetLocalIPAddress(),ViewState["EditID"].ToString()}, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtCaseSubject.Text = "";
                        txtCaseSubjectCode.Text = "";
                        txtCaseSubjectDetail.Text = "";
                    }
                }
                BindGridCaseSubject();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
        }
    }
    protected void chkActice_CheckedChanged(object sender, EventArgs e)
    {
        try
        {

            HiddenField hdnCaseSubjectID = (HiddenField)((CheckBox)sender).Parent.FindControl("hdnCaseSubjectID");

            ds = objdb.ByProcedure("Sp_CaseSubject", new string[] { "flag", "CaseSubjectID" }, new string[] { "3", hdnCaseSubjectID.Value }, "dataset");



            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
        }
    }


    protected void grdCaseSubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCaseSubject.PageIndex = e.NewPageIndex;
        BindGridCaseSubject();
    }
    protected void grdCaseSubject_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditDetails")
            {
                ViewState["EditID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblCaseSubjectCode = (Label)row.FindControl("lblCaseSubjectCode");
                Label lblCaseSubject = (Label)row.FindControl("lblCaseSubject");
                Label lblCaseSubjectDetail = (Label)row.FindControl("lblCaseSubjectDetail");
            
                txtCaseSubjectCode.Text = lblCaseSubjectCode.Text;
                txtCaseSubject.Text = lblCaseSubject.Text;
                txtCaseSubjectDetail.Text = lblCaseSubjectDetail.Text;
                btnSave.Text = "Update";
                ViewState["EditID"] = e.CommandArgument;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
        }
    }
}