using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_EditOld_PendingCases : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
        {
            if (!IsPostBack)
            {
                bindDropDowns();
                string ID = Convert.ToString(Request.QueryString["ID"]);
                FindGridData(sender, e);
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    protected void FindGridData(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "flag", "UniqueNo" },
                new string[] { "2", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                txtFilingNo.Text = ds.Tables[0].Rows[0]["FilingNo"].ToString();
                txtCourt.Text = ds.Tables[0].Rows[0]["Court"].ToString();
                txtPetitioner.Text = ds.Tables[0].Rows[0]["Petitioner"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                if (ds.Tables[0].Rows[0]["OICId"].ToString() != "")
                {
                    ddlOICNameOpen.ClearSelection();
                    ddlOICNameOpen.Items.FindByValue(ds.Tables[0].Rows[0]["OICId"].ToString()).Selected = true;
                    txtOICMobileNoOpen.Text = ds.Tables[0].Rows[0]["OICMobileNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CaseSubjectId"].ToString() != "")
                {
                    ddlCaseSubject.ClearSelection();
                    ddlCaseSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubjectId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["CaseSubSubjectId"].ToString() != "")
                {
                    //ddlCaseSubject_SelectedIndexChanged(sender, e);
                    //ddlCaseSubSubject.ClearSelection();
                    ddlCaseSubSubject.Items.FindByValue(ds.Tables[0].Rows[0]["CaseSubSubjectId"].ToString()).Selected = true;
                }
                if (ds.Tables[0].Rows[0]["RespondentOffice"].ToString() != "")
                {
                    string ROIds = ds.Tables[0].Rows[0]["RespondentOffice"].ToString().Trim();
                    string[] RespondentOffice = ROIds.Split(',');
                    if (!string.IsNullOrEmpty(ROIds))
                    {
                        for (int i = 0; i < RespondentOffice.Length; i++)
                        {
                            ddlRespondentOffice.Items.FindByValue(RespondentOffice[i]).Selected = true;
                        }
                    }
                    else
                    {
                        ddlRespondentOffice.SelectedIndex = -1;
                    }
                }
                if (ds.Tables[0].Rows[0]["CaseType"].ToString() != "")
                {
                    txtRespondent.Text = ds.Tables[0].Rows[0]["CaseType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    ddlIsComplaince.ClearSelection();
                    ddlIsComplaince.Items.FindByText(ds.Tables[0].Rows[0]["Status"].ToString()).Selected = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    private void bindDropDowns()
    {
        try
        {
            dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlOICNameOpen.DataSource = dsCase.Tables[0];
                ddlOICNameOpen.DataTextField = "OICName";
                ddlOICNameOpen.DataValueField = "OICMaster_ID";
                ddlOICNameOpen.DataBind();
                ddlOICNameOpen.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlOICNameOpen.DataSource = null;
                ddlOICNameOpen.DataBind();
                ddlOICNameOpen.Items.Insert(0, new ListItem("Select", "0"));
            }

            dsCase = obj.ByDataSet("select Respondent_Office,Respondent_office_Id from tblRespondentOffice");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlRespondentOffice.DataSource = dsCase.Tables[0];
                ddlRespondentOffice.DataTextField = "Respondent_Office";
                ddlRespondentOffice.DataValueField = "Respondent_office_Id";
                ddlRespondentOffice.DataBind();
                // ddlRespondentOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlRespondentOffice.DataSource = null;
                ddlRespondentOffice.DataBind();
                ddlRespondentOffice.Items.Insert(0, new ListItem("Select", "0"));
            }
            ddlRespondentOffice.ClearSelection();
            ddlRespondentOffice.SelectedIndex = -1;

            dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {

                ddlCaseSubject.DataSource = dsCase.Tables[0];
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

            dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubSubj_Id,CaseSubSubject from tbl_CaseSubSubjectMaster");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {

                ddlCaseSubSubject.DataSource = dsCase.Tables[0];
                ddlCaseSubSubject.DataTextField = "CaseSubSubject";
                ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlCaseSubSubject.DataBind();
                ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseSubSubject.DataSource = null;
                ddlCaseSubSubject.DataBind();
                ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
            }

            #region Extra Dropdown Bind

            dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlYear.DataSource = dsCase.Tables[0];
                ddlYear.DataTextField = "year";
                ddlYear.DataValueField = "year";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlYear.DataSource = null;
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("Select", "0"));
            }

            dsCase = obj.ByDataSet("select CourtTypeName,CourtType_ID from tbl_LegalCourtType order by CourtTypeName");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCourt.DataSource = dsCase.Tables[0];
                ddlCourt.DataTextField = "CourtTypeName";
                ddlCourt.DataValueField = "CourtType_ID";
                ddlCourt.DataBind();
                ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCourt.DataSource = null;
                ddlCourt.DataBind();
                ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
            }

            dsCase = obj.ByDataSet("select Casetype_Name,Casetype_ID from tbl_Legal_Casetype order by Casetype_Name");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = dsCase.Tables[0];
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

            #endregion


        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    public string convertQuotes(string str)
    {
        return str.Replace("'", "''");
    }

    protected void ddlOICNameOpen_SelectedIndexChanged(object sender, EventArgs e)
    {
        int OICID = Convert.ToInt32(ddlOICNameOpen.SelectedItem.Value);
        dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICID);
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            txtOICMobileNoOpen.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        }
    }

    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubSubj_Id,CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectId=" + Convert.ToInt32(ddlCaseSubject.SelectedItem.Value));
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {

            ddlCaseSubSubject.DataSource = dsCase.Tables[0];
            ddlCaseSubSubject.DataTextField = "CaseSubSubject";
            ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
            ddlCaseSubSubject.DataBind();
            ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlCaseSubSubject.DataSource = null;
            ddlCaseSubSubject.DataBind();
            ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnUpdate.Text == "Update")
            {
                var selItems = ddlRespondentOffice.Items.Cast<ListItem>().Where(x => x.Selected).Select(x => x.Value).ToArray();
                var result = String.Join(",", selItems);

                dsCase = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "OICId", "RespondentOffice", "CaseSubjectId", "CaseSubSubjectId", "OICMobileNo", "Remarks", "Status", "flag", "UniqueNo" },
                     new string[] { ddlOICNameOpen.SelectedValue, result, ddlCaseSubject.SelectedValue, ddlCaseSubSubject.SelectedValue, txtOICMobileNoOpen.Text.Trim(), txtRemarks.Text.Trim(), ddlIsComplaince.SelectedItem.Text, "1", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
                if (dsCase != null && dsCase.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = dsCase.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (dsCase.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        FindGridData(sender, e);
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string UniqueNo = Convert.ToString(Request.QueryString["ID"]);
            string filePath = Server.MapPath("~/Legal/OldCaseDocument");
            string filename1 = string.Empty;
            string filename2 = string.Empty;
            string filename3 = string.Empty;
            string filename4 = string.Empty;
            if (FU1.HasFile)
                filename1 = FU1.FileName;
            if (FU2.HasFile)
                filename2 = FU2.FileName;
            if (FU3.HasFile)
                filename3 = FU3.FileName;
            if (FU4.HasFile)
                filename4 = FU4.FileName;

            if (FU1.HasFile)
            {
                dsCase = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "CaseNo", "CaseYear", "CaseType", "Court", "DocName", "DocLink", "flag", "UniqueNo" },
                    new string[] { txtCaseNo.Text.Trim(), ddlYear.SelectedItem.Text, ddlCaseType.SelectedItem.Text, ddlCourt.SelectedItem.Text, "केस का विवरण", filename1, "3", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
                string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo+"_"+dsCase.Tables[0].Rows[0][0].ToString();
                string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                FU1.SaveAs(filePath + "/" + fname + ext);
            }
            if (FU2.HasFile)
            {
                dsCase = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "CaseNo", "CaseYear", "CaseType", "Court", "DocName", "DocLink", "flag", "UniqueNo" },
                    new string[] { txtCaseNo.Text.Trim(), ddlYear.SelectedItem.Text, ddlCaseType.SelectedItem.Text, ddlCourt.SelectedItem.Text, "कार्यवाही का विवरण", filename2, "3", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
                string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo + "_" + dsCase.Tables[0].Rows[0][0].ToString();
                string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                FU1.SaveAs(filePath + "/" + fname + ext);
            }
            if (FU3.HasFile)
            {
                dsCase = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "CaseNo", "CaseYear", "CaseType", "Court", "DocName", "DocLink", "flag", "UniqueNo" },
                    new string[] { txtCaseNo.Text.Trim(), ddlYear.SelectedItem.Text, ddlCaseType.SelectedItem.Text, ddlCourt.SelectedItem.Text, "निर्णय", filename3, "3", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
                string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo + "_" + dsCase.Tables[0].Rows[0][0].ToString();
                string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                FU1.SaveAs(filePath + "/" + fname + ext);
            }
            if (FU4.HasFile)
            {
                dsCase = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "CaseNo", "CaseYear", "CaseType", "Court", "DocName", "DocLink", "flag", "UniqueNo" },
                    new string[] { txtCaseNo.Text.Trim(), ddlYear.SelectedItem.Text, ddlCaseType.SelectedItem.Text, ddlCourt.SelectedItem.Text, "अन्य", filename4, "3", Convert.ToString(Request.QueryString["ID"]) }, "dataset");
                string fname = Path.GetFileNameWithoutExtension(FU1.PostedFile.FileName) + "_" + UniqueNo + "_" + dsCase.Tables[0].Rows[0][0].ToString();
                string ext = System.IO.Path.GetExtension(FU1.PostedFile.FileName);
                FU1.SaveAs(filePath + "/" + fname + ext);
            }
            txtCaseNo.Text = "";
            ddlCaseType.SelectedIndex = 0;
            ddlCourt.SelectedIndex = 0;
            ddlYear.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}