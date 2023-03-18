using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
//CODE CHANGES START BY CHINMAY ON 11-JUL-2019
using System.Text;
//CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Legal_AllLegalCasesDetail : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Office_ID"] != null && Session["Emp_ID"] != null)
            {

                
                if (Request.QueryString["OfficeID"] != "" && Request.QueryString["OfficeID"] != null)
                {
                    ViewState["OfficeID"] = objdb.Decrypt(Request.QueryString["OfficeID"]);
                }
                if (Request.QueryString["myparam1"] != "" && Request.QueryString["myparam1"] != null)
                {
                    string Id = objdb.Decrypt(Request.QueryString["myparam1"]);
                    if (Id != "")
                    {
                        if (Id.ToString() == "AllCase")
                        {
                            lblCase.Text = "Total Legal Cases";
                            FillAllCaseswithOfficeId();
                        }
                        else if (Id.ToString() == "OpenCase")
                        {
                            lblCase.Text = "Open Legal Cases";
                            FillOpenCaseswithOfficeId();
                        }
                        else if (Id.ToString() == "CloseCase")
                        {
                            lblCase.Text = "Closed Legal Cases";
                            FillCloseCaseswithOfficeId();
                        }
                    }

                }
                if (Request.QueryString["myparam"] != "" && Request.QueryString["myparam"] != null)
                {
                    string myparam = Request.QueryString["myparam"];
                    if (myparam != "")
                    {
                        if (myparam.ToString() == "All")
                        {
                            lblCase.Text = "Total Legal Cases";
                            FillAllCases();
                        }
                        else if (myparam.ToString() == "Open")
                        {
                            lblCase.Text = "Open Legal Cases";
                            FillOpenCases();
                        }
                        else if (myparam.ToString() == "Close")
                        {
                            lblCase.Text = "Closed Legal Cases";
                            FillCloseCases();
                        }
                    }
                }



                ViewState["Office_ID"] = Session["Office_ID"].ToString();

            }
            else
            {
                Response.Redirect("~/index.aspx");
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillAllCases()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag" }, new string[] { "12" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOpenCases()
    {
        try
        {

            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag" }, new string[] { "22" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }

        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCloseCases()
    {
        try
        {
            GridView1.DataSource = new string[] { };
            GridView1.DataBind();
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            GridView1.UseAccessibleHeader = true;
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag" }, new string[] { "23" }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillAllCaseswithOfficeId()
    {
        try
        {
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Office_ID" }, new string[] { "25", ViewState["OfficeID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOpenCaseswithOfficeId()
    {
        try
        {
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Office_ID" }, new string[] { "26", ViewState["OfficeID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    protected void FillCloseCaseswithOfficeId()
    {
        try
        {
            ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Office_ID" }, new string[] { "27", ViewState["OfficeID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                GridView1.UseAccessibleHeader = true;
            }
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }

    //CODE CHANGES START BY CHINMAY ON 11-JUL-2019
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strCaseID = objdb.Encrypt(e.CommandArgument.ToString());

        if (e.CommandName == "View")
        {
            string url = "CaseDetail.aspx?Case_ID=" + strCaseID;

            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.open('");
            sb.Append(url);
            sb.Append("', '_blank');");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
        }
    }
    //CODE CHANGES ENDED BY CHINMAY ON 11-JUL-2019
}