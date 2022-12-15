﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_WeekelyHearingCaseRpt : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

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
            grdSubjectWiseCasedtl.DataSource = null;
            grdSubjectWiseCasedtl.DataBind();

            DataTable dt = (DataTable)ViewState["dtCol"];

            if (dt.Columns.Count > 0)
            {
                if (ddlWeek.SelectedItem.Text == "Current Week")
                {
                    dt.Rows.Add("1", "DPI Case", rbWPCOnt.SelectedItem.Text, "स्थानांतरण", "Ct001202", "Jabalpur High Court", "Mohan Lal Singh", "Gouri Shanker", "8952232325", "gourishanker46@gmail.com", "Srikant Parte", "7895641563", "Srikantp8955@gmail.com", DateTime.Now.AddDays(1).ToString("dd/MM/yyyy"), "Narendra Rao", "6589744512", "Narendrrao8745@gmail.com", "Case In Progress");
                    dt.Rows.Add("2", "PP Case", rbWPCOnt.SelectedItem.Text, "वेतन वृद्धि", "Ct001995", "Gwalior High Court", "Sharman Singh", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Mohan Parte", "8895641563", "Mohantp8955@gmail.com", DateTime.Now.AddDays(3).ToString("dd/MM/yyyy"), "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress");
                   
                }
                else if (ddlWeek.SelectedItem.Text == "Comming Week")
                {
                    dt.Rows.Add("3", "DPI Case", rbWPCOnt.SelectedItem.Text, "स्थानांतरण", "Ct001202", "Jabalpur High Court", "Mohan Lal Singh", "Gouri Shanker", "8952232325", "gourishanker46@gmail.com", "Srikant Parte", "7895641563", "Srikantp8955@gmail.com", DateTime.Now.AddDays(6).ToString("dd/MM/yyyy"), "Narendra Rao", "6589744512", "Narendrrao8745@gmail.com", "Case In Progress");
                    dt.Rows.Add("4", "PP Case", rbWPCOnt.SelectedItem.Text, "वेतन वृद्धि", "Ct001995", "Gwalior High Court", "Sharman Singh", "Narendra Rao", "6652232325", "narendra46@gmail.com", "Mohan Parte", "8895641563", "Mohantp8955@gmail.com", DateTime.Now.AddDays(9).ToString("dd/MM/yyyy"), "Vishal Verma", "6589744512", "VermaVisl8745@gmail.com", "Case In Progress");
                   
                }
            }
            ds.Tables.Add(dt);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdSubjectWiseCasedtl.DataSource = ds;
                grdSubjectWiseCasedtl.DataBind();
                dt.Clear();
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
}