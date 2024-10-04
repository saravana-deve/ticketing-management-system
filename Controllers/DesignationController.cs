using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Models.Designation;

namespace TMS.Controllers
{
    [NoCache]
    public class DesignationController : Controller
    {
        // GET: Designation
        DesignationDB Db = new DesignationDB();
        string Msg = "";
        [SessionTimeOut]
        public ActionResult Index()
        {
            Designation Dn = new Designation();
            try
            {
                Dn.Btn = "Save";
                Dn.Type = "GET_DESIGNATION_CODE";
                Db.Get_DesiCode(Dn);

                Dn.Type = "GET_DATA";
                Db.Common(Dn);

            }
            catch (Exception ex)
            {
                TempData["Desi_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return View(Dn);
        }
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Index(Designation Dn)
        {
            var Model = new Designation();
            try
            {
                if (ModelState.IsValid)
                {
                    if (Dn.Btn == "Save" || Dn.Btn == null)
                        Dn.Type = "INSERT";
                    else
                        Dn.Type = "UPDATE";

                    Msg = Db.Common(Dn);
                    TempData["Desi_Alert"] = Msg;
                    ModelState.Clear();
                }

                Model.Btn = "Save";
                Model.Type = "GET_DESIGNATION_CODE";
                Db.Get_DesiCode(Model);
                Model.Type = "GET_DATA";
                Db.Common(Model);

            }
            catch (Exception ex)
            {
                TempData["Desi_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();

            }

            return View(Model);
        }
        [SessionTimeOut]
        public ActionResult Delete(Designation Dn)
        {
            try
            {
                Dn.Type = "DELETE";
                Msg = Db.Common(Dn);
                TempData["Desi_Alert"] = Msg;
            }
            catch(Exception ex)
            {
                TempData["Desi_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return RedirectToAction("Index");
        }
    }
}