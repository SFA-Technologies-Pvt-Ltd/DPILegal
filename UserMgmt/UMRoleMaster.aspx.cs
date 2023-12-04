using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class UserMgmt_UMRoleMaster : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_Id"] != null)
            {
                if (!IsPostBack)
                {
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Emp_ID"] = Session["Emp_Id"].ToString();
                    ViewState["Role_ID"] = "0";
                    FillGrid();
                    lblMsg.Text = "";
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpUMRoleMaster",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");
            GridView1.DataSource = ds;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
           
            string Role_IsActive = "1";
            
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds = objdb.ByProcedure("SpUMRoleMaster",
                          new string[] { "flag", "Role_Name", "Role_ID" },
                          new string[] { "4", txtRole_Name.Text.Trim(), ViewState["Role_ID"].ToString() }, "dataset");

                    if (btnSave.Text == "Save" && ViewState["Role_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMRoleMaster",
                        new string[] { "flag", "Role_IsActive", "Role_Name", "Role_Name_H", "Role_UpdatedBy", "CreatedByIP" },
                        new string[] { "0", Role_IsActive, txtRole_Name.Text.Trim(),txtRole_Name_H.Text.Trim(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Operation Successfully Completed', 'success')", true);
                    }
                    else if (btnSave.Text == "Edit" && ViewState["Role_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMRoleMaster",
                        new string[] { "flag", "Role_ID", "Role_Name", "Role_Name_H", "Role_UpdatedBy", "CreatedByIP" },
                        new string[] { "5", ViewState["Role_ID"].ToString(), txtRole_Name.Text.Trim(), txtRole_Name_H.Text.Trim(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Operation Successfully Completed', 'success')", true);

                        ViewState["Role_ID"] = "0";
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','This Role Is Already Exist.' , 'warning')", true);
                    }

                    txtRole_Name.Text = "";
                    txtRole_Name_H.Text = "";
                    btnSave.Text = "Save";
                    FillGrid();
                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Menu Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string Role_ID = chk.ToolTip.ToString();
            string Role_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Role_IsActive = "1";
            }
            objdb.ByProcedure("SpUMRoleMaster",
                       new string[] { "flag", "Role_IsActive", "Role_ID", "Role_UpdatedBy", "CreatedByIP" },
                       new string[] { "6", Role_IsActive, Role_ID, ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["Role_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpUMRoleMaster",
                       new string[] { "flag", "Role_ID" },
                       new string[] { "3", ViewState["Role_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRole_Name.Text = ds.Tables[0].Rows[0]["Role_Name_E"].ToString();
                txtRole_Name_H.Text = ds.Tables[0].Rows[0]["Role_Name_H"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void lnkClear_Click(object sender, EventArgs e)
    {
       
        lblMsg.Text = string.Empty;
        GridView1.SelectedIndex = -1;
        btnSave.Text = "Save";
        txtRole_Name.Text = "";
        txtRole_Name_H.Text = "";
    }

}