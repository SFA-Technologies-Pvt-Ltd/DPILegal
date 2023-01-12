using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Legal_CaseReport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
               // BindDesignation();
               
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    

    //protected void BindDesignation()
    //{
    //    try
    //    {
    //        ddlTransferOfficeName.Items.Clear();
    //        ds = obj.ByProcedure("USP_Select_Usertype", new string[] { }, new string[] { }, "dataset");
    //        if (ds != null && ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlTransferOfficeName.DataTextField = "UserType_Name";
    //            ddlTransferOfficeName.DataValueField = "UserType_Id";
    //            ddlTransferOfficeName.DataSource = ds;
    //            ddlTransferOfficeName.DataBind();
    //        }
    //        ddlTransferOfficeName.Items.Insert(0, new ListItem("Select", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //       lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
    //    }
    //}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                GrdCaseDetails.DataSource = null;
                GrdCaseDetails.DataBind();

                ds = obj.ByProcedure("USP_Legal_Select_CaseRegiDetails", new string[] { "date"}
                    , new string[] { Convert.ToDateTime(txtDate.Text, cult).ToString("yyyy/MM/dd") }, "dataset");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    GrdCaseDetails.DataSource = ds;
                    GrdCaseDetails.DataBind();
                }
                else
                {
                    GrdCaseDetails.DataSource = null;
                    GrdCaseDetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrdCaseDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);            
            string ID = e.CommandArgument.ToString();
            Response.Redirect("../Legal/EditCaseDetail.aspx?ID=" + Server.UrlEncode(ID));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}