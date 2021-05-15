using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCommerce.Models;

namespace eCommerce.Areas.Admin.Controllers
{
    public class BlogsController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/Blogs
        public ActionResult Index()
        {
            return View(db.Blogs.ToList());
        }

        // GET: Admin/Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Image")] Blog blog, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                if ((double)Image.ContentLength / (1024 * 1024) <= 2)
                {
                    if (
                        Image.ContentType == "image/jpg" ||
                        Image.ContentType == "image/png" ||
                        Image.ContentType == "image/jpeg" ||
                        Image.ContentType == "image/gif"
                    )
                    {
                        // Image
                        string currentImage = DateTime.Now.ToString("_yyyyMMdd_hhmmss") /*to save unique named image*/ + Path.GetFileName(Image.FileName); // get only name of uploaded image
                        string path = Path.Combine(Server.MapPath("~/Public/img/blog/"), currentImage); // where to save uploaded image
                        Image.SaveAs(path); // save in this path

                        blog.Image = currentImage;
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Image type is invalid");
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB");
                }
            }
            else
            {
                ModelState.AddModelError("Image", "Image can not be empty");
            }

            if (ModelState.IsValid)
            {
                blog.Date = DateTime.Now;
                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Admin/Blogs/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Admin/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Image")] Blog blog, HttpPostedFileBase Image, string oldImage)
        {
            if (Image != null)
            {
                if ((double)Image.ContentLength / (1024 * 1024) <= 2)
                {
                    if (
                        Image.ContentType == "image/jpg" ||
                        Image.ContentType == "image/png" ||
                        Image.ContentType == "image/jpeg" ||
                        Image.ContentType == "image/gif"
                    )
                    {
                        if (ModelState.IsValid)
                        {
                            // Image
                            string currentImage = DateTime.Now.ToString("_yyyyMMdd_hhmmss") /*to save unique named image*/ + Path.GetFileName(Image.FileName); // get only name of uploaded image
                            string path = Path.Combine(Server.MapPath("~/Public/img/blog/"), currentImage); // where to save uploaded image
                            Image.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/blog/"), oldImage); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            blog.Image = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Image type is invalid");
                        blog.Image = oldImage;
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB");
                    blog.Image = oldImage;
                }
            }
            else
            {
                blog.Image = oldImage; // eyer wekil null geldise
            }

            if (ModelState.IsValid)
            {
                blog.Date = DateTime.Now;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            blog.Image = oldImage;
            return View(blog);
        }

        // GET: Admin/Blogs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();

            // Image
            string path = Path.Combine(Server.MapPath("~/Public/img/blog/"), blog.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
