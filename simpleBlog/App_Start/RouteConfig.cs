
using authcontroller.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace authcontroller
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
          
            var namespaces=new []{typeof(PostsController).Namespace};
            
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute("Home", "", new {controller="Posts",action="index"},namespaces);
            
            routes.MapRoute("Login", "login", new { controller="Auth",action="login"},namespaces);
            routes.MapRoute("logout", "logout", new { controller = "Auth", action = "logout" }, namespaces);

            routes.MapRoute("PostForRealThisTime", "post/{idAndSlug}", new { controller = "Posts", action = "Show" }, namespaces);
            routes.MapRoute("Post", "post/{id}-{slug}", new { controller = "Posts", action = "Show" }, namespaces);

            routes.MapRoute("TagForRealThisTime", "tag/{idAndSlug}", new { controller = "Posts", action = "Tag" }, namespaces);
            routes.MapRoute("Tag", "tag/{id}-{slug}", new { controller = "Posts", action = "Tag" }, namespaces);
            routes.MapRoute("Sidebar", "sidebar", new { controller = "Layout", action = "Sidebar" }, namespaces);
        }
    }
}