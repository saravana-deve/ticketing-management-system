using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Common;
using TMS.Models.Home;

namespace TMS.Controllers
{
    [NoCache]
    public class HomeController : Controller
    {
        // GET: Home
        readonly HomeDB db = new HomeDB();
        Global_Data Gd = new Global_Data();
        string Msg = "";
        [SessionTimeOut]
        public ActionResult Index()
        {
            Home h = new Home();
            try
            {

            }
            catch (Exception ex)
            {
                TempData["HMAlert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return View(h);
        }
        public string Dashboard_Count(Home h)
        {
            try
            {
                h.Type = "DASHBOARD_COUNT";
                Msg = db.Dashboard(h);
            }
            catch (Exception ex)
            {
                Msg = JsonConvert.SerializeObject(ex.Message.Replace("\n", " ").Replace("\r", " ").ToString());
            }
            return Msg;
        }
        public string GetData(Home h)
        {
            try
            {
                Msg = db.Dashboard(h);
                return Msg;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message.Replace("\n", " ").Replace("\r", " ").ToString());
            }

        }
        public string LoadImagesByTicket_Id(Home h)
        {
            try
            {
                h.Type = "GET_IMAGES";
                Msg = db.Dashboard(h);
                return Msg;
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message.Replace("\n", " ").Replace("\r", " ").ToString());
            }
        }
        [SessionTimeOut]
        public ActionResult Change_Password()
        {
            Home h = new Home();

            return View(h);
        }
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Change_Password(Home h)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    h.Type = "CHECK_OLD_PASSWORD";
                    Msg = db.Change_Pass(h);

                    if (Msg == "True")
                    {
                        if (h.CNF_Pass == h.New_Pass)
                        {
                            if (Gd.Decrypt(h.Old_Pass) != h.New_Pass)
                            {
                                h.Type = "CHANGE_PASSWORD";
                                Msg = db.Change_Pass(h);

                                if (Msg == "Password Changed Successfully")
                                {
                                    TempData["LAlert"] = Msg;
                                    return RedirectToActionPermanent("Index", "Login");
                                }
                                else
                                    TempData["HMAlert"] = Msg;
                            }
                            else
                                TempData["HMAlert"] = "New password Will be Same in Old Password!";
                        }
                        else
                        {
                            ModelState.AddModelError("New_Pass", "New Password is Invalid!");
                            ModelState.AddModelError("CNF_Pass", "Confirm New Password is Invalid!");
                        }
                    }
                    else
                        ModelState.AddModelError("Old_Pass", "Old Password is Invalid!");

                }
            }
            catch (Exception ex)
            {
                TempData["HMAlert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return View(h);
        }
    }
}