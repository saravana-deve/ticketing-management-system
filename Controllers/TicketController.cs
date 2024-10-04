using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Common;
using TMS.Models.Ticket;

namespace TMS.Controllers
{
    [NoCache]
    public class TicketController : Controller
    {
        Global_Data Gd = new Global_Data();
        TicketDB Db = new TicketDB();
        string Msg = "";
        // GET: Ticket
        [SessionTimeOut]
        public ActionResult Index()
        {
            Ticket Tt = new Ticket();
            try
            {
                Db.Drop(Tt);
                Tt.Btn = "Save";
                Tt.Type = "GET_TICKET_ID";
                Db.Get_Ticket(Tt);
                Session["Mapped_User_List"] = null;
                Session["Sub_Cate_List"] = null;

                Tt.Type = "GET_DATA";
                Db.Common(Tt);

            }
            catch (Exception ex)
            {
                TempData["Tkt_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
                TempData["Alert_Type"] = "Error";
            }
            return View(Tt);
        }
        //[HttpPost]
        //[SessionTimeOut]
        //public ActionResult Index(Ticket Tt, HttpPostedFileBase file)
        //{
        //    var Model = new Ticket();
        //    Db.Drop(Model);
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (Tt.Btn == "Save" || Tt.Btn == null)
        //                Tt.Type = "INSERT";
        //            else
        //                Tt.Type = "UPDATE";

        //            Msg = Db.Common(Tt, file);
        //            TempData["Tkt_Alert"] = Msg;
        //            TempData["Alert_Type"] = "Success";
        //            ModelState.Clear();
        //            Model.U_List = null;
        //            Session["Mapped_User_List"] = null;
        //            Session["Sub_Cate_List"] = null;
        //        }
        //        Model.Btn = "Save";
        //        Model.Type = "GET_TICKET_ID";
        //        Db.Get_Ticket(Model);
        //        Model.Type = "GET_DATA";
        //        Db.Common(Model, file);

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Tkt_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
        //        TempData["Alert_Type"] = "Error";
        //    }

        //    return View(Model);
        //}
        [HttpPost]
        [SessionTimeOut]
        public ActionResult Index(Ticket Tt, HttpPostedFileBase[] file)
        {
            var Model = new Ticket();
            Db.Drop(Model);
            try
            {
                if (ModelState.IsValid)
                {
                    if (Tt.Btn == "Save" || Tt.Btn == null)
                        Tt.Type = "INSERT";
                    else
                        Tt.Type = "UPDATE";

                    List<Image> imageList = new List<Image>();
                    if (file != null && file.Any())
                    {
                        foreach (var  image in file)
                        {
                            if (image != null && image.ContentLength > 0)
                            {
                                // Convert image to byte array
                                byte[] imageData = null;
                                using (var binaryReader = new BinaryReader(image.InputStream))
                                {
                                    imageData = binaryReader.ReadBytes(image.ContentLength);
                                }

                                imageList.Add(new Image
                                {
                                    FileName = Path.GetFileName(image.FileName),
                                    ImageData = imageData,
                                    //ContentType = image.ContentType
                                });
                            }
                        }
                    }
                    Tt. ImageData = JsonConvert.SerializeObject(imageList);

                    Msg = Db.Common(Tt);
                    TempData["Tkt_Alert"] = Msg;
                    TempData["Alert_Type"] = "Success";
                    ModelState.Clear();
                    Model.U_List = null;
                    Session["Mapped_User_List"] = null;
                    Session["Sub_Cate_List"] = null;
                }
                Model.Btn = "Save";
                Model.Type = "GET_TICKET_ID";
                Db.Get_Ticket(Model);
                Model.Type = "GET_DATA";
                Db.Common(Model);

            }
            catch (Exception ex)
            {
                TempData["Tkt_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
                TempData["Alert_Type"] = "Error";
            }

            return View(Model);
        }
        public string Get_Users(Ticket Tt)
        {
            try
            {
                Msg = Db.Get_Users(Tt);
            }
            catch (Exception ex)
            {
                Msg = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return Msg;
        }
    }
}