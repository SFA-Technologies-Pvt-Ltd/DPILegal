using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_OICWiseCaseDtl : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetOicName();
                GetCaseType();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx");
        }
    }

    private void GetOicName()
    {
        try
        {
            ds = obj.ByDataSet("select OICNAME,Respondent_ID from  tbl_LegalCaseRespondentDetail");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlOicName.DataSource = ds.Tables[0];
                ddlOicName.DataTextField = "OICNAME";
                ddlOicName.DataValueField = "Respondent_ID";
                ddlOicName.DataBind();
                ddlOicName.Items.Insert(0, "Select OIC Name");
            }
            else
            {
                ddlOicName.DataSource = null;
                ddlOicName.DataBind();
                ddlOicName.Items.Insert(0, "Select Case Subject");
            }
        }
        catch (Exception)
        {
        }

    }
    private void GetCaseType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_Legal_Casetype");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = ds.Tables[0];
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, "Select Case Type");
            }
            else
            {
                ddlCaseType.DataSource = null;
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, "Select Case Subject");
            }
        }
        catch (Exception)
        {
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            if (Page.IsValid)
            {
                ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag", "Casetype_ID", "Respondent_ID" }, new string[] { "2", ddlCaseType.SelectedItem.Value, ddlOicName.SelectedItem.Value }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //DataTable dt = (DataTable)ViewState["dtCol"];
                    DataTable dt = ds.Tables[0];
                    grdOICWiseCasedtl.DataSource = dt;
                    grdOICWiseCasedtl.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void grdOICWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMsg.Text = "";
        if (e.CommandName == "ViewDtl")
        {
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            Label lblCaseSubject = (Label)row.FindControl("lblCaseSubject");
            Label lblOICName = (Label)row.FindControl("LabelOICName");
            Label lblOICMObile = (Label)row.FindControl("LabelOICMObile");
            Label lblOICEmail = (Label)row.FindControl("LabelOICEmail");
            Label lblNodalName = (Label)row.FindControl("LabelNodalName");
            Label lblNodalMobile = (Label)row.FindControl("LabelNodalMobile");
            Label lblNodalEmail = (Label)row.FindControl("LabelNodalEmail");
            Label lblAdvocateName = (Label)row.FindControl("LabelAdvocateName");
            Label lblAdvocateMobile = (Label)row.FindControl("LabelAdvocateMobile");
            Label lblAdvocateEmail = (Label)row.FindControl("LabelAdvocateEmail");
            Label lblHearingDate = (Label)row.FindControl("LabelHearingDate");
            Label lblRespondertype = (Label)row.FindControl("LabelRespondertype");
            Label lblCaseNO = (Label)row.FindControl("lblCaseNO");
            Label lblPetitionerName = (Label)row.FindControl("lblPetitionerName");
            Label lblCourtName = (Label)row.FindControl("lblCourtName");
            Label lblCaseDetail = (Label)row.FindControl("lblCaseDetail");
            Label lblCasetype = (Label)row.FindControl("lblCasetype");
            Label lblRespondentName = (Label)row.FindControl("lblRespondentName");
            Label lblRespondentMobileNo = (Label)row.FindControl("lblRespondentMobileNo");

            txtCaseno.Text = lblCaseNO.Text;
            txtCourtName.Text = lblCourtName.Text;
            txtRespondertype.Text = lblRespondertype.Text;
            txtRespondentName.Text = lblRespondentName.Text;
            txtRespondentMobileno.Text = lblRespondentMobileNo.Text;
            txtNodalName.Text = lblNodalName.Text;
            txtNodalMobile.Text = lblNodalMobile.Text;
            txtNodalEmailID.Text = lblNodalEmail.Text;
            txtOICName.Text = lblOICName.Text;
            txtOICMObile.Text = lblOICMObile.Text;
            txtOICEmail.Text = lblOICEmail.Text;
            //txtAdvocatename.Text = lblAdvocateName.Text;
            //txtAdvocatemobile.Text = lblAdvocateMobile.Text;
            //txtAdvocateEmailID.Text = lblAdvocateEmail.Text;
            // txtNextHearingDate.Text = lblHearingDate.Text;
            txtPetitionerName.Text = lblPetitionerName.Text;
            txtCasesubject.Text = lblCaseSubject.Text;
            txtCaseDtl.Text = lblCaseDetail.Text;
            txtCasetype.Text = lblCasetype.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
        }
    }
}