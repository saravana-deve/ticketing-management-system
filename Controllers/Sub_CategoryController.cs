using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Common;
using TMS.Models.Sub_Category;

namespace TMS.Controllers
{
    [NoCache]
    public class Sub_CategoryController : Controller
    {
        Global_Data Gd = new Global_Data();
        Sub_CategoryDB Db = new Sub_CategoryDB();
        [SessionTimeOut]
        // GET: Sub_Category
        public ActionResult Index()
        {
            Sub_Category Sc = new Sub_Category();
            try
            {
                Sc.Btn = "Save";
                Sc.Type = "GET_SUB_CATEGORY_CODE";
                Db.Get_SC_Code(Sc);
                Db.Drop(Sc);

                Sc.Type = "GET_DATA";
                Db.Common(Sc, this);

            }
            catch (Exception ex)
            {
                TempData["Alert_Type"] = "Error";
                TempData["Sub_Cy_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return View(Sc);
        }
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Index(Sub_Category Sc)
        {
            var Model = new Sub_Category();
            Db.Drop(Model);
            try
            {
                if (ModelState.IsValid)
                {
                    if (Sc.Btn == "Save" || Sc.Btn == null)
                        Sc.Type = "INSERT";
                    else
                        Sc.Type = "UPDATE";

                    Db.Common(Sc, this);
                    ModelState.Clear();
                }
                Model.Btn = "Save";
                Model.Type = "GET_SUB_CATEGORY_CODE";
                Db.Get_SC_Code(Model);
                Model.Type = "GET_DATA";
                Db.Common(Model, this);

            }
            catch (Exception ex)
            {
                TempData["Alert_Type"] = "Error";
                TempData["Sub_Cy_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return View(Model);
        }
        [SessionTimeOut]
        public ActionResult Delete(Sub_Category Sc)
        {
            try
            {
                Sc.Type = "DELETE";
                Db.Common(Sc,this);
            }
            catch (Exception ex)
            {
                TempData["Alert_Type"] = "Error";
                TempData["Sub_Cy_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return RedirectToAction("Index");
        }
    }
}