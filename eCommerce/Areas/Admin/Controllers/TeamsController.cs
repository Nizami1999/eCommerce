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
    public class TeamsController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/Teams
        public ActionResult Index()
        {
            return View(db.Teams.ToList());
        }

        // GET: Admin/Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Exclude = "Image")] Team team, HttpPostedFileBase Image)
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
                        string path = Path.Combine(Server.MapPath("~/Public/img/team/"), currentImage); // where to save uploaded image
                        Image.SaveAs(path); // save in this path

                        team.Image = currentImage;
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
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Admin/Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Admin/Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Exclude = "Image")] Team team, HttpPostedFileBase Image, string oldImage)
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
                            string path = Path.Combine(Server.MapPath("~/Public/img/team/"), currentImage); // where to save uploaded image
                            Image.SaveAs(path); // save in this path

                            string activeImage = Path.Combine(Server.MapPath("~/Public/img/team/"), oldImage); // find previous image from server
                            System.IO.File.Delete(activeImage); // delete previous image

                            team.Image = currentImage;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Image type is invalid");
                        team.Image = oldImage;
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Image size must be less than 2 MB");
                    team.Image = oldImage;
                }
            }
            else
            {
                team.Image = oldImage; // eyer wekil null geldise
            }

            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            team.Image = oldImage;
            return View(team);
        }

        // GET: Admin/Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Admin/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();

            // Image
            string path = Path.Combine(Server.MapPath("~/Public/img/team/"), team.Image);

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
