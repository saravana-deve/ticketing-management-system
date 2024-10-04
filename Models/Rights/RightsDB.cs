using Business_Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Common;

namespace TMS.Models.Rights
{
    public class RightsDB
    {
        DataTable dt = new DataTable();
        string Msg = "";
        public dynamic Common(Rights R,Controller C)
        {
            Msg = "";

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_RIGHTS_MASTER", new List<object> { R.Type, R.Rights_Id == null ? "" : R.Rights_Id, R.Rights_Name == null ? "" : R.Rights_Name.ToUpper().Trim(), HttpContext.Current.Session["UserCode"],"", "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (R.Type == "GET_DATA")
                {
                    R.List = new List<Rights>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        R.List.Add(new Rights()
                        {
                            Rights_Id = dr["RIGHTS_ID"].ToString(),
                            Rights_Name = dr["RIGHTS_NAME"].ToString(),

                        });
                    }
                }
                else if (R.Type == "DELETE")
                {
                    C.TempData["R_Alert"] = dt.Rows[0]["RESULT"].ToString();
                    C.TempData["Alert_Type"] = dt.Rows[0]["TYPE"].ToString();
                    Msg = JsonConvert.SerializeObject(dt);
                }
                else
                {
                    C.TempData["R_Alert"] = dt.Rows[0]["RESULT"].ToString();
                    C.TempData["Alert_Type"] = dt.Rows[0]["TYPE"].ToString();
                }
            }

            return Msg;
        }
        public void Get_RightsId(Rights R)
        {
            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { R.Type, "", "", "", "", "", "", "" });

            if (dt.Rows.Count > 0)
                R.Rights_Id = dt.Rows[0]["RIGHTS ID"].ToString();
        }
    }
}