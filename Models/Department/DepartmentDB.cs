using Business_Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TMS.Common;

namespace TMS.Models.Department
{
    public class DepartmentDB
    {
        DataTable dt = new DataTable();
        string Msg = "";
        public dynamic Common(Department Dm)
        {
            Msg = "";

            //dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_DEPARTMENT_MASTER", new { STATEMENT_TYPE = Dm.Type, DEPT_CODE = Dm.Dept_Code == null ? "" : Dm.Dept_Code, DEPT_NAME = Dm.Dept_Name == null ? "" : Dm.Dept_Name.ToUpper().Trim(), USER = HttpContext.Current.Session["UserCode"], ADDL_PARM1 = "", ADDL_PARM2 = "", ADDL_PARM3 = "" });

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_DEPARTMENT_MASTER", new List<object> { Dm.Type, Dm.Dept_Code == null ? "" : Dm.Dept_Code, Dm.Dept_Name == null ? "" : Dm.Dept_Name.ToUpper().Trim(), HttpContext.Current.Session["UserCode"], "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (Dm.Type == "GET_DATA")
                {
                    Dm.List = new List<Department>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        Dm.List.Add(new Department()
                        {
                            Dept_Code = dr["DEPT_CODE"] as string,
                            Dept_Name = dr["DEPT_NAME"] as string,

                        });
                    }
                }
                else
                    Msg = dt.Rows[0]["RESULT"] as string;
            }

            return Msg;
        }

        public void Get_DeptCode(Department Dm)
        {
            dt.Clear();
            //dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new { STATEMENT_TYPE = Dm.Type, CODE = "", Value = "", USERS = "", ADDL_PARM1 = "", ADDL_PARM2 = "", ADDL_PARM3 = "", });

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { Dm.Type, "","", "", "", "", "", "" });

            if (dt.Rows.Count > 0)
                Dm.Dept_Code = dt.Rows[0]["DEPT_ID"] as string;
        }
    }
}