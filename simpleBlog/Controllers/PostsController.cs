using authcontroller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using authcontroller.Models;
using authcontroller.Infrastructure;
using System.Text.RegularExpressions;

namespace authcontroller.Controllers
{
    public class PostsController:Controller
    {
        private static int postPerPage = 10;

        public ActionResult index(int page=1)
        {
            
            var totalPostCount = Database.Session.Query<Post>().Count();

            var currentItems = Database.Session.Query<Post>()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * postPerPage)
                .Take(postPerPage)
                .ToList();


            return View(new PostsIndex(){Posts=new PagedData<Post>(currentItems,totalPostCount,page,postPerPage)});
        }

        public ActionResult Show(string idAndSlug) {
            var parts = SeperateIdAndSlug(idAndSlug);


            if (parts == null)
                return HttpNotFound();

            var post = Database.Session.Load<Post>(parts.Item1);

            if (post == null | post.IsDeleted) {
                return HttpNotFound();
            }

            if (!post.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Post", new { id = parts.Item1, slug = post.Slug });


            return View(new PostsShow() { Post = post });
        }

        private System.Tuple<int,string> SeperateIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");
            return Tuple.Create(id, slug);


        }


        public ActionResult Tag(string idAndSlug, int page = 1)
        {
            var parts = SeperateIdAndSlug(idAndSlug);

            if (parts == null)
                return HttpNotFound();

            var tag = Database.Session.Load<Tag>(parts.Item1);

            if (tag == null)
                return HttpNotFound();

            if (!tag.Slug.Equals(parts.Item2, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("tag", new { id = parts.Item1, slug = tag.Slug });

            var totalPostCount = tag.Posts.Count();

            var currentItems = tag.Posts
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * postPerPage)
                .Take(postPerPage)
                .Where(t => t.DeletedAt == null)
                .ToList();

            return View(new PostsTag() { 
                    Tag=tag,
                    Posts=new PagedData<Post>(currentItems,totalPostCount,page,postPerPage)

            });
            
        }
    }
}