using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_CourtTypeMaster : System.Web.UI.Page
{
    DataSet ds = null;
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Emp_Id"] != null)
            {
                if (!IsPostBack)
                {
                    BindGridCourtType();
                    lblMsg.Text = "";
                    lblRecord.Text = "";

                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
    private void BindGridCourtType()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("Sp_CourtType", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt = (DataTable)ViewState["dtCol"];
                DataTable dt = ds.Tables[0];
                grdCourtType.DataSource = dt;
                grdCourtType.DataBind();
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
            ds = objdb.ByProcedure("Sp_CourtType", new string[] { "flag", "CourtTypeName", }, new string[] {
                        "1",txtCourtType.Text}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtCourtType.Text = "";
                }
            }
            BindGridCourtType();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
        }
    }
    //protected void chkActice_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        HiddenField hdnCaseSubjectID = (HiddenField)((CheckBox)sender).Parent.FindControl("hdnCaseSubjectID");
    //        ds = objdb.ByProcedure("Sp_CaseSubject", new string[] { "flag", "CaseSubjectID" }, new string[] { "3", hdnCaseSubjectID.Value }, "dataset");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
    //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
    //            {
    //                lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
    //                txtCourtType.Text = "";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
    //    }
    //}


    protected void grdCaseSubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCourtType.PageIndex = e.NewPageIndex;
        BindGridCourtType();
    }
}