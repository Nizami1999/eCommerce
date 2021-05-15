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
    public class AboutsController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/Abouts
        public ActionResult Index()
        {
            return View(db.Abouts.ToList());
        }

        // GET: Admin/Abouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // POST: Admin/Abouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Image")] About about, HttpPostedFileBase Image, string oldImage)
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
                            string path = Path.Combine(Server.MapPath("~/Public/img/about/"), currentImage); // where to save uploaded image
                            Image.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/about/"), oldImage); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            about.Image = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Image type is invalid");
                        about.Image = oldImage;
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB");
                    about.Image = oldImage;
                }
            }
            else
            {
                about.Image = oldImage; // eyer wekil null geldise
            }

            if (ModelState.IsValid)
            {
                db.Entry(about).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            about.Image = oldImage;
            return View(about);
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
