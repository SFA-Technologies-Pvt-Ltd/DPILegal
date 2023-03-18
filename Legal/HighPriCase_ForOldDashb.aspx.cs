using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_HighPriCase_ForOldDashb : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    bindData();
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
    private void bindData()
    {
        try
        {

            // thise is old Query
            //ds = objdb.ByDataSet("select distinct UniqueNo, FilingNo, Court, Petitioner, Respondent, HearingDate,(Select CaseSubject from tbl_LegalMstCaseSubject b where b.CaseSubjectID = a.CaseSubjectId) CaseSubject,(select OICName from tblOICMaster c where c.OICMaster_ID = a.OICId) OICName from tbl_OldCaseDetail a where a.CaseType = 'CONC'");
            //This is New Query
            ds = objdb.ByProcedure("USP_GetHighPriCase_ForOldDashb", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = ds.Tables[0];
                GrdHighpriorityCase.DataSource = ds;
                GrdHighpriorityCase.DataBind();
                GrdHighpriorityCase.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdHighpriorityCase.UseAccessibleHeader = true;
            }
            else
            {
                GrdHighpriorityCase.DataSource = null;
                GrdHighpriorityCase.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}