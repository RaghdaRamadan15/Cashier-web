using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Resources;
using Project.service;

namespace Project.Controllers
{
    [SessionAuthorize]
    public class CategoryController : Controller
    {
        SalesContext context = new SalesContext();
        // GET: Category
        #region show list of catogray
        public ActionResult Index()
        {
            return View(context.Categories.ToList());
        }
        #endregion


        /********************************************************************************************************/


        #region add new catogray
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var is_exait = context.Categories.FirstOrDefault(c => c.Name == category.Name);
                if (is_exait == null)
                {
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return RedirectToAction("Index");

                }


            }
            ModelState.AddModelError("", @Resource.vaild_data);

            return View();
        }

        #endregion



        /***************************************************************************************************/
        #region edit

        [HttpGet()]
        public ActionResult edit(int id)
        {
            var Category = context.Categories.FirstOrDefault(x => x.id == id);
            return View(Category);

        }
        [HttpPost]
        public ActionResult edit(Category cust)
        {
            var cot = context.Categories.FirstOrDefault(x => x.id == cust.id);
            cot.Description = cust.Description;
            cot.Name = cust.Name;
            context.Entry<Category>(cot).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        #endregion




        #region delete
        [HttpGet()]
        public ActionResult delete(int id)
        {

            var cot = context.Categories.Find(id);
            context.Categories.Remove(cot);
            context.SaveChanges();
            return RedirectToAction("Index");


        }
        #endregion

    }
}