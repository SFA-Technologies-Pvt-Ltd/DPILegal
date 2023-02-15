using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.IO;

public partial class Legal_CabinetMeetingMaster : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    #region Fill FridView
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("Usp_CabintMeetingSelect", new string[] { }, new string[] { }, "Dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                gridview.DataSource = ds.Tables[0];
                gridview.DataBind();
            }
            else
            {
                gridview.DataSource = null;
                gridview.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    #region Save Button Event
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ViewState["FileUploadDOC1"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (RefrenceDocument.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {
                    string fileExt = System.IO.Path.GetExtension(RefrenceDocument.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf", "JPG", "jpg" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (RefrenceDocument.PostedFile.ContentLength > 512000) // 500 KB = 1024 * 500
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = RefrenceDocument.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "CabinetDecision-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/CabinetMeetingDoc/"), strFileName);
                        RefrenceDocument.SaveAs(path);
                        ViewState["FileUploadDOC1"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 500 KB \\n"; }
                if (errormsg == "")
                {
                    if (btnSave.Text == "Save")
                    {
                        ds = objdb.ByProcedure("Usp_CabintMeetingInsert", new string[] { "MeetingDate", "CabinetDetail", "CabinetDocument", "CreatedBy", "CreatedByIP" },
                            new string[] { Convert.ToDateTime(txtMeetingdate.Text, cult).ToString("yyyy/MM/dd"), txtDetail.Text, ViewState["FileUploadDOC1"].ToString(), ViewState["Emp_Id"].ToString(), objdb.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSave.Text == "Update" && ViewState["CabinetId"].ToString() != "" && ViewState["CabinetId"].ToString() != null)
                    {
                        if (ViewState["Doc"] != ViewState["FileUploadDOC1"])
                        {
                            string path = Path.Combine(Server.MapPath("../Legal/CabinetMeetingDoc/"), ViewState["Doc"].ToString());
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                        ds = objdb.ByProcedure("Usp_CabinetMeetingUpdate", new string[] { "MeetingDate", "CabinetDocument", "CabinetDetail", "LastUpdatedByIP", "CabinetId", "LastUpdatedBy" },
                             new string[] { Convert.ToDateTime(txtMeetingdate.Text, cult).ToString("yyyy/MM/dd"), ViewState["FileUploadDOC1"].ToString(), txtDetail.Text, objdb.GetLocalIPAddress(), ViewState["CabinetId"].ToString(), ViewState["Emp_Id"].ToString() }, "dataset");
                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds != null && ds.Tables[0].Rows[0]["stat"].ToString() == "OK")
                        {
                            btnSave.Text = "Save";
                            FillGrid();
                            ClearData();
                            lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thanks !", ds.Tables[0].Rows[0]["msg"].ToString());
                        }
                    }
                    else
                    {
                        lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["msg"].ToString());
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
                
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    #endregion
    protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            lblMsg.Text = "";
            gridview.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void gridview_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                ViewState["CabinetId"] = "";
                ViewState["Doc"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblCabinetId = (Label)row.FindControl("lblCabinetId");
                Label lblMeetingDate = (Label)row.FindControl("lblMeetingDate");
                Label lblCabinetDetail = (Label)row.FindControl("lblCabinetDetail");
                Label lblFileUpload = (Label)row.FindControl("lblFileUpload");

                txtMeetingdate.Text = lblMeetingDate.Text;
                txtDetail.Text = lblCabinetDetail.Text;
                ViewState["CabinetId"] = e.CommandArgument;
                ViewState["Doc"] = lblFileUpload.Text;
                ViewState["FileUploadDOC1"] = lblFileUpload.Text;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally { if (ds != null) { ds.Dispose(); } }
    }
    protected void ClearData()
    {
        txtMeetingdate.Text = "";
        txtDetail.Text = "";
        btnSave.Text = "Save";


    }
}

