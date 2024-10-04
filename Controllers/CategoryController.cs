using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMS.App_Start;
using TMS.Common;
using TMS.Models.Category;

namespace TMS.Controllers
{
    [NoCache]
    public class CategoryController : Controller
    {
        Global_Data Gd = new Global_Data();
        CategoryDB Db = new CategoryDB();
        string Msg = "";
        // GET: Category

        [SessionTimeOut]
        public ActionResult Index()
        {

            Category Cy = new Category();
            try
            {
                Cy.Btn = "Save";
                Cy.Type = "GET_CATEGORY_CODE";
                Db.Get_NatureCode(Cy);
                Db.Drop(Cy);

                Cy.Type = "GET_DATA";
                Db.Common(Cy);

            }
            catch (Exception ex)
            {
                TempData["Cy_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return View(Cy);

        }
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Index(Category Cy)
        {
            var Model = new Category();
            Db.Drop(Model);
            try
            {

                if (ModelState.IsValid)
                {
                    if (Cy.Btn == "Save" || Cy.Btn == null)
                        Cy.Type = "INSERT";
                    else
                        Cy.Type = "UPDATE";

                    Msg = Db.Common(Cy);
                    TempData["Cy_Alert"] = Msg;
                    ModelState.Clear();
                }
                Model.Btn = "Save";
                Model.Type = "GET_CATEGORY_CODE";
                Db.Get_NatureCode(Model);
                Model.Type = "GET_DATA";
                Db.Common(Model);

            }
            catch (Exception ex)
            {
                TempData["Cy_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }

            return View(Model);
        }
        [SessionTimeOut]
        public ActionResult Delete(Category Cy)
        {
            try
            {
                Cy.Type = "DELETE";
                Msg = Db.Common(Cy);
                TempData["Cy_Alert"] = Msg;
            }
            catch (Exception ex)
            {
                TempData["Cy_Alert"] = ex.Message.Replace("\n", " ").Replace("\r", " ").ToString();
            }
            return RedirectToAction("Index");
        }
    }
}