using Project.Models;
using Project.Resources;
using Project.service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [SessionAuthorize]
    public class ProductController : Controller
    {
        SalesContext context = new SalesContext();
        // GET: Product

        #region display products 
        public ActionResult Index()
        {

            ViewData["CategoryId"] = new SelectList(context.Categories.ToList(), "id", "Name");
            return View();

        }

        //return products by select frist    catogrey id

        [HttpGet]
        public ActionResult getproducts(int id)
        {

            var products = context.Products.Where(p => p.CategoryId == id).ToList();
            return PartialView("_getproduct", products);
        }

        #endregion





        /*********************************************************************************/

        #region add new product
        [HttpGet]
        public ActionResult AddProduct()
        {

            ViewData["CategoryId"] = new SelectList(context.Categories.ToList(), "id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(HttpPostedFileBase file, Product p)
        {
            if (file == null )
            {
                ModelState.AddModelError("", Resource.error_imag);
                ViewData["CategoryId"] = new SelectList(context.Categories.ToList(), "id", "Name");
                return View();
            }
            if (ModelState.IsValid)
            {
                var imageName = Path.GetFileName(file.FileName);
                string path = Server.MapPath("~/Content/Images");
                var imagePath = Path.Combine(path, imageName);
                file.SaveAs(imagePath);
                Product product = new Product
                {
                    image = "/Content/Images/" + imageName,
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CategoryId = p.CategoryId
                };
                context.Products.Add(product);
                context.SaveChanges();
                Console.WriteLine(product);

                return RedirectToAction("Index");



            }
            ViewData["CategoryId"] = new SelectList(context.Categories.ToList(), "id", "Name");
            return View();
        }

        #endregion


        /*********************************************************************************/

        #region delete
        [HttpGet]
        public ActionResult delete(int id)
        {

            var p = context.Products.FirstOrDefault(pr => pr.id == id);

            context.Products.Remove(p);
            context.SaveChanges();
            return RedirectToAction("Index");


        }
        #endregion

        /********************************************************************************/

        #region edit
        [HttpGet]
        public ActionResult edit(int id)
        {

            var p = context.Products.FirstOrDefault(pr => pr.id == id);
            return View(p);
        }
        [HttpPost]
        public ActionResult edit(HttpPostedFileBase file, Product p)
        {
            if (ModelState.IsValid)
            {
                var product = context.Products.FirstOrDefault(pr => pr.id == p.id);
                if (file != null && file.ContentLength > 0)
                {
                    var imageName = Path.GetFileName(file.FileName);
                    string path = Server.MapPath("~/Content/Images");
                    var imagePath = Path.Combine(path, imageName);
                    file.SaveAs(imagePath);
                    product.image = "/Content/Images" + imageName;
                }
                product.ProductName = p.ProductName;
                product.Quantity = p.Quantity;
                product.Price = p.Price;

                context.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(p);
        }

        #endregion




    }
}