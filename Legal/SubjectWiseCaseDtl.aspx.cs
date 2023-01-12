using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_SubjectWiseCaseDtl : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseSubject();
                GetCaseType();
                //DataTable dtcol = new DataTable();
                //dtcol.Columns.Add("ID", typeof(int));
                //dtcol.Columns.Add("Respondertype", typeof(string));
                //dtcol.Columns.Add("CaseType", typeof(string));
                //dtcol.Columns.Add("CaseSubject", typeof(string));
                //dtcol.Columns.Add("CaseNo", typeof(string));
                //dtcol.Columns.Add("CourtName", typeof(string));
                //dtcol.Columns.Add("PetitionerName", typeof(string));
                //dtcol.Columns.Add("NodalName", typeof(string));
                //dtcol.Columns.Add("NodalMobileNo", typeof(string));
                //dtcol.Columns.Add("NodalEmailID", typeof(string));
                //dtcol.Columns.Add("OICName", typeof(string));
                //dtcol.Columns.Add("OICMobileNo", typeof(string));
                //dtcol.Columns.Add("OICEmailID", typeof(string));
                //dtcol.Columns.Add("NextHearingDate", typeof(string));
                //dtcol.Columns.Add("AdvocateName", typeof(string));
                //dtcol.Columns.Add("AdvocateMobileNo", typeof(string));
                //dtcol.Columns.Add("AdvocateEmailID", typeof(string));
                //dtcol.Columns.Add("CaseDetail", typeof(string));

                //ViewState["dtCol"] = dtcol;
            }
        }
        else
        {
            Response.Redirect("/Login.aspx");
        }
    }

    private void GetCaseSubject()
    {
        try
        {
            ds = obj.ByDataSet("select * from tbl_LegalMstCaseSubject");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataSource = ds.Tables[0];
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
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseType.DataSource = null;
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
        }

    }
    protected void BindGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Legal_CaseRpt", new string[] { "flag","Casetype_ID", "CaseSubjectID" }, 
                    new string[] {"1", ddlCaseType.SelectedItem.Value, ddlCaseSubject.SelectedItem.Value }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdSubjectWiseCasedtl.DataSource = ds;
                    grdSubjectWiseCasedtl.DataBind();
                }
                else
                {
                    grdSubjectWiseCasedtl.DataSource = null;
                    grdSubjectWiseCasedtl.DataBind();
                }
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {          
            if (Page.IsValid)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
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



    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/legal/subjectwisecasedtl.aspx");
    }
    protected void grdSubjectWiseCasedtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            grdSubjectWiseCasedtl.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception)
        {
            throw;
        }
    }
}