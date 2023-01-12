using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Library : System.Web.UI.Page
{
    DataSet ds = null;
    AbstApiDBApi objdb = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                BindGridLibrary();
                //List<CaseLibrary> lstCL = new List<CaseLibrary>();
                //CaseLibrary clObj = null;

                //clObj = new CaseLibrary(1, "Pradeep Singh", "WA", "WA-1175-2010", "Jabalpur", "02-Apr-2022", "../PDF_Files/CaseNo_WP00101.pdf",2010);
                //lstCL.Add(clObj);
                //clObj = new CaseLibrary(2, "Umesh Shrivastava", "WP", "WP-69-2017", "Jabalpur", "12-Apr-2022", "../PDF_Files/CaseNo_WP00102.pdf",2017);
                //lstCL.Add(clObj);
                //clObj = new CaseLibrary(3, "MAHESHWARI PANCHAYAT MANDIR TRUST, HARDA", "WP", "WP No. 264/2017", "Jabalpur", "04-Mar-2022", "../PDF_Files/CaseNo_WP00103.pdf",2017);
                //lstCL.Add(clObj);
                //clObj = new CaseLibrary(4, "Ajay Pratap Singh", "WP", "WP-4148-2000, WP-4149-2000, WP-4151-2000 & WP-4152-2000", "Jabalpur", "02-May-2022", "../PDF_Files/CaseNo_WP00104.pdf", 2017);
                //lstCL.Add(clObj);
                //clObj = new CaseLibrary(5, "S.P. Rai", "WP", "WP-4649/2014", "Jabalpur", "02-Dec-2022", "../PDF_Files/CaseNo_WP00105.pdf", 2014);
                //lstCL.Add(clObj);

                //grdCaseLibrary.DataSource = lstCL;
                //grdCaseLibrary.DataBind();
            }
        }
        else
            Response.Redirect("/Login.aspx");
    }
    private void BindGridLibrary()
    {
        try
        {
            ds = new DataSet();
            ds = objdb.ByProcedure("Sp_librarydetail", new string[] { "flag" }, new string[] { "2" }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //DataTable dt = (DataTable)ViewState["dtCol"];
                DataTable dt = ds.Tables[0];
                grdCaseLibrary.DataSource = dt;
                grdCaseLibrary.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}
public class CaseLibrary
{
    public CaseLibrary(int SrNo,string PartyName, string CaseType, string CaseNo, string RelatedOffice, string DecisionDate, string PDFViewLink,int Year)
    {
        this.SrNo = SrNo;
        this.PartyName = PartyName;
        this.CaseType = CaseType;
        this.CaseNo = CaseNo;
        this.RelatedOffice = RelatedOffice;
        this.DecisionDate = DecisionDate;
        this.PDFViewLink = PDFViewLink;
        this.Year = Year;
    }

    public int SrNo { get; set; }
    public string CaseType { get; set; }
    public string PartyName { get; set; }
    public string CaseNo { get; set; }
    public string RelatedOffice { get; set; }
    public string DecisionDate { get; set; }
    public int Year { get; set; }
    public string PDFViewLink { get; set; }

}