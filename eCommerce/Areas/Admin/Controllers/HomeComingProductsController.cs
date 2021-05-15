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
    public class HomeComingProductsController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/HomeComingProducts
        public ActionResult Index()
        {
            return View(db.HomeComingProducts.ToList());
        }

        // GET: Admin/HomeComingProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeComingProduct homeComingProduct = db.HomeComingProducts.Find(id);
            if (homeComingProduct == null)
            {
                return HttpNotFound();
            }
            return View(homeComingProduct);
        }

        // POST: Admin/HomeComingProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Image")] HomeComingProduct homeComingProduct, HttpPostedFileBase Image, string oldImage)
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
                            string path = Path.Combine(Server.MapPath("~/Public/img/coming/"), currentImage); // where to save uploaded image
                            Image.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/coming/"), oldImage); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            homeComingProduct.Image = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Image type is invalid");
                        homeComingProduct.Image = oldImage;
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB");
                    homeComingProduct.Image = oldImage;
                }
            }
            else
            {
                homeComingProduct.Image = oldImage; // eyer wekil null geldise
            }

            if (ModelState.IsValid)
            {
                db.Entry(homeComingProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            homeComingProduct.Image = oldImage;
            return View(homeComingProduct);
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
