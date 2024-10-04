using Business_Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TMS.Common
{
    public class Global_Data
    {
        DataTable dt = new DataTable();

        public static string Conn = (ConfigurationManager.ConnectionStrings["TMS"].ToString());

        public string Value { get; set; }
        public string Text { get; set; }
        public List<Global_Data> Get_Drop(string Statement, string Type)
        {
            var list = new List<Global_Data>();
            if (Type != "Manual")
            {
                dt.Clear();
                dt = DB_Connectivity.ExecuteDataTable(Conn, "SP_GETNAMES", new List<object> { Statement, "", "","", "", "", "", "" });
                if (dt.Rows.Count > 0)
                {
                    if (Statement == "GET_STATION_CODE")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Global_Data()
                            {
                                Value = dr["STATION_CODE"] as string,
                                Text = dr["STATION_CODE"] as string,

                            });
                        }
                    }
                    else if (Statement == "GET_DEPARTMENT")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Global_Data()
                            {
                                Value = dr["DEPT_CODE"] as string,
                                Text = dr["DEPT_NAME"] as string,

                            });
                        }
                    }
                    else if (Statement == "GET_DESIGNATION")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Global_Data()
                            {
                                Value = dr["DESIG_CODE"] as string,
                                Text = dr["DESIG_NAME"] as string,

                            });
                        }
                    }
                    else if (Statement == "GET_CATEGORY")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Global_Data()
                            {
                                Value = dr["CATE_CODE"] as string,
                                Text = dr["CATE_NAME"] as string,

                            });
                        }
                    }     
                    else if (Statement == "GET_USER_RIGHTS")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Global_Data()
                            {
                                Value = dr["RIGHTS_ID"] as string,
                                Text = dr["RIGHTS_NAME"] as string,

                            });
                        }
                    }
                }

            }
            else
            {
                if (Statement == "GET_PRIORITY")
                {
                    list.Add(new Global_Data() { Value = "1", Text = "High" });
                    list.Add(new Global_Data() { Value = "2", Text = "Medium" });
                    list.Add(new Global_Data() { Value = "3", Text = "Low" });
                }
                else if (Statement == "GET_YES_OR_NO")
                {
                    list.Add(new Global_Data() { Value = "Y", Text = "Yes" });
                    list.Add(new Global_Data() { Value = "N", Text = "No" });
                }
            }
            return list;
        }
        public string Encrypt(string encryptString)
        {
            string EncryptionKey = "rePuteTms";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }

            return encryptString;
        }
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "rePuteTms";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
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
}