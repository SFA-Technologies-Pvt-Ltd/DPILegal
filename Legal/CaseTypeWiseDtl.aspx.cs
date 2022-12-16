using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class Legal_CaseTypeWiseDtl : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    IFormatProvider culture = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != "" && Session["Office_Id"] != "")
        {
            if (!IsPostBack)
            {
                DataTable dtcol = new DataTable();
                dtcol.Columns.Add("ID", typeof(int));
                dtcol.Columns.Add("Respondertype", typeof(string));
                dtcol.Columns.Add("CaseType", typeof(string));
                dtcol.Columns.Add("CaseSubject", typeof(string));
                dtcol.Columns.Add("CaseNo", typeof(string));
                dtcol.Columns.Add("CourtName", typeof(string));
                dtcol.Columns.Add("PetitionerName", typeof(string));
                dtcol.Columns.Add("NodalName", typeof(string));
                dtcol.Columns.Add("NodalMobileNo", typeof(string));
                dtcol.Columns.Add("NodalEmailID", typeof(string));
                dtcol.Columns.Add("OICName", typeof(string));
                dtcol.Columns.Add("OICMobileNo", typeof(string));
                dtcol.Columns.Add("OICEmailID", typeof(string));
                dtcol.Columns.Add("NextHearingDate", typeof(string));
                dtcol.Columns.Add("AdvocateName", typeof(string));
                dtcol.Columns.Add("AdvocateMobileNo", typeof(string));
                dtcol.Columns.Add("AdvocateEmailID", typeof(string));
                dtcol.Columns.Add("CaseDetail", typeof(string));
                dtcol.Columns.Add("Status", typeof(string));

                ViewState["dtCol"] = dtcol;
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
                grdCaseTypedtl.DataSource = null;
                grdCaseTypedtl.DataBind();

                DataTable dt = (DataTable)ViewState["dtCol"];

                if (dt.Columns.Count > 0)
                {
                    string sdate = DateTime.Now.ToString("dd/MM/yyyy");
                    string sd = sdate.Replace("-", "/");
                    txtNextHearingDate.Text = sd;

                    dt.Rows.Add("1", "DPI Case",ddlCaseType.SelectedItem.Text, "स्थानांतरण", "Ct001202", "Jabalpur High Court", "Mohan Lal Singh", "Gouri Shanker", "8952232325", "gourishanker46@gmail.com", "Srikant Parte", "7895641563", "Srikantp8955@gmail.com", DateTime.Now.AddDays(5).ToString("dd/MM/yyyy"), "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress", "Pending");
                    dt.Rows.Add("2", "PP Case", ddlCaseType.SelectedItem.Text, "नियूक्ति", "Ct001995", "Gwalior High Court", "Sharman Singh", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Mohan Parte", "8895641563", "Mohantp8955@gmail.com", DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"), "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress", "Pending");
                    dt.Rows.Add("3", "JD Case", ddlCaseType.SelectedItem.Text, "प्रतिनियुक्ति", "Ct001995", "Indore High Court", "Sharman Singh", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Ajay Mishra", "8895641563", "Mohantp8955@gmail.com", "", "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress", "Dispose");
                    dt.Rows.Add("4", "DO Case", ddlCaseType.SelectedItem.Text, "वेतन वृद्धि", "Ct001995", "Indore High Court", "Sharman Singh", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Ajay Mishra", "8895641563", "Mohantp8955@gmail.com", "", "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress", "Dispose");
                }
                ds.Tables.Add(dt);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    grdCaseTypedtl.DataSource = ds;
                    grdCaseTypedtl.DataBind();
                    dt.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void grdCaseTypedtl_RowCommand(object sender, GridViewCommandEventArgs e)
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

            txtCaseno.Text = lblCaseNO.Text;
            txtCourtName.Text = lblCourtName.Text;
            txtRespondertype.Text = lblRespondertype.Text;
            txtRespondentName.Text = "Goutam Mishra";
            txtRespondentMobileno.Text = "7894562563";
            txtRespondentEmailID.Text = "goutam5689@gmail.com";
            txtNodalName.Text = lblNodalName.Text;
            txtNodalMobile.Text = lblNodalMobile.Text;
            txtNodalEmailID.Text = lblNodalEmail.Text;
            txtOICName.Text = lblOICName.Text;
            txtOICMObile.Text = lblOICMObile.Text;
            txtOICEmail.Text = lblOICEmail.Text;
            txtAdvocatename.Text = lblAdvocateName.Text;
            txtAdvocatemobile.Text = lblAdvocateMobile.Text;
            txtAdvocateEmailID.Text = lblAdvocateEmail.Text;
            txtNextHearingDate.Text = lblHearingDate.Text;
            txtPetitionerName.Text = lblPetitionerName.Text;
            txtCasesubject.Text = lblCaseSubject.Text;
            txtCaseDtl.Text = lblCaseDetail.Text;
            txtCasetype.Text = lblCasetype.Text;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
        }
    }
    //protected void grdCaseTypedtl_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        for (int i = 0; i < grdCaseTypedtl.Rows.Count; i++)
    //        {
    //            string vr;
    //            vr = grdCaseTypedtl.Rows[i].Cells[5].Text;
    //            if (e.Row.Cells[1].Text == "Pending")
    //            {
    //                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
    //            }
    //            if (e.Row.Cells[1].Text == "Dispose")
    //            {
    //                e.Row.Cells[1].ForeColor = System.Drawing.Color.Green;
    //            }
    //        }
    //    }
    //}
}