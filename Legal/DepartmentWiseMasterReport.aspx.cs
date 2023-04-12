using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_DepartmentWiseMasterReport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillYear();
                FillDepartment();
                FillDistrict();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
    }

    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 1950; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    protected void FillDepartment()
    {
        try
        {
            ddlDepartment.Items.Clear();
            DataSet dsDept = obj.ByDataSet("select Dept_ID, Dept_Name from tblDepartmentMaster where Isactive = 1");
            if (dsDept.Tables.Count > 0)
            {
                if (dsDept.Tables[0].Rows.Count > 0)
                {
                    ddlDepartment.DataTextField = "Dept_Name";
                    ddlDepartment.DataValueField = "Dept_ID";
                    ddlDepartment.DataSource = dsDept;
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillDistrict()
    {
        try
        {
            ddlDistrict.Items.Clear();
            DataSet dsDept = obj.ByDataSet("select District_ID, District_Name from Mst_District where Isactive = 1");
            if (dsDept.Tables.Count > 0)
            {
                if (dsDept.Tables[0].Rows.Count > 0)
                {
                    ddlDistrict.DataTextField = "District_Name";
                    ddlDistrict.DataValueField = "District_ID";
                    ddlDistrict.DataSource = dsDept;
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                }
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
            lblMsg.Text = "";
            if (btnSearch.Text == "Export")
            {
                ds = obj.ByProcedure("USP_GetDepartmentWiseRpt", new string[] { "District_Id", "CaseYear", "Department_Id" },
                  new string[] { ddlDistrict.SelectedValue, ddlCaseYear.SelectedItem.Text.Trim(), ddlDepartment.SelectedValue }, "dataset");

            }
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtG = ds.Tables[0];
                string fileName = "BonusSheet_Accepted_" + DateTime.Now.ToString() + ".xls";
                //Add Response header
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + "Department wise Rpt" + "_" + DateTime.Now.ToString("dd/MM/yyyyhh_mm_ss") + ".csv");
                System.Type.GetType("System.String");
                Response.Charset = "";
                Response.ContentType = "application/vnd.xls";
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

                int ig = 0;
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Sr#"
                        + "\t Filling No."
                        + "\t Petitioner"
                        + "\t Petitioner Post"
                        + "\t PetitionerMobileNo"
                        + "\t RepondentName"
                        + "\t RespondentMobileNo"
                        + "\t RespondentDepartment"
                        + "\t RepondentAddress"
                        + "\t Petitioner Advocate Name"
                        + "\t OIC NAME"
                        + "\t OIC Mobile"
                        + "\t Case Subject"
                        + "\t Case Sub Subject"
                        + "\t Petitioner Advocate Mobile"
                        + "\t Department Advocate Name"
                        + "\t Department Advocate MobileNO"
                        + "\t Next Hearing Date"
                        + "\t Hearing Detail"
                        + "\t High Priority Case"
                        + "\t Case Status"
                        + "\t Case Dispose Type"
                        + "\t Case Dispose Date"
                        + "\t Case Disposal Status"
                        + "\t Implement Days"
                        //  + "\t Case Detail"
                        + "\t Case Reply"
                        + "\t Document Name"
                        + "\t Document View"
                       );
                    Response.Write(sb.ToString() + "\n");
                    Response.Flush();
                    int Sr = 1;
                    foreach (DataRow table in dtG.Rows)
                    {
                        sb = new StringBuilder();
                        ig++;
                        sb.Append(Sr.ToString()
                       + "\t" + (table["CaseNo"].ToString() == "" ? "NA" : table["CaseNo"].ToString())
                       + "\t" + (table["PetitionerName"].ToString() == "" ? "NA" : table["PetitionerName"].ToString())
                       + "\t" + (table["Designation_Name"].ToString() == "" ? "NA" : table["Designation_Name"].ToString())
                       + "\t" + (table["PetitionerMobileNo"].ToString() == "" ? "NA" : table["PetitionerMobileNo"].ToString())
                       + "\t" + (table["RespondentName"].ToString() == "" ? "NA" : table["RespondentName"].ToString())
                       + "\t" + (table["RespondentMobileNo"].ToString() == "" ? "NA" : table["RespondentMobileNo"].ToString())
                       + "\t" + (table["RespondentDepartment"].ToString() == "" ? "NA" : table["RespondentDepartment"].ToString())
                       + "\t" + (table["RepondentAddress"].ToString() == "" ? "NA" : table["RepondentAddress"].ToString())
                       + "\t" + (table["petiAdvocateName"].ToString() == "" ? "NA" : table["petiAdvocateName"].ToString())
                       + "\t" + (table["OICNAME"].ToString() == "" ? "NA" : table["OICNAME"].ToString())
                       + "\t" + (table["OICMobile"].ToString() == "" ? "NA" : table["OICMobile"].ToString())
                       + "\t" + (table["CaseSubject"].ToString() == "" ? "NA" : table["CaseSubject"].ToString())
                       + "\t" + (table["CaseSubSubject"].ToString() == "" ? "NA" : table["CaseSubSubject"].ToString())
                       + "\t" + (table["PetiAdvocateMobile"].ToString() == "" ? "NA" : table["PetiAdvocateMobile"].ToString())
                       + "\t" + (table["DeptAdvocateName"].ToString() == "" ? "NA" : table["DeptAdvocateName"].ToString())
                       + "\t" + (table["DeptAdvocateMobileNO"].ToString() == "" ? "NA" : table["DeptAdvocateMobileNO"].ToString())
                       + "\t" + (table["NextHearingDate"].ToString() == "" ? "NA" : table["NextHearingDate"].ToString())
                       + "\t" + (table["HearingDtl"].ToString() == "" ? "NA" : table["HearingDtl"].ToString())
                       + "\t" + (table["HighPriorityCase"].ToString() == "" ? "NA" : table["HighPriorityCase"].ToString())
                       + "\t" + (table["CaseStatus"].ToString() == "" ? "NA" : table["CaseStatus"].ToString())
                       + "\t" + (table["CaseDisposeType"].ToString() == "" ? "NA" : table["CaseDisposeType"].ToString())
                       + "\t" + (table["CaseDisposeDate"].ToString() == "" ? "NA" : table["CaseDisposeDate"].ToString())
                       + "\t" + (table["CaseDisposal_Status"].ToString() == "" ? "NA" : table["CaseDisposal_Status"].ToString())
                       + "\t" + (table["ImplementDays"].ToString() == "" ? "NA" : table["ImplementDays"].ToString())
                            //  + "\t" + (table["CaseDetail"].ToString() == "" ? "NA" : table["CaseDetail"].ToString())
                       + "\t" + (table["CaseReplyStatus"].ToString() == "" ? "NA" : table["CaseReplyStatus"].ToString())
                       + "\t" + (table["Doc_Name"].ToString() == "" ? "NA" : table["Doc_Name"].ToString())
                       + "\t" + (table["Doc_Path"].ToString() == "" ? "NA" : table["Doc_Path"].ToString())
                       ); 
                        Response.Write(sb.ToString() + "\n");
                        Response.Flush();                       
                        Sr++;
                    }

                }
                catch (Exception ex)
                {
                    string aa = ig.ToString();
                    Response.Write(ex.Message);
                }
                finally
                {
                    //command.Connection.Close();
                    //connection.Close();
                }

                Response.End();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Record Not Found.')", true);
            }

        }
        catch (Exception ex)
        {

            throw;
        }
    }
}