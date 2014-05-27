using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DocumentFormat.OpenXml.Spreadsheet;
using Portal.Areas.Authentification.Models;
using Portal.Areas.Authentification.Repositories;
using Portal.Controllers;
using Portal.Helpers;

namespace Portal.Areas.Authentification.Controllers
{
    public class AuthController : BaseController
    {
        public readonly AuthentificationRepository _authentificationRepository;

        public AuthController()
        {
            _authentificationRepository = new AuthentificationRepository(db);
        }


        //
        // GET: /Authentification/Authentification/
        public ActionResult Index(bool isWorking)
        {
            var model = new AuthModels()
            {
                Login = new LoginModel(),
                Register = new RegisterModel(),
                IsWorking = isWorking
            };
     

            return View(model);
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthModels model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {

                if (!_authentificationRepository.Login(model))
                {
                    ModelState.AddModelError("","Username sau parola incorecte");
                }
                else
                {
                    var user = new UserData(db, model.Login.UserName);
                    HttpContext.Session["UserData"] = user;
                    FormsAuthentication.SetAuthCookie(model.Login.UserName, true);

                    if (Url.IsLocalUrl(ReturnUrl) && ReturnUrl.Length > 1 && ReturnUrl.StartsWith("/")
                        && !ReturnUrl.StartsWith("//") && !ReturnUrl.StartsWith("/\\"))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dash", new {area = "Profile"});
                    }
                }

                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public ActionResult Register(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Register(AuthModels model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {

                if (!_authentificationRepository.Register(model).HasValue)
                {
                    ModelState.AddModelError("", "Username sau parola incorecte");
                }
                else
                {
                    return RedirectToAction("Index");
                }


            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

	}
}