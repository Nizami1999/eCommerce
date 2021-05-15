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
    public class SmartphonesController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/Smartphones
        public ActionResult Index()
        {
            var smartphones = db.Smartphones.Include(s => s.Brand);
            return View(smartphones.ToList());
        }

        // GET: Admin/Smartphones/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            return View();
        }

        // POST: Admin/Smartphones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Image")] Smartphone smartphone, HttpPostedFileBase Image)
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
                            string path = Path.Combine(Server.MapPath("~/Public/img/Smartphones/"), currentImage); // where to save uploaded image
                            Image.SaveAs(path); // save in this path

                            smartphone.Image = currentImage;
                        }
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
                db.Smartphones.Add(smartphone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", smartphone.BrandId);
            return View(smartphone);
        }

        // GET: Admin/Smartphones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smartphone smartphone = db.Smartphones.Find(id);
            if (smartphone == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", smartphone.BrandId);
            return View(smartphone);
        }

        // POST: Admin/Smartphones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Image")] Smartphone smartphone, HttpPostedFileBase Image, string oldImage)
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
                            string path = Path.Combine(Server.MapPath("~/Public/img/Smartphones/"), currentImage); // where to save uploaded image
                            Image.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/Smartphones/"), oldImage); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            smartphone.Image = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Image type is invalid");
                        smartphone.Image = oldImage;
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB");
                    smartphone.Image = oldImage;
                }
            }
            else
            {
                smartphone.Image = oldImage; // eyer wekil null geldise
            }

            if (ModelState.IsValid)
            {
                db.Entry(smartphone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", smartphone.BrandId);

            smartphone.Image = oldImage;
            return View(smartphone);
        }

        // GET: Admin/Smartphones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smartphone smartphone = db.Smartphones.Find(id);
            if (smartphone == null)
            {
                return HttpNotFound();
            }
            return View(smartphone);
        }

        // POST: Admin/Smartphones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Smartphone smartphone = db.Smartphones.Find(id);
            db.Smartphones.Remove(smartphone);
            db.SaveChanges();

            // Image
            string path = Path.Combine(Server.MapPath("~/Public/img/Smartphones/"), smartphone.Image);

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
