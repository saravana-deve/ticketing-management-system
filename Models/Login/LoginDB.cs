using Business_Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TMS.Common;

namespace TMS.Models.Login
{
    public class LoginDB
    {
        DataTable dt = new DataTable();
        Global_Data Gd = new Global_Data();
        string Msg = "";
        public string Login(Login L)
        {
            L.Pword = Gd.Encrypt(L.Pword);
            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_LOGIN", new List<object> { "LOGED_IN", L.UCode, L.Pword, "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("RESULT"))
                {
                    HttpContext.Current.Session["UserCode"] = dt.Rows[0]["UCODE"] as string;
                    HttpContext.Current.Session["UserName"] = dt.Rows[0]["UNAME"] as string;
                    HttpContext.Current.Session["Department"] = dt.Rows[0]["DEPT_CODE"] as string;
                    HttpContext.Current.Session["Designation"] = dt.Rows[0]["DESIG_CODE"] as string;
                    HttpContext.Current.Session["STNCODE"] = dt.Rows[0]["STATION_CODE"] as string;
                    HttpContext.Current.Session["Branch"] = dt.Rows[0]["BRANCH_CODE"] as string;

                    Msg = "True";
                }
                else
                    Msg = dt.Rows[0]["RESULT"] as string;
            }

            return Msg;
        }
    }
}