using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Common;
using TMS.Models.Department;

namespace TMS.Controllers
{
    [NoCache]
    public class DepartmentController : Controller
    {
        Global_Data Gd = new Global_Data();
        DepartmentDB Db = new DepartmentDB();
        string Msg = "";
        // GET: Department 
        [SessionTimeOut]
        public ActionResult Index()
        {
            Department Dm = new Department();
            try
            {
                Dm.Btn = "Save";
                Dm.Type = "GET_DEPARTMENT_CODE";
                Db.Get_DeptCode(Dm);

                Dm.Type = "GET_DATA";
                Db.Common(Dm);

            }
            catch (Exception ex)
            {
                TempData["Dep_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return View(Dm);
        }
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Index(Department Dm)
        {
            var Model = new Department();
            try
            {

                if (ModelState.IsValid)
                {
                    if (Dm.Btn == "Save" || Dm.Btn ==null)
                        Dm.Type = "INSERT";
                    else
                        Dm.Type = "UPDATE";

                    Msg = Db.Common(Dm);
                    TempData["Dep_Alert"] = Msg;
                    ModelState.Clear();
                }
                Model.Btn = "Save";
                Model.Type = "GET_DEPARTMENT_CODE";
                Db.Get_DeptCode(Model);
                Model.Type = "GET_DATA";
                Db.Common(Model);

            }
            catch (Exception ex)
            {
                TempData["Dep_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();

            }

            return View(Model);
        }
        [SessionTimeOut]
        public ActionResult Delete(Department Dm)
        {
            try
            {
                Dm.Type = "DELETE";
                Msg = Db.Common(Dm);
                TempData["Dep_Alert"] = Msg;
            }
            catch (Exception ex)
            {
                TempData["Dep_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return RedirectToAction("Index");
        }
    }
}