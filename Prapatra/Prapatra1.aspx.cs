using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Prapatra_Prapatra1 : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(! IsPostBack)
        {
            BindCaseCount();
        }
    }

    protected void BindCaseCount()
    {
        try
        {
            GrdCaseCount.DataSource = null;
            GrdCaseCount.DataBind();

            ds = obj.ByProcedure("USP_Legal_GetWACaseDtlForDasboard", new string[] { }, new string[] { }, "dataset");
            if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdCaseCount.DataSource = ds;
                GrdCaseCount.DataBind();
            }
        }
        catch (Exception ex)
        {
           lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}