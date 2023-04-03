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
                    FillCasetype();
                    FillYear();
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
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ex.Message.ToString() + "' , 'warning')", true);
        }
    }

    #region CaseType
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = objdb.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 2018; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    #endregion
   
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
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ex.Message.ToString() + "' , 'warning')", true);
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
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ex.Message.ToString() + "' , 'warning')", true);
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
                ds = objdb.ByProcedure("Sp_librarydetail", new string[] { "flag", "Casetype_ID", "PartyName", "CaseNo", "RelatedOffice", "DecisionDate", "CaseYear", "PDFViewLink", "RespondentName", "CaseSubjectId", "Case_Infavourof", "CreatedBy", "CreatedByIP" }, new string[] {
                        "1",ddlCasetype.SelectedValue,txtPartyName.Text,txtCaseNo.Text,txtRelatedOffice.Text, Convert.ToDateTime(txtDecisionDate.Text, cult).ToString("yyyy/MM/dd"),ddlCaseYear.SelectedItem.Text,"../PDF_Files/"+fileName, txtrespondentName.Text.Trim(),ddlCaseSubject.SelectedItem.Value, ddlDecisionFavourin.SelectedItem.Text.Trim(),ViewState["Emp_Id"].ToString(), objdb.GetLocalIPAddress()}, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtCaseNo.Text = "";
                        ddlCasetype.ClearSelection();
                        ddlCaseYear.ClearSelection();
                        txtCaseNo.Text = "";
                        txtDecisionDate.Text = "";
                        txtPartyName.Text = "";
                        txtRelatedOffice.Text = "";
                        txtrespondentName.Text = "";
                        ddlDecisionFavourin.ClearSelection();
                        BindGridLibrary();
                        btnSave.Text = "Save";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                    }
                }

            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Thanks !", ex.Message.ToString());
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ex.Message.ToString() + "' , 'warning')", true);
        }
    }


}
