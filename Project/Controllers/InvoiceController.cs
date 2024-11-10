using Project.Models;
using Project.service;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    [SessionAuthorize]
    public class InvoiceController : Controller
    {
        SalesContext context = new SalesContext();
        // GET: Invoice
        #region display all invoice
        public ActionResult Index()
        {
            return View(context.Invoices.ToList());
        }
        #endregion

        /*************************************************************************/

        #region create new invoice
        [HttpGet]
        public ActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(context.Customers.ToList(), "id", "CustomerName");
             ViewData["CategoryId"] = new SelectList(context.Categories.ToList(), "id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Create_Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice1 = new Invoice();
                invoice1.InvoiceDate = invoice.InvoiceDate;
                invoice1.Discount = invoice.Discount;
                invoice1.CustomerId = invoice.CustomerId;
                invoice1.TotalAmount = invoice.TotalAmount;
                context.Invoices.Add(invoice1);
                foreach (var item in invoice.Items)
                {
                    InvoiceItem invoiceItem = new InvoiceItem();
                    invoiceItem.TotalPrice = item.TotalPrice;
                    invoiceItem.UnitPrice = item.UnitPrice;
                    invoiceItem.Quantity = item.Quantity;
                    invoiceItem.Discount = item.Discount;
                    invoiceItem.ProductId = item.ProductId;
                    invoiceItem.InvoiceId = invoice1.id;
                    context.InvoiceItems.Add(invoiceItem);
                    var product = context.Products.Find(item.ProductId); 
                    if (product != null)
                    {
                        product.Quantity -= item.Quantity;
                        context.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
                    }

                }

                context.SaveChanges();
                //return RedirectToAction("Index");

                bool isSuccess = true;

                if (isSuccess)
                {

                    return Json(new { success = true, redirectUrl = Url.Action("Index") });
                }



            }
            ViewData["CustomerId"] = new SelectList(context.Customers.ToList(), "id", "CustomerName");
             ViewData["CategoryId"] = new SelectList(context.Categories.ToList(), "id", "Name");
            return View();

        }

        #endregion
    }
}