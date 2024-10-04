using Business_Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TMS.Common;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace TMS.Models.User
{
    public class UserDB
    {
        DataTable dt = new DataTable();
        Global_Data Gd = new Global_Data();
        string Msg = "";
        public dynamic Common(User Ur)
        {
            Msg = "";
            if (Ur.Pword != null)
                Ur.Pword = Gd.Encrypt(Ur.Pword);

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_USER_MASTER", new List<object> { Ur.Type, Ur.Ucode == null ? "" : Ur.Ucode.Trim(), Ur.UName == null ? "" : Ur.UName.ToUpper().Trim(), Ur.Pword == null ? "" : Ur.Pword.Trim(), Ur.Dept_Code == null ? "" : Ur.Dept_Code, Ur.Desi_Code == null ? "" : Ur.Desi_Code, Ur.Stn_Code == null ? "" : Ur.Stn_Code, Ur.BCode == null ? "" : Ur.BCode, HttpContext.Current.Session["UserCode"], Ur.Rights == null ? "" : Ur.Rights, Ur.Contact == null ? "" : Ur.Contact, "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (Ur.Type == "GET_DATA")
                {
                    Ur.List = new List<User>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        Ur.List.Add(new User()
                        {
                            Ucode = dr["UCODE"] as string,
                            UName = dr["UNAME"] as string,
                            Pword = Gd.Decrypt(dr["PWORD"] as string),
                            Desi_Name = dr["DESIG_NAME"] as string,
                            Dept_Name = dr["DEPT_NAME"] as string,
                            Dept_Code = dr["DEPT_CODE"] as string,
                            Desi_Code = dr["DESIG_CODE"] as string,
                            Stn_Code = dr["STATION_CODE"] as string,
                            BCode = dr["BRANCH_CODE"] as string,
                            Rights = dr["RIGHTS_ID"] as string,
                            Rights_Name = dr["RIGHTS_NAME"] as string,
                            Contact = dr["CONTACT_NO"] as string,

                        });
                    }
                }
                else
                    Msg = dt.Rows[0]["RESULT"] as string;
            }

            return Msg;
        }

        public void Drop(User Ur)
        {
            if (HttpContext.Current.Session["Branch_List"] != null)
            {
                Ur.Branch_List = ((List<User>)HttpContext.Current.Session["Branch_List"]).Select(x => new SelectListItem()
                {
                    Value = x.BCode,
                    Text = x.Dept_Name,
                });
            }

            Ur.Stn_List = Gd.Get_Drop("GET_STATION_CODE", "DB").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
            Ur.Dep_List = Gd.Get_Drop("GET_DEPARTMENT", "DB").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
            Ur.Desi_List = Gd.Get_Drop("GET_DESIGNATION", "DB").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
            Ur.Right_List = Gd.Get_Drop("GET_USER_RIGHTS", "DB").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
        }

        public string GetBranch(User Ur)
        {
            var List = new List<User>();
            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { Ur.Type, Ur.Stn_Code, "", "", "", "", "", "" });

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    List.Add(new User()
                    {
                        BCode = dr["SUB_BRANCH_CODE"] as string,
                        Dept_Name = dr["SUB_BRANCH_CODE"] as string,

                    });
                }
                HttpContext.Current.Session["Branch_List"] = List;


                Msg = JsonConvert.SerializeObject(dt);
            }
            else
                Msg = "No Data";

            return Msg;
        }
    }
}