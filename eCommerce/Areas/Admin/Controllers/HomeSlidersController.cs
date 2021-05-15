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
using eCommerce.MVCFilters;

namespace eCommerce.Areas.Admin.Controllers
{
    [AuthorizationFilter]
    public class HomeSlidersController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/HomeSliders
        public ActionResult Index()
        {
            return View(db.HomeSliders.ToList());
        }

        // GET: Admin/HomeSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HomeSliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Image")] HomeSlider homeSlider, HttpPostedFileBase Image)
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
                        string path = Path.Combine(Server.MapPath("~/Public/img/slider/home1/"), currentImage); // where to save uploaded image
                        Image.SaveAs(path); // save in this path

                        homeSlider.Image = currentImage;
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
                db.HomeSliders.Add(homeSlider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homeSlider);
        }

        // GET: Admin/HomeSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeSlider homeSlider = db.HomeSliders.Find(id);
            if (homeSlider == null)
            {
                return HttpNotFound();
            }
            return View(homeSlider);
        }

        // POST: Admin/HomeSliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Image")] HomeSlider homeSlider, HttpPostedFileBase Image, string oldImage)
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
                            string path = Path.Combine(Server.MapPath("~/Public/img/slider/home1/"), currentImage); // where to save uploaded image
                            Image.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/slider/home1/"), oldImage); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            homeSlider.Image = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Image type is invalid");
                        homeSlider.Image = oldImage;
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB");
                    homeSlider.Image = oldImage;
                }
            }
            else
            {
                homeSlider.Image = oldImage; // eyer wekil null geldise
            }

            if (ModelState.IsValid)
            {
                db.Entry(homeSlider).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            homeSlider.Image = oldImage;
            return View(homeSlider);
        }

        // GET: Admin/HomeSliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeSlider homeSlider = db.HomeSliders.Find(id);
            if (homeSlider == null)
            {
                return HttpNotFound();
            }
            return View(homeSlider);
        }

        // POST: Admin/HomeSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomeSlider homeSlider = db.HomeSliders.Find(id);

            db.HomeSliders.Remove(homeSlider);
            db.SaveChanges();

            // Image
            string path = Path.Combine(Server.MapPath("~/Public/img/slider/home1/"), homeSlider.Image);

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
