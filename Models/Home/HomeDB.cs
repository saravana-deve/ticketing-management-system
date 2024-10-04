using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Business_Logic;
using Newtonsoft.Json;
using TMS.Common;
namespace TMS.Models.Home
{
    public class HomeDB
    {
        string Msg = "";
        DataTable dt = new DataTable();
        Global_Data Gd = new Global_Data();
        public dynamic Dashboard(Home h)
        {
            var Msg = "";
            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_DASHBOARD", new List<object> { h.Type, HttpContext.Current.Session["UserCode"], h.Assign_To == null ? "" : h.Assign_To, HttpContext.Current.Session["STNCODE"], HttpContext.Current.Session["Department"], h.Ticket_Id == null ? "" : h.Ticket_Id, h.Status == null ? "" : h.Status, h.Remarks == null ? "" : h.Remarks, h.Date == null ? "" : h.Date, "", "", "" });

            if (dt.Rows.Count > 0)
            {
                if (h.Type == "GET_IMAGES")
                {
                    var imageList = new List<object>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        var imageObject = new
                        {
                            ImageData = $"data:image/jpeg;base64,{Convert.ToBase64String(dr["IMAGE"] as byte[])}",
                            FileName = dr["IMAGE_NAME"] as string, // You can customize this as needed
                            //ContentType = "image/jpeg" // Adjust based on your actual content type
                        };

                        imageList.Add(imageObject);
                    }
                    Msg = JsonConvert.SerializeObject(imageList);

                }
                else
                    Msg = DB_Connectivity.Serialize(dt);
                //Msg = JsonConvert.SerializeObject(dt.AsEnumerable().Select(row => dt.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => row.IsNull(col) ? "" : row[col])).ToList(), Formatting.Indented);




            }
            else
                Msg = JsonConvert.SerializeObject("");

            return Msg;
        }
        public string Change_Pass(Home h)
        {
            if (h.Old_Pass != null && h.Type == "CHECK_OLD_PASSWORD")
                h.Old_Pass = Gd.Encrypt(h.Old_Pass);
            else
                h.New_Pass = Gd.Encrypt(h.New_Pass);

            dt.Clear();
            dt = DB_Connectivity.ExecuteDataTable(Global_Data.Conn, "SP_GETNAMES", new List<object> { h.Type, h.Old_Pass == null ? "" : h.Old_Pass, h.New_Pass == null ? "" : h.New_Pass, HttpContext.Current.Session["UserCode"], HttpContext.Current.Session["STNCODE"], "", "", "" });

            if (dt.Rows.Count > 0)
                Msg = dt.Rows[0]["RESULT"] as string;

            return Msg;

        }
    }


}