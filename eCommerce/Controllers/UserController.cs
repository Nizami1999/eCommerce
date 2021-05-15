using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class UserController : Controller
    {
        EcommerceEntities db = new EcommerceEntities();

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("Empty", "Password and Username must be filled!");
                return View("Index");
            }

            User loggedUser = db.Users.FirstOrDefault(u => u.Username == username);

            if (loggedUser == null)
            {
                ModelState.AddModelError("Fill", "Username or password is wrong!");
                return View("Index");
            }

            if (!System.Web.Helpers.Crypto.VerifyHashedPassword(loggedUser.Password, password))
            {
                ModelState.AddModelError("Fill", "Username or password is wrong!");
                return View("Index");
            }

            Session["loggedUser"] = loggedUser;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["loggedUser"] = null;
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult Register(User user, string re_pass)
        {
            if (user.Password != re_pass)
            {
                ModelState.AddModelError("re_pass", "Password and confirm password do not match");
            }

            if (db.Users.Any(us => us.Email == user.Email))
            {
                ModelState.AddModelError("email", "Email is dublicated");
            }

            if (db.Users.Any(us => us.Username == user.Username))
            {
                ModelState.AddModelError("username", "Username is dublicated");
            }

            if (ModelState.IsValid)
            {
                user.Password = System.Web.Helpers.Crypto.HashPassword(user.Password);

                db.Users.Add(user);
                db.SaveChanges();

                ViewBag.SuccessMessage = "You have been successfully registered!";
                return View("Index");
            }

            ViewBag.ErrorMessage = "Please, check your inputs!";
            return View("Index");
        }

        public ActionResult Account()
        {
            User loggedUser = Session["loggedUser"] as User;
            if (loggedUser != null)
            {
                var userId = loggedUser.Id;

                if (db.Orders.Where(o => o.UserId == userId && o.Status == true).FirstOrDefault() != null)
                {
                    return View(db.Orders.Where(o => o.Status == true).ToList());
                }
                else
                {
                    ViewBag.NullBooks = "You do not have any smartphones";
                    return View();
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult Submit(List<int> SmartphoneId, List<int> qtybutton, List<decimal> SmartphonePrice)
        {
            User user = Session["loggedUser"] as User;

            for (int i = 0; i < SmartphoneId.Count; i++)
            {
                db.OrderSubmits.Add(new OrderSubmit()
                {
                    SmartphoneId = SmartphoneId[i],
                    UserId = user.Id,
                    Quantity = qtybutton[i],
                    TotalPrice = qtybutton[i] * SmartphonePrice[i],
                    Date = DateTime.Now
                });
            }

            db.SaveChanges();

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Orders]");
            return RedirectToAction("Account", "User");
        }
    }
}