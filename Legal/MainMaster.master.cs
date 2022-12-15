using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_MainMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                //1. spnUsername.InnerHtml = Session["UserName"].ToString() + "<br/><small>" + Session["Designation_Name"].ToString() + "</small>";
                spnUsername.InnerHtml = "<small>" + Session["UserName"].ToString() + "</small>";
                //spnOfficenaame.InnerHtml += "<li class='header' style='font-weight: 600;text-transform: uppercase;letter-spacing: 2px;'>" + Session["Office_Name"].ToString() + "</li>";
                //lblOfficeName.InnerHtml = Session["Office_Name"].ToString();
            }
        }
        else
        {
            //Response.Redirect("~/Login.aspx");
            Response.Redirect("../Login.aspx");
        }

    }
    protected void btnlogout_Click(object sender, EventArgs e)
    {

        // Response.Redirect("~/Login.aspx");
        Response.Redirect("../Login.aspx");
    }
}
