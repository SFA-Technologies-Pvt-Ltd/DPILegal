using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_New_pheLegalRepot : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("/Login.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ds = obj.ByProcedure("USP_LegalGetPheLegalContemptRpt", new string[] { "FromDate", "EndDate" }
                    , new string[] { Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd"), Convert.ToDateTime(txtEndDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    grdSubjectWiseCasedtl.DataSource = ds;
                    grdSubjectWiseCasedtl.DataBind();
                    // grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                    //  grdSubjectWiseCasedtl.UseAccessibleHeader = true;
                    ViewState["ds"] = dt;
                }
                else
                {
                    grdSubjectWiseCasedtl.DataSource = ds;
                    grdSubjectWiseCasedtl.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
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
                Label lblNextHearingdate = (Label)row.FindControl("lblNextHearingdate");
                Label lblOfficetypeName = (Label)row.FindControl("lblOfficetypeName");
                Label lblOfficeName = (Label)row.FindControl("lblOfficeName");

                txtOfficeName.Text = lblOfficeName.Text;
                txtOfficeType.Text = lblOfficetypeName.Text;
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
                txtNextHearingDate.Text = lblNextHearingdate.Text;
                txtPetitionerName.Text = lblPetitionerName.Text;
                txtCasesubject.Text = lblCaseSubject.Text;
                txtCaseDtl.Text = lblCaseDetail.Text;
                txtCasetype.Text = lblCasetype.Text;
                //grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                // grdSubjectWiseCasedtl.UseAccessibleHeader = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void grdSubjectWiseCasedtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataTable dt1 = ViewState["ds"] as DataTable;
            grdSubjectWiseCasedtl.PageIndex = e.NewPageIndex;
            grdSubjectWiseCasedtl.DataSource = dt1;
            grdSubjectWiseCasedtl.DataBind();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }


    }
}