using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{

    APIProcedure obj = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_Id"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_Id"].ToString();

                }
            }
            else
            {
                Response.Redirect("~/Legal/Login.aspx");
            }
        }
        catch (Exception ex)
        {

        }
    }
}