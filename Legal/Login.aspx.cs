using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_Login : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetRandomText();
        }
    }

    public void GetRandomText()
    {
        StringBuilder randomText = new StringBuilder();
        string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz!@#$%&*~";
        Random r = new Random();
        for (int j = 0; j <= 10; j++)
        { randomText.Append(alphabets[r.Next(alphabets.Length)]); }
        ViewState["RandomText"] = obj.Encrypt(randomText.ToString());
    }

    private string ConvertText_SHA512_And_Salt(string Text)
    {
        return obj.SHA512_HASH(String.Concat(obj.SHA512_HASH(Text), ViewState["RandomText"].ToString()));
    }

    private bool CompaireHashCode(string DataBasePassword, string ClientPasswordWithHashing)
    {
        bool i;
        if (obj.SHA512_HASH(String.Concat(DataBasePassword, ViewState["RandomText"].ToString())).Equals(ClientPasswordWithHashing))
        { i = true; }
        else { i = false; }
        return i;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (txtPassword.Text != "" && txtUserName.Text != "")
                {
                    ds = obj.ByProcedure("USP_Login", new string[] { "UserName" }, new string[] { txtUserName.Text.Trim() }, "dataset");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        //if (CompaireHashCode(ds.Tables[0].Rows[0]["Password"].ToString(), txtpassword.Text))
                        //{
                        Session["Emp_Id"] = ds.Tables[0].Rows[0]["Emp_Id"].ToString();
                        Session["UserName"] = ds.Tables[0].Rows[0]["UserName"].ToString();
                        Session["Designation_Name"] = ds.Tables[0].Rows[0]["Designation_Name"].ToString();
                        Session["Designation_ID"] = ds.Tables[0].Rows[0]["UserType_Id"].ToString();
                        Session["Office_Id"] = ds.Tables[0].Rows[0]["Office_Id"].ToString();
                        Session["Office_Name"] = ds.Tables[0].Rows[0]["OfficeName"].ToString();
                        Session["Division_Id"] = ds.Tables[0].Rows[0]["Division_Id"].ToString();
                        Session["District_Id"] = ds.Tables[0].Rows[0]["District_Id"].ToString();
                        Session["OfficeType_Id"] = ds.Tables[0].Rows[0]["OfficeType_Id"].ToString();
                        Response.Redirect("~/Legal/LegalDashboard.aspx");
                        //}
                        //else
                        //{
                        //    Labelmsg.ForeColor = System.Drawing.Color.Red;
                        //    Labelmsg.Text = "Invalid Login Credentials!";
                        //}
                    }
                    else
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        lblMsg.Text = "Login Failed!<br /> UserName or Password is not correct";
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
}