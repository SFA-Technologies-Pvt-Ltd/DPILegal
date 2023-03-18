using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Legal_CaseDetailReport : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Office_ID"] != null && Session["Emp_ID"] != null)
            {

                ViewState["OfficeID"] = objdb.Decrypt(Request.QueryString["OfficeID"]);
                string casetype = objdb.Decrypt(Request.QueryString["casetype"]);
                if (casetype != "")
                {
                    if (casetype == "CivilCase")
                    {
                        ViewState["casetype"] = "Civil Case";
                    }
                    else if (casetype == "ConsumerCase")
                    {
                        ViewState["casetype"] = "Consumer Case";
                    }
                    else if (casetype == "CriminalCase")
                    {
                        ViewState["casetype"] = "Criminal Case";
                    }

                    else if (casetype == "IncometaxCase")
                    {
                        ViewState["casetype"] = "Income tax Case";
                    }
                    else if (casetype == "GSTCase")
                    {
                        ViewState["casetype"] = "GST Case";
                    }
                    else if (casetype == "ServiceMaster")
                    {
                        ViewState["casetype"] = "Service Master";
                    }
                    else
                    {
                        ViewState["casetype"] = casetype;
                    }
                }
                string courttype = objdb.Decrypt(Request.QueryString["courttype"]);
                if (courttype != "")
                {

                    if (courttype == "ConsumerCourt")
                    {
                        ViewState["courttype"] = "Consumer Court";
                    }
                    else if (courttype == "LabourCourt")
                    {
                        ViewState["courttype"] = "Labour Court";
                    }
                    else if (courttype == "DistrictCourt")
                    {
                        ViewState["courttype"] = "District Court";
                    }

                    else if (courttype == "HighCourt-Jabalpur")
                    {
                        ViewState["courttype"] = "High Court - Jabalpur";
                    }
                    else if (courttype == "HighCourt-Indore")
                    {
                        ViewState["courttype"] = "High Court -  Indore";
                    }
                    else if (courttype == "HighCourt-Gwalior")
                    {
                        ViewState["courttype"] = "High Court - Gwalior";
                    }
                    else if (courttype == "SupremeCourt")
                    {
                        ViewState["courttype"] = "Supreme Court";
                    }
                    else
                    {
                        ViewState["courttype"] = courttype;
                    }
                }
                FillGrid();


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
    protected void FillGrid()
    {
        try
        {
            ds = objdb.ByProcedure("Sp_LegalReports", new string[] { "flag", "Office_ID", "Case_Type", "Case_CourtType" }, new string[] { "4", ViewState["OfficeID"].ToString(), ViewState["casetype"].ToString(), ViewState["courttype"].ToString() }, "dataset");
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
}