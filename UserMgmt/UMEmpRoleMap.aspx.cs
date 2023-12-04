using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class mis_UserMgmt_UMEmpRoleMap : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divGrid.Visible = false;
            if (Session["Emp_Id"] != null)
            {
                if (!IsPostBack)
                {
                    GetUserType();
                    Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["Emp_ID"] = Session["Emp_Id"].ToString();
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPageTokan"] = Session["PageTokan"];
    }
    private void GetUserType()
    {
        try
        {
            ds = objdb.ByProcedure("SpEmployeeRoleMap",
                        new string[] { "flag" },
                        new string[] { "7" }, "dataset");

            if (ds.Tables[0].Rows.Count != 0)
            {
                ddlUserType.DataSource = ds;
                ddlUserType.DataTextField = "UserTypeName";
                ddlUserType.DataValueField = "UserTypeId";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ds.Clear();
                ddlUserType.DataSource = ds;
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
        finally
        {
            if (ds != null) { ds.Dispose(); }
        }
        
    }
    protected void ddlEmployye_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        divGrid.Visible = false;
        lblMsg.Text = "";
        if (ddlEmployye_Name.SelectedIndex > 0)
        {
            divGrid.Visible = true;
            ds = objdb.ByProcedure("SpEmployeeRoleMap",
                       new string[] { "flag", "Emp_ID", "UserTypeId" },
                       new string[] { "4", ddlEmployye_Name.SelectedValue.ToString(), ddlUserType.SelectedValue }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = true;
            }
            else if (ds != null && ds.Tables[0].Rows.Count == 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                btnSave.Visible = false;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnSave.Visible = false;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                if (ViewState["UPageTokan"].ToString() == Session["PageTokan"].ToString())
                {
                    string IPAddress = Request.ServerVariables["REMOTE_ADDR"];
                    bool minoneselect = false;
                    int RowNo = 0;

                    ds = objdb.ByProcedure("SpEmployeeRoleMap",
                        new string[] { "flag", "Emp_ID", "UserTypeId" },
                        new string[] { "3", ddlEmployye_Name.SelectedValue.ToString(), ddlUserType.SelectedValue }, "dataset");

                    foreach (GridViewRow gvrow in GridView1.Rows)
                    {
                        CheckBox chk = (CheckBox)gvrow.Cells[0].FindControl("chkSelect") as CheckBox;
                        if (chk.Checked)
                        {
                            minoneselect = true;
                            Label lblRole_ID = (Label)gvrow.FindControl("lblRole_ID");

                            string RoleID = lblRole_ID.Text;
                            string EmployeeID = ddlEmployye_Name.SelectedValue.ToString();


                            objdb.ByProcedure("SpEmployeeRoleMap",
                                new string[] { "flag", "Emp_ID", "Role_ID", "UserTypeId", "EmpRoleMap_UpdatedBy", "CreatedByIP" },
                                new string[] { "2", EmployeeID, RoleID, ddlUserType.SelectedValue, ViewState["Emp_ID"].ToString(), IPAddress }, "dataset");
                            RowNo++;
                            divGrid.Visible = true;
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Operation Successfully Completed', 'success')", true);
                            //lblMsg.Text = objdb.Alert("fa-check", "alert-success", "Thank You!", "Operation Successfully Completed");
                        }
                        else
                        {
                            minoneselect = true;
                            RowNo++;
                            divGrid.Visible = true;
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','Select Employee Name' , 'warning')", true);
                    //lblMsg.Text = objdb.Alert("fa-warning", "alert-warning", "Warning!", " Select Employee Name");
                }
                Session["PageTokan"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divGrid.Visible = false;
            lblMsg.Text = "";
            rfv1.ErrorMessage = "Select " + ddlUserType.SelectedItem.Text;
            rfv1.Text = "<i class='fa fa-exclamation-circle' title='Select " + ddlUserType.SelectedItem.Text +" !'></i>";
            if (ddlUserType.SelectedIndex > 0)
            {
                lblUserName.Text = ddlUserType.SelectedItem.Text;
                divGrid.Visible = true;
                ds = objdb.ByProcedure("SpEmployeeRoleMap",
                           new string[] { "flag", "OfficeType_Id" },
                           new string[] { "6", ddlUserType.SelectedValue }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlEmployye_Name.DataSource = ds;
                    ddlEmployye_Name.DataTextField = "Emp";
                    ddlEmployye_Name.DataValueField = "UserId";
                    ddlEmployye_Name.DataBind();
                    ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                {
                    ds.Clear();
                    ddlEmployye_Name.DataSource = ds;
                    ddlEmployye_Name.DataBind();
                    ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
            else
            {
                lblUserName.Text = "Employee";
                ds.Clear();
                ddlEmployye_Name.DataSource = ds;
                ddlEmployye_Name.DataBind();
                ddlEmployye_Name.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}