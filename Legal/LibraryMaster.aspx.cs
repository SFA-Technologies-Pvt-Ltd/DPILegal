using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_LibraryMaster : System.Web.UI.Page
{
    DataSet ds = null;
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                    ViewState["Office_Id"] = Session["Office_Id"].ToString();
                    BindGridLibrary();
                    GetCaseSubject();
                    lblMsg.Text = "";
                    lblRecord.Text = "";

                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    private void BindGridLibrary()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("Sp_librarydetail", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt = (DataTable)ViewState["dtCol"];
                DataTable dt = ds.Tables[0];
                grdCaseLibrary.DataSource = dt;
                grdCaseLibrary.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void GetCaseSubject()
    {
        try
        {
            ds = objdb.ByDataSet("select CaseSubjectID,CaseSubject from tbl_LegalMstCaseSubject");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();

            string fileName = Path.GetFileName(FU1.PostedFile.FileName);
            FU1.PostedFile.SaveAs(Server.MapPath("~/PDF_Files/") + fileName);
            if (!FU1.HasFile)
            {
                lblMsg.Text = "Please Select File"; //if file uploader has no file selected  
            }
            if (FU1.HasFile)
            {
                ds = objdb.ByProcedure("Sp_librarydetail", new string[] { "flag", "CaseType", "PartyName", "CaseNo", "RelatedOffice", "DecisionDate", "Case_Year", "PDFViewLink", "RespondentName", "CaseSubjectId", "Case_Infavourof", "CreatedBy", "CreatedByIP" }, new string[] {
                        "1",txtCasetype.Text,txtPartyName.Text,txtCaseNo.Text,txtRelatedOffice.Text, Convert.ToDateTime(txtDecisionDate.Text, cult).ToString("yyyy/MM/dd"),txtCaseYear.Text,"../PDF_Files/"+fileName, txtrespondentName.Text.Trim(),ddlCaseSubject.SelectedItem.Value, ddlDecisionFavourin.SelectedItem.Text.Trim(),ViewState["Emp_Id"].ToString(), objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtCaseNo.Text = "";
                        txtCasetype.Text = "";
                        txtCaseYear.Text = "";
                        txtCaseNo.Text = "";
                        txtDecisionDate.Text = "";
                        txtPartyName.Text = "";
                        txtRelatedOffice.Text = "";
                        txtrespondentName.Text = "";
                        ddlDecisionFavourin.ClearSelection();
                        BindGridLibrary();
                        btnSave.Text = "Save";
                    }
                }

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
        }
    }


}
