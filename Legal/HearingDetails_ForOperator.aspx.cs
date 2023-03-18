using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class mis_Legal_HearingDetails_ForOperator : System.Web.UI.Page
{
    DataSet ds;
    DataSet ds11;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {

                    ViewState["CID"] = objdb.Decrypt(Request.QueryString["CID"].ToString());
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    FillHearingConclusion();
                    FillCaseDetail();
                    FillHearingDate();
                    FillExpenseGrid();
                    //divExpense.Visible = false;
                }
            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }
        catch (Exception Ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }

    }
    protected void FillCaseDetail()
    {
        if (ViewState["CID"].ToString() != "" && ViewState["CID"].ToString() != null)
        {
            ds = null;
            ds11 = null;
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "7", ViewState["CID"].ToString() }, "dataset");
            ds11 = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "9", ViewState["CID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Advocate_Name"] = ds.Tables[0].Rows[0]["Advocate_Name"].ToString();
                Subjectofcase.InnerText = ds.Tables[0].Rows[0]["Case_Description"].ToString();
                string Case_Status = ds.Tables[0].Rows[0]["Case_Status"].ToString();
                string UploadedDoc1 = ds.Tables[0].Rows[0]["Case_UploadedDoc1"].ToString();
                string UploadedDoc2 = ds.Tables[0].Rows[0]["Case_UploadedDoc2"].ToString();
                string UploadedDoc3 = ds.Tables[0].Rows[0]["Case_UploadedDoc3"].ToString();
                if (UploadedDoc1 != "")
                {
                    Case_UploadedDoc1.Visible = true;
                    Case_UploadedDoc1.NavigateUrl = "../Uploads/" + UploadedDoc1;

                }
                else
                {
                    Case_UploadedDoc1.Visible = false;
                }
                if (UploadedDoc2 != "")
                {
                    Case_UploadedDoc2.Visible = true;
                    Case_UploadedDoc2.NavigateUrl = "../Uploads/" + UploadedDoc2;

                }
                else
                {
                    Case_UploadedDoc2.Visible = false;
                }
                if (UploadedDoc3 != "")
                {
                    Case_UploadedDoc3.Visible = true;
                    Case_UploadedDoc3.NavigateUrl = "../Uploads/" + UploadedDoc3;

                }
                else
                {
                    Case_UploadedDoc3.Visible = false;
                }
                if (Case_Status == "Close")
                {
                    lnkBtnReopen.Visible = true;
                    btnAddExpenses.Visible = true;
                    btnEditCaseDetail.Visible = false;
                    divHearingDetail.Visible = false;

                }

                else
                {
                    lnkBtnReopen.Visible = false;
                    divHearingDetail.Visible = true;
                    btnAddExpenses.Visible = true;
                    btnEditCaseDetail.Visible = true;
                }
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
    protected void FillHearingDate()
    {
        try
        {

            ds11 = null;
            ds11 = objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID" }, new string[] { "11", ViewState["CID"].ToString() }, "dataset");
            ds = objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID" }, new string[] { "10", ViewState["CID"].ToString() }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Hearing_Date"].ToString() != "NA")
                {
                    var dateAndTime = DateTime.Now;
                    var date = dateAndTime.ToString("dd/MM/yyyy");
                    string d11 = Convert.ToDateTime(date, cult).ToString("yyyy/MM/dd");
                    string d22 = Convert.ToDateTime(ds.Tables[0].Rows[0]["Hearing_Date"].ToString(), cult).ToString("yyyy/MM/dd");
                    DateTime d1 = DateTime.Parse(d11);
                    DateTime d2 = DateTime.Parse(d22);
                    if (d1 >= d2)
                    {
                        divHearingDetail.Visible = true;


                    }
                    else
                    {
                        divHearingDetail.Visible = false;
                    }
                    HearingDate.InnerHtml = ds.Tables[0].Rows[0]["Hearing_Date"].ToString();
                    ViewState["Hearing_ID"] = ds.Tables[0].Rows[0]["Hearing_ID"].ToString();
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
                    lblNextHearingDate.InnerHtml = "Hearing Date Not Available";
                    divHearingDetail.Visible = false;
                }

            }
            else
            {
                divHearingDetail.Visible = false;
                lblNextHearingDate.Visible = false;
            }

        }
        catch (Exception Ex)
        {
            LblHearingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }

    }
    protected void FillHearingConclusion()
    {
        try
        {
            ds = null;
            ds = objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID" }, new string[] { "6", ViewState["CID"].ToString() }, "dataset");
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
            LblHearingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            LblHearingMsg.Text = "";
            string msg = "";
            if (ddlCase_Status.SelectedIndex == 0)
            {
                msg += "Select Case Status. <br/>";
            }
            if (ddlCase_Status.SelectedIndex > 0)
            {
                if (txtHearingDate.Visible == true)
                {
                    if (txtHearingDate.Text == "")
                    {
                        msg += "Enter Next Hearing Date. <br/>";
                    }
                }
                //else if (ddlCaseResult.Visible == true)
                //{
                //    if (ddlCaseResult.SelectedIndex == 0)
                //    {
                //        msg += "Select Case Result. <br/>";
                //    }
                //}
            }
            if (txtHearingConclusion.Text == "")
            {
                msg += "Select Conclusion of Hearing. <br/>";

            }
            if (msg == "")
            {
                LblHearingMsg.Text = "";
                string Document1 = "";
                string Document2 = "";
                if (Hearing_Doc1.HasFile)
                {
                    Document1 = "../Legal/Uploads/" + Guid.NewGuid() + Hearing_Doc1.FileName;
                    Hearing_Doc1.SaveAs(Server.MapPath(Document1));
                }
                if (Hearing_Doc2.HasFile)
                {
                    Document2 = "../Legal/Uploads/" + Guid.NewGuid() + Hearing_Doc2.FileName;
                    Hearing_Doc2.SaveAs(Server.MapPath(Document2));
                }
                if (ddlCase_Status.SelectedIndex > 0)
                {
                    if (ddlCase_Status.SelectedItem.Text == "Still Open")
                    {
                        ds = null;
                        objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Hearing_ID", "Case_ID", "Case_Status", "Hearing_Conclusion", "Hearing_Doc1", "Hearing_Doc2", "Hearing_UpdatedBy" }, new string[] { "7", ViewState["Hearing_ID"].ToString(), ViewState["CID"].ToString(), ddlCase_Status.SelectedValue.ToString(), txtHearingConclusion.Text, Document1, Document2, ViewState["Emp_ID"].ToString() }, "dataset");
                        objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Hearing_Date", "Hearing_IsActive", "Case_ID", "Hearing_UpdatedBy" }, new string[] { "0", Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd"), "1", ViewState["CID"].ToString(), ViewState["Emp_ID"].ToString() }, "dataset");
                        FillHearingConclusion();
                        ddlCase_Status.ClearSelection();
                        txtHearingDate.Text = "";
                        txtHearingConclusion.Text = "";
                        FillHearingDate();
                        LblHearingMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");


                    }
                    else if (ddlCase_Status.SelectedItem.Text == "Closed")
                    {
                        ds = objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Hearing_ID", "Case_ID", "Case_Status", "Hearing_Conclusion", "Hearing_Doc1", "Hearing_Doc2", "Hearing_UpdatedBy" }, new string[] { "7", ViewState["Hearing_ID"].ToString(), ViewState["CID"].ToString(), ddlCase_Status.SelectedValue.ToString(), txtHearingConclusion.Text, Document1, Document2, ViewState["Emp_ID"].ToString() }, "dataset");
                        ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID", "Case_Status", "Case_Result", "Case_UpdatedBy" }, new string[] { "8", ViewState["CID"].ToString(), ddlCase_Status.SelectedValue.ToString(), ddlCaseResult.SelectedItem.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                        ddlCase_Status.ClearSelection();
                        ddlCaseResult.ClearSelection();
                        txtHearingConclusion.Text = "";
                        FillHearingDate();
                        LblHearingMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");
                        FillHearingConclusion();
                        FillCaseDetail();
                        lnkBtnReopen.Visible = true;

                    }

                }

            }
            else
            {
                //lblHearingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
            }

        }
        catch (Exception Ex)
        {
            //lblHearingMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callalert()", true);
        }
    }
    protected void ddlCase_Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        LblHearingMsg.Text = "";
        if (ddlCase_Status.SelectedItem.Text == "Closed")
        {
            DivHearingDate.Visible = false;
            DivResult.Visible = true;

        }
        if (ddlCase_Status.SelectedItem.Text == "Still Open")
        {
            DivHearingDate.Visible = true;
            DivResult.Visible = false;

        }
    }

    protected void btnAddExpenses_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
        LblExpenseMsg.Text = "";
        btnSaveExpenses.Text = "Save";
        DivExpenseFor.Visible = true;
        DivAdvocate.Visible = false;
        txtExpenseDetail.Text = "";
        txtExpenses.Text = "";
        txtDate.Text = "";
        ddlExpenseFor.ClearSelection();

    }
    protected void btnSaveExpenses_Click(object sender, EventArgs e)
    {

        try
        {
            LblExpenseMsg.Text = "";
            string msg = "";
            if (ddlExpenseFor.Visible == true)
            {
                if (ddlExpenseFor.SelectedIndex == 0)
                {
                    msg += "Select Expense for.<br/>";
                }
            }
            if (txtDate.Text == "")
            {
                msg += "Enter Date.<br/>";
            }
            if (txtExpenses.Text == "")
            {
                msg += "Enter Expenses Amount.<br/>";
            }
            if (msg == "")
            {
                LblExpenseMsg.Text = "";
                if (btnSaveExpenses.Text == "Save")
                {
                    if (ddlExpenseFor.SelectedItem.Text == "Other")
                    {
                        ds = objdb.ByProcedure("SpLegalExpensesDetail", new string[] { "flag", "Expense_IsActive", "Expense_Date", "Case_ID", "Expense_For", "Expenses_Amount", "Expense_Description", "Expense_UpdatedBy" }, new string[] { "0", "1", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["CID"].ToString(), ddlExpenseFor.SelectedItem.Text, txtExpenses.Text, txtExpenseDetail.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
                        LblExpenseMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");
                        ddlExpenseFor.ClearSelection();
                        txtExpenseDetail.Text = "";
                        txtExpenses.Text = "";
                        txtDate.Text = "";
                        FillExpenseGrid();
                    }
                    else if (ddlExpenseFor.SelectedItem.Text == "Advocate")
                    {
                        ds = objdb.ByProcedure("SpLegalExpensesDetail", new string[] { "flag", "Expense_IsActive", "Expense_Date", "Case_ID", "Expense_For", "Expenses_Amount", "Expense_Description", "Expense_UpdatedBy" }, new string[] { "0", "1", Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), ViewState["CID"].ToString(), ViewState["Expense_For"].ToString(), txtExpenses.Text, txtExpenseDetail.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
                        LblExpenseMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");
                        ddlExpenseFor.ClearSelection();
                        txtExpenseDetail.Text = "";
                        txtExpenses.Text = "";
                        txtDate.Text = "";
                        FillExpenseGrid();
                        DivAdvocate.Visible = false;
                    }

                }
                else if (btnSaveExpenses.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpLegalExpensesDetail", new string[] { "flag", "Expense_ID", "Expense_Date", "Expenses_Amount", "Expense_Description", "Expense_UpdatedBy" }, new string[] { "8", ViewState["EditExpense_ID"].ToString(), Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd"), txtExpenses.Text, txtExpenseDetail.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
                    LblExpenseMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully");
                    btnSaveExpenses.Text = "Update";
                    FillExpenseGrid();
                    DivAdvocate.Visible = false;
                }

            }
            else
            {
                LblExpenseMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", msg);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);

            }


        }
        catch (Exception Ex)
        {
            LblExpenseMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }
    }
    protected void ddlExpenseFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LblExpenseMsg.Text = "";
            if (ddlExpenseFor.SelectedItem.Text == "Other")
            {
                ViewState["Expense_For"] = ddlExpenseFor.SelectedItem.Text;
                DivAdvocate.Visible = false;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
            }
            if (ddlExpenseFor.SelectedItem.Text == "Advocate")
            {
                DivAdvocate.Visible = true;
                txtAdvocate.Text = ViewState["Advocate_Name"].ToString();
                ViewState["Expense_For"] = txtAdvocate.Text;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
            }
        }
        catch (Exception Ex)
        {
            LblExpenseMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }

    }
    protected void FillExpenseGrid()
    {
        try
        {
            lblGridExpenseMsg.Text = "";
            ds = objdb.ByProcedure("SpLegalExpensesDetail", new string[] { "flag", "Case_ID" }, new string[] { "6", ViewState["CID"].ToString() }, "dataSet");
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
            lblGridExpenseMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }
    }
    //Edit Expense Detail
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
            LblExpenseMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        try
        {

            LblExpenseMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            LinkButton lnkEdit = (LinkButton)GridViewExpense.Rows[selRowIndex].FindControl("lnkEdit");
            string Expense_ID = lnkEdit.ToolTip.ToString();
            ViewState["EditExpense_ID"] = Expense_ID;
            ds = objdb.ByProcedure("SpLegalExpensesDetail", new string[] { "flag", "Expense_ID" }, new string[] { "7", Expense_ID }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
                txtDate.Text = ds.Tables[0].Rows[0]["Expense_Date"].ToString();
                txtExpenses.Text = ds.Tables[0].Rows[0]["Expenses_Amount"].ToString();
                txtExpenseDetail.Text = ds.Tables[0].Rows[0]["Expense_Description"].ToString();
                btnSaveExpenses.Text = "Update";
                DivAdvocate.Visible = false;
                DivExpenseFor.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
            LblExpenseMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());

        }
    }
    protected void btnExpensesClear_Click(object sender, EventArgs e)
    {
        LblExpenseMsg.Text = "";
        DivExpenseFor.Visible = true;
        ddlExpenseFor.ClearSelection();
        txtExpenseDetail.Text = "";
        txtExpenses.Text = "";
        txtDate.Text = "";
        btnSaveExpenses.Text = "Save";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ExpenseModal()", true);
    }
    //Edit Case Detail
    protected void btnEditCaseDetail_Click(object sender, EventArgs e)
    {
        try
        {
            string Case_ID = ViewState["CID"].ToString();
            Response.Redirect("AddNewCase.aspx?Case_ID=" + objdb.Encrypt(Case_ID));

        }
        catch (Exception Ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }
    }
    //Reopen Case
    protected void lnkBtnReopen_Click(object sender, EventArgs e)
    {
        try
        {
            string ReopenCase_ID = ViewState["CID"].ToString();
            Response.Redirect("AddNewCase.aspx?ReopenCase_ID=" + objdb.Encrypt(ReopenCase_ID));

        }
        catch (Exception Ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", Ex.Message.ToString());
        }
    }





}