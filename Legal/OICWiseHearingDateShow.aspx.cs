using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_OICWiseHearingDateShow : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["U"]) && !string.IsNullOrEmpty(Request.QueryString["F"]) && !string.IsNullOrEmpty(Request.QueryString["O"]) && !string.IsNullOrEmpty(Request.QueryString["C"]))
                {
                    int OicId = Convert.ToInt32(Request.QueryString["O"]);
                    int Flag = Convert.ToInt32(Request.QueryString["F"]);
                    string court = Convert.ToString(Request.QueryString["C"]);
                    if (OicId > 0)
                    {
                        string[] uniqueno = Request.QueryString["U"].Split('-');
                        uniqueno = uniqueno.Distinct().ToArray();
                        string UniqueNos = "'" + string.Join("','", uniqueno) + "'";

                        if (Convert.ToString(Request.QueryString["HF"]) != "NA" && Convert.ToString(Request.QueryString["HT"]) != "NA")
                        {
                            BindGrid(UniqueNos, Flag, court, OicId, Convert.ToString(Request.QueryString["HF"]), Convert.ToString(Request.QueryString["HT"]));
                        }
                        else
                        {
                            BindGrid(UniqueNos, Flag, court, OicId, "","");
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }
    }

    protected void BindGrid(string uniquenos, int flag, string court, int OicId, string HF, string HT)
    {
        try
        {
            if (flag == 1)
                dsCase = HF != "" && HT != "" ?
                    obj.ByDataSet("select distinct UniqueNo,CaseType,FilingNo, Court, Petitioner, Respondent, RespondentOffice, " +
                    "(select OICName from tblOICMaster where OICMaster_ID= a.OICId) OICName," +
                    " OICMobileNo,Status," +
                    "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId = a.CaseSubjectId) CaseSubject, CaseSubjectId," +
                    "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id = a.CaseSubSubjectId)" +
                    "CaseSubSubject,Remarks,HearingDate,CaseNo,CaseSubSubjectId,RespondentOffice" +
                    " from tbl_OldCaseDetail a where Court='" + court + "' " +
                    "and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),'" + HF + "',110) and  Convert(varchar(10),'" + HT + "',110) " +
                    "and OICId=" + Convert.ToInt32(OicId) + " and Status='Pending' and UniqueNo in (" + Convert.ToString(uniquenos) + ")") :
                    obj.ByDataSet("select distinct UniqueNo,CaseType,FilingNo, Court, Petitioner, Respondent, RespondentOffice, " +
                    "(select OICName from tblOICMaster where OICMaster_ID= a.OICId) OICName," +
                    " OICMobileNo,Status," +
                    "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId = a.CaseSubjectId) CaseSubject, CaseSubjectId," +
                    "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id = a.CaseSubSubjectId)" +
                    "CaseSubSubject,Remarks,HearingDate,CaseNo,CaseSubSubjectId,RespondentOffice" +
                    " from tbl_OldCaseDetail a where Court='" + court + "' " +
                    "and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),DATEADD(DAY, -30, GETDATE()),110) and  Convert(varchar(10),GETDATE(),110) " +
                    "and OICId=" + Convert.ToInt32(OicId) + " and Status='Pending' and UniqueNo in (" + Convert.ToString(uniquenos) + ")");
            else if (flag == 2)
                dsCase = HF != "" && HT != "" ?
                      obj.ByDataSet("select distinct UniqueNo,CaseType,FilingNo, Court, Petitioner, Respondent, RespondentOffice," +
                    "(select OICName from tblOICMaster where OICMaster_ID= a.OICId) OICName," +
                    " OICMobileNo,Status," +
                "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId = a.CaseSubjectId) CaseSubject, CaseSubjectId," +
                "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id = a.CaseSubSubjectId)" +
                "CaseSubSubject,Remarks,HearingDate,CaseNo,CaseSubSubjectId,RespondentOffice" +
                " from tbl_OldCaseDetail a " +
                "where Court='" + court + "' and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),'" + HF + "',110) and  Convert(varchar(10),'" + HT + "',110) " +
                "and OICId=" + Convert.ToInt32(OicId) + " and UniqueNo in (" + Convert.ToString(uniquenos) + ")") :
                obj.ByDataSet("select distinct UniqueNo,CaseType,FilingNo, Court, Petitioner, Respondent, RespondentOffice," +
                    "(select OICName from tblOICMaster where OICMaster_ID= a.OICId) OICName," +
                    " OICMobileNo,Status," +
                "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId = a.CaseSubjectId) CaseSubject, CaseSubjectId," +
                "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id = a.CaseSubSubjectId)" +
                "CaseSubSubject,Remarks,HearingDate,CaseNo,CaseSubSubjectId,RespondentOffice" +
                " from tbl_OldCaseDetail a " +
                "where Court='" + court + "' and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),DATEADD(DAY, -30, GETDATE()),110) and  Convert(varchar(10),GETDATE(),110) " +
                "and OICId=" + Convert.ToInt32(OicId) + " and UniqueNo in (" + Convert.ToString(uniquenos) + ")");
            else if (flag == 3)
                dsCase = obj.ByDataSet("select distinct UniqueNo,CaseType,FilingNo, Court, Petitioner, Respondent, RespondentOffice, " +
                   "(select OICName from tblOICMaster where OICMaster_ID= a.OICId) OICName," +
                    "OICMobileNo,Status," +
                    "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId = a.CaseSubjectId) CaseSubject, CaseSubjectId," +
                    "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id = a.CaseSubSubjectId)" +
                    "CaseSubSubject,Remarks,HearingDate,CaseNo,CaseSubSubjectId,RespondentOffice" +
                    " from tbl_OldCaseDetail a where Court='" + court + "' " +
                    "and OICId=" + Convert.ToInt32(OicId) + " and Status='Yes' and UniqueNo in (" + Convert.ToString(uniquenos) + ")");
            else if (flag == 4)
                dsCase = obj.ByDataSet("select distinct UniqueNo,CaseType,FilingNo, Court, Petitioner, Respondent, RespondentOffice," +
                    "(select OICName from tblOICMaster where OICMaster_ID= a.OICId) OICName," +
                    " OICMobileNo,Status," +
                    "(select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectId = a.CaseSubjectId) CaseSubject, CaseSubjectId," +
                    "(select CaseSubSubject from tbl_CaseSubSubjectMaster c where c.CaseSubSubj_Id = a.CaseSubSubjectId)" +
                    "CaseSubSubject,Remarks,HearingDate,CaseNo,CaseSubSubjectId,RespondentOffice" +
                    " from tbl_OldCaseDetail a where Court='" + court + "' " +
                    "and OICId=" + Convert.ToInt32(OicId) + " and Status='No' and UniqueNo in (" + Convert.ToString(uniquenos) + ")");


            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = null;
                ViewState["dt"] = dsCase.Tables[0];
                grdData.DataSource = dsCase.Tables[0];
                grdData.DataBind();
            }
            else
            {
                grdData.DataSource = null;
                grdData.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No record found')", true);
            }

        }
        catch (Exception ex)
        {

        }
    }
}