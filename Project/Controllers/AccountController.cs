using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Security.Cryptography;
using Project.service;
using Microsoft.Ajax.Utilities;
using Project.ViewModel;
using System.Data.Entity.Migrations;
using Project.Resources;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        SalesContext context=new SalesContext();
        // GET: Account
        string x;
       

        /**********************************************************************************************************************/

        #region create Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {
                var is_exait = context.Users.FirstOrDefault(u => u.email == user.email);
                if (is_exait == null)
                {
                    string pass = Encryption.EncryptStringToBytes_Aes(user.PasswordHash);
                    x = Encryption.DecryptStringFromBytes_Aes(pass);


                    user.PasswordHash = pass;
                    context.Users.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", @Resource.Register_email);
                    return View();

                }


            }


            return View();
        }

        #endregion

        /*********************************************************************************************************************/
        #region login

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(login log)
        {
            if (ModelState.IsValid)
            {
                var user = context.Users.FirstOrDefault(u => u.email == log.email);
                if (user != null)
                {
                    if (Encryption.DecryptStringFromBytes_Aes(user.PasswordHash) == log.PasswordHash)
                    {
                        if (user.ActiveSession == null)
                        {
                            string sessionid = Guid.NewGuid().ToString();
                            string name = user.Username;
                            HttpCookie user_login = new HttpCookie("sessionid", sessionid);
                            HttpCookie user_name = new HttpCookie("name", name);
                            user_login.Expires = DateTime.Now.AddHours(3);
                            user_name.Expires = DateTime.Now.AddHours(3);
                            Response.Cookies.Add(user_login);
                            Response.Cookies.Add(user_name);
                            user.ActiveSession = sessionid;
                            user.LastLogin = DateTime.Now;
                            context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            return RedirectToAction("Index", "Home");

                        }
                        else if(user.ActiveSession !=null && (DateTime.Now.Subtract((DateTime)user.LastLogin).Hours>=3))
                        {
                            string sessionid = Guid.NewGuid().ToString();
                            string name = user.Username;
                            HttpCookie user_login = new HttpCookie("sessionid", sessionid);
                            HttpCookie user_name = new HttpCookie("name", name);
                            user_login.Expires = DateTime.Now.AddHours(3);
                            user_name.Expires = DateTime.Now.AddHours(3);
                            Response.Cookies.Add(user_login);
                            Response.Cookies.Add(user_name);
                            user.ActiveSession = sessionid;
                            user.LastLogin = DateTime.Now;
                            context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", @Resource.login_email);

                            return View();

                        }
                    }




                }

            }
            ModelState.AddModelError("", @Resource.error_email_password);
            return View();
        }

        #endregion
        
        
        
        
        
        /*********************************************************************************************************************/
        
        #region logout
        [HttpGet]
        public ActionResult Logout()
        {
            if (Request.Cookies["sessionid"].Value != null)
            {
                string sessionid = Request.Cookies["sessionid"].Value;
                var user = context.Users.FirstOrDefault(u => u.ActiveSession == sessionid);
                if (user != null)
                {

                    user.ActiveSession = null;
                    var authCookie = Request.Cookies["sessionid"];
                    var user_name = Request.Cookies["name"];
                    user_name.Expires = DateTime.Now.AddDays(-1);
                    authCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(user_name);
                    Response.Cookies.Add(authCookie);
                    context.Entry<User>(user).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();

                }
            }
            return RedirectToAction("Login");
        }


        #endregion




    }



}