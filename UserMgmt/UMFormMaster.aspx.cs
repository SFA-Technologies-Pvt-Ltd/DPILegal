using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class UserMgmt_UMFormMaster : System.Web.UI.Page
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
                    ViewState["Emp_ID"] = Session["Emp_Id"].ToString();
                    ViewState["Form_ID"] = "0";
                    FillModule();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                   // FillGrid();
                    lblMsg.Text = "";
                    lblRecord.Text = "";
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
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    protected void FillModule()
    {
        try
        {
           DataSet dsM = objdb.ByProcedure("SpUMModuleMaster",
                new string[] { "flag" },
                new string[] { "2" }, "dataset");

           if (dsM != null && dsM.Tables[0].Rows.Count > 0)
            {
                ddlModule_Name.DataSource = dsM;
                ddlModule_Name.DataTextField = "Module_Name_E";
                ddlModule_Name.DataValueField = "Module_ID";
                ddlModule_Name.DataBind();
                ddlModule_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillGrid()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpUMFormMaster",new string[] { "flag" },new string[] { "6" }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblRecord.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FG_AfterInsertion()
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            ds = objdb.ByProcedure("SpUMFormMaster", new string[] { "flag" , "Module_ID" }, new string[] { "9", ddlModule_Name.SelectedValue }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblRecord.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

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
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            string Form_IsActive = "1";
           
            if (Page.IsValid)
            {
                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {

                    ds = objdb.ByProcedure("SpUMFormMaster",
                          new string[] { "flag", "Form_Name", "Form_ID" },
                          new string[] { "4", txtForm_Name.Text.Trim(), ViewState["Form_ID"].ToString() }, "dataset");

                    if (btnSave.Text == "Save" && ViewState["Form_ID"].ToString() == "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMFormMaster",
                        new string[] { "flag", "Form_IsActive", "Form_Name", "Form_Name_H", "Module_ID", "Form_Path", "OrderBy", "Form_UpdatedBy", "CreatedByIP" },
                        new string[] { "0", Form_IsActive, txtForm_Name.Text.Trim(), txtForm_Name_Hi.Text.Trim(), ddlModule_Name.SelectedValue, txtForm_Path.Text.Trim(), txtOrderBy.Text.Trim(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Operation Successfully Completed', 'success')", true);
                        //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");

                    }

                    else if (btnSave.Text == "Edit" && ViewState["Form_ID"].ToString() != "0" && ds.Tables[0].Rows.Count == 0)
                    {
                        objdb.ByProcedure("SpUMFormMaster",
                        new string[] { "flag", "Form_ID", "Form_Name", "Form_Name_H", "Module_ID", "Form_Path", "OrderBy", "Form_UpdatedBy", "CreatedByIP" },
                        new string[] { "7", ViewState["Form_ID"].ToString(), txtForm_Name.Text.Trim(), txtForm_Name_Hi.Text.Trim(), ddlModule_Name.SelectedValue, txtForm_Path.Text.Trim(), txtOrderBy.Text.Trim(), ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Operation Successfully Completed', 'success')", true);
                        ViewState["Form_ID"] = 0;
                    }
                    else
                    {
                        string Form_Name = ds.Tables[0].Rows[0]["Form_Name_E"].ToString();
                        string OrderBy = ds.Tables[0].Rows[0]["OrderBy"].ToString();
                        if (Form_Name == txtForm_Name.Text)
                        {
                            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "This Form Name Is Already Exist.");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('This Form Name Is Already Exist');", true);
                        }
                        if (OrderBy == txtOrderBy.Text)
                        {
                            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Alert !", "Order Number Is Already Exist.");
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "alert('Order Number Is Already Exist');", true);
                        }

                    }

                }
                else
                {
                    lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Enter Form Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());

                ClearField();
                btnSave.Text = "Save";
                FG_AfterInsertion();
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
            string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
            lblMsg.Text = "";
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox chk = (CheckBox)GridView1.Rows[selRowIndex].FindControl("chkSelect");
            string Form_ID = chk.ToolTip.ToString();
            string Form_IsActive = "0";
            if (chk != null & chk.Checked)
            {
                Form_IsActive = "1";
            }
            objdb.ByProcedure("SpUMFormMaster",
                       new string[] { "flag", "Form_IsActive", "Form_ID", "Form_UpdatedBy", "CreatedByIP" },
                       new string[] { "8", Form_IsActive, Form_ID, ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
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
            ViewState["Form_ID"] = GridView1.SelectedValue.ToString();
            lblMsg.Text = "";
            ds = objdb.ByProcedure("SpUMFormMaster",
                       new string[] { "flag", "Form_ID" },
                       new string[] { "3", ViewState["Form_ID"].ToString() }, "dataset");

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlModule_Name.ClearSelection();
                ddlModule_Name.Items.FindByValue(ds.Tables[0].Rows[0]["Module_ID"].ToString()).Selected = true;
                txtForm_Name.Text = ds.Tables[0].Rows[0]["Form_Name_E"].ToString();
                txtForm_Name_Hi.Text = ds.Tables[0].Rows[0]["Form_Name_H"].ToString();
                txtForm_Path.Text = ds.Tables[0].Rows[0]["Form_Path"].ToString();
                txtOrderBy.Text = ds.Tables[0].Rows[0]["OrderBy"].ToString();
                btnSave.Text = "Edit";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ClearField()
    {
       // ddlModule_Name.ClearSelection();
        txtForm_Name.Text = "";
        txtForm_Path.Text =  "";
        txtOrderBy.Text = "";
        txtForm_Name_Hi.Text = "";
    }
    protected void ddlModule_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            lblRecord.Text = "";
            txtForm_Name.Text = "";
            txtForm_Path.Text = "";
            txtOrderBy.Text = "";
            ds = objdb.ByProcedure("SpUMFormMaster",
                      new string[] { "flag", "Module_ID" },
                      new string[] { "5", ddlModule_Name.SelectedValue }, "dataset");

            if(ds.Tables[0].Rows.Count > 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();   
            }
            else if (ds.Tables[0].Rows.Count == 0)
            {
                lblRecord.Text = "";
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                lblRecord.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void lnkClear_Click(object sender, EventArgs e)
    {
        txtForm_Name.Text = string.Empty;
        txtForm_Name_Hi.Text = "";
        txtForm_Path.Text = string.Empty;
        txtOrderBy.Text = string.Empty;
        lblMsg.Text = string.Empty;
        GridView1.SelectedIndex = -1;
        ddlModule_Name.SelectedIndex = -1;
    }
}