using authcontroller.Areas.Admin.ViewModels;
using authcontroller.Infrastructure;
using authcontroller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace authcontroller.Areas.Admin.Controllers
{
    [SelectedTabAttribute("users")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
       
        public ActionResult Index()
        {
            return View(
                new UsersIndex() { 
                    Users = Database.Session.QueryOver<User>().List()
                });
        }

        public ActionResult New()
        {
            List<RoleCheckBox> roles = new List<RoleCheckBox>();

            var allRoles = Database.Session.QueryOver<Role>().List();
            
            foreach (var r in allRoles)
            {
                roles.Add(new RoleCheckBox() { 
                    Id=r.Id,
                    Name=r.Name,
                    isChecked=false
                
                });    
            }

            
            return View(new UsersNew() { Roles=roles});
            
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult New(UsersNew form)
        {
            if (Database.Session.QueryOver<User>().Where(p => p.UserName == form.Username).RowCount() > 0)
            {
                ModelState.AddModelError("username", "username must be unique");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var user = new User() { 
                UserName=form.Username,
                Email=form.Email
            };

            List<Role> selectedRoles = new List<Role>();

            var dbRoles = Database.Session.QueryOver<Role>().List();

            foreach (var r in form.Roles)
            {
                if (r.isChecked)
                {
                    selectedRoles.Add(dbRoles.SingleOrDefault(p=>p.Id==r.Id));
                }
            }


            user.setPassword(form.Password);
            user.Roles = selectedRoles;
            Database.Session.Save(user);

            return RedirectToAction("index");
        }

        public ActionResult Edit(int id)
        {

            var user = Database.Session.Load<User>(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            List<RoleCheckBox> selectedRoles = new List<RoleCheckBox>();

            var dbRoles = Database.Session.QueryOver<Role>().List();

            foreach (var r in dbRoles)
            {
                selectedRoles.Add(new RoleCheckBox() { 
                    Id=r.Id,
                    Name=r.Name,
                    isChecked=user.Roles.Where(p=>p.Id==r.Id).Count()==0?false:true
                });
            }

            return View(new UsersEdit() { 
                Username=user.UserName,
                Email=user.Email,
                Roles=selectedRoles
            });
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Edit(int id,UsersEdit form)
        {

            var user = Database.Session.Load<User>(id);


            if (user == null)
            {
                return HttpNotFound();
            }




            if (Database.Session.QueryOver<User>().Where(p => p.UserName == form.Username && p.Id != user.Id).RowCount() > 0)
            {
                ModelState.AddModelError("username", "username must be unique");
            }

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            //var user = new User()
            //{
            //    UserName = form.Username,
            //    Email = form.Email
            //};

            user.UserName = form.Username;
            user.Email = form.Email;
            
            Database.Session.Update(user);

            return RedirectToAction("index");
        }

        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Load<User>(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new UsersResetPassword() { 
                Username=user.UserName
            });
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult ResetPassword(int id,UsersResetPassword form)
        {
            var user = Database.Session.Load<User>(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            user.setPassword(form.Password);

            Database.Session.Update(user);

            return RedirectToAction("index");
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = Database.Session.Load<User>(id);

            if (user == null)
            {
                return HttpNotFound();
            }

           

            Database.Session.Delete(user);

            return RedirectToAction("index");
        }

    }



}
