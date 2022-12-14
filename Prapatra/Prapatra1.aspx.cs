using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Prapatra_Prapatra1 : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCaseCount();
        }
    }

    protected void BindCaseCount()
    {
        try
        {
            GrdCaseCount.DataSource = null;
            GrdCaseCount.DataBind();

            ds = obj.ByProcedure("USP_Legal_GetWACaseDtlForDasboard", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GrdCaseCount.DataSource = ds;
                GrdCaseCount.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
   
    protected void GrdCaseCount_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.RowSpan = 1;
                
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdCaseCount.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderCell = new TableCell();
                HeaderCell.Text = "रीट याचिका";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdCaseCount.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderCell = new TableCell();
                HeaderCell.Text = "अवमानना";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdCaseCount.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderCell = new TableCell();
                HeaderCell.Text = "पारित आदेश/पालन";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdCaseCount.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderCell = new TableCell();
                HeaderCell.Text = "अपील (पालन से शेष रहे प्रकरणों' में से दर्ज अपील)";
                HeaderCell.ColumnSpan = 3;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdCaseCount.Controls[0].Controls.AddAt(0, HeaderGridRow);

                HeaderCell = new TableCell();
                HeaderCell.Text = "वास्तविक पालन से शेष";
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);
                GrdCaseCount.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
}