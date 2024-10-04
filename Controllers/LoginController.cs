using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Models.Login;
namespace TMS.Controllers
{
    [NoCache]
    public class LoginController : Controller
    {
        LoginDB Db = new LoginDB();
        string Msg = "";
        // GET: Login
        public ActionResult Index()
        {

            if (Session["UserCode"] != null)
            {
                Session.Abandon();
                Session.Clear();
            }

            return View();
        }
        [HttpPost]
        public ActionResult Index(Login L)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Msg = Db.Login(L);
                    if (Msg == "True")
                    {
                        TempData["LogedIn"] = "Login";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                        TempData["LAlert"] = Msg;
                    
                }
            }
            catch (Exception ex)
            {
                TempData["LAlert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return View(L);
        }
    }
}