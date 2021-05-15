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
    public class HomeCollectionsController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/HomeCollections
        public ActionResult Index()
        {
            return View(db.HomeCollections.ToList());
        }

        // GET: Admin/HomeCollections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeCollection homeCollection = db.HomeCollections.Find(id);
            if (homeCollection == null)
            {
                return HttpNotFound();
            }
            return View(homeCollection);
        }

        // POST: Admin/HomeCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "LeftImage,CenterImage,RightImage")] HomeCollection homeCollection,
                                                                                      HttpPostedFileBase ImageL,
                                                                                      HttpPostedFileBase ImageC,
                                                                                      HttpPostedFileBase ImageR,
                                                                                      string oldImageL,
                                                                                      string oldImageC,
                                                                                      string oldImageR
                                                                                      )
        {

            if (ImageL != null)
            {
                if ((double)ImageL.ContentLength / (1024 * 1024) <= 2)
                {
                    if (
                        ImageL.ContentType == "image/jpg" ||
                        ImageL.ContentType == "image/png" ||
                        ImageL.ContentType == "image/jpeg" ||
                        ImageL.ContentType == "image/gif"
                    )
                    {
                        if (ModelState.IsValid)
                        {
                            // Image
                            string currentImage = DateTime.Now.ToString("_yyyyMMdd_hhmmss1") /*to save unique named image*/ + Path.GetFileName(ImageL.FileName); // get only name of uploaded image
                            string path = Path.Combine(Server.MapPath("~/Public/img/collect/"), currentImage); // where to save uploaded image
                            ImageL.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/collect/"), oldImageL); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            homeCollection.LeftImage = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageL", "Image Left type is invalid");
                        homeCollection.LeftImage = oldImageL;
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageL", "Image Left size must be less than 2 MB");
                    homeCollection.LeftImage = oldImageL;
                }
            }
            else
            {
                homeCollection.LeftImage = oldImageL; // eyer wekil null geldise
            }

            if (ImageC != null)
            {
                if ((double)ImageC.ContentLength / (1024 * 1024) <= 2)
                {
                    if (
                        ImageC.ContentType == "image/jpg" ||
                        ImageC.ContentType == "image/png" ||
                        ImageC.ContentType == "image/jpeg" ||
                        ImageC.ContentType == "image/gif"
                    )
                    {
                        if (ModelState.IsValid)
                        {
                            // Image
                            string currentImage = DateTime.Now.ToString("_yyyyMMdd_hhmmss3") /*to save unique named image*/ + Path.GetFileName(ImageC.FileName); // get only name of uploaded image
                            string path = Path.Combine(Server.MapPath("~/Public/img/collect/"), currentImage); // where to save uploaded image
                            ImageC.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/collect/"), oldImageC); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            homeCollection.CenterImage = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageC", "Image Center type is invalid");
                        homeCollection.CenterImage = oldImageC;
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageC", "Image Center size must be less than 2 MB");
                    homeCollection.CenterImage = oldImageC;
                }
            }
            else
            {
                homeCollection.CenterImage = oldImageC; // eyer wekil null geldise
            }

            if (ImageR != null)
            {
                if ((double)ImageR.ContentLength / (1024 * 1024) <= 2)
                {
                    if (
                        ImageR.ContentType == "image/jpg" ||
                        ImageR.ContentType == "image/png" ||
                        ImageR.ContentType == "image/jpeg" ||
                        ImageR.ContentType == "image/gif"
                    )
                    {
                        if (ModelState.IsValid)
                        {
                            // Image
                            string currentImage = DateTime.Now.ToString("_yyyyMMdd_hhmmss2") /*to save unique named image*/ + Path.GetFileName(ImageR.FileName); // get only name of uploaded image
                            string path = Path.Combine(Server.MapPath("~/Public/img/collect/"), currentImage); // where to save uploaded image
                            ImageR.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/collect/"), oldImageR); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            homeCollection.RightImage = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImageR", "Image Right type is invalid");
                        homeCollection.RightImage = oldImageR;
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageR", "Image Right size must be less than 2 MB");
                    homeCollection.RightImage = oldImageR;
                }
            }
            else
            {
                homeCollection.RightImage = oldImageR; // eyer wekil null geldise
            }



            if (ModelState.IsValid)
            {
                db.Entry(homeCollection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            homeCollection.LeftImage = oldImageL;
            homeCollection.CenterImage = oldImageC;
            homeCollection.RightImage= oldImageR;

            return View(homeCollection);
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
