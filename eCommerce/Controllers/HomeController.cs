using eCommerce.Models;
using eCommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();
        public ActionResult Index()
        {
            HomeIndexVM vm = new HomeIndexVM()
            {
                HomeSlider = db.HomeSliders.ToList(),
                HomeCollection = db.HomeCollections.First(),
                HomeComingProducts = db.HomeComingProducts.ToList(),
                AllSmartphones = db.Smartphones.ToList(),
                AllNewSmartphones = db.Smartphones.Where(s => s.Badge == "new").ToList(),
                Brands = db.Brands.ToList(),
                Blog = db.Blogs.ToList()
            };

            return View(vm);
        }

        public ActionResult Details(int? id)
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
    }
}