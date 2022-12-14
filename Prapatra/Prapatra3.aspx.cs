using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Prapatra_Prapatra3 : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCaseCount();
        }
    }

    protected void BindCaseCount()
    {
        try
        {
            GrdPrapatraThree.DataSource = null;
            GrdPrapatraThree.DataBind();

            ds = obj.ByProcedure("USP_Legal_GetWACaseDtlForDasboard", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdPrapatraThree.DataSource = ds;
                GrdPrapatraThree.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}