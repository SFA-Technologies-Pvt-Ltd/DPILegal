using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class Legal_AddNewCase : System.Web.UI.Page
{
    DataSet ds;
    DataSet dsRecord;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblMsg.Text = "";
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"];
                    ViewState["Office_ID"] = Session["Office_ID"];
                    FillOfficeDropDown();
                   // FillOICDropdown();
                    FillAdvocateDropdown();
                    FillDistrict();
                   // ddlDistrict.Enabled = false;
                    if (Request.QueryString["Case_ID"] != null)
                    {
                        ViewState["Case_ID"] = objdb.Decrypt(Request.QueryString["Case_ID"].ToString());
                        FillCaseDetail();
                        btnClear.Enabled = false;
                    }
                    if (Request.QueryString["ReopenCase_ID"] != null)
                    {
                        ViewState["ReopenCase_ID"] = objdb.Decrypt(Request.QueryString["ReopenCase_ID"].ToString());
                        FillCaseDetail();
                    }
                }
                
            }
            else
            {
                Response.Redirect("~/Legal/Dpi_Login.aspx");
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillOfficeDropDown()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Legal_Select_OfficeName",
                         new string[] { },
                         new string[] { }, "dataset");

            ddloffice.DataSource = ds;
            ddloffice.DataTextField = "OfficeName";
            ddloffice.DataValueField = "Office_Id";
            ddloffice.DataBind();
            ddloffice.Items.Insert(0, new ListItem("Select", "0"));
            ddloffice.SelectedValue = ViewState["Office_ID"].ToString();
            if (ViewState["Office_Id"].ToString() == "1")
            {
                ddloffice.Enabled = true;
            }
            else
            {
                ddloffice.Enabled = false;
            }
            
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    //protected void FillOICDropdown()
    //{
    //    try
    //    {
    //        lblMsg.Text = "";
    //        txtDesignation.Text = "";
    //        txtDepartmentName.Text = "";
    //        txtOICMobileNo.Text = "";
    //        txtOICEmail.Text = "";
    //        ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag", "Office_ID" }, new string[] { "11", ddloffice.SelectedValue.ToString() }, "dataset");
    //        ds = objdb.ByProcedure("SpHREmployee", new string[] { "flag" }, new string[] { "39" }, "dataset");
    //        ddlOIC.DataSource = ds.Tables[0];
    //        ddlOIC.DataTextField = "Emp_Name";
    //        ddlOIC.DataValueField = "Emp_ID";
    //        ddlOIC.DataBind();
    //        ddlOIC.Items.Insert(0, new ListItem("Select", "0"));


    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
    //    }
    //}
    protected void FillAdvocateDropdown()
    {
        try
        {
            lblMsg.Text = "";
            //txtAdvocateName.Text = "";
            //txtAdvocateEmail.Text = "";
            //txtAdvocateAddress.Text = "";
            //txtAdvocateMobileNo.Text = "";
            //ds = objdb.ByProcedure("SpLegalAdvocateRegistration", new string[] { "flag" }, new string[] { "7" }, "dataset");
            //ddlAdvocate.DataSource = ds.Tables[0];
            //ddlAdvocate.DataTextField = "Advocate_Name";
            //ddlAdvocate.DataValueField = "Advocate_ID";
            //ddlAdvocate.DataBind();
            ddlAdvocate.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDistrict()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Select_District", new string[] {}, new string[] {}, "dataset");
            if(ds!= null && ds.Tables[0].Rows.Count >0)
            {
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlOIC_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            lblMsg.Text = "";
            txtDepartmentName.Text = "";
            txtOICMobileNo.Text = "";
            txtOICEmail.Text = "";
            txtDesignation.Text = "";
            if (ddlOIC.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Emp_ID" }, new string[] { "10", ddlOIC.SelectedValue.ToString() }, "dataset");
                txtDepartmentName.Text = ds.Tables[0].Rows[0]["Department_Name"].ToString();
                txtDesignation.Text = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                txtOICMobileNo.Text = ds.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
                txtOICEmail.Text = ds.Tables[0].Rows[0]["Emp_Email"].ToString();
                
            }    
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }


    }
    protected void ddlAdvocate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            txtAdvocateName.Text = "";
            txtAdvocateEmail.Text = "";
            txtAdvocateAddress.Text = "";
            txtAdvocateMobileNo.Text = "";
            if (ddlAdvocate.SelectedIndex > 0)
            {
                ds = objdb.ByProcedure("SpLegalAdvocateRegistration", new string[] { "flag", "Advocate_ID" }, new string[] { "3", ddlAdvocate.SelectedValue.ToString() }, "dataset");
                txtAdvocateName.Text = ds.Tables[0].Rows[0]["Advocate_Name"].ToString();
                txtAdvocateMobileNo.Text = ds.Tables[0].Rows[0]["Advocate_MobileNo"].ToString();
                txtAdvocateEmail.Text = ds.Tables[0].Rows[0]["Advocate_Email"].ToString();
                txtAdvocateAddress.Text = ds.Tables[0].Rows[0]["Advocate_Address"].ToString();
            }

            ddlAdvocate.Focus();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void lnkbtnAdvocate_Click(object sender, EventArgs e)
    {
        lbl.Text = "";
        lblMsg.Text = "";
        try
        {

            if (ddloffice.SelectedIndex > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AdvocateDetailModal()", true);
                FillGridAdvocateDetail();
                Advocate_ClearText();
                lbladvocatemsg.Text = "";
            }
            else
            {

                lbl.Text = "Please Select Office(Supervision by)";
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }
    protected void FillGridAdvocateDetail()
    {

        try
        {
            lblGridAdvocateMsg.Text = "";
            GridViewAdvocateDetail.DataSource = null;
            GridViewAdvocateDetail.DataBind();
            ds = objdb.ByProcedure("SpLegalAdvocateRegistration", new string[] { "flag" }, new string[] { "6" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GridViewAdvocateDetail.DataSource = ds.Tables[0];
                GridViewAdvocateDetail.DataBind();

            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                lblGridAdvocateMsg.Text = "Sorry! No Record Found";
            }

        }
        catch (Exception ex)
        {

            lblGridAdvocateMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btnAdvocateSave_Click(object sender, EventArgs e)
    {
        try
        {

            string msg = "";
            if (txtAdvocate_Name.Text == "")
            {
                msg += "Enter Advocate Name.\\n";
            }
            if (txtAdvocate_MobileNo.Text == "")
            {
                msg += "Enter Advocate MobileNo.\\n";
            }
            //if (txtAdvocate_Email.Text == "")
            //{
            //    msg += "Enter Advocate Email Address\\n";
            //}
            //if (txtAdvocate_Address.Text == "")
            //{
            //    msg += "Enter Advocate Address\\n";
            //}
            if (msg == "")
            {
                if (btnAdvocateSave.Text == "Save")
                {
                    ds = objdb.ByProcedure("SpLegalAdvocateRegistration", new string[] { "flag", "Office_ID", "Advocate_IsActive", "Advocate_Name", "Advocate_MobileNo", "Advocate_Email", "Advocate_Address", "Advocate_UpdatedBy" }, new string[] { "0", ddloffice.SelectedValue.ToString(), "1", txtAdvocate_Name.Text, txtAdvocate_MobileNo.Text, txtAdvocate_Email.Text, txtAdvocate_Address.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    Advocate_ClearText();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AdvocateDetailModal()", true);
                    lbladvocatemsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");
                    FillGridAdvocateDetail();
                    FillAdvocateDropdown();



                }
                else if (btnAdvocateSave.Text == "Update")
                {
                    ds = objdb.ByProcedure("SpLegalAdvocateRegistration", new string[] { "flag", "Advocate_ID", "Advocate_Name", "Advocate_MobileNo", "Advocate_Email", "Advocate_Address", "Advocate_UpdatedBy" }, new string[] { "4", ViewState["Advocate_ID"].ToString(), txtAdvocate_Name.Text, txtAdvocate_MobileNo.Text, txtAdvocate_Email.Text, txtAdvocate_Address.Text, ViewState["Emp_ID"].ToString() }, "dataset");
                    Advocate_ClearText();
                    btnAdvocateSave.Text = "Save";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AdvocateDetailModal()", true);
                    lbladvocatemsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record UpdatedSuccessfully");
                    FillGridAdvocateDetail();
                    FillAdvocateDropdown();

                }
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AdvocateDetailModal()", true);


            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void Advocate_ClearText()
    {
        txtAdvocate_Name.Text = "";
        txtAdvocate_Email.Text = "";
        txtAdvocate_MobileNo.Text = "";
        txtAdvocate_Address.Text = "";
        btnAdvocateSave.Text = "Save";
    }
    protected void GridViewAdvocateDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lbladvocatemsg.Text = "";
            string Advocate_ID = GridViewAdvocateDetail.SelectedDataKey.Value.ToString();
            ViewState["Advocate_ID"] = Advocate_ID;
            ds = objdb.ByProcedure("SpLegalAdvocateRegistration", new string[] { "flag", "Advocate_ID" }, new string[] { "3", Advocate_ID }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtAdvocate_Name.Text = ds.Tables[0].Rows[0]["Advocate_Name"].ToString();
                txtAdvocate_Email.Text = ds.Tables[0].Rows[0]["Advocate_Email"].ToString();
                txtAdvocate_MobileNo.Text = ds.Tables[0].Rows[0]["Advocate_MobileNo"].ToString();
                txtAdvocate_Address.Text = ds.Tables[0].Rows[0]["Advocate_Address"].ToString();
                btnAdvocateSave.Text = "Update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AdvocateDetailModal()", true);
                FillGridAdvocateDetail();
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }

    }
    protected void btnAdvocateClear_Click(object sender, EventArgs e)
    {
        lbladvocatemsg.Text = "";
        txtAdvocate_Name.Text = "";
        txtAdvocate_Email.Text = "";
        txtAdvocate_MobileNo.Text = "";
        txtAdvocate_Address.Text = "";
        btnAdvocateSave.Text = "Save";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AdvocateDetailModal()", true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = "";
            //string Hearing_Date = txtHearingDate.Text;
            string Document1 = "";
            string Document2 = "";
            string Document3 = "";

            if (txtCaseNo.Text == "")
            {
                msg += "Enter Case Number.\\n";
            }
            if (ddloffice.SelectedIndex == 0)
            {
                msg += "Select Office(Supervision By.\\n";
            }
            //if (txtCaseOldRefNo.Text == "")
            //{
            //    msg += "Enter Old Case Refrence Number.\\n";
            //}
            if (ddlCourtType.SelectedIndex < 0)
            {
                msg += "Select Court Type.\\n";
            }
            if (ddlCaseType.SelectedIndex < 0)
            {
                msg += "Select Case Category.\\n";
            }
            //if (txtDepartmentConcerned.Text == "")
            //{
            //    msg += "Enter Department Concerned.\\n";
            //}
            if (txtCaseDescription.Text == "")
            {
                msg += "Enter Subject Of Case.\\n";
            }
            if (ddlOIC.SelectedIndex == 0)
            {
                msg += "Select OIC.\\n";
            }
            if (ddlAdvocate.SelectedIndex == 0)
            {
                msg += "Select Advocate.\\n";
            }
            //if (Hearing_Date == "")
            //{
            //    msg += "Select Hearing Date.\\n";
            //}
            if (FileUpload1.HasFile)
            {
                Document1 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath(Document1));
            }
            else if (ViewState["Case_UploadedDoc1"] != null)
            {
                Document1 = ViewState["Case_UploadedDoc1"].ToString();
            }
            if (FileUpload2.HasFile)
            {
                Document2 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload2.FileName;
                FileUpload2.SaveAs(Server.MapPath(Document2));
            }
            else if (ViewState["Case_UploadedDoc2"] != null)
            {
                Document2 = ViewState["Case_UploadedDoc2"].ToString();
            }
            if (FileUpload3.HasFile)
            {
                Document3 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload3.FileName;
                FileUpload3.SaveAs(Server.MapPath(Document3));
            }
            else if (ViewState["Case_UploadedDoc3"] != null)
            {
                Document3 = ViewState["Case_UploadedDoc3"].ToString();
            }
            if (msg == "")
            {
                string Hearing_Date = "";
                string DateofReceipt = "";
                string DateofFiling = "";
                if(txtHearingDate.Text!= "")
                {
                    Hearing_Date = Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd");
                }
                else
                {
                    Hearing_Date = "";
                }
                if (txtDateOfReceipt.Text != "")
                {
                    DateofReceipt = Convert.ToDateTime(txtDateOfReceipt.Text, cult).ToString("yyyy/MM/dd");
                }
                else
                {
                    DateofReceipt = "";
                }
                if (txtDateOfFiling.Text != "")
                {
                    DateofFiling = Convert.ToDateTime(txtDateOfFiling.Text, cult).ToString("yyyy/MM/dd");
                }
                else
                {
                    DateofFiling = "";
                }
                lblMsg.Text = "";
                if (btnSubmit.Text == "Save")
                {

                    ds = objdb.ByProcedure("SpLegalCaseRegistration",
                                   new string[] {"flag"
                                                ,"Office_ID"
		                                        ,"Case_IsActive"
											    , "Case_No"
											    , "Case_OldRefNo"
											    , "Case_CourtType"
                                                , "District_ID"
											    , "Case_Type"
											    , "Case_Status"
											    , "Case_Result"
											    , "Case_DateOfReceipt"
											    , "Case_DateOfFiling"
											    , "Case_SubjectOfCase"
											    , "Case_InterimOrder"
											    , "Case_FinalOrder"
											    , "Case_ClaimAmount"
											    , "Case_DepartmentConcerned"
											    , "Case_Description"
											    , "OIC_ID"
											    , "Advocate_ID"
											    , "Case_PetitionerAppName"
											    , "Case_PetitionerAppMobileNo"
											    , "Case_PetitionerAppEmail"
											    , "Case_PetitionerAppAddress"
											    , "Case_PetitionerAdvName"
											    , "Case_PetitionerAdvMobileNo"
											    , "Case_PetitionerAdvEmail"
											    , "Case_PetitionerAdvAddress"
											    , "Case_UploadedDoc1"
											    , "Case_UploadedDoc2"
											    , "Case_UploadedDoc3"
											    , "Case_UpdatedBy"},
                                   new string[] { "0"
                                                ,ddloffice.SelectedValue.ToString()
		                                        ,"1"
											    , txtCaseNo.Text
											    , txtCaseOldRefNo.Text
											    , ddlCourtType.SelectedItem.Text
                                                , ddlDistrict.SelectedValue.ToString()
											    , ddlCaseType.SelectedItem.Text
											    , "Open"
											    , "Open"
											    , DateofReceipt
                                                , DateofFiling
											    , txtSubjectOfCase.Text
											    , txtInterimOrder.Text
											    , txtFinalOrder.Text
											    , txtClaimAmount.Text
											    , txtDepartmentConcerned.Text
											    , txtCaseDescription.Text
											    , ddlOIC.SelectedValue.ToString()
											    , ddlAdvocate.SelectedValue.ToString()
											    , txtPetitionerAppName.Text
											    , txtPetitionerAppMobileNo.Text
											    , txtPetitionerAppEmail.Text
											    , txtPetitionerAppAddress.Text
											    , txtPetitionerAdvName.Text
											    , txtPetitionerAdvMobileNo.Text
											    , txtPetitionerAdvEmail.Text
											    , txtPetitionerAdvAddress.Text
											    , Document1
											    , Document2
											    , Document3
											    , ViewState["Emp_ID"].ToString() }, "dataset");
                    if (ds.Tables[0].Rows[0]["Status"].ToString() == "true")
                    {
                        string Case_ID = ds.Tables[0].Rows[0]["Case_ID"].ToString();
                        objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID", "Hearing_IsActive", "Hearing_Date", "Hearing_UpdatedBy" }, new string[] { "0", Case_ID, "1", Hearing_Date, ViewState["Emp_ID"].ToString() }, "dataset");
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Inserted Successfully");
                        ClearText();
                        if (Request.QueryString["ReopenCase_ID"] != null)
                        {
                            Response.Redirect("AddNewCase.aspx");
                        }

                        //FillGrid();
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-Danger", "Sorry!", "Record Not Save");
                    }
                }
                else if (btnSubmit.Text == "Update")
                {
                    if (FileUpload1.HasFile)
                    {
                        Document1 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload1.FileName;
                        FileUpload1.SaveAs(Server.MapPath(Document1));
                    }
                    else
                    {
                        Document1 = ViewState["Case_UploadedDoc1"].ToString();
                    }
                    if (FileUpload2.HasFile)
                    {
                        Document2 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload2.FileName;
                        FileUpload2.SaveAs(Server.MapPath(Document2));
                    }
                    else
                    {
                        Document2 = ViewState["Case_UploadedDoc2"].ToString();
                    }
                    if (FileUpload3.HasFile)
                    {
                        Document3 = "../Legal/Uploads/" + Guid.NewGuid() + FileUpload3.FileName;
                        FileUpload3.SaveAs(Server.MapPath(Document3));
                    }
                    else
                    {
                        Document3 = ViewState["Case_UploadedDoc3"].ToString();
                    }
                    ds = objdb.ByProcedure("SpLegalCaseRegistration",
                                  new string[] {"flag"
                                                ,"Office_ID"
                                                ,"Case_ID"
											    , "Case_No"
											    , "Case_OldRefNo"
											    , "Case_CourtType"
                                                , "District_ID"
											    , "Case_Type"
											    , "Case_Status"
											    , "Case_Result"
											    , "Case_DateOfReceipt"
											    , "Case_DateOfFiling"
											    , "Case_SubjectOfCase"
											    , "Case_InterimOrder"
											    , "Case_FinalOrder"
											    , "Case_ClaimAmount"
											    , "Case_DepartmentConcerned"
											    , "Case_Description"
											    , "OIC_ID"
											    , "Advocate_ID"
											    , "Case_PetitionerAppName"
											    , "Case_PetitionerAppMobileNo"
											    , "Case_PetitionerAppEmail"
											    , "Case_PetitionerAppAddress"
											    , "Case_PetitionerAdvName"
											    , "Case_PetitionerAdvMobileNo"
											    , "Case_PetitionerAdvEmail"
											    , "Case_PetitionerAdvAddress"
											    , "Case_UploadedDoc1"
											    , "Case_UploadedDoc2"
											    , "Case_UploadedDoc3"                                                
											    , "Case_UpdatedBy"},
                                  new string[] { "4"
                                                ,ddloffice.SelectedValue.ToString()
                                                ,ViewState["Case_ID"].ToString()
											    , txtCaseNo.Text
											    , txtCaseOldRefNo.Text
											    , ddlCourtType.SelectedItem.Text
                                                , ddlDistrict.SelectedValue.ToString()
											    , ddlCaseType.SelectedItem.Text
											    , "Open"
											    , "Open"
											    , DateofReceipt
                                                , DateofFiling
											    , txtSubjectOfCase.Text
											    , txtInterimOrder.Text
											    , txtFinalOrder.Text
											    , txtClaimAmount.Text
											    , txtDepartmentConcerned.Text
											    , txtCaseDescription.Text
											    , ddlOIC.SelectedValue.ToString()
											    , ddlAdvocate.SelectedValue.ToString()
											    , txtPetitionerAppName.Text
											    , txtPetitionerAppMobileNo.Text
											    , txtPetitionerAppEmail.Text
											    , txtPetitionerAppAddress.Text
											    , txtPetitionerAdvName.Text
											    , txtPetitionerAdvMobileNo.Text
											    , txtPetitionerAdvEmail.Text
											    , txtPetitionerAdvAddress.Text
											    , Document1
											    , Document2
											    , Document3 
											    , ViewState["Emp_ID"].ToString() }, "dataset");
                    if (int.Parse(ViewState["Count"].ToString()) == 1)
                    {
                        objdb.ByProcedure("SpLegalHearingDetail", new string[] { "flag", "Case_ID", "Hearing_Date", "Hearing_UpdatedBy" }, new string[] { "12", ViewState["Case_ID"].ToString(), Hearing_Date, ViewState["Emp_ID"].ToString() }, "dataset");
                    }
                    FillCaseDetail();
                    lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Updated Successfully");


                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('" + msg + "');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearQueryString()
    {
        PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
        // make collection editable
        isreadonly.SetValue(this.Request.QueryString, false, null);
        // remove
        this.Request.QueryString.Remove("ReopenCase_ID");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearText();

    }
    protected void ClearText()
    {
        txtCaseNo.Text = "";
        txtCaseOldRefNo.Text = "";
        ddlCourtType.ClearSelection();
        ddlCaseType.ClearSelection();
        txtSubjectOfCase.Text = "";
        txtDepartmentConcerned.Text = "";
        txtDesignation.Text = "";
        txtDateOfReceipt.Text = "";
        txtDateOfFiling.Text = "";
        txtInterimOrder.Text = "";
        txtFinalOrder.Text = "";
        txtClaimAmount.Text = "";
        txtCaseDescription.Text = "";
        ddlOIC.ClearSelection();
        txtDepartmentName.Text = "";
        txtOICMobileNo.Text = "";
        txtOICEmail.Text = "";
        ddlAdvocate.ClearSelection();
        txtAdvocateName.Text = "";
        txtAdvocateMobileNo.Text = "";
        txtAdvocateEmail.Text = "";
        txtAdvocateAddress.Text = "";
        txtPetitionerAppName.Text = "";
        txtPetitionerAppMobileNo.Text = "";
        txtPetitionerAppEmail.Text = "";
        txtPetitionerAppAddress.Text = "";
        txtPetitionerAdvName.Text = "";
        txtPetitionerAdvMobileNo.Text = "";
        txtPetitionerAdvEmail.Text = "";
        txtPetitionerAdvAddress.Text = "";
        txtHearingDate.Text = "";
        HyperLink1.Visible = false;
        HyperLink2.Visible = false;
        HyperLink3.Visible = false;
    }
    protected void FillCaseDetail()
    {
        try
        {

            lblMsg.Text = "";
            if (ViewState["Case_ID"] != null)
            {
                dsRecord = null;
                dsRecord = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "7", ViewState["Case_ID"].ToString() }, "dataset");
                if (dsRecord.Tables[0].Rows.Count > 0)
                {
                    ddloffice.ClearSelection();
                    ddloffice.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
                    txtCaseNo.Text = dsRecord.Tables[0].Rows[0]["Case_No"].ToString();
                    txtCaseOldRefNo.Text = dsRecord.Tables[0].Rows[0]["Case_OldRefNo"].ToString();
                    ddlCourtType.ClearSelection();
                    ddlCourtType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_CourtType"].ToString()).Selected = true;
                    ddlCaseType.ClearSelection();
                    ddlCaseType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_Type"].ToString()).Selected = true;
                    txtSubjectOfCase.Text = dsRecord.Tables[0].Rows[0]["Case_SubjectOfCase"].ToString();
                    txtDepartmentConcerned.Text = dsRecord.Tables[0].Rows[0]["Case_DepartmentConcerned"].ToString();
                    txtDateOfReceipt.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfReceipt"].ToString();
                    txtDateOfFiling.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfFiling"].ToString();
                    txtInterimOrder.Text = dsRecord.Tables[0].Rows[0]["Case_InterimOrder"].ToString();
                    txtFinalOrder.Text = dsRecord.Tables[0].Rows[0]["Case_FinalOrder"].ToString();
                    txtClaimAmount.Text = dsRecord.Tables[0].Rows[0]["Case_ClaimAmount"].ToString();
                    txtCaseDescription.Text = dsRecord.Tables[0].Rows[0]["Case_Description"].ToString();

                    txtPetitionerAppName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppName"].ToString();
                    txtPetitionerAppMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppMobileNo"].ToString();
                    txtPetitionerAppEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppEmail"].ToString();
                    txtPetitionerAppAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppAddress"].ToString();
                    txtPetitionerAdvName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvName"].ToString();
                    txtPetitionerAdvMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvMobileNo"].ToString();
                    txtPetitionerAdvEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvEmail"].ToString();
                    txtPetitionerAdvAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvAddress"].ToString();
                    ddlOIC.ClearSelection();
                   // FillOICDropdown();
                    ddlOIC.Items.FindByValue(dsRecord.Tables[0].Rows[0]["OIC_ID"].ToString()).Selected = true;
                    ddlAdvocate.ClearSelection();
                    FillAdvocateDropdown();
                    ddlAdvocate.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Advocate_ID"].ToString()).Selected = true;
                    txtDepartmentName.Text = dsRecord.Tables[0].Rows[0]["Emp_Name"].ToString();
                    txtOICMobileNo.Text = dsRecord.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
                    txtOICEmail.Text = dsRecord.Tables[0].Rows[0]["Emp_Email"].ToString();
                    txtDesignation.Text = dsRecord.Tables[0].Rows[0]["Designation_Name"].ToString();
                    txtAdvocateName.Text = dsRecord.Tables[0].Rows[0]["Advocate_Name"].ToString();
                    txtAdvocateMobileNo.Text = dsRecord.Tables[0].Rows[0]["Advocate_MobileNo"].ToString();
                    txtAdvocateEmail.Text = dsRecord.Tables[0].Rows[0]["Advocate_Email"].ToString();
                    txtAdvocateAddress.Text = dsRecord.Tables[0].Rows[0]["Advocate_Address"].ToString();
                    if (dsRecord.Tables[0].Rows[0]["District_ID"].ToString() != "")
                    {
                        ddlDistrict.ClearSelection();
                        ddlDistrict.Items.FindByValue(dsRecord.Tables[0].Rows[0]["District_ID"].ToString()).Selected = true;
                    }
                    ViewState["Case_UploadedDoc1"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc1"].ToString();
                    ViewState["Case_UploadedDoc2"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc2"].ToString();
                    ViewState["Case_UploadedDoc3"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc3"].ToString();
                    txtHearingDate.Text = dsRecord.Tables[1].Rows[0]["Hearing_Date"].ToString();
                    ViewState["Count"] = dsRecord.Tables[2].Rows[0]["Count"].ToString();
                    if (ViewState["Case_UploadedDoc1"].ToString() != "")
                    {
                        HyperLink1.Visible = true;
                        HyperLink1.NavigateUrl = ViewState["Case_UploadedDoc1"].ToString();
                        HyperLink1.Text = "View";
                    }
                    else
                    {
                        HyperLink1.Visible = true;
                        HyperLink1.Text = "NA";

                    }
                    if (ViewState["Case_UploadedDoc2"].ToString() != "")
                    {
                        HyperLink2.Visible = true;
                        HyperLink2.NavigateUrl = ViewState["Case_UploadedDoc2"].ToString();
                        HyperLink2.Text = "View";
                    }
                    else
                    {
                        HyperLink2.Visible = true;
                        HyperLink2.Text = "NA";

                    }

                    if (ViewState["Case_UploadedDoc3"].ToString() != "")
                    {
                        HyperLink3.Visible = true;
                        HyperLink3.NavigateUrl = ViewState["Case_UploadedDoc3"].ToString();
                        HyperLink3.Text = "View";
                    }
                    else
                    {
                        HyperLink3.Visible = true;
                        HyperLink3.Text = "NA";

                    }
                    btnSubmit.Text = "Update";
                    if(int.Parse(ViewState["Count"].ToString()) > 1)
                    {
                        txtHearingDate.Enabled = false;
                    }
                    else
                    {
                        txtHearingDate.Enabled = true;
                    }
                    
                }
            }
            if (ViewState["ReopenCase_ID"] != null)
            {
                dsRecord = null;
                dsRecord = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Case_ID" }, new string[] { "7", ViewState["ReopenCase_ID"].ToString() }, "dataset");
                if (dsRecord.Tables[0].Rows.Count > 0)
                {
                    ddloffice.ClearSelection();
                    ddloffice.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Office_ID"].ToString()).Selected = true;
                    txtCaseOldRefNo.Text = dsRecord.Tables[0].Rows[0]["Case_No"].ToString();
                    ddlCourtType.ClearSelection();
                    ddlCourtType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_CourtType"].ToString()).Selected = true;
                    ddlCaseType.ClearSelection();
                    ddlCaseType.Items.FindByText(dsRecord.Tables[0].Rows[0]["Case_Type"].ToString()).Selected = true;
                    txtSubjectOfCase.Text = dsRecord.Tables[0].Rows[0]["Case_SubjectOfCase"].ToString();
                    txtDepartmentConcerned.Text = dsRecord.Tables[0].Rows[0]["Case_DepartmentConcerned"].ToString();
                    txtDateOfReceipt.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfReceipt"].ToString();
                    txtDateOfFiling.Text = dsRecord.Tables[0].Rows[0]["Case_DateOfFiling"].ToString();
                    txtInterimOrder.Text = dsRecord.Tables[0].Rows[0]["Case_InterimOrder"].ToString();
                    txtFinalOrder.Text = dsRecord.Tables[0].Rows[0]["Case_FinalOrder"].ToString();
                    txtClaimAmount.Text = dsRecord.Tables[0].Rows[0]["Case_ClaimAmount"].ToString();
                    txtCaseDescription.Text = dsRecord.Tables[0].Rows[0]["Case_Description"].ToString();

                    txtPetitionerAppName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppName"].ToString();
                    txtPetitionerAppMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppMobileNo"].ToString();
                    txtPetitionerAppEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppEmail"].ToString();
                    txtPetitionerAppAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAppAddress"].ToString();
                    txtPetitionerAdvName.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvName"].ToString();
                    txtPetitionerAdvMobileNo.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvMobileNo"].ToString();
                    txtPetitionerAdvEmail.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvEmail"].ToString();
                    txtPetitionerAdvAddress.Text = dsRecord.Tables[0].Rows[0]["Case_PetitionerAdvAddress"].ToString();
                    ddlOIC.ClearSelection();
                  //  FillOICDropdown();
                    ddlOIC.Items.FindByValue(dsRecord.Tables[0].Rows[0]["OIC_ID"].ToString()).Selected = true;
                    ddlAdvocate.ClearSelection();
                    FillAdvocateDropdown();
                    ddlAdvocate.Items.FindByValue(dsRecord.Tables[0].Rows[0]["Advocate_ID"].ToString()).Selected = true;
                    txtDepartmentName.Text = dsRecord.Tables[0].Rows[0]["Emp_Name"].ToString();
                    txtOICMobileNo.Text = dsRecord.Tables[0].Rows[0]["Emp_MobileNo"].ToString();
                    txtOICEmail.Text = dsRecord.Tables[0].Rows[0]["Emp_Email"].ToString();
                    txtDesignation.Text = dsRecord.Tables[0].Rows[0]["Designation_Name"].ToString();
                    txtAdvocateName.Text = dsRecord.Tables[0].Rows[0]["Advocate_Name"].ToString();
                    txtAdvocateMobileNo.Text = dsRecord.Tables[0].Rows[0]["Advocate_MobileNo"].ToString();
                    txtAdvocateEmail.Text = dsRecord.Tables[0].Rows[0]["Advocate_Email"].ToString();
                    txtAdvocateAddress.Text = dsRecord.Tables[0].Rows[0]["Advocate_Address"].ToString();
                    ViewState["Case_UploadedDoc1"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc1"].ToString();
                    ViewState["Case_UploadedDoc2"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc2"].ToString();
                    ViewState["Case_UploadedDoc3"] = dsRecord.Tables[0].Rows[0]["Case_UploadedDoc3"].ToString();
                    if (ViewState["Case_UploadedDoc1"].ToString() != "")
                    {
                        HyperLink1.Visible = true;
                        HyperLink1.Text = "View";
                        HyperLink1.NavigateUrl = ViewState["Case_UploadedDoc1"].ToString();
                    }
                    else
                    {
                        HyperLink1.Visible = true;
                        HyperLink1.Text = "NA";

                    }
                    if (ViewState["Case_UploadedDoc2"].ToString() != "")
                    {
                        HyperLink2.Visible = true;
                        HyperLink2.NavigateUrl = ViewState["Case_UploadedDoc2"].ToString();
                        HyperLink2.Text = "View";
                    }
                    else
                    {
                        HyperLink2.Visible = true;
                        HyperLink2.Text = "NA";

                    }

                    if (ViewState["Case_UploadedDoc3"].ToString() != "")
                    {
                        HyperLink3.Visible = true;
                        HyperLink3.Text = "View";
                        HyperLink3.NavigateUrl = ViewState["Case_UploadedDoc3"].ToString();
                    }
                    else
                    {
                        HyperLink3.Visible = true;
                        HyperLink3.Text = "NA";

                    }

                }
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddloffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //FillAdvocateDropdown();
            //FillOICDropdown();
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridViewAdvocateDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Advocate_ID = GridViewAdvocateDetail.DataKeys[e.RowIndex].Value.ToString();
            string Advocate_IsActive = "0";
            objdb.ByProcedure("SpLegalAdvocateRegistration", new string[] { "flag", "Advocate_ID", "Advocate_IsActive", "Advocate_UpdatedBy" }, new string[] { "5", Advocate_ID, Advocate_IsActive, ViewState["Emp_ID"].ToString() }, "dataset");
            lbladvocatemsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Record Deleted Successfully");
            FillAdvocateDropdown();
            FillGridAdvocateDetail();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AdvocateDetailModal()", true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddlCourtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if(ddlCourtType.SelectedIndex > 0)
            //{
            //    if(ddlCourtType.SelectedItem.Text == "District Court")
            //    {
            //        ddlDistrict.Enabled = true;
            //    }
            //    else
            //    {
            //        ddlDistrict.Enabled = false;
            //        ddlDistrict.ClearSelection();
            //    }
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}

