using Business_Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Common;

namespace TMS.Models.Sub_Category
{
    public class Sub_CategoryDB
    {
        DataTable dt = new DataTable();
        Global_Data Gd = new Global_Data();
        object Msg = "";
        public dynamic Common(Sub_Category Sc, Controller con)
        {
            Msg = "";

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_SUB_CATEGORY_MASTER", new List<object> { Sc.Type, Sc.Sub_Cate_Code == null ? "" : Sc.Sub_Cate_Code, Sc.Sub_Cate_Name == null ? "" : Sc.Sub_Cate_Name.Trim(), Sc.Category == null ? "" : Sc.Category, Sc.R_Approval == null ? "" : Sc.R_Approval, HttpContext.Current.Session["UserCode"], "", "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (Sc.Type == "GET_DATA")
                {
                    Sc.List = new List<Sub_Category>();

                    //Dictionary<int, List<string>> dictionary = new Dictionary<int, List<string>>();
                    //foreach (DataRow dr in dt.Rows)
                    //{
                    //    int key = Convert.ToInt32(dr["SUB_CATE_CODE"]);
                    //    string value = dr["SUB_CATE_CODE"].ToString();

                    //    if (!dictionary.ContainsKey(key))
                    //    {
                    //        dictionary[key] = new List<string>();
                    //    }

                    //    // Add the value to the list for the key
                    //    dictionary[key].Add(value);
                    //}
            //        Dictionary<string, string> dictionary = new Dictionary<string, string>()
            //    {
            //        { "Basic", basic.ToString() },
            //        { "DA",  da.ToString() },
            //        { "HRA", hra.ToString() },
            //        { "Conveyance", conv.ToString() },
            //        { "MedRi", medi.ToString() },
            //        { "CHEA", chea.ToString() },
            //        { "CEA", cea.ToString() },
            //        { "LTC", ltc.ToString() },
            //        { "OALT", "0" },
            //        { "RISK", "0" },
            //        { "AsscAmt", xAsscAmt.ToString() },
            //        { "PFMaxAmt", xPFMaxAmt.ToString() },
            //        { "ESIMaxAmt", xESIMaxAmt.ToString() }
            //};
            //        return dictionary;

                    foreach (DataRow dr in dt.Rows)
                    {
                        Sc.List.Add(new Sub_Category()
                        {
                            Sub_Cate_Code = dr["SUB_CATE_CODE"] as string,
                            Sub_Cate_Name = dr["SUB_CATE_NAME"] as string,
                            Category = dr["CATE_CODE"] as string,
                            Category_Name = dr["CATE_NAME"] as string,

                            R_Approval = dr["REQUIRED_APPROVAL"] as string,

                        });
                    }
                }
                else
                {
                    con.TempData["Sub_Cy_Alert"] = dt.Rows[0]["RESULT"] as string;
                    con.TempData["Alert_Type"] = dt.Rows[0]["TYPE"] as string;
                }
                //Msg = dt.Rows[0]["RESULT"] as string;
            }

            return Msg;
        }

        public void Get_SC_Code(Sub_Category Sc)
        {
            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { Sc.Type, "","", "", "", "", "", "" });

            if (dt.Rows.Count > 0)
                Sc.Sub_Cate_Code = dt.Rows[0]["SUB_CATEGORY_CODE"] as string;
        }

        public void Drop(Sub_Category Sc)
        {
            Sc.Cat_List = Gd.Get_Drop("GET_CATEGORY", "DB").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
            Sc.R_App_List = Gd.Get_Drop("GET_YES_OR_NO", "Manual").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
        }

    }
}