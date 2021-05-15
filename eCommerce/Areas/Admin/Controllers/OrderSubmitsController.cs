using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCommerce.Models;

namespace eCommerce.Areas.Admin.Controllers
{
    public class OrderSubmitsController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/OrderSubmits
        public ActionResult Index()
        {
            var orderSubmits = db.OrderSubmits.Include(o => o.Smartphone).Include(o => o.User);
            return View(orderSubmits.ToList());
        }

        // GET: Admin/OrderSubmits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSubmit orderSubmit = db.OrderSubmits.Find(id);
            if (orderSubmit == null)
            {
                return HttpNotFound();
            }
            return View(orderSubmit);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderSubmit orderSubmit = db.OrderSubmits.Find(id);
            if (orderSubmit == null)
            {
                return HttpNotFound();
            }
            return View(orderSubmit);
        }

        // POST: Admin/OrderSubmits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderSubmit orderSubmit = db.OrderSubmits.Find(id);
            db.OrderSubmits.Remove(orderSubmit);
            db.SaveChanges();
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
