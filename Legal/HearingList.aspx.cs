using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_HearingList : System.Web.UI.Page
{
    DataSet ds;
    APIProcedure objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LblMsg.Text = "";
            if (Session["Office_ID"] != null && Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    FillOffice();
                    FillGrid();
                }
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
    protected void FillOffice()
    {
        try
        {
            ds = objdb.ByProcedure("SpAdminOffice",
                        new string[] { "flag" },
                        new string[] { "1" }, "dataset");

            ddloffice.DataSource = ds;
            ddloffice.DataTextField = "Office_Name";
            ddloffice.DataValueField = "Office_ID";
            ddloffice.DataBind();
            ddloffice.Items.Insert(0, new ListItem("ALL", "0"));
            ddloffice.SelectedValue = ViewState["Office_ID"].ToString();
            ddloffice.Enabled = false;
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CID = GridView1.SelectedDataKey.Value.ToString();
        Response.Redirect("HearingDetails.aspx?CID=" + objdb.Encrypt(CID));
    }
    protected void FillGrid()
    {
        try
        {
            LblMsg.Text = "";
            if(ddloffice.SelectedIndex >0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "myFunction()", true);
                GridView1.DataSource = null;
                GridView1.DataBind();
                ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag", "Office_ID" }, new string[] { "6", ddloffice.SelectedValue.ToString() }, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                }
                else
                {

                    LblMsg.Text = "There is no case registered from selected office.";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    

                }
            }
            else 
            {
                
                GridView1.DataSource = null;
                GridView1.DataBind();
                ds = objdb.ByProcedure("SpLegalCaseRegistration", new string[] { "flag" }, new string[] { "12"}, "dataset");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                    GridView1.UseAccessibleHeader = true;

                }
                else
                {

                    LblMsg.Text = "There is no case registered.";
                    GridView1.DataSource = ds;
                    GridView1.DataBind();


                }

            }
            
        }
        catch (Exception ex)
        {
            LblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void ddloffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        LblMsg.Text = "";
        FillGrid();
    }
    
}