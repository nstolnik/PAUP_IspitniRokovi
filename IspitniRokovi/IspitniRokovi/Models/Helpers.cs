using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Web.Routing;

namespace IspitniRokovi.Models
{
    class UserManager
    {
        MySqlDB baza = new MySqlDB();

        public List<Korisnici> SearchUser(string username, string password)
        {
            return baza.korisnici.Where(u => u.username == username && u.password == password).ToList();
        }
        public bool ExistingUser(string username, string email)
        {
            if (baza.korisnici.Where(u => u.username == username).SingleOrDefault() != null)
                return true;
            else if (baza.korisnici.Where(u => u.email == email).SingleOrDefault() != null)
                return true;
            else
                return false;
        }
        public void AddUser(string username, string password, string email, string name, string surname)
        {
            var max = baza.korisnici.Count();
            if (max == 0)
                max = 0;
            else
                max = baza.korisnici.Max(u => u.id);

            int id = max+1;
            Korisnici korisnik = new Korisnici
            {
                id = id,
                username = username,
                password = password,
                email = email,
                ime = name,
                prezime = surname
            };
            baza.korisnici.Add(korisnik);
            baza.SaveChanges();
        }
    }
    public static class IdentityInfo{

        public static string GetId(this IIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue(ClaimTypes.PrimarySid);
            }
            return null;
        }

        public static string GetName(this IIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue(ClaimTypes.Name);
            }
            return null;
        }

        public static string GetEmail(this IIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                return ci.FindFirstValue(ClaimTypes.Email);
            }
            return null;
        }
    }
}