using Business_Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.Common;

namespace TMS.Models.Ticket
{
    public class TicketDB
    {
        DataTable dt = new DataTable();
        Global_Data Gd = new Global_Data();
        string Msg = "";
        public dynamic Common(Ticket Tt)
        {
            Msg = "";
            //if (file != null && file.ContentLength > 0)
            //{
            //    Stream stream = file.InputStream;
            //    BinaryReader reader = new BinaryReader(stream);
            //    Tt.ImageData = reader.ReadBytes((int)stream.Length);
            //}

            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_TICKET_MASTER", new List<object> { Tt.Type, Tt.Ticket_id == null ? "" : Tt.Ticket_id, HttpContext.Current.Session["UserCode"], Tt.Category == null ? "" : Tt.Category.Trim(), Tt.Sub_Category == null ? "" : Tt.Sub_Category.Trim(), Tt.Narration == null ? "" : Tt.Narration.Trim(), Tt.Priority == null ? "" : Tt.Priority.Trim(), HttpContext.Current.Session["STNCODE"], HttpContext.Current.Session["Branch"], HttpContext.Current.Session["Department"], "", Tt.ImageData, Tt.Contact_No == null ? "" : Tt.Contact_No, Tt.Notes == null ? "" : Tt.Notes, Tt.NUser == null ? "" : Tt.NUser, "", "", "", "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (Tt.Type == "GET_DATA")
                {
                    Tt.List = new List<Ticket>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        Tt.List.Add(new Ticket()
                        {
                            Ticket_id = dr["TICKET_ID"] as string,
                            Date = dr["TICKET_DATE"] as string,
                            Narration = dr["NARRATION"] as string,
                            Category = dr["CATE_NAME"] as string,
                            Category_Code = dr["CATE_CODE"] as string,
                            Sub_Category = dr["SUB_CATE_CODE"] as string,
                            Sub_Category_Name = dr["SUB_CATE_NAME"] as string,
                            NUser = dr["ASSIGN"] as string,
                            Priority = dr["PRIORITY"].ToString().Trim() == "1" ? "High" : dr["PRIORITY"].ToString().Trim() == "2" ? "Medium" : "Low",
                            Frm_Stn_Code = dr["FROM_STN"] as string,
                            //ImgByte = dr["IMAGE"] == DBNull.Value ? null : (byte[])dr["IMAGE"],
                            //Notes = dr["NOTES"] as string,
                            Frm_BCode = dr["FROM_BRANCH"] as string,
                            Dept_Name = dr["DEPT_NAME"] as string,
                            Contact_No = dr["CONTACT_NO"] as string,


                        });
                    }
                }
                else
                    Msg = dt.Rows[0]["RESULT"] as string;
            }

            return Msg;
        }
        public void Get_Ticket(Ticket Tt)
        {
            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { Tt.Type, "", "", "", "", "", "", "" });

            if (dt.Rows.Count > 0)
                Tt.Ticket_id = dt.Rows[0]["TICKET_ID"] as string;
        }
        public string Get_Users(Ticket Tt)
        {
            var List = new List<Global_Data>();
            DataSet Ds = DB_Connectivity.ExecuteDataSet(Global_Data.Conn, "SP_GETNAMES", new List<object> { Tt.Type, Tt.Category, "", HttpContext.Current.Session["UserCode"], HttpContext.Current.Session["STNCODE"], "", "", "" });

            if (Ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in Ds.Tables[0].Rows)
                {
                    List.Add(new Global_Data()
                    {
                        Value = dr["UCODE"] as string,
                        Text = dr["UNAME"] as string,

                    });
                }
                HttpContext.Current.Session["Mapped_User_List"] = List;
                Msg = JsonConvert.SerializeObject(Ds);
            }
            else
            {
                HttpContext.Current.Session["Mapped_User_List"] = null;
                Msg = JsonConvert.SerializeObject("Users not Available in Category!");
            }
            if (Ds.Tables[1].Rows.Count > 0)
            {
                List = new List<Global_Data>();

                foreach (DataRow dr in Ds.Tables[1].Rows)
                {
                    List.Add(new Global_Data()
                    {
                        Value = dr["VALUE"] as string,
                        Text = dr["TEXT"] as string,

                    });
                }
                HttpContext.Current.Session["Sub_Cate_List"] = List;
                Msg = JsonConvert.SerializeObject(Ds);

            }
            else
            {
                HttpContext.Current.Session["Sub_Cate_List"] = null;
                Msg = Msg == "" ? JsonConvert.SerializeObject("") : Msg;
            }

            return Msg;
        }
        public void Drop(Ticket Tt)
        {
            if (HttpContext.Current.Session["Mapped_User_List"] != null)
            {
                Tt.U_List = ((List<Global_Data>)HttpContext.Current.Session["Mapped_User_List"]).Select(x => new SelectListItem()
                {
                    Value = x.Value,
                    Text = x.Text,
                });
            }
            if (HttpContext.Current.Session["Sub_Cate_List"] != null)
            {
                Tt.Sub_Cate_List = ((List<Global_Data>)HttpContext.Current.Session["Sub_Cate_List"]).Select(x => new SelectListItem()
                {
                    Value = x.Value,
                    Text = x.Text,
                });
            }
            Tt.N_List = Gd.Get_Drop("GET_CATEGORY", "DB").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
            Tt.Priot_List = Gd.Get_Drop("GET_PRIORITY", "Manual").Select(x => new SelectListItem()
            {
                Value = x.Value,
                Text = x.Text,
            });
        }
    }
}