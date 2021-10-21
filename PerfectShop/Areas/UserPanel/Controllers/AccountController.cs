using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using DataLayer;
using System.Web.Security;

namespace PerfectShop.Areas.UserPanel.Controllers
{
    public class AccountController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        // GET: UserPanel/Account
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel userModel)
        {
            if (ModelState.IsValid)
            {

                Users user = db.userRepository.GetUserByUserName(User.Identity.Name);
                string pss = FormsAuthentication.HashPasswordForStoringInConfigFile(userModel.OldPassword, "MD5");
                if (pss == user.Password)
                {
                    user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(userModel.Password, "MD5");
                    db.userRepository.Update(user);
                    db.Save();
                    return View("SuccessChangePassword");
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "Password is not true");
                }
            }
            return View();
        }

    }
}