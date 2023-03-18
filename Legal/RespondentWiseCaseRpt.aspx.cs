using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_RespondentWiseCaseRpt : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                FillDitrict();
                GetCaseType();
            }
        }
        else
        {
            Response.Redirect("/Login.aspx", false);
        }
    }

    private void GetCaseType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_Legal_Casetype");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = ds.Tables[0];
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseType.DataSource = null;
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillDitrict()
    {
        try
        {
            ddlDistrict.Items.Clear();
            if (Session["Role_ID"].ToString() == "2")
            {
                string Division_Id = Session["Division_Id"].ToString();
                ds = obj.ByDataSet("select District_ID, District_Name from  Mst_District where Division_Id =" + Division_Id + "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = ds;
                    ddlDistrict.DataBind();
                    ddlDistrict.Enabled = true;
                }
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }
            else if (Session["Role_ID"].ToString() == "4")
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByDataSet("select District_ID, District_Name from  Mst_District where District_ID = " + District_Id + "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = ds;
                    ddlDistrict.DataBind();
                    ddlDistrict.Enabled = false;
                }
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                string District_Id = Session["District_Id"].ToString();
                ds = obj.ByDataSet("select T1.District_ID, District_Name from  Mst_District T1 inner join tbl_LegalCourtType T2 on T1.District_ID=T2.District_Id and T2.Isactive=1"+
                                    "where T1.District_Id="+ District_Id+"");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = ds;
                    ddlDistrict.DataBind();
                    ddlDistrict.Enabled = false;
                }
            }
            else
            {
                ds = obj.ByDataSet("select District_ID, District_Name from  Mst_District");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = ds;
                    ddlDistrict.DataBind();
                    ddlDistrict.Enabled = true;
                }
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }

    protected void BindGrid()
    {
        try
        {
            string OICID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
            if (Session["Role_ID"].ToString() == "4")
            {
                ds = obj.ByProcedure("USP_RespondentWIseCaseList", new string[] { "flag", "Casetype_ID", "District_ID", "OICMaster_Id" },
                   new string[] { "2", ddlCaseType.SelectedItem.Value, ddlDistrict.SelectedItem.Value, OICID }, "dataset");
            
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                ds = obj.ByProcedure("USP_RespondentWIseCaseList", new string[] { "flag", "Casetype_ID", "District_ID", "OICMaster_Id" },
                  new string[] { "3", ddlCaseType.SelectedItem.Value, ddlDistrict.SelectedItem.Value, OICID }, "dataset");
            }
            else
            {
                ds = obj.ByProcedure("USP_RespondentWIseCaseList", new string[] { "flag", "Casetype_ID", "CourtLocation_Id", "OICMaster_Id" },
                   new string[] { "1", ddlCaseType.SelectedItem.Value, ddlDistrict.SelectedItem.Value, OICID }, "dataset");
            
            }
               
            if (ds.Tables[0].Rows.Count > 0)
            {

                grdSubjectWiseCasedtl.DataSource = ds;
                grdSubjectWiseCasedtl.DataBind();
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
            }
            else
            {
                grdSubjectWiseCasedtl.DataSource = null;
                grdSubjectWiseCasedtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + e.CommandArgument.ToString() + "&pageID=" + 5, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }

}
