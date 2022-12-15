using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_UserRegistration : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                BIndOfficeType();
                BindUserDetails();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }

    protected void BindUserDetails()
    {
        try
        {
            grdUserDetails.DataSource = null;
            grdUserDetails.DataBind();
            ds = obj.ByProcedure("USP_Legal_Select_UserMaster", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                grdUserDetails.DataSource = ds;
                grdUserDetails.DataBind();
            }
            else
            {
                grdUserDetails.DataSource = ds;
                grdUserDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void BIndOfficeType()
    {
        try
        {
            ddlofficetype.Items.Clear();
            ds = obj.ByProcedure("USP_Select_Officetype", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlofficetype.DataTextField = "OfficeType_Name";
                ddlofficetype.DataValueField = "OfficeType_Id";
                ddlofficetype.DataSource = ds;
                ddlofficetype.DataBind();
            }
            ddlofficetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (ViewState["UPAGETOKEN"].ToString() == Session["PAGETOKEN"].ToString())
                {

                    if (btnSave.Text == "Save")
                    {
                        ds = obj.ByProcedure("USP_Legal_Insert_UserMaster", new string[] { "EMPName", "UserName", "MobileNo", "Office_Id", "UserType_Id", "CreatedBy", "CreatedByIP" }
                            , new string[] { txtEmpployeeName.Text.Trim(), ddlOfficeName.SelectedItem.Text.Trim(), txtMobileNo.Text.Trim(), ddlOfficeName.SelectedValue, ddlUsertype.SelectedValue, "1", obj.GetLocalIPAddress() }, "dataset");

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                                ddlUsertype.ClearSelection();
                                ddlOfficeName.ClearSelection();
                                txtEmpployeeName.Text = "";
                                ddlofficetype.ClearSelection();
                                txtMobileNo.Text = "";
                            }
                            else
                            {
                                lblMsg.Text = obj.Alert("fa-check", "alert-warning", "Warning !", ErrMsg);
                            }
                        }
                        else
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                        }
                    }
                    Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                btnSave.Text = "Save";
                BindUserDetails();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlofficetype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlofficetype.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataTextField = "OfficeName";
                ddlOfficeName.DataValueField = "Office_Id";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
            }
            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                ddlUsertype.DataTextField = "UserType_Name";
                ddlUsertype.DataValueField = "UserType_Id";
                ddlUsertype.DataSource = ds.Tables[1];
                ddlUsertype.DataBind();
            }
            ddlUsertype.Items.Insert(0, new ListItem("Select", "0"));
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}