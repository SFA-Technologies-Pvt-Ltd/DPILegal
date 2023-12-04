using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Legal_OrderByDirectionCase_ForOldDashb : System.Web.UI.Page
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
                    FillDtlDirection();
                }
            }
            else Response.Redirect("~/Login.aspx");
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void FillDtlDirection()
    {
        try
        {
            ds = objdb.ByProcedure("USP_GetOrderByDirection_ForOldDashb", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = ds.Tables[0];
                GrdOrderByDirectionCase.DataSource = ds;
                GrdOrderByDirectionCase.DataBind();
                GrdOrderByDirectionCase.HeaderRow.TableSection = TableRowSection.TableHeader;
                GrdOrderByDirectionCase.UseAccessibleHeader = true;
                
            }
            else
            {
                GrdOrderByDirectionCase.DataSource = null;
                GrdOrderByDirectionCase.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    
}