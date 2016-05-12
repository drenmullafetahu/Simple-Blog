using authcontroller.Models;
using authcontroller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace authcontroller.Controllers
{

    

    public class AuthController : Controller
    {


        public static string UserKey = "SimpleBlog.User";

        public static User User
        {
            get
            {
                
                if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;


                var user = System.Web.HttpContext.Current.Items[UserKey] as User;
                
                 
                if (user == null)
                {
                    user = Database.Session.QueryOver<User>().Where(p => p.UserName == System.Web.HttpContext.Current.User.Identity.Name).SingleOrDefault();
                    if (user==null){
                        return null;
                    }

                    System.Web.HttpContext.Current.Items[UserKey] = user;

                }


                return user;
            }
        }

        public ActionResult logout()
        {
            
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");

        }

        public ActionResult login()
        {
            return View(new AuthLogin() { Test="value from controller's login action requested by get method"});
            
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult login(AuthLogin form,string returnUrl)
        {

            var user = Database.Session.QueryOver<User>().Where(p => p.UserName == form.UserName).SingleOrDefault();



            //if (user == null)
            //{
            //    new User().FakeHash();
            //    ModelState.AddModelError("username", "Username or password is incorrect");
            //    return View(form);
            //}

            //if (!user.CheckPassword(form.Password))
            //{
            //    ModelState.AddModelError("username", "Username or password is incorrect");
            //}

            if (!ModelState.IsValid)
            {
                return View(form);
            }


            FormsAuthentication.SetAuthCookie(form.UserName, true);
            //form.Test= "value from post method";

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
                return RedirectToRoute("home");


        }

       
    }
}
