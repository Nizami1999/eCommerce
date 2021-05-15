using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class BlogController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }

            return View(blog);
        }

    }
}