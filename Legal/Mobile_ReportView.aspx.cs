using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mobile_ReportView : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ID"] = Request.QueryString["ID"];
            BindDetails(sender, e);
        }
    }
    protected void BindDetails(object sender, EventArgs e)
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_NewCaseRegis", new string[] { "Case_ID" }
               , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //lblPetitionorName.Text = ds.Tables[3].Rows[0]["PetitionerName"].ToString();
                //lblRespondentname.Text = ds.Tables[1].Rows[0]["RespondentName"].ToString();
                ////lblOICName.Text = ds.Tables[0].Rows[0]["OICName"].ToString();
                //lblCaseSubject.Text = ds.Tables[0].Rows[0]["CaseSubSubject"].ToString();
                //lblCaseSummry.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString();
                //lblCaseOrderSummry.Text = ds.Tables[6].Rows[0]["OrderSummary"].ToString();
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}