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
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (Request.QueryString["CaseID"] != "")
            {
                if (!IsPostBack)
                {
                    ViewState["CaseID"] = Request.QueryString["CaseID"].ToString();
                    BindCaseDetail();
                }
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void BindCaseDetail()
    {
        try
        {
            // lblMsg.Text = "";
            grdPetitionerDtl.DataSource = null;
            grdPetitionerDtl.DataBind();
            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdHearingDtl.DataSource = null;
            GrdHearingDtl.DataBind();

            ds = obj.ByProcedure("USP_ViewWPPendingCaseFullDtlRpt", new string[] { "Case_ID" }
                , new string[] { ViewState["CaseID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdPetitionerDtl.DataSource = ds;
                grdPetitionerDtl.DataBind();
                GrdResponderDtl.DataSource = ds;
                GrdResponderDtl.DataBind();
                if (ds.Tables[0].Rows[0]["HearingDtl"].ToString() != "")
                {
                    FieldHearingDtl.Visible = true;
                    GrdHearingDtl.DataSource = ds;
                    GrdHearingDtl.DataBind();
                }
            }
            else
            {
                grdPetitionerDtl.DataSource = null;
                grdPetitionerDtl.DataBind();
                GrdResponderDtl.DataSource = null;
                GrdResponderDtl.DataBind();
                GrdHearingDtl.DataSource = null;
                GrdHearingDtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}