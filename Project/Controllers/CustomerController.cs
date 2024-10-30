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
    public class CustomerController : Controller
    {
        SalesContext context = new SalesContext();
        // GET: Customer
        #region display all customer
        [HttpGet]
        public ActionResult Index()
        {
            return View(context.Customers.ToList());
        }
        #endregion

        /*************************************************************************/
        #region create customer
        [HttpGet]
        public ActionResult create()
        {
            return View();
        
        }

        [HttpPost]
        public ActionResult create(Customer customer )
        {
            if (ModelState.IsValid)
            {
                var is_exait = context.Customers.FirstOrDefault(u => u.Email == customer.Email);
                var is_phone = context.Customers.FirstOrDefault(u =>u.PhoneNumber== customer.PhoneNumber);
                if (is_exait == null && is_phone == null)
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                    return RedirectToAction("Index");

                }
                else if (is_exait != null)
                {
                    
                        ModelState.AddModelError("", @Resource.Register_email);
                        return View(customer);
                    
                }
                else if(is_phone != null)
                {
                    ModelState.AddModelError("", @Resource.error_PhoneNumber);
                    return View(customer);
                }
                else
                {
                    ModelState.AddModelError("", @Resource.error_phone_email);
                    return View(customer);
                }
            }
            ModelState.AddModelError("", @Resource.vaild_data);
            return View(customer);

        }
        #endregion
        /***********************************************************************/
        #region edit
        [HttpGet()]
        public ActionResult edit(int id)
        { 
            var customer = context.Customers.FirstOrDefault(x => x.id == id);
            return View(customer); 
        
        }
        [HttpPost]
        public ActionResult edit(Customer cust)
        {
            var customer = context.Customers.FirstOrDefault(x => x.id == cust.id);
            customer.PhoneNumber= cust.PhoneNumber;
            customer.Address= cust.Address;
            customer.Email= cust.Email;
            customer.CustomerName= cust.CustomerName;
            context.Entry<Customer>(customer).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");

        }
        #endregion



        /*****************************************************************************/
        #region delete
        [HttpGet()]
        public ActionResult delete(int id)
        {

            var customer = context.Customers.Find(id);
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index");


        }
        #endregion



    }

}