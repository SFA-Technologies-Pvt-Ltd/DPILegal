﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;


public partial class mis_Legal_LegalDashboard : System.Web.UI.Page
{
    DataSet ds;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    BIndWACaseCount();
                    UpComingHearing();
                }
            }
            else
            {
                Response.Redirect("~/Legal/Login.aspx");
            }
        }

        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }

    }

    protected void UpComingHearing()
    {
        ds = objdb.ByProcedure("USP_GetUpcoming_HearingDate", new string[] { }, new string[] { }, "dataset");
        string Marquee = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    Marquee += ds.Tables[0].Rows[i]["HearingDate"].ToString();
                }
                else
                {
                    Marquee += ", " + ds.Tables[0].Rows[i]["HearingDate"].ToString();
                }

            }
            spnHearing.InnerHtml = Marquee;
        }
    }

    protected void BIndWACaseCount()
    {
        try
        {
            ds = objdb.ByProcedure("USP_Get_WACaseCount", new string[] { }, new string[] { }, "dataset");

            // Indore Court Case Count
            if (ds.Tables[0].Rows[0]["IndoreCase"].ToString() != "")
            {
                lblIndoreCases.Text = ds.Tables[0].Rows[0]["IndoreCase"].ToString() + " No's";
            }
            else { lblIndoreCases.Text = "00 No's"; }
            // Jabalpur Court Case Count
            if (ds.Tables[0].Rows[0]["JabalpurCase"].ToString() != "")
            {
                lblJabalpur.Text = ds.Tables[0].Rows[0]["JabalpurCase"].ToString() + " No's";
            }
            else { lblJabalpur.Text = "00 No's"; }
            // Gwalior Court Case Count
            if (ds.Tables[0].Rows[0]["GwaliorCase"].ToString() != "")
            {
                lblGwalior.Text = ds.Tables[0].Rows[0]["GwaliorCase"].ToString() + " No's";
            }
            else { lblGwalior.Text = "00 No's"; }
            // WA Case Count
            if (ds.Tables[0].Rows[0]["WACaseCount"].ToString() != "")
            {
                lblWACase.Text = ds.Tables[0].Rows[0]["WACaseCount"].ToString() + " No's";
            }
            else { lblWACase.Text = "00 No's"; }
            // WP Case Count
            if (ds.Tables[0].Rows[0]["WPCaseCount"].ToString() != "")
            {
                lblWPCase.Text = ds.Tables[0].Rows[0]["WPCaseCount"].ToString() + " No's";
            }
            else { lblWPCase.Text = "00 No's"; }
            // PP Case
            if (ds.Tables[0].Rows[0]["PPCase"].ToString() != "")
            {
                lblPPCase.Text = ds.Tables[0].Rows[0]["PPCase"].ToString() + " No's";
            }
            else { lblPPCase.Text = "00 No's"; }

            // DPI Case
            if (ds.Tables[0].Rows[0]["DPICase"].ToString() != "")
            {
                lblDPICase.Text = ds.Tables[0].Rows[0]["DPICase"].ToString() + " No's";
            }
            else { lblDPICase.Text = "00 No's"; }
            // JD Case
            if (ds.Tables[0].Rows[0]["JDCase"].ToString() != "")
            {
                lblJDCases.Text = ds.Tables[0].Rows[0]["JDCase"].ToString() + " No's";
            }
            else { lblJDCases.Text = "00 No's"; }
            // DEO Case
            if (ds.Tables[0].Rows[0]["DEOCase"].ToString() != "")
            {
                lblDEOCases.Text = ds.Tables[0].Rows[0]["DEOCase"].ToString() + " No's";
            }
            else { lblDEOCases.Text = "00 No's"; }
            // RSK Case
            if (ds.Tables[0].Rows[0]["RSKCase"].ToString() != "")
            {
                lblRskCases.Text = ds.Tables[0].Rows[0]["RSKCase"].ToString() + " No's";
            }
            else { lblRskCases.Text = "00 No's"; }
            // TBC Case
            if (ds.Tables[0].Rows[0]["TBCCase"].ToString() != "")
            {
                lblTBCCases.Text = ds.Tables[0].Rows[0]["TBCCase"].ToString() + " No's";
            }
            else { lblTBCCases.Text = "00 No's"; }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnJabalpur_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnJabalpur.Text == "View Detail")
            {
                string ID = "2";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnIndoreCases_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnIndoreCases.Text == "View Detail")
            {
                string ID = "3";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnGwalior_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnGwalior.Text == "View Detail")
            {
                string ID = "4";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?ID=" + Server.UrlEncode(ID));
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnWPCase_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnWPCase.Text == "View Detail")
            {
                string ID = "WPCase";
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?WPID=WPCase");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnWACase_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnWACase.Text == "View Detail")
            {
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?WAID=WACase");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void btnPPCase_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (btnPPCase.Text == "View Detail")
            {
                Response.Redirect("../Legal/Dashboard_ViewCaseDetail.aspx?WAID=WACase");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}