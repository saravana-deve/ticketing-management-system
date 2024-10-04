using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Models.Rights;

namespace TMS.Controllers
{
    [NoCache]
    public class RightsController : Controller
    {
        RightsDB Db = new RightsDB();
        string Msg = "";
        // GET: Rights
        [SessionTimeOut]
        public ActionResult Index()
        {
            Rights R = new Rights();
            try
            {
                R.Btn = "Save";
                R.Type = "GET_RIGHTS_ID";
                Db.Get_RightsId(R);

                R.Type = "GET_DATA";
                Db.Common(R, this);

            }
            catch (Exception ex)
            {
                TempData["Alert_Type"] = "Error";
                TempData["R_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return View(R);
        }
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Index(Rights R)
        {
            var Model = new Rights();
            try
            {

                if (ModelState.IsValid)
                {
                    if (R.Btn == "Save" || R.Btn == null)
                        R.Type = "INSERT";
                    else
                        R.Type = "UPDATE";

                    Db.Common(R, this);
                    ModelState.Clear();
                }
                Model.Btn = "Save";
                Model.Type = "GET_RIGHTS_ID";
                Db.Get_RightsId(Model);
                Model.Type = "GET_DATA";
                Db.Common(Model, this);

            }
            catch (Exception ex)
            {
                TempData["Alert_Type"] = "Error";
                TempData["R_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();

            }

            return View(Model);
        }
        [SessionTimeOut]
        public string Delete(Rights R)
        {
            try
            {
                R.Type = "DELETE";
                Msg = Db.Common(R, this);
            }
            catch (Exception ex)
            {
                Msg = JsonConvert.SerializeObject(ex.Message.Replace("\n", " ").Replace("\r", " ").ToString());
            }

            return Msg;
        }
    }
}