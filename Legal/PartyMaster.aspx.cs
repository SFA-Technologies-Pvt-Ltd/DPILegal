using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Legal_PartyMaster : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                DesignationBind();
                BindGrid();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }
    #region Designation Bind
    protected void DesignationBind()
    {
        try
        {
            ddlDesignationName.Items.Clear();
            ds = obj.ByDataSet("select Designation_Id, Designation_Name from tblDesignationMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignationName.DataTextField = "Designation_Name";
                ddlDesignationName.DataValueField = "Designation_Id";
                ddlDesignationName.DataSource = ds;
                ddlDesignationName.DataBind();
            }
            ddlDesignationName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    protected void BindGrid()
    {
        try
        {

            ds = obj.ByProcedure("USP_Select_PartyMst", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdPartyName.DataSource = ds;
                GrdPartyName.DataBind();
            }
            else
            {
                GrdPartyName.DataSource = null;
                GrdPartyName.DataBind();
            }
            GrdPartyName.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrdPartyName.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("Usp_Insert_PartyMst", new string[] { "PartyName", "Designation_Id", "CreatedBy", "CreatedByIP" },
                        new string[] { txtPartyName.Text.Trim(), ddlDesignationName.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["Party_ID"].ToString() != "" && ViewState["Party_ID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_PartyMst", new string[] { "PartyName", "Designation_Id", "LastUpdatedBy", "LastUpdatedByIp", "Party_ID" }
                        , new string[] { txtPartyName.Text.Trim(), ddlDesignationName.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Party_ID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtPartyName.Text = "";
                        ddlDesignationName.ClearSelection();
                        BindGrid();
                        btnSave.Text = "Save";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                    }
                    else
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-warning", "Warning !", ErrMsg);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                        txtPartyName.Text = "";
                        ddlDesignationName.ClearSelection();
                    }

                }
                GrdPartyName.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdPartyName.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdPartyName_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdPartyName.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdPartyName_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["Party_ID"] = "";
            if (e.CommandName == "EditDetails")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblPartyName = (Label)row.FindControl("lblPartyName");
                Label lblID = (Label)row.FindControl("lblID");
                Label lblDesignationId = (Label)row.FindControl("lblDesignationId");
                txtPartyName.Text = lblPartyName.Text;
                if (lblDesignationId.Text != "")
                {
                    ddlDesignationName.ClearSelection();
                    ddlDesignationName.Items.FindByValue(lblDesignationId.Text).Selected = true;
                }
                ViewState["Party_ID"] = e.CommandArgument;
                btnSave.Text = "Update";

            }
            if (e.CommandName == "DeleteDetails")
            {
                int Party_ID = Convert.ToInt32(e.CommandArgument);
                obj.ByTextQuery("delete from tblPartyMaster where Party_ID=" + Party_ID);
                BindGrid();
            }
            GrdPartyName.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrdPartyName.UseAccessibleHeader = true;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}