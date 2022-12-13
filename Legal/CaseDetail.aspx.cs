using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Legal_CaseDetail : System.Web.UI.Page
{
    DataSet ds;
    DataSet ds11;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null && Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    
                    ViewState["Case_ID"] = objdb.Decrypt(Request.QueryString["Case_ID"].ToString());
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillCaseDetail();
                    FillHearingDate();
                    FillExpenseGrid();
                    FillHearingConclusion();

                }
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCaseDetail()
    {
        if (ViewState["Case_ID"].ToString() != "" && ViewState["Case_ID"].ToString() != null)
        {
            ds = null;
            ds11 = null;
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "7", ViewState["Case_ID"].ToString() }, "dataset");
            ds11 = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "9", ViewState["Case_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Advocate_Name"] = ds.Tables[0].Rows[0]["Advocate_Name"].ToString();
                Subjectofcase.InnerText = ds.Tables[0].Rows[0]["Case_Description"].ToString();
                DetailsView1.DataSource = ds;
                DetailsView1.DataBind();

                DetailsView2.DataSource = ds;
                DetailsView2.DataBind();

                DetailsView3.DataSource = ds;
                DetailsView3.DataBind();

                DetailsView4.DataSource = ds;
                DetailsView4.DataBind();

                DetailsView5.DataSource = ds;
                DetailsView5.DataBind();

                //DetailsView6.DataSource = ds;
                //DetailsView6.DataBind();

            }
        }
    }
    protected void FillExpenseGrid()
    {
        try
        {
            lblGridExpenseMsg.Text = "";
            ds = objdb.ByProcedure("SpLegalExpensesDetail", new string[] { "flag", "Case_ID" }, new string[] { "6", ViewState["Case_ID"].ToString() }, "dataSet");
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridViewExpense.DataSource = ds.Tables[0];
                GridViewExpense.DataBind();
            }
            else
            {
                GridViewExpense.DataSource = null;
                GridViewExpense.DataBind();
                lblGridExpenseMsg.Text = "No Expense Record Found.";
            }
        }
        catch (Exception Ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }
    }
    protected void FillHearingDate()
    {
        try
        {

            ds11 = null;
            ds11 = objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID" }, new string[] { "11", ViewState["Case_ID"].ToString() }, "dataset");
            ds = objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID" }, new string[] { "10", ViewState["Case_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds11.Tables[0].Rows.Count > 0)
                {
                    if (ds11.Tables[0].Rows[0]["Count"].ToString() == "1")
                    {
                        lblNextHearingDate.InnerHtml = "First Hearing Date" + " " + ":" + " " + ds.Tables[0].Rows[0]["Hearing_Date"].ToString();
                    }
                    else
                    {
                        lblNextHearingDate.InnerHtml = "Next Hearing Date" + " " + ":" + " " + ds.Tables[0].Rows[0]["Hearing_Date"].ToString();
                    }


                }
            }
            else
            {
                
            }

        }
        catch (Exception Ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }

    }
    protected void FillHearingConclusion()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID" }, new string[] { "6", ViewState["Case_ID"].ToString() }, "dataset");
            int NoOfRecords = ds.Tables[0].Rows.Count;
            if (ds != null && NoOfRecords > 0)
            {
                //lblMsg.Text = "";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < NoOfRecords; i++)
                {
                    sb.Append(" <div class='direct-chat-msg'>");
                    sb.Append("<div class='direct-chat-info clearfix'>");
                    sb.Append("<span class='direct-chat-name pull-left'>" + "Hearing Date :" + "  " + ds.Tables[0].Rows[i]["Hearing_Date"].ToString() + "</span><br/>");
                    sb.Append("<span class='direct-chat-name pull-left'>" + "Hearing Conclusion :" + " " + "</span>");
                    sb.Append("<span class='direct-chat-timestamp pull-right'>" + ds.Tables[0].Rows[i]["Hearing_UpdatedOn"].ToString() + "</span>");     //16 Aug 2:00 pm
                    sb.Append("</div>");
                    //.direct-chat-info -->
                    sb.Append("<img class='direct-chat-img' src='../image/User1.png' alt='message user image'/>");
                    //.direct-chat-img -->
                    sb.Append("<div class='direct-chat-text form-group' style='word-wrap:break-word; min-height:80px;'>" + ds.Tables[0].Rows[i]["Hearing_Conclusion"].ToString());
                    //sb.Append("RTI Act has been made by legislation of Parliament of India on 15 June 2005.");

                    sb.Append("<div class='attachment text-right''><br />");
                    string Hearing_Doc1 = ds.Tables[0].Rows[i]["Hearing_Doc1"].ToString();
                    if (Hearing_Doc1 != null && Hearing_Doc1 != "")
                    {
                        sb.Append(" <a href='../Uploads/" + Hearing_Doc1 + "' target='blank'>Attachment 1</a>");
                    }
                    string Hearing_Doc2 = ds.Tables[0].Rows[i]["Hearing_Doc2"].ToString();
                    if (Hearing_Doc2 != null && Hearing_Doc2 != "")
                    {
                        sb.Append(" <a href='../Uploads/" + Hearing_Doc2 + "' target='blank' style='word-wrap:break-word'>/ Attachment 2</a>");
                    }
                    sb.Append("</div></div></div>");
                    dvChat.InnerHtml = sb.ToString();
                    //FillHearingDate();
                    //Div2.InnerHtml = sb.ToString();
                }

            }
            else { }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridViewExpense_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseViewModal()", true);
            string Expense_ID = GridViewExpense.SelectedDataKey.Value.ToString();
            ViewState["Expense_ID"] = Expense_ID;
            ds = objdb.ByProcedure("SpLegalExpensesDetail", new string[] { "flag", "Expense_ID" }, new string[] { "7", ViewState["Expense_ID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblExpDate.Text = ds.Tables[0].Rows[0]["Expense_Date"].ToString();
                lblExpFor.Text = ds.Tables[0].Rows[0]["Expense_For"].ToString();
                lblExpAmnt.Text = ds.Tables[0].Rows[0]["Expenses_Amount"].ToString();
                ExpDescription.InnerHtml = ds.Tables[0].Rows[0]["Expense_Description"].ToString();
            }

        }
        catch (Exception Ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }
    }
}