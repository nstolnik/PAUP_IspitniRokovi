using IspitniRokovi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IspitniRokovi.Controllers
{
    [Authorize(Roles = "Korisnik, Admin")]
    public class IspitiController : Controller
    {
        MySqlDB baza = new MySqlDB();
        

        
        public ActionResult PrijavaIspita()
        {
            int id = int.Parse(User.Identity.GetId());
            var kolegiji = baza.kolegiji.Where(i=>i.korisnik_id==id).Select(k => new {
                id = k.id,
                name = k.ime
            }).ToList();

            ViewBag.Kolegiji = new SelectList(kolegiji, "id", "name");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PrijavaIspita([Bind(Include = "kolegij_id, datum, ak_godina, tip")] Ispiti ispit)
        {

            try { 
                if (ModelState.IsValid)
                {
                    ViewBag.AlertMessage = "Popis kolegija je prazan!";
                    Kolegiji kolegij = baza.kolegiji.SingleOrDefault(x => x.id == ispit.kolegij_id);

                
                    if (!baza.examExists(kolegij.korisnik_id, ispit.datum))
                    {
                        baza.ispiti.Add(ispit);
                        baza.SaveChanges();
                        return RedirectToAction("PopisIspita");
                    }
                    else
                    {
                        var kolegiji = baza.kolegiji.Select(k => new {
                            id = k.id,
                            name = k.ime
                        }).ToList();

                        ViewBag.Kolegiji = new SelectList(kolegiji, "id", "name");
                        ViewBag.Error = "Već postoji prijavljeni ispit za to vrijeme!";
                        return View(ispit);
                    }
                }
            }
            catch 
            {
                return View();
            }

            return View();
        }

        public ActionResult PopisIspita()
        {
            var ispiti = (
                from i in baza.ispiti
                join k in baza.kolegiji on i.kolegij_id equals k.id
                join s in baza.studiji on k.studij_id equals s.id
                join p in baza.korisnici on k.korisnik_id equals p.id
                select new PopisIspitaViewModel { ispit = i, kolegij = k, profesor = p, studij = s }
            ).ToList();

            return View(ispiti);
        }
    }
}