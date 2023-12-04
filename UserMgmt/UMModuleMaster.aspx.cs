using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class UserMgmt_UMModuleMaster : System.Web.UI.Page
{
    DataSet ds1 = new DataSet();
    AbstApiDBApi objdb = new APIProcedure();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_Id"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_Id"].ToString();
                    ViewState["Module_ID"] = "0";
                    FillGrid();
                    //lblMsg.Text = "";
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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

            ds1 = objdb.ByProcedure("SpUMModuleMaster",
                new string[] { "flag" },
                new string[] { "1" }, "dataset");

            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds1.Tables[0];
                GridView1.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Module_IsActive = "1";

            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    ds1 = objdb.ByProcedure("SpUMModuleMaster",
                           new string[] { "flag", "Module_Name", "Module_ID" },
                           new string[] { "4", txtModule_Name.Text.Trim(), ViewState["Module_ID"].ToString() }, "dataset");


                    if (btnSave.Text == "Save" && ViewState["Module_ID"].ToString() == "0" && ds1.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMModuleMaster",
                        new string[] { "flag", "Module_IsActive", "Module_Name", "Module_Name_H", "Module_UpdatedBy", "CreatedByIP" },
                        new string[] { "0", Module_IsActive, txtModule_Name.Text.Trim(), txtModule_Name_Hi.Text.Trim(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Operation Successfully Completed', 'success')", true);
                    }


                    else if (btnSave.Text == "Edit" && ViewState["Module_ID"].ToString() != "0" && ds1.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMModuleMaster",
                        new string[] { "flag", "Module_ID", "Module_Name", "Module_Name_H", "Module_UpdatedBy", "CreatedByIP" },
                        new string[] { "5", ViewState["Module_ID"].ToString(), txtModule_Name.Text.Trim(),txtModule_Name_Hi.Text.Trim()
                            , ViewState["Emp_ID"].ToString(), IPAddress}, "dataset");

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Operation Successfully Completed', 'success')", true);
                    }
                    else
                    {
                        //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Module Is Already Exist.");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Module  Is Already Exist');", true);
                    }

                    txtModule_Name.Text = "";
                    txtModule_Name_Hi.Text = "";
                    btnSave.Text = "Save";
                    FillGrid();
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','Enter Module Name' , 'warning')", true);
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
            string Module_ID = chk.ToolTip.ToString();
            string Module_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Module_IsActive = "1";
            }
            objdb.ByProcedure("SpUMModuleMaster",
                       new string[] { "flag", "Module_IsActive", "Module_ID", "Module_UpdatedBy", "CreatedByIP" },
                       new string[] { "6", Module_IsActive, Module_ID, ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
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
            ViewState["Module_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds1 = objdb.ByProcedure("SpUMModuleMaster",
                       new string[] { "flag", "Module_ID" },
                       new string[] { "3", ViewState["Module_ID"].ToString() }, "dataset");

            if (ds1.Tables[0].Rows.Count > 0)
            {
                txtModule_Name.Text = ds1.Tables[0].Rows[0]["Module_Name_E"].ToString();
                txtModule_Name_Hi.Text = ds1.Tables[0].Rows[0]["Module_Name_H"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds1 != null) { ds1.Dispose(); }
        }
    }
    protected void lnkClear_Click(object sender, EventArgs e)
    {
        txtModule_Name.Text = string.Empty;
        txtModule_Name_Hi.Text = "";
        lblMsg.Text = string.Empty;
        GridView1.SelectedIndex = -1;
    }
}