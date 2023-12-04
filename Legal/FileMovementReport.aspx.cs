using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Legal_FileMovementReport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                FillCourt();
                FillCasetype();
                FillHODName();
            }
        }
        else { Response.Redirect("~/Login.aspx", false); }
    }

    protected void FillCourt()
    {
        try
        {
            ddlCourtName.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = new DataTable();
            if (Session["Role_ID"].ToString() == "5")// Court.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "4")// District Office.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;

            }
            else if (Session["Role_ID"].ToString() == "3")// OIC Login
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "2")// Division Office.
            {
                string Division_Id = Session["Division_Id"].ToString();
                dtCourt = court.GetCourtByDivision(Division_Id) as DataTable;

            }
            else dtCourt = court.GetCourt() as DataTable;
            if (dtCourt.Rows.Count > 0)
            {
                ddlCourtName.DataValueField = "CourtType_ID";
                ddlCourtName.DataTextField = "CourtTypeName";
                ddlCourtName.DataSource = dtCourt;
                ddlCourtName.DataBind();
                ddlCourtName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillCasetype()
    {
        try
        {
            Helper hl = new Helper();
            DataTable dt = hl.GetCasetype() as DataTable;
            if (dt.Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = dt;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillCaseNo()
    {
        try
        {
            ddlCaseNo.Items.Clear();
            DataTable dtCN = new DataTable();
            Helper CaseNo = new Helper();
            if (Session["Role_ID"].ToString() == "4")
            {
                dtCN = CaseNo.GetDistrictWiseCaseNo(Session["District_Id"].ToString()) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "2")
            {
                string Division_Id = Session["Division_Id"].ToString();
                dtCN = CaseNo.GetDvisionWiseCaseNo(Division_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                string District_Id = Session["District_Id"].ToString();
                dtCN = CaseNo.GetCourtWiseCaseNo(District_Id) as DataTable;
            }
            else
            {
                if (!string.IsNullOrEmpty(Session["OICMaster_ID"].ToString()))
                {
                    dtCN = CaseNo.GetOICWiseCaseNo(Session["OICMaster_ID"].ToString()) as DataTable;
                }
                else
                {
                    string CourtType_Id = ddlCourtName.SelectedValue;
                    dtCN = CaseNo.GetCaseNoByCourt(CourtType_Id) as DataTable;

                }
            }

            if (dtCN != null && dtCN.Rows.Count > 0)
            {
                ddlCaseNo.DataValueField = "Case_ID";
                ddlCaseNo.DataTextField = "CaseNo";
                ddlCaseNo.DataSource = dtCN;
                ddlCaseNo.DataBind();
                
            }
            ddlCaseNo.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillHODName()
    {
        try
        {
            ddlHodName.ClearSelection();
            DataSet dsHod = obj.ByDataSet("select HOD_ID, HodName from tblHODMaster where HOD_Id in(2,3,4,5,6,7)");
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlHodName.DataTextField = "HodName";
                ddlHodName.DataValueField = "HOD_ID";
                ddlHodName.DataSource = dsHod;
                ddlHodName.DataBind();
                ddlHodName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }


    protected void ddlCourtName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillCaseNo();
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
            GrdFileMovementReport.DataSource = null;
            GrdFileMovementReport.DataBind();

            ds = obj.ByProcedure("Usp_GetFileMovementRpt", new string[] { "Casetype_ID", "Court_Id", "CaseNo", "HOD_Id" }
                       , new string[] { ddlCasetype.SelectedValue, ddlCourtName.SelectedValue, ddlCaseNo.SelectedItem.Text, ddlHodName.SelectedValue, }, "dataset");

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GrdFileMovementReport.DataSource = ds;
                    GrdFileMovementReport.DataBind();
                    GrdFileMovementReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GrdFileMovementReport.UseAccessibleHeader = true;
                }
            }
            else
            {
                GrdFileMovementReport.DataSource = null;
                GrdFileMovementReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}