using eCommerce.Models;
using eCommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        EcommerceEntities db = new EcommerceEntities();
        public ActionResult Index()
        {
            HomeIndexVM vm = new HomeIndexVM()
            {
                HomeCollection = db.HomeCollections.First(),
                AllSmartphones = db.Smartphones.ToList(),
                Brands = db.Brands.ToList(),
            };

            return View(vm);
        }
    }
}