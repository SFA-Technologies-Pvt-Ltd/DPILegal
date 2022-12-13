using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MessageC
/// </summary>
public class MessageC
{
    public void Msg(string msg)
    {
        Page pg = HttpContext.Current.Handler as Page;
        string strScript = "javascript: alert('" + msg + "')";
        //pg.ClientScript.RegisterStartupScript(typeof(Page), "key", "<script>alert('" + msg + "')</script>");
        ScriptManager.RegisterStartupScript(pg, this.GetType(), "strScript", strScript, true);
    }
    public void MsgBox(string msg)
    {
        Page pg = HttpContext.Current.Handler as Page;
        string strScript = "javascript: alert('" + msg + "')";
        //pg.ClientScript.RegisterStartupScript(typeof(Page), "key", "<script>alert('" + msg + "')</script>");
        ScriptManager.RegisterStartupScript(pg, this.GetType(), "strScript", strScript, true);
    }

    public void MsgScriptwith(string msgsc, string url)
    {
        Page page = HttpContext.Current.Handler as Page;
        string strScript = "javascript: alert('" + msgsc + "');window.location='" + url + "';";
        //string strScript = "<script>";
        //strScript += "alert('" + msgsc + "');";
        //strScript += "window.location='" + url + "';";
        //strScript += "</script>";
        // page.ClientScript.RegisterStartupScript(this.GetType(), "Startup", strScript);
        ScriptManager.RegisterStartupScript(page, this.GetType(), "strScript", strScript, true);
    }
    
}
