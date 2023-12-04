using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_MainMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                //1. spnUsername.InnerHtml = Session["UserName"].ToString() + "<br/><small>" + Session["Designation_Name"].ToString() + "</small>";
                spnUsername.InnerHtml = "<small>" + " [ " + Session["UserName"].ToString() + " ] - " + Session["EMPName"].ToString() + " - " + Session["Designation_Name"].ToString() + "</small>";
                //spnOfficenaame.InnerHtml += "<li class='header' style='font-weight: 600;text-transform: uppercase;letter-spacing: 2px;'>" + Session["Office_Name"].ToString() + "</li>";
                //lblOfficeName.InnerHtml = Session["Office_Name"].ToString();

                #region Bind  Menues
                //spnUsername.InnerHtml = Session["UserName"].ToString() + "<br/><small>" + Session["Designation_Name"].ToString() + "</small>";

                //string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                ////Response.Write(baseUrl.ToString());
                //// Navigation.InnerHtml = "<div class='user-panel'><div class='pull-left info'><p style='font-size:12px;'>" + Session["UserName"].ToString() + "</p><a href='#'><i class='fa fa-circle text-success'></i> Online</a></div></div>";
                ////Navigation.InnerHtml = "<div class='user-panel'><div class='pull-left image'><img src='" + baseUrl + "mis/HR/" + Session["Emp_ProfileImage"].ToString() + "' class='img-circle' alt='User Image'></div><div class='pull-left info'><p>" + Session["Designation_Name"].ToString() + "</p><a href='#'><i class='fa fa-circle text-success'></i> Online</a></div></div>";
                //Navigation.InnerHtml += "<ul class='nav nav-pills nav-sidebar flex-column' data-widget='treeview' role='menu' data-accordion='false'>";
                //// Navigation.InnerHtml += "<li class='header' style='font-weight: 600;text-transform: uppercase;letter-spacing: 2px;'>" + Session["Office_Name"].ToString() + "</li>";
                //// Navigation.InnerHtml += "<li><hr style='margin: 0' /></li>";
                //Navigation.InnerHtml += "<li class='nav-item'><a href='../Legal/LegalDashboard.aspx?IsMainPage=1' class='nav-link'><i class='nav-icon fas fa-list-ul text-light'></i><p class='text-light'><span style='color: #e5e5e5; font-size: 16px;'>Main Page</span><i class='fas fa-angle-left right'></i></p></a></li>";

                //if (Request.QueryString["Module_ID"] != null)
                //{
                //    Session["Module_Id"] = Request.QueryString["Module_ID"].ToString();
                //}

                //if (Session["Module_Id"] == null)
                //{

                //    DataTable dtAccess = Session["AccessModule"] as DataTable;

                //    for (int i = 0; i < dtAccess.Rows.Count; i++)
                //    {
                //        // Navigation.InnerHtml += "<li><a href='../Legal/LegalDashboard.aspx?Module_Id=" + dtAccess.Rows[i]["Module_ID"].ToString() + "' class='Aselect1'><i class='fa fa fa-hand-o-right'></i>&nbsp;" + "<span>" + dtAccess.Rows[i]["Module_Name"].ToString() + "</span></a></li>";
                //        //Navigation.InnerHtml += "<li class='nav-item'><a href='../Legal/LegalDashboard.aspx?IsMainPage=" + dtAccess.Rows[i]["Module_ID"].ToString() + "' class='nav-link'><i class='nav-icon fas fa-list-ul text-light'></i><p class='text-light'><span style='color: #e5e5e5; font-size: 16px;'>" + dtAccess.Rows[i]["Module_Name"].ToString() + "</span><i class='fas fa-angle-left right'></i></p></a></li>";
                //        Navigation.InnerHtml += "<li class='nav-item'>";
                //        //Navigation.InnerHtml += "<a href='#' class='nav-link'>" + dtAccess.Rows[i]["Menu_Icon"].ToString() + "<span>" + dtAccess.Rows[i]["Menu_Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                //        Navigation.InnerHtml += "<a href='#' class='nav-link'><i class='" + dtAccess.Rows[i]["Menu_Icon"].ToString() + "'></i><p class='text-light'><span style='color: #e5e5e5; font-size: 16px;'>" + dtAccess.Rows[i]["Menu_Name"].ToString() + "</span><i class='fas fa-angle-left right'></i></p></a>";
                //    }

                //}
                //else //if (Session["Navigation"] == null) // New Code
                //{
                //    DataTable dtAccessForm = Session["AccessForm"] as DataTable;
                //    DataView dv = new DataView();
                //    dv = dtAccessForm.DefaultView;
                //    dv.RowFilter = "Module_ID = '" + Session["Module_Id"].ToString() + "'";
                //    DataTable dtAccess = dv.ToTable();


                //    string Menu_Name = "";
                //    string NavigationLi = "";
                //    int IsMainPage = 0;
                //    for (int i = 0; i < dtAccess.Rows.Count; i++)
                //    {
                //        NavigationLi = "";
                //        IsMainPage = 0;
                //        Menu_Name = dtAccess.Rows[i]["Menu_Name"].ToString();

                //        while (dtAccess.Rows[i]["Menu_Name"].ToString() == Menu_Name)
                //        {
                //            IsMainPage++;

                //            // <a href="../UserMgmt/UMModuleMaster.aspx" class="nav-link custom_bg_color">
                //            //<i class="far fa-circle nav-icon text-light"></i>
                //            //<p class=''>Module Master</p></a>
                //            NavigationLi += "<li class='nav-item'>" +
                //                "<a href='" + dtAccess.Rows[i]["Form_Path"].ToString() + "' class='nav-link custom_bg_color'><i class='far fa-circle nav-icon text-light'></i><p class=''>Module Master</p></a></li>";
                //           //<a href='" + dtAccess.Rows[i]["Form_Path"].ToString() + "' class='nav-link'><i class='nav-icon fas fa-list-ul text-light'></i><p class=''><span style = 'color: #e5e5e5; font-size: 16px;'>" + dtAccess.Rows[i]["Form_Name"].ToString() + "</span><i class='fas fa-angle-left right'></i></p></a></li>";
                //           // "//class='Aselect1'><i class='fa fa-hand-o-right'></i>" + "<span>" + dtAccess.Rows[i]["Form_Name"].ToString() + "</span>" + "</a></li>";
                //            i++;
                //            if (dtAccess.Rows.Count == i)
                //            {
                //                break;
                //            }
                //        }
                //        i--;
                //        if (IsMainPage == 1)
                //        {
                //            Navigation.InnerHtml += NavigationLi;
                //        }
                //        else
                //        {
                //            //Navigation.InnerHtml += "<li class='nav-item'>";
                //            ////Navigation.InnerHtml += "<a href='#' class='nav-link'>" + dtAccess.Rows[i]["Menu_Icon"].ToString() + "<span>" + dtAccess.Rows[i]["Menu_Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>";
                //            //Navigation.InnerHtml += "<a href='#' class='nav-link'><i class='" + dtAccess.Rows[i]["Menu_Icon"].ToString() + "'></i><p class='text-light'><span style='color: #e5e5e5; font-size: 16px;'>" + dtAccess.Rows[i]["Menu_Name"].ToString() + "</span><i class='fas fa-angle-left right'></i></p></a>";
                //            Navigation.InnerHtml += "<ul class='nav nav-treeview'>";
                //            Navigation.InnerHtml += NavigationLi;
                //            Navigation.InnerHtml += "</ul>";
                //            Navigation.InnerHtml += "</li>";


                //        }
                //    }

                //}


                ////Navigation.InnerHtml += "<li><a href='../Admin/AdminChangePassword.aspx' class='Aselect1'><i class='fa fa-key'></i><span>Change Password</span></a></li>";
                ////Navigation.InnerHtml += "<li><a href='../Login.aspx' class='Aselect1'><i class='fa fa-power-off'></i><span>Logout</span></a></li>";
                //Navigation.InnerHtml += "</ul>";

                #endregion

                #region Rebind Menues
                if (Session["Role_ID"].ToString() == "3")
                    Navigation.InnerHtml = "<ul class='nav nav-pills nav-sidebar flex-column' data-widget='treeview' role='menu' data-accordion='false'>"
                                + "<li class='nav-item'>"
                                    + "<a href='../Legal/OICWiseOldCaseDashBoard.aspx' class='nav-link'>"
                                        + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                                        + "<p class='text-light'>"
                                            + "<span style='color: #e5e5e5; font-size: 16px;'>Dashboard</span>"
                                            //  + "<i class='fas fa-angle-left right'></i>" 
                                            + "</p></a></li>'";
                else if (Session["Role_ID"].ToString() == "1")
                    Navigation.InnerHtml = "<ul class='nav nav-pills nav-sidebar flex-column' data-widget='treeview' role='menu' data-accordion='false'>"
                                    + "<li class='nav-item'>"
                                        + "<a href='../Legal/OldCaseDashBoard.aspx' class='nav-link'>"
                                            + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                                            + "<p class='text-light'>"
                                                + "<span style='color: #e5e5e5; font-size: 16px;'>Dashboard</span>"
                                                //  + "<i class='fas fa-angle-left right'></i>" 
                                                + "</p></a></li>"
												+ "<li class='nav-item'>"
                                        + "<a href='../Legal/NewDashbord.aspx' class='nav-link'>"
                                            + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                                            + "<p class='text-light'>"
                                                + "<span style='color: #e5e5e5; font-size: 16px;'>Dynamic Graph</span>"
                                                //  + "<i class='fas fa-angle-left right'></i>" 
                                                + "</p></a></li>'";
                else if (Session["Role_ID"].ToString() == "4")
                    Navigation.InnerHtml = "<ul class='nav nav-pills nav-sidebar flex-column' data-widget='treeview' role='menu' data-accordion='false'>"
                                    + "<li class='nav-item'>"
                                        + "<a href='../Legal/DistricAdminDashBoard.aspx' class='nav-link'>"
                                            + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                                            + "<p class='text-light'>"
                                                + "<span style='color: #e5e5e5; font-size: 16px;'>Dashboard</span>"
                        //  + "<i class='fas fa-angle-left right'></i>" 
                                                + "</p></a></li>'";
                else if (Session["Role_ID"].ToString() == "2")
                    Navigation.InnerHtml = "<ul class='nav nav-pills nav-sidebar flex-column' data-widget='treeview' role='menu' data-accordion='false'>"
                                    + "<li class='nav-item'>"
                                        + "<a href='../Legal/DivisionDashBoard.aspx' class='nav-link'>"
                                            + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                                            + "<p class='text-light'>"
                                                + "<span style='color: #e5e5e5; font-size: 16px;'>Dashboard</span>"
                        //  + "<i class='fas fa-angle-left right'></i>" 
                                                + "</p></a></li>'";
                else 
                    Navigation.InnerHtml = "<ul class='nav nav-pills nav-sidebar flex-column' data-widget='treeview' role='menu' data-accordion='false'>"
                                    + "<li class='nav-item'>"
                                        + "<a href='../Legal/JDLegalDashBoard.aspx' class='nav-link'>"
                                            + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                                            + "<p class='text-light'>"
                                                + "<span style='color: #e5e5e5; font-size: 16px;'>Dashboard</span>"
                        //  + "<i class='fas fa-angle-left right'></i>" 
                                                + "</p></a></li>'";
                //  DataTable dtAccess = Session["AccessModule"] as DataTable;
                DataTable dtAccessForm = Session["AccessForm"] as DataTable;
                string menuname = string.Empty;
                DataView objdbview = new DataView(dtAccessForm);
                DataTable objdistinctitem = objdbview.ToTable(true, "Menu_Name");
                for (int i = 0; i < objdistinctitem.Rows.Count; i++)
                {
                    menuname = objdistinctitem.Rows[i]["Menu_Name"].ToString();
                    string str = string.Format("Menu_Name = '{0}'", menuname);
                    Navigation.InnerHtml = Navigation.InnerHtml.TrimEnd(Convert.ToChar("'"));
                    DataRow[] rslt = dtAccessForm.Select(str);
                    menuname = "";
                    foreach (DataRow row in rslt)
                    {
                        Navigation.InnerHtml += "<li class='nav-item'>";
                        if (menuname != row[2].ToString()) //dtAccessForm.Rows[i]["Menu_Name"].ToString())
                        {
                            menuname = row[2].ToString();

                            Navigation.InnerHtml += "<a href='#' class='nav-link'>"
                                + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                                + "<p class='text-light'>"
                                    + "<span style='color: #e5e5e5; font-size: 16px;'>" + menuname + "</span>"
                                    + "<i class='fas fa-angle-left right'></i>"
                                + "</p></a>";
                            Navigation.InnerHtml += "<ul class='nav nav-treeview'>";
                            Navigation.InnerHtml += "<li class='nav-item'>"
                                                + "<a href='" + row[4].ToString() + "' class='nav-link custom_bg_color'>"
                                                    + "<i class='far fa-circle nav-icon text-light'></i>"
                                                    + "<p class=''>" + row[3].ToString() + "</p>"
                                                + "</a></li>";
                        }
                        else
                        {
                            Navigation.InnerHtml += "<li class='nav-item'>"
                                                + "<a href='" + row[4].ToString() + "' class='nav-link custom_bg_color'>"
                                                    + "<i class='far fa-circle nav-icon text-light'></i>"
                                                    + "<p class=''>" + row[3].ToString() + "</p>"
                                                + "</a></li>";
                        }

                    }

                    Navigation.InnerHtml += "</ul>";
                    //if (i == dtAccessForm.Rows.Count)
                    Navigation.InnerHtml += "</li>";
                }
                //Navigation.InnerHtml += "<li class='nav-item'>"
                //                    + "<a href='../Library.aspx' class='nav-link'>"
                //                        + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                //                        + "<p class='text-light'>"
                //                            + "<span style='color: #e5e5e5; font-size: 16px;'>Library</span>"
                //                            //  + "<i class='fas fa-angle-left right'></i>" 
                //                            + "</p></a></li>'";

                Navigation.InnerHtml += "</ul>";
                #endregion
            }
        }
        else
        {
            //Response.Redirect("~/Login.aspx");
            Response.Redirect("~/Login.aspx",false);
        }

    }
    protected void btnlogout_Click(object sender, EventArgs e)
    {

      Session.Abandon();
        Session.Clear();
        // Response.Redirect("~/Login.aspx");
        Response.Redirect("~/Login.aspx", false);
    }
}
