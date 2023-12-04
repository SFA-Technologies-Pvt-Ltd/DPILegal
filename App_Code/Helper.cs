using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    APIProcedure obj = null;
    public Helper()
    {

    }

    public DataTable GetPartyName()
    {
        obj = new APIProcedure();
        DataSet DsParty = obj.ByDataSet("select Party_ID, PartyName from tblPartyMaster");
        if (DsParty != null && DsParty.Tables[0].Rows.Count > 0)
            return DsParty.Tables[0];
        else
            return null;
    }

    public DataTable GetOIC(string CourtId)
    {
        obj = new APIProcedure();
        DataSet DSOIC = obj.ByDataSet("select OICMaster_ID, OICName, OICMobileNo, OICEmailID from tblOICMaster OM inner join tbl_DistrictCourtMaping_Mst CD on CD.District_ID = OM.District_Id where CD.CourtName_ID = " + CourtId);
        if (DSOIC != null && DSOIC.Tables[0].Rows.Count > 0)
            return DSOIC.Tables[0];

        else
            return null;
    }
    public DataTable GetOICAll()
    {
        obj = new APIProcedure();
        DataSet DSOIC = obj.ByDataSet("select OICMaster_ID, OICName, OICMobileNo, OICEmailID from tblOICMaster");
        if (DSOIC != null && DSOIC.Tables[0].Rows.Count > 0)
            return DSOIC.Tables[0];

        else
            return null;
    }

    public DataTable GetCasetype()
    {
        obj = new APIProcedure();
        DataSet DSOIC = obj.ByDataSet("select Casetype_ID, Casetype_Name from tbl_Legal_Casetype");
        if (DSOIC != null && DSOIC.Tables[0].Rows.Count > 0)
            return DSOIC.Tables[0];

        else
            return null;
    }

    public DataTable GetCaseNo()
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetCaseNoByCourt(string CourtType_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 and CourtType_Id=" + CourtType_Id + "  order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetOICWiseCaseNo(string OICMaster_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 and OICMaster_Id=" + OICMaster_Id + " order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetDistrictWiseCaseNo(string District_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 and District_Id=" + District_Id + " order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetDvisionWiseCaseNo(string Division_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration CR " +
        "left join Mst_District DM on DM.District_ID=CR.District_ID " +
        "where CR.Isactive = 1 and CR.District_Id in (select District_ID from Mst_District where Division_ID=" + Division_Id + ") order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetCourtWiseCaseNo(string Court_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration CR " +
        "where CR.Isactive = 1 and CR.CourtLocation_Id in (" + Court_Id + ") order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
	
	public DataTable GetCourtByDivision(string Division)
    {
        obj = new APIProcedure();
        DataSet DsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { "Division_ID", "flag" }, new string[] { Division, "5" }, "datatset");
        if (DsCourt != null && DsCourt.Tables[1].Rows.Count > 0)
            return DsCourt.Tables[1];

        else
            return null;
    }
	
    public DataTable GetCourt()
    {
        obj = new APIProcedure();
        DataSet DsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "datatset");
        if (DsCourt != null && DsCourt.Tables[0].Rows.Count > 0)
            return DsCourt.Tables[0];

        else
            return null;
    }
    public DataTable GetCourtForCourt(string District_Id)
    {
        obj = new APIProcedure();
        DataSet DsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { "District_Id", "flag" }, new string[] { District_Id, "1" }, "datatset");
        if (DsCourt != null && DsCourt.Tables[1].Rows.Count > 0)
            return DsCourt.Tables[1];

        else
            return null;
    }
    public string Encrypt(string clearText)
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
    public string Decrypt(string cipherText)
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