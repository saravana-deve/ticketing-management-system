using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Models.User;

namespace TMS.Controllers
{
    [NoCache]
    public class UserController : Controller
    {
        UserDB Db = new UserDB();
        string Msg = "";
        // GET: User
        [SessionTimeOut]
        public ActionResult Index()
        {
            var Model = new User();
            try
            {
                Db.Drop(Model);
                Model.Btn = "Save";
                Model.Type = "GET_DATA";
                Db.Common(Model);
                Session["Branch_List"] = null;

            }
            catch (Exception ex)
            {
                TempData["UM_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return View(Model);
        }
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Index(User Ur)
        {
            var Model = new User();
            Db.Drop(Model);
            //Db.Drop_Branch(Model);

            try
            {
                if (ModelState.IsValid)
                {
                    if (Ur.Btn == "Save" || Ur.Btn == null)
                        Ur.Type = "INSERT";
                    else
                        Ur.Type = "UPDATE";

                    Msg = Db.Common(Ur);
                    TempData["UM_Alert"] = Msg;
                    ModelState.Clear();
                    Model.Branch_List = null;
                    Session["Branch_List"] = null;
                }

                Model.Btn = "Save";
                Model.Type = "GET_DATA";
                Db.Common(Model);

            }
            catch (Exception ex)
            {
                TempData["UM_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return View(Model);
        }
        [SessionTimeOut]
        public ActionResult Delete(User Ur)
        {
            try
            {
                Ur.Type = "DELETE";
                Msg = Db.Common(Ur);
                TempData["UM_Alert"] = Msg;
            }
            catch (Exception ex)
            {
                TempData["UM_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return RedirectToAction("Index");
        }
        public string Get_Branch(User Ur)
        {
            try
            {
                Msg = Db.GetBranch(Ur);
            }
            catch (Exception ex)
            {
                Msg = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return Msg;
        }
    }
}