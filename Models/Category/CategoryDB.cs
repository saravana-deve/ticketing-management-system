using Business_Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Common;

namespace TMS.Models.Category
{
    public class CategoryDB
    {
        DataTable dt = new DataTable();
        Global_Data Gd = new Global_Data();
        string Msg = "";
        public dynamic Common(Category Cy)
        {
            Msg = "";

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_CATEGORY_MASTER", new List<object> { Cy.Type, Cy.Category_Code == null ? "" : Cy.Category_Code, Cy.Category_Name == null ? "" : Cy.Category_Name.ToUpper().Trim(), Cy.MDept_Code == null ? "" : Cy.MDept_Code, HttpContext.Current.Session["UserCode"], "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (Cy.Type == "GET_DATA")
                {
                    Cy.List = new List<Category>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        Cy.List.Add(new Category()
                        {
                            Category_Code = dr["CATE_CODE"] as string,
                            Category_Name = dr["CATE_NAME"] as string,
                            MDept_Code = dr["MAPPED_DEPT_CODE"] as string,
                            MDept_Name = dr["DEPT_NAME"] as string,

                        });
                    }
                }
                else
                    Msg = dt.Rows[0]["RESULT"] as string;
            }

            return Msg;
        }

        public void Get_NatureCode(Category Cy)
        {
            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { Cy.Type, "", "","", "", "", "", "" });

            if (dt.Rows.Count > 0)
                Cy.Category_Code = dt.Rows[0]["CATEGORY_CODE"] as string;
        }

        public void Drop(Category Cy)
        {
            Cy.Dep_List = Gd.Get_Drop("GET_DEPARTMENT", "DB").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
        }
    }
}