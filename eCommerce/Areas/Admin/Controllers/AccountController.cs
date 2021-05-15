using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace eCommerce.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private EcommerceEntities db = new EcommerceEntities();

        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult LoginPOST(string username, string password)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                AdminAccount account = db.AdminAccounts.Find(1);

                if (account.Username != username || !Crypto.VerifyHashedPassword(account.Password, password))
                {
                    ModelState.AddModelError("LoginError", "Username or password is wrong");
                    return View();
                }

                // All is correct
                Session["AdminLogged"] = true;
                Session["AdminAccount"] = account;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("LoginError", "Username and password should be filled");
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session["AdminLogged"] = false;
            return RedirectToAction("Login", "Account");
        }

    }
}