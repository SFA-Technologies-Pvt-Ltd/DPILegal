using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResetPassword : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                string enc = url.Split('?')[1].ToString();


                if (!string.IsNullOrEmpty(enc))
                {
                    DataSet ds = obj.ByDataSet("select a.UserEmail,a.EMPName,a.UserName,a.UserPassword" +
                        "" +
                        ",a.UserId,b.CreatedDate,b.ExpiryDate from tblUserMaster a inner join tblManageResetPassword b on a.UserId = b.UserId  where b.Isactive=1 order by b.CreatedDate desc");
                    if (ds.Tables.Count > 0)
                    {
                        ViewState["dt"] = ds.Tables[0];
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ConvertText_SHA512_And_Salt("num=" + ds.Tables[0].Rows[0]["userid"].ToString()) == enc)
                            {
                                ViewState["CreatedDate"] = ds.Tables[0].Rows[0]["CreatedDate"].ToString();
                                ViewState["ExpiryDate"] = ds.Tables[0].Rows[0]["ExpiryDate"].ToString();
                                ViewState["userid"] = ds.Tables[0].Rows[0]["userid"].ToString();
                                ViewState["UserEmail"] = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                                txtUserPassword.Visible = true;
                                txtConfirmPassword.Visible = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    txtUserPassword.Visible = false;
                    txtConfirmPassword.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Email sent.');", true);
                } 
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    private string ConvertText_SHA512_And_Salt(string Text)
    {
        return obj.SHA512_HASH(Text);
    }
    protected void btnResetPassword_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime Curr_Date = Convert.ToDateTime(ViewState["CreatedDate"]);
            DateTime Expi_Date = Convert.ToDateTime(ViewState["ExpiryDate"]);

            if (Expi_Date > Curr_Date && txtUserPassword.Visible)
            {
                string userid = Convert.ToString(ViewState["userid"]);
                string email = Convert.ToString(ViewState["UserEmail"]);
                DataSet ds = obj.ByProcedure("Sp_UpdatePassword", new string[] { "userpassword", "userid", "UserEmail" }
                            , new string[] { ConvertText_SHA512_And_Salt(txtUserPassword.Text), userid, email }, "dataset");
                if (ds != null)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + "Password Changed Successfully" + "', 'success')", true);
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                string userid = Convert.ToString(ViewState["userid"]);
                string email = Convert.ToString(ViewState["UserEmail"]);
                obj.ByTextQuery("update tblManageResetPassword set Isactive=0 where UserId =" + userid + " and UserEmail='" + email+"'");
                //lblMsg.Text = "Reset Password link is Expired. Please try again to Forget Password";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Reset Password link is Expired. Please try again to Forget Password');", true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Reset Password link is Expired. Please try again to Forget Password";
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}