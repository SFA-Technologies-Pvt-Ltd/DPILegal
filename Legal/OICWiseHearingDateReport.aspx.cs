using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class Legal_OICWiseHearingDateReport : System.Web.UI.Page
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
                GetCourtType();
                // txtHearingdt.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        else
        {
            Response.Redirect("/Login.aspx");
        }
    }
    #region MyRegion
    //private void GetOicName()
    //{
    //    try
    //    {
    //        ds = obj.ByDataSet("Select OICMaster_ID,OICName from tblOICMaster");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ddlOICName.DataSource = ds.Tables[0];
    //            ddlOICName.DataTextField = "OICNAME";
    //            ddlOICName.DataValueField = "OICMaster_ID";
    //            ddlOICName.DataBind();
    //            ddlOICName.Items.Insert(0, "Select OIC Name");
    //        }
    //        else
    //        {
    //            ddlOICName.DataSource = null;
    //            ddlOICName.DataBind();
    //            ddlOICName.Items.Insert(0, "Select Case Subject");
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }

    //} 
    #endregion
    private void GetCourtType()
    {
        try
        {
            ds = new DataSet();
            // ds = obj.ByDataSet("select * from tbl_LegalCourtType order by CourtTypeName");
            ds = obj.ByDataSet("select distinct Court from tbl_OldCaseDetail order by Court");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataSource = ds.Tables[0];
                ddlCourtType.DataTextField = "Court";
                ddlCourtType.DataValueField = "Court";
                ddlCourtType.DataBind();
                ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCourtType.DataSource = null;
                ddlCourtType.DataBind();
                ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception)
        {
        }

    }

    private void GetOICData(string HearingfromDate, string HearingTodate, string Court)
    {
        try
        {
            DataTable mytable = new DataTable();
            mytable.Columns.Add("OicId", typeof(int));
            mytable.Columns.Add("OicName", typeof(string));
            mytable.Columns.Add("Court", typeof(string));
            mytable.Columns.Add("Last30daysPendingCaseCount", typeof(string));
            mytable.Columns.Add("Last30daysCaseCount", typeof(int));
            mytable.Columns.Add("SolvedCasesCount", typeof(int));
            mytable.Columns.Add("RemainingCasesCount", typeof(int));
            mytable.Columns.Add("UniqueNo1", typeof(string));
            mytable.Columns.Add("UniqueNo2", typeof(string));
            mytable.Columns.Add("UniqueNo3", typeof(string));
            mytable.Columns.Add("UniqueNo4", typeof(string));
            mytable.Columns.Add("HearingFrom", typeof(string));
            mytable.Columns.Add("HearingTo", typeof(string));
            ds = obj.ByDataSet("select OICMaster_ID,OICName from tblOICMaster order by OICName");
            string court = ddlCourtType.SelectedItem.Text.Trim();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    DataSet ds1 = !string.IsNullOrEmpty(HearingfromDate) && !string.IsNullOrEmpty(HearingTodate) ?
                        obj.ByDataSet("Select COUNT(distinct UniqueNo) A,string_AGG(isnull(CAST(UniqueNo as nvarchar(MAX)),'NA'),'-') UniqueNo1 from tbl_OldCaseDetail where Court='" + court + "' and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),'" + HearingfromDate + "',110) and  Convert(varchar(10),'" + HearingTodate + "',110) and OICId=" + Convert.ToInt32(ds.Tables[0].Rows[i]["OICMaster_ID"].ToString()) + " and Status='Pending' group by OICId") :
                    obj.ByDataSet("Select COUNT(distinct UniqueNo) A,string_AGG(isnull(CAST(UniqueNo as nvarchar(MAX)),'NA'),'-') UniqueNo1 from tbl_OldCaseDetail where Court='" + court + "' and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),DATEADD(DAY, -30, GETDATE()),110) and  Convert(varchar(10),GETDATE(),110) and OICId=" + Convert.ToInt32(ds.Tables[0].Rows[i]["OICMaster_ID"].ToString()) + " and Status='Pending' group by OICId");

                    DataSet ds2 = !string.IsNullOrEmpty(HearingfromDate) && !string.IsNullOrEmpty(HearingTodate) ?
                    obj.ByDataSet("Select COUNT(distinct UniqueNo) B,string_AGG(isnull(CAST(UniqueNo as nvarchar(MAX)),'NA'),'-') UniqueNo2 from tbl_OldCaseDetail where Court='" + court + "' and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),'" + HearingfromDate + "',110) and  Convert(varchar(10),'" + HearingTodate + "',110) and OICId=" + Convert.ToInt32(ds.Tables[0].Rows[i]["OICMaster_ID"].ToString()) + " group by OICId") :
                    obj.ByDataSet("Select COUNT(distinct UniqueNo) B,string_AGG(isnull(CAST(UniqueNo as nvarchar(MAX)),'NA'),'-') UniqueNo2 from tbl_OldCaseDetail where Court='" + court + "' and Convert(varchar(10),HearingDate,110) between Convert(varchar(10),DATEADD(DAY, -30, GETDATE()),110) and  Convert(varchar(10),GETDATE(),110) and OICId=" + Convert.ToInt32(ds.Tables[0].Rows[i]["OICMaster_ID"].ToString()) + " group by OICId");

                    DataSet ds3 = obj.ByDataSet("Select COUNT(distinct UniqueNo) C,string_AGG(isnull(CAST(UniqueNo as nvarchar(MAX)),'NA'),'-') UniqueNo3 from tbl_OldCaseDetail where Court='" + court + "' and OICId=" + Convert.ToInt32(ds.Tables[0].Rows[i]["OICMaster_ID"].ToString()) + " and Status='Yes' group by OICId");
                    DataSet ds4 = obj.ByDataSet("Select COUNT(distinct UniqueNo) D,string_AGG(isnull(CAST(UniqueNo as nvarchar(MAX)),'NA'),'-') UniqueNo4 from tbl_OldCaseDetail where Court='" + court + "' and OICId=" + Convert.ToInt32(ds.Tables[0].Rows[i]["OICMaster_ID"].ToString()) + " and Status='No' group by OICId");
                    DataRow dr = mytable.NewRow(); //Creating Row  
                    dr["Court"] = ddlCourtType.SelectedIndex > 0 ? Convert.ToString(ddlCourtType.SelectedItem.Text) : "NA";
                    dr["OicId"] = ds.Tables.Count > 0 ? Convert.ToInt32(ds.Tables[0].Rows[i]["OICMaster_ID"].ToString()) : 0;
                    dr["OicName"] = ds.Tables.Count > 0 ? ds.Tables[0].Rows[i]["OICName"].ToString() : "";
                    dr["Last30daysPendingCaseCount"] = ds1.Tables[0].Rows.Count > 0 ? Convert.ToInt32(ds1.Tables[0].Rows[0][0].ToString()) : 0;
                    dr["UniqueNo1"] = ds1.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds1.Tables[0].Rows[0][1].ToString()) ? Convert.ToString(ds1.Tables[0].Rows[0][1].ToString()) : "NA";
                    dr["Last30daysCaseCount"] = ds2.Tables[0].Rows.Count > 0 ? Convert.ToInt32(ds2.Tables[0].Rows[0][0].ToString()) : 0;
                    dr["UniqueNo2"] = ds2.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds2.Tables[0].Rows[0][1].ToString()) ? Convert.ToString(ds2.Tables[0].Rows[0][1].ToString()) : "NA";
                    dr["SolvedCasesCount"] = ds3.Tables[0].Rows.Count > 0 ? Convert.ToInt32(ds3.Tables[0].Rows[0][0].ToString()) : 0;
                    dr["UniqueNo3"] = ds3.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds3.Tables[0].Rows[0][1].ToString()) ? Convert.ToString(ds3.Tables[0].Rows[0][1].ToString()) : "NA";
                    dr["RemainingCasesCount"] = ds4.Tables[0].Rows.Count > 0 ? Convert.ToInt32(ds4.Tables[0].Rows[0][0].ToString()) : 0;
                    dr["UniqueNo4"] = ds4.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(ds4.Tables[0].Rows[0][1].ToString()) ? Convert.ToString(ds4.Tables[0].Rows[0][1].ToString()) : "NA";
                    dr["HearingFrom"] = !string.IsNullOrEmpty(HearingfromDate.Trim()) ? HearingfromDate : "NA";
                    dr["HearingTo"] = !string.IsNullOrEmpty(HearingTodate.Trim()) ? HearingTodate : "NA";
                    mytable.Rows.Add(dr);
                }
            }
            if (mytable.Rows.Count > 0)
            {
                grdTodayHearingdtl.DataSource = mytable;
                grdTodayHearingdtl.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    #region not using
    //protected void BindGrid()
    //{
    //    try
    //    {
    //        ds = obj.ByProcedure("Sp_OICWiseHearingDateReport", new string[] { "Court" }, new string[] { ddlCourtType.SelectedItem.Value }, "dataset");
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {

    //            grdTodayHearingdtl.DataSource = ds;
    //            grdTodayHearingdtl.DataBind();
    //        }
    //        else
    //        {
    //            grdTodayHearingdtl.DataSource = null;
    //            grdTodayHearingdtl.DataBind();
    //        }
    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {

                string HearingFromDate = !string.IsNullOrEmpty(txtHearingFromDate.Text) ? (Convert.ToDateTime(txtHearingFromDate.Text, cult).ToString("yyyy/MM/dd")) : "";
                string HearingToDate = !string.IsNullOrEmpty(txtHearingToDate.Text) ? Convert.ToDateTime(txtHearingToDate.Text, cult).ToString("yyyy/MM/dd") : "";
                if (!string.IsNullOrEmpty(HearingFromDate))
                {
                    if (!string.IsNullOrEmpty(HearingFromDate))
                    {
                        if (ddlCourtType.SelectedIndex > 0)
                            GetOICData(Convert.ToDateTime(txtHearingFromDate.Text, cult).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtHearingToDate.Text, cult).ToString("dd/MM/yyyy"), ddlCourtType.SelectedItem.Text);
                        else
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "AlertMessage()", true);
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select Hearing Date To')", true);
                }
                else if (ddlCourtType.SelectedIndex > 0)
                    GetOICData("", "", ddlCourtType.SelectedItem.Text);
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "AlertMessage()", true);
                ////Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Select Hearing Date From')", true);
            }
            catch (Exception ex)
            {
                lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            }
        }
    }


    protected void grdTodayHearingdtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            //grdTodayHearingdtl.PageIndex = e.NewPageIndex;
            // BindGrid();
        }
        catch (Exception)
        {

            throw;
        }
    }


}