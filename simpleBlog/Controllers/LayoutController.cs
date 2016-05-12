using authcontroller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using authcontroller.Models;
namespace authcontroller.Controllers
{
    public class LayoutController : Controller
    {
        //
        // GET: /Layout/
        [ChildActionOnly]
        public ActionResult SideBar()
        {

            LayoutSideBar l = new LayoutSideBar()
            {
                isLoggedIn = AuthController.User != null,
                Username = AuthController.User != null ? AuthController.User.UserName : "",
                isAdmin = User.IsInRole("admin"),
                Tags = Database.Session.Query<Tag>().Select(tag => new
                {
                    tag.Id,
                    tag.Name,
                    tag.Slug,
                    PostCount = tag.Posts.Count
                }).Where(t => t.PostCount > 0).OrderByDescending(p => p.PostCount).Select(
                    tag => new SidebarTag(tag.Id, tag.Name, tag.Slug, tag.PostCount)).ToList()
            };

            return View(l);
        }

    }
}
