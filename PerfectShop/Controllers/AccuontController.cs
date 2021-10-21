using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewModels;
using DataLayer;

namespace PerfectShop.Controllers
{
    public class AccuontController : Controller
    {
        UnitOfWork db = new UnitOfWork();
        

        // GET: Accuont
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                if (db.userRepository.AnyByEmail(userViewModel.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered");
                }
                else
                {
                    Users user = new Users()
                    {
                        Email = userViewModel.Email,
                        UserName = userViewModel.UserName,
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(userViewModel.Password, "MD5"),
                        AciveCode = Guid.NewGuid().ToString(),
                        IsActive = false,
                        RegisterDate = DateTime.Now,
                        RoleID = 1
                    };

                    db.userRepository.Insert(user);
                    db.Save();

                    string body = PartialToStringClass.RenderPartialView("ManageEmail", "ActivationEmail", user);
                    SendEmail.Send(userViewModel.Email, "Activation Email", body);

                    return View("SuccessRegister", user);
                }
            }
            return View();
        }
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");
                Users user = db.userRepository.GetUserByEmailAndPass(login.Email, password);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "User is not Active");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "No user found");
                }
            }
            return View();
        }

        public ActionResult ActiveUser(string id)
        {
            Users user = db.userRepository.GetUserByActiveCode(id);
            if (user != null)
            {
                user.AciveCode = Guid.NewGuid().ToString();
                user.IsActive = true;
                db.userRepository.Update(user);
                db.Save();
            }
            else
            {
                return HttpNotFound();
            }
            ViewBag.userName = user.UserName;
            return View();
        }
        [Route("LogOff")]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                if (db.userRepository.AnyByEmail(userModel.Email))
                {
                    Users user = db.userRepository.GetByEmail(userModel.Email);
                    if (user.IsActive)
                    {
                        string body = PartialToStringClass.RenderPartialView("ManageEmail","RecoveryPass", user);
                        SendEmail.Send(userModel.Email, "Recovery Pass", body);
                        
                        return View("SuccessForgotPass");
                    }
                    else
                    {
                        ModelState.AddModelError("Email","User is not Active");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email","User Not Found");
                }
            }
            return View();
        }

        public ActionResult RecoveryPassword(int id)
        {
            RecoveryPasswordViewModel user = new RecoveryPasswordViewModel();
            user.userID = id;
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecoveryPassword(RecoveryPasswordViewModel recoveryPass)
        {
            if (ModelState.IsValid)
            {
                Users user = db.userRepository.GetByID(recoveryPass.userID);
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(recoveryPass.Password, "MD5");
                db.userRepository.Update(user);
                db.Save();
            }
            return View("SuccessRecoveryPass");
        }







    }
}