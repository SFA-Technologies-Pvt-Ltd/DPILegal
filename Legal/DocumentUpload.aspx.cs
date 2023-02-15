using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_DocumentUpload : System.Web.UI.Page
{
    DataSet ds;
    //AbstApiDB objdb = new common();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //string ProfileImagePath = "";
        //string ProfileImagePath2 = "";
        //string ProfileImagePath3 = "";
        //if (FileUpload1.HasFile)
        //{
        //    ProfileImagePath = "/Legal/Document/" + Guid.NewGuid() + FileUpload1.FileName;
        //    FileUpload1.SaveAs(Server.MapPath(ProfileImagePath));
        //}
        //if (FileUpload2.HasFile)
        //{
        //    ProfileImagePath2 = "/Legal/Document/" + Guid.NewGuid() + FileUpload2.FileName;
        //    FileUpload2.SaveAs(Server.MapPath(ProfileImagePath2));
        //}
        //if (FileUpload3.HasFile)
        //{
        //    ProfileImagePath3 = "/Legal/Document/" + Guid.NewGuid() + FileUpload3.FileName;
        //    FileUpload3.SaveAs(Server.MapPath(ProfileImagePath3));
        //}
        //objdb.ByProcedure("spLegalDocumentUpload", new string[] { "flag", "Document_Type", "Case_Document", "Case_Document2", "Case_Document3" }, new string[] { "1", ddlDocumentType.SelectedItem.Text, ProfileImagePath, ProfileImagePath2, ProfileImagePath3 }, "dataset");
    }
}