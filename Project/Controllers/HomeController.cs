using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.Resources;
namespace Project.Controllers
{
    public class HomeController : Controller
    {
        SalesContext context = new SalesContext();

        #region home page
        public ActionResult Index()
        {
            if (Request.Cookies["sessionid"] == null)
            {


                return RedirectToAction("Login", "Account");





            }

            return View();
        }


        #endregion



        [HttpPost]
        public ActionResult Change(string lang) {
            if (lang != null)
            { 
                HttpCookie langaue = new HttpCookie("culture",lang);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
                Response.Cookies.Add(langaue);

            }

            return RedirectToAction("Index");
        
        }
    }
}