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
public partial class Legal_Cabinet : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillGrid();
        }
    }
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
        catch (Exception)
        {

        }
    }

    protected void gridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
