using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IspitniRokovi.Models;
using System.Data.Entity.Infrastructure;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace IspitniRokovi.Controllers
{
    public class KorisniciController : Controller
    {
        MySqlDB baza = new MySqlDB();

        public ActionResult Test()
        {
            var korisnici = baza.korisnici.ToList();
            return View(korisnici);
        }

        public ActionResult Index()
        {
            var korisnici = baza.korisnici.ToList();
            return View(korisnici);
        }

        public ActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Prijava([Bind(Include = "username, password")] LoginViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manager = new UserManager();
                    var users = manager.SearchUser(user.username, user.password);
                    if (users.Count > 0)
                    {
                        var userData = manager.SearchUser(user.username, user.password).Single();
                        var ident = new ClaimsIdentity(
                            new[] {
                        new Claim(ClaimTypes.NameIdentifier, userData.username),
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                        new Claim(ClaimTypes.Name, userData.username),
                        new Claim(ClaimTypes.PrimarySid, userData.id.ToString()),
                        new Claim(ClaimTypes.Email, userData.email),
                        new Claim(ClaimTypes.Role, (userData.uloga==0)? "Korisnik":"Admin")
                            },
                            DefaultAuthenticationTypes.ApplicationCookie
                        );

                        HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
                    }
                    else 
                    {
                        /*return Content("<script language='javascript' type='text/javascript'>alert('Netocni podaci za prijavu. Pokusajte ponovno.');</script>" +
                            "<script language='javascript' type='text/javascript'>alert('Netocni podaci za prijavu. Pokusajte ponovno.');</script>"); */


                        ModelState.AddModelError("", "Netocni podaci za prijavu. Pokusajte ponovo.");
                        return View();



                    }
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Pogreška kod prijave, pokušajte ponovno.");
            }

            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Odjava()
        {
            if (Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registracija(RegisterViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manager = new UserManager();
                    var users = manager.ExistingUser(user.username, user.email);
                    if (users)
                    {
                        ViewBag.Error = "Korisnik već postoji s tim korisničkim imenom ili email-om";
                        return View();
                    }
                    else
                    {
                        if (user.password.Equals(user.confirmPassword))
                        {
                            manager.AddUser(user.username, user.password, user.email, user.name, user.surname);
                            RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.Error = "Lozinke se ne podudaraju!";
                            return View();
                        }
                    }
                }
            }
            catch(RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Pogreška kod prijave, pokušajte ponovno");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}