using Business_Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TMS.Common;

namespace TMS.Models.Designation
{
    public class DesignationDB
    {
        DataTable dt = new DataTable();
        string Msg = "";
        public dynamic Common(Designation Dn)
        {
            Msg = "";
            //dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_DESIGNATION_MASTER", new { STATEMENT_TYPE = Dn.Type, DESIG_CODE = Dn.Desi_Code == null ? "" : Dn.Desi_Code, DESIG_NAME = Dn.Desi_Name == null ? "" : Dn.Desi_Name.ToUpper().Trim(), USER = HttpContext.Current.Session["UserCode"], ADDL_PARM1 = "", ADDL_PARM2 = "", ADDL_PARM3 = "" });

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_DESIGNATION_MASTER", new List<object> { Dn.Type, Dn.Desi_Code == null ? "" : Dn.Desi_Code, Dn.Desi_Name == null ? "" : Dn.Desi_Name.ToUpper().Trim(), HttpContext.Current.Session["UserCode"], "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (Dn.Type == "GET_DATA")
                {
                    Dn.List = new List<Designation>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        Dn.List.Add(new Designation()
                        {
                            Desi_Code = dr["DESIG_CODE"] as string,
                            Desi_Name = dr["DESIG_NAME"] as string,

                        });
                    }
                }
                else
                    Msg = dt.Rows[0]["RESULT"] as string;
            }

            return Msg;
        }

        public void Get_DesiCode(Designation Dn)
        {
            dt.Clear();
            //dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new { STATEMENT_TYPE = Dn.Type, CODE = "", Value = "", USERS = "", ADDL_PARM1 = "", ADDL_PARM2 = "", ADDL_PARM3 = "", });

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { Dn.Type, "","", "", "", "", "", "" });

            if (dt.Rows.Count > 0)
                Dn.Desi_Code = dt.Rows[0]["DESI_CODE"] as string;
        }
    }
}