using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_Dashboard_ViewCaseDetail : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != "" && Session["Office_Id"] != "")
        {
            if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["ID"] = Request.QueryString["ID"].ToString();
                    BindCaseDtl();
                }
            }
            else
            {
                if (Request.QueryString["WPID"] == "WPCase")
                {
                    GrdCaseDetail.DataSource = null;
                    GrdCaseDetail.DataBind();
                    ds = obj.ByProcedure("USP_Legal_GetCaseDtlForDasboard", new string[] { }
                          , new string[] { }, "dataset");

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        GrdCaseDetail.DataSource = ds;
                        GrdCaseDetail.DataBind();
                    }
                    else
                    {
                        GrdCaseDetail.DataSource = null;
                        GrdCaseDetail.DataBind();
                    }
                }
                else if (Request.QueryString["WAID"] == "WACase")
                {
                    GrdCaseDetail.DataSource = null;
                    GrdCaseDetail.DataBind();
                    ds = obj.ByProcedure("USP_Legal_GetWACaseDtlForDasboard", new string[] { }
                       , new string[] { }, "dataset");

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        GrdCaseDetail.DataSource = ds;
                        GrdCaseDetail.DataBind();
                    }
                    else
                    {
                        GrdCaseDetail.DataSource = null;
                        GrdCaseDetail.DataBind();
                    }
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void BindCaseDtl()
    {
        try
        {
            GrdCaseDetail.DataSource = null;
            GrdCaseDetail.DataBind();

            ds = obj.ByProcedure("USP_Legal_GetCaseDtlForDasboard", new string[] { "CourtType_Id" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdCaseDetail.DataSource = ds;
                GrdCaseDetail.DataBind();
            }
            else
            {
                GrdCaseDetail.DataSource = null;
                GrdCaseDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}