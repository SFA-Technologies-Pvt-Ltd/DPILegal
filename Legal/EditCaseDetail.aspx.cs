using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;

public partial class Legal_EditCaseDetail : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
        {
            if (!IsPostBack)
            {
                ViewState["ID"] = Request.QueryString["ID"].ToString();
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                BindDetails();
                BindDistrict();
                BindDisposeType();
                CaseDisposeStatus();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }

    protected void CaseDisposeStatus() // Case Dispose By Default On NO condtiton
    {
        foreach (ListItem item in rdCaseDispose.Items)
        {
            if (item.Text.Contains("No"))
            {
                item.Selected = true;
                break;

            }
            caseDisposeYes.Visible = false;
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
        }
    }
    protected void BindDisposeType()
    {
        try
        {
            ddlDisponsType.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDisponsType.DataTextField = "CaseDisposeType";
                ddlDisponsType.DataValueField = "CaseDisposeType_Id";
                ddlDisponsType.DataSource = ds;
                ddlDisponsType.DataBind();
            }
            ddlDisponsType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void UploadOrderDoc() // when Case Dispose Order Doc Filled.
    {
        ViewState["FileOrderDOC"] = "";
        int DocFailedCntExt = 0;
        int DocFailedCntSize = 0;
        string strFileName = "";
        string strExtension = "";
        string strTimeStamp = "";
        if (FielUpcaseDisposeOrderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
        {

            string fileExt = System.IO.Path.GetExtension(FielUpcaseDisposeOrderDoc.FileName).Substring(1);
            string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
            if (!supportedTypes.Contains(fileExt))
            {
                DocFailedCntExt += 1;
            }
            else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
            {
                DocFailedCntSize += 1;
            }
            else
            {

                strFileName = FielUpcaseDisposeOrderDoc.FileName.ToString();
                strExtension = Path.GetExtension(strFileName);
                strTimeStamp = DateTime.Now.ToString();
                strTimeStamp = strTimeStamp.Replace("/", "-");
                strTimeStamp = strTimeStamp.Replace(" ", "-");
                strTimeStamp = strTimeStamp.Replace(":", "-");
                string strName = Path.GetFileNameWithoutExtension(strFileName);
                strFileName = strName + "-Supplier-" + strTimeStamp + strExtension;
                string path = Path.Combine(Server.MapPath("../Legal/UploadOrderDoc/"), strFileName);
                FielUpcaseDisposeOrderDoc.SaveAs(path);

                ViewState["FileOrderDOC"] = strFileName;
                path = "";
                strFileName = "";
                strName = "";
            }

        }
    }

    protected void BindDistrict()
    {
        try
        {
            ddlDistrict.Items.Clear();
            ds = obj.ByProcedure("USP_Get_DistrictName", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataBind();
            }
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
        // finally { ds.Clear(); }
    }

    protected void FieldClose()
    {
        Case_EditField.Visible = false;
        FieldSet_CaseDetail.Visible = false; ;
        FieldSet_DocumentDetail.Visible = false;
        FieldSet_ResponderDetail.Visible = false;
        Field_AddResponder.Visible = false;
    }

    protected void BindDetails()
    {
        try
        {
            DtlViewCaseReport.DataSource = null;
            DtlViewCaseReport.DataBind();
            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdCaseDoc.DataSource = null;
            GrdCaseDoc.DataBind();
            dtlCaseDispose.DataSource = null;
            dtlCaseDispose.DataBind();
            FieldClose();
            ds = obj.ByProcedure("USP_Legal_CaseRegis_SelectForEdit", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                // Case Dipose Dtl
                if (ds.Tables[0].Rows[0]["CaseDispose_Status"].ToString() != "")
                {
                    Fieldset_CaseDispose.Visible = true;
                    dtlCaseDispose.DataSource = ds.Tables[0];
                    dtlCaseDispose.DataBind();
                }
                else { Fieldset_CaseDispose.Visible = false; }
                if (ds.Tables[0].Rows[0]["CaseSubject"].ToString() != "")
                {
                    lblCaseOfSubjectDtl.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString();
                }
                FieldSet_CaseDetail.Visible = true;
                FieldSet_DocumentDetail.Visible = true;
                FieldSet_ResponderDetail.Visible = true;
              
                DtlViewCaseReport.DataSource = ds; // Case And Petitoner Dtl Bind In DtlView.
                DtlViewCaseReport.DataBind();

                GrdResponderDtl.DataSource = ds.Tables[0];  // Responder Dtl Bind.
                GrdResponderDtl.DataBind();

                GrdCaseDoc.DataSource = ds.Tables[1]; // Documnets Bind.
                GrdCaseDoc.DataBind();
            }
            else
            {
                DtlViewCaseReport.DataSource = null;
                DtlViewCaseReport.DataBind();
                GrdResponderDtl.DataSource = null;
                GrdResponderDtl.DataBind();
                GrdCaseDoc.DataSource = null;
                GrdCaseDoc.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    protected void GrdResponderDtl_RowCommand(object sender, GridViewCommandEventArgs e)  // Navigate on the Edit Case Detail Div.
    {
        try
        {
            if (e.CommandName == "EditResponder")
            {
                lblMsg.Text = "";
                ViewState["ResponderID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblResponderID = (Label)row.FindControl("lblResponderID");
                Label lblResponderName = (Label)row.FindControl("lblResponderName");
                Label lblResponderNo = (Label)row.FindControl("lblResponderNo");
                Label lblDepartent = (Label)row.FindControl("lblDepartent");
                Label lblAddress = (Label)row.FindControl("lblAddress");

                txtResponderName.Text = lblResponderName.Text;
                txtResponderNo.Text = lblResponderNo.Text;
                txtDepartment.Text = lblDepartent.Text;
                txtAddress.Text = lblAddress.Text;
                ViewState["ResponderID"] = lblResponderID.Text;
                btnAddResponder.Text = "Update";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myModal()", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }

    }
    protected void lnkEditCaseDtl_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
            {
                ds = obj.ByProcedure("USP_Legal_CaseRegis_SelectForEdit", new string[] { "Case_ID" }
                    , new string[] { ViewState["ID"].ToString() }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    Case_EditField.Visible = true;
                    FieldSet_CaseDetail.Visible = false; ;
                    FieldSet_DocumentDetail.Visible = false;
                    FieldSet_ResponderDetail.Visible = false;
                    Field_AddResponder.Visible = false;
                    Fieldset_CaseDispose.Visible = false;
                    lblCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString();
                    txtPetitionerName.Text = ds.Tables[0].Rows[0]["Petitoner_Name"].ToString();
                    if (ds.Tables[0].Rows[0]["WPCaseNo"].ToString() != null)
                    {
                        txtWPCaseNo.Text = ds.Tables[0].Rows[0]["WPCaseNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["WPOrder"].ToString() != "")
                    {
                        txtOrder.Text = ds.Tables[0].Rows[0]["WPOrder"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Whether_WA_RP"].ToString() != "")
                    {
                        txtWhether_WA_RP.Text = ds.Tables[0].Rows[0]["Whether_WA_RP"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CaseSubject"].ToString() != "")
                    {
                        txtCaseSubject.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString();
                       
                    }
                    if (ds.Tables[0].Rows[0]["NodalOfficer_Name"].ToString() != "")
                    {
                        txtNOdalOfficerName.Text = ds.Tables[0].Rows[0]["NodalOfficer_Name"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["NodalOfficerMobileNo"].ToString() != "")
                    {
                        txtNodalOfficerMobileNo.Text = ds.Tables[0].Rows[0]["NodalOfficerMobileNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["OICName"].ToString() != "")
                    {
                        txtOicName.Text = ds.Tables[0].Rows[0]["OICName"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["OICMobileNo"].ToString() != "")
                    {
                        txtOicMobileNO.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["Action_TakenByDistrict"].ToString() != "")
                    {
                        txtActionByDistrict.Text = ds.Tables[0].Rows[0]["Action_TakenByDistrict"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["CaseDispose_Status"].ToString() == "Yes")
                    {
                        rdCaseDispose.ClearSelection();
                        rdCaseDispose.Items.FindByText(ds.Tables[0].Rows[0]["CaseDispose_Status"].ToString()).Selected = true;

                        rdCaseDispose_SelectedIndexChanged( sender,  e);
                       
                        ddlDisponsType.ClearSelection();
                        ddlDisponsType.Items.FindByValue(ds.Tables[0].Rows[0]["CaseDisposeType_Id"].ToString()).Selected = true;
                        ddlDisponsType_SelectedIndexChanged(sender, e);
                        txtCaseDispose_OrderNo.Text = ds.Tables[0].Rows[0]["CaseDispose_OrderNo"].ToString();
                        ViewDoc_CaseDipose.Visible = true;
                        hyPerlinkViewDisposeDoc.NavigateUrl = "UploadOrderDoc/" + ds.Tables[0].Rows[0]["CaseDispose_OrderDoc"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["District_ID"].ToString() != "")
                    {
                        ddlDistrict.ClearSelection();
                        ddlDistrict.Items.FindByValue(ds.Tables[0].Rows[0]["District_ID"].ToString()).Selected = true;

                    }
                }
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
        finally { ds.Clear(); }
    }
    protected void btnUpdate_Click(object sender, EventArgs e) // Only Case & Petitioner INformation Update.
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnUpdate.Text == "Update" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_Update_CaseRegistraion", new string[] { "Petitoner_Name", "District_ID", "CaseSubject", "WPCaseNo", "WPOrder", "Whether_WA_RP", "NodalOfficer_Name", "NodalOfficerMobileNo", "OICName", "OICMobileNo", "LastupdatedBy", "LastupdatedByIp", "Case_ID", "Action_TakenByDistrict" }
                        , new string[] { txtPetitionerName.Text.Trim(), ddlDistrict.SelectedValue, txtCaseSubject.Text.Trim(), txtWPCaseNo.Text.Trim(), txtOrder.Text.Trim(), txtWhether_WA_RP.Text.Trim(), txtNOdalOfficerName.Text.Trim(), txtNodalOfficerMobileNo.Text.Trim(), txtOicName.Text.Trim(), txtOicMobileNO.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["ID"].ToString(), txtActionByDistrict.Text.Trim() }, "dataset");

                    if (rdCaseDispose.SelectedItem.Text == "Yes")
                    {
                        UploadOrderDoc();
                        ds = obj.ByProcedure("USP_Legal_CaseDispose", new string[] { "Case_ID", "CaseDisposeType_Id", "CaseDispose_Status", "CaseDispose_OrderNo", "CaseDispose_OrderDoc", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { ViewState["ID"].ToString(), ddlDisponsType.SelectedValue, rdCaseDispose.SelectedItem.Text.Trim(), txtCaseDispose_OrderNo.Text.Trim(), ViewState["FileOrderDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                        txtPetitionerName.Text = "";
                        ddlDistrict.ClearSelection();
                        txtCaseSubject.Text = "";
                        txtWPCaseNo.Text = "";
                        txtOrder.Text = "";
                        txtWhether_WA_RP.Text = "";
                        txtNOdalOfficerName.Text = "";
                        txtNodalOfficerMobileNo.Text = "";
                        txtOicName.Text = "";
                        txtOicMobileNO.Text = "";
                        txtActionByDistrict.Text = "";
                        ddlDisponsType.ClearSelection();
                        CaseDisposeStatus();
                        txtCaseDispose_OrderNo.Text = "";
                        ViewState["FileOrderDOC"] = "";
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
                BindDetails();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void lnkAddResponderDtl_Click(object sender, EventArgs e) // Navigate on the Add Responder Div.
    {
        try
        {
            lblMsg.Text = "";
            Field_AddResponder.Visible = true;
            Case_EditField.Visible = false;
            FieldSet_CaseDetail.Visible = false; ;
            FieldSet_DocumentDetail.Visible = false;
            FieldSet_ResponderDetail.Visible = false;
            Fieldset_CaseDispose.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnAddResponder_Click(object sender, EventArgs e) // Add New Responder.
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnAddResponder.Text == "Add" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_Insert_ResponderName", new string[] { "Case_ID", "Respondent_Name", "RespondentNo", "Address", "Department", "CreatedBy", "CreatedByIP" }
                        , new string[] { ViewState["ID"].ToString(), txtAddResponderName.Text.Trim(), txtAddResponderNo.Text.Trim(), txtAddResponderAddress.Text.Trim(), txtAddResponderDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnAddResponder.Text == "Update" && ViewState["ResponderID"].ToString() != null && ViewState["ResponderID"].ToString() != "")
                {
                    ds = obj.ByProcedure("USP_Legal_Update_ResponderDtl", new string[] { "Respondent_ID", "Case_ID", "Respondent_Name", "RespondentNo", "Address", "Department", "LastupdatedBy", "LastupdatedByIp" }
                        , new string[] { ViewState["ResponderID"].ToString(), ViewState["ID"].ToString(), txtResponderName.Text.Trim(), txtResponderNo.Text.Trim(), txtAddress.Text.Trim(), txtDepartment.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                        txtResponderName.Text = "";
                        txtResponderNo.Text = "";
                        txtAddress.Text = "";
                        txtDepartment.Text = "";
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
                BindDetails();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void lnkAddEditDoc_Click(object sender, EventArgs e) // For Add New Document
    {
        try
        {
            lblMsg.Text = "";
            Field_AddResponder.Visible = false;
            Case_EditField.Visible = false;
            FieldSet_CaseDetail.Visible = true; ;
            FieldSet_DocumentDetail.Visible = true;
            FieldSet_ResponderDetail.Visible = true;
            BindDetails();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnSaveDoc_Click(object sender, EventArgs e) // Add & Edit Document.
    {
        if (Page.IsValid)
        {


            try
            {
                lblMsg.Text = "";
                ViewState["FileUploadDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileUpload1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName).Substring(1);
                    string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileUpload1.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = FileUpload1.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-Supplier-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/Documents/"), strFileName);
                        FileUpload1.SaveAs(path);

                        ViewState["FileUploadDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }

                }
                else if (fileUpload2_EditDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(fileUpload2_EditDoc.FileName).Substring(1);
                    string[] supportedTypes = { "png", "jpg", "jpeg", "pdf", "JPG", "JPEG", "PNG", "PDF" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (fileUpload2_EditDoc.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = fileUpload2_EditDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-Supplier-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/Documents/"), strFileName);
                        fileUpload2_EditDoc.SaveAs(path);

                        ViewState["FileUploadDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in(png, jpg, jpeg, pdf, JPG, JPEG, PNG, PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 500 KB \\n"; }

                if (errormsg == "")
                {
                    if (btnSaveDoc.Text == "Upload Doc")
                    {

                        ds = obj.ByProcedure("USP_Legal_Insert_AddDocument", new string[] { "Case_ID", "Doc_Name", "Doc_Path", "CreatedBy", "CreatedByIP" }
                            , new string[] { ViewState["ID"].ToString(), txtDocumentName.Text.Trim(), ViewState["FileUploadDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSaveDoc.Text == "Edit Doc" && ViewState["DocID"].ToString().ToString() != "" && ViewState["DocID"].ToString() != null)
                    {
                        ds = obj.ByProcedure("USP_Legal_Update_CaseDocument", new string[] { "CaseDoc_ID", "Case_ID", "Doc_Name", "Doc_Path", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { ViewState["DocID"].ToString(), ViewState["ID"].ToString(), txtEditDocumentsName.Text.Trim(), ViewState["FileUploadDOC"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            ViewState["FileUploadDOC"] = "";
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                        }
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                    }
                    BindDetails();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            }
        }
    }
    protected void GrdCaseDoc_RowCommand(object sender, GridViewCommandEventArgs e)// on row command Event for Edit Document
    {
        try
        {
            ViewState["DocID"] = "";
            if (e.CommandName == "EditDocument")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDocumentID = (Label)row.FindControl("lblDocumentID");
                Label lblDocName = (Label)row.FindControl("lblDocName");
                txtEditDocumentsName.Text = lblDocName.Text;
                ViewState["DocID"] = lblDocumentID.Text;
                btnSaveDoc.Text = "Edit Doc";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#MymodalEditDocuments').modal('show')", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void ddlDisponsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            if (ddlDisponsType.SelectedValue == "2")
            {
                OrderBy1.Visible = true;
                OrderBy2.Visible = true;
            }
            else
            {
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void rdCaseDispose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            caseDisposeYes.Visible = false;
            if (rdCaseDispose.SelectedValue == "1")
            {
                caseDisposeYes.Visible = true;
            }
            else { caseDisposeYes.Visible = false; }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }  
}
