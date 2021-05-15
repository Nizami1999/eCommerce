using eCommerce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace eCommerce.Controllers
{

    public class AJAXController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();

        // GET: AJAX
        public ActionResult Details(int smartphoneId)
        {
            if (smartphoneId > 0)
            {
                return Json(db.Smartphones.Where(s => s.Id == smartphoneId)
                    .Select(s => new
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Image = s.Image,
                        NewPrice = s.NewPrice,
                        PrevPrice = s.PrevPrice,
                        Badge = s.Badge,
                        Info = s.Info,
                        Color1 = s.Color1,
                        Color2 = s.Color2,
                        Color3 = s.Color3,
                        Memory = s.Memory,
                        Camera = s.Camera,
                        BrandId = s.BrandId
                    })
                    .ToList(), JsonRequestBehavior.AllowGet);
            }
            return Content("Id is not specified");
        }

        // GET: AJAX ~ AddSmartphone
        public ActionResult AddSmartphone(int? smartphoneId)
        {
            User loggedUser = Session["loggedUser"] as User;
            if (loggedUser != null)
            {
                var userId = loggedUser.Id;

                if (db.Orders.Any(o => o.UserId == userId && o.SmartphoneId == smartphoneId && o.Status == true))
                {
                    return Json(new
                    {
                        status = 401,
                        message = "",
                        error = "This smartphones is already added!"
                    }, JsonRequestBehavior.AllowGet);
                }

                if (db.Orders.Where(o => o.UserId == userId && o.Status == true).ToList().Count == 8)
                {
                    return Json(new
                    {
                        status = 402,
                        message = "",
                        error = "You can add maximum 8 smartphones, you are limited!"
                    }, JsonRequestBehavior.AllowGet);
                }

                db.Orders.Add(new Order
                {
                    UserId = userId,
                    SmartphoneId = smartphoneId,
                    Date = DateTime.Now,
                    Status = true
                });

                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    message = "You have successfully add this smartphone",
                    error = "",
                    smartphoneName = db.Smartphones.Find(smartphoneId).Name,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = 404,
                message = "",
                error = "User is not logged",
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveBook(int? smartphoneId)
        {
            User loggedUser = Session["loggedUser"] as User;
            if (loggedUser != null)
            {
                var userId = loggedUser.Id;

                db.Orders.Where(o => o.UserId == userId && o.SmartphoneId == smartphoneId && o.Status == true).First().Status = false;
                db.SaveChanges();

                return Json(new
                {
                    status = 200,
                    message = "You have removed this smartphone successfully",
                    error = "",
                    smartphoneName = db.Smartphones.Find(smartphoneId).Name,
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = 404,
                message = "",
                error = "User is not logged",
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchSmartphone(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                return Json(db.Smartphones.Where(s => s.Name.Contains(value)).Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    NewPrice = s.NewPrice,
                    Image = s.Image,
                    Memory = s.Memory,
                    Color1 = s.Color1,
                    Brand = s.Brand.Name

                }).ToList(), JsonRequestBehavior.AllowGet);
            }
            return Content("Value is null");
        }

    }
}