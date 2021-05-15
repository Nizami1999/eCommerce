using eCommerce.Models;
using eCommerce.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class AboutController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();

        // GET: About
        public ActionResult Index()
        {
            HomeIndexVM vm = new HomeIndexVM()
            {
                About = db.Abouts.First(),
                Team = db.Teams.ToList()
            };

            return View(vm);
        }
    }
}