using authcontroller.Areas.Admin.ViewModels;
using authcontroller.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using authcontroller.Models;
using authcontroller.Controllers;
using authcontroller.Infrastructure.Extensions;

namespace authcontroller.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("posts")]
    public class PostsController : Controller
    {
        //
        // GET: /Admin/Posts/
       private const int postPerPage = 10;


        public ActionResult Index(int page=1)
        {
            var totalPostCount=Database.Session.Query<Post>().Count();

            var currentItems= Database.Session.Query<Post>()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((page - 1) * postPerPage)
                .Take(postPerPage)
                .ToList();


            return View(new PostsIndex() { 
                Posts=new PagedData<Post>(currentItems,totalPostCount,page,postPerPage)
            });


        }

        public ActionResult New()
        {

            return View("form", new PostsForm() { isNew = true, 
                Tags = Database.Session.Query<Tag>().Select(
                tag => 
                    new TagCheckBox() { 
                        Id=tag.Id,
                        Name=tag.Name,
                        isChecked=false
                        }
                    ).ToList() });
        }

        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View("form", new PostsForm() { 
                    isNew = false,
                    postId=post.Id,
                    Content=post.Content,
                    Title=post.Title,
                    Slug=post.Slug,
                    Tags=Database.Session.Query<Tag>().Select(tag=>new TagCheckBox(){
                        Id=tag.Id,
                        Name=tag.Name,
                        isChecked=post.Tags.Contains(tag)
                    }).ToList()
            });
        }

        
        [HttpPost,ValidateAntiForgeryToken,ValidateInput(false)]
        public ActionResult Form(PostsForm form)
        {
            form.isNew = form.postId == null;

            if (!ModelState.IsValid)
            {
                return View(form);
            }


            var selectedTags = ReconsileTags(form.Tags);

            var post = new Post();

            if (form.isNew)
            {
                post = new Post()
                {
                    CreatedAt=DateTime.UtcNow,
                    User=AuthController.User
                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.postId);
                if (post == null) {
                    return HttpNotFound();
                }
                
                post.UpdatedAt = DateTime.UtcNow;

            }


            post.Tags = selectedTags.ToList();
            post.Title = form.Title;
            post.Slug=form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);


            return RedirectToAction("index");
        }

        private IEnumerable<Tag> ReconsileTags(IList<TagCheckBox> tags)
        {
           foreach(var tag in tags.Where(t=>t.isChecked)){
               if (tag.Id != null)
               {
                   yield return Database.Session.Load<Tag>(tag.Id);
                   continue;
               }

               var existingTag = Database.Session.Query<Tag>().FirstOrDefault(t => t.Name == tag.Name);
               if (existingTag != null)
               {
                   yield return existingTag;
                   continue;
               }

               var newTag = new Tag() { 
                    Name=tag.Name,
                    Slug=tag.Name.Slugify()
               };

               Database.Session.Save(newTag);
               yield return newTag;


           }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            post.DeletedAt = DateTime.UtcNow;
            Database.Session.SaveOrUpdate(post);

            
            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            post.DeletedAt = null;
            Database.Session.SaveOrUpdate(post);


            return RedirectToAction("index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            Database.Session.Delete(post);


            return RedirectToAction("index");
        }
        
    }
}
