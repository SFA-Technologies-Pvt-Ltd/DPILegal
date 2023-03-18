using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_ViewDocumentByUniqNo : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != "" && Request.QueryString["Flag"] != "")
        {
            if (!IsPostBack)
            {
                FillDoc();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }
        
    }

    protected void FillDoc()
    {
        try
        {
            string ID = Request.QueryString["ID"].ToString();
            string Flag = Request.QueryString["Flag"].ToString();
            dsCase = obj.ByProcedure("USP_Update_OldPendingCase", new string[] { "flag", "UniqueNo", "OldNewCase" },
                new string[] { "4", ID, Flag }, "dataset");
            if (dsCase.Tables.Count > 0 && dsCase != null)
            {
                StringBuilder Sb = new StringBuilder();
                Sb.Append("<table class='table table-bordered'>");
                Sb.Append("<tr>");
                Sb.Append("<th>S.No.</th>");
                Sb.Append("<th>Document Name</th>");
                Sb.Append("<th>Link</th>");
                Sb.Append("</tr>");
                int RowCount = 1;
                for (int i = 0; i < dsCase.Tables[0].Rows.Count; i++)
                {
  
                    Sb.Append("<tr>");
                    Sb.Append("<td>");
                    Sb.Append(" " + RowCount + " ");
                    Sb.Append("</td>");
                    Sb.Append("<td>");
                    Sb.Append(" " + dsCase.Tables[0].Rows[i]["PDF"].ToString() + " ");
                    Sb.Append("</td>");
                    Sb.Append("<td>");
                   // Sb.Append(" " + dsCase.Tables[0].Rows[i]["PDFLink"].ToString() + " ");
                    Sb.Append("<a href='" + dsCase.Tables[0].Rows[i]["PDFLink"].ToString() + "' target='_blank' class='fa fa-eye'></a>");
                    Sb.Append("</td>");
                    Sb.Append("</tr>");
                    RowCount++;
                }
               
                Sb.Append("</table>");
                DivDocument.InnerHtml = Sb.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}