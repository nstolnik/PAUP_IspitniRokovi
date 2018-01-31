using IspitniRokovi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IspitniRokovi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        MySqlDB baza = new MySqlDB();

        // Korisnici
        public ActionResult PopisKorisnika()
        {
            var korisnici = baza.korisnici.ToList();
            return View(korisnici);
        }
        public ActionResult DodajKorisnika()
        {
            return View();
        }

        // Studiji
        public ActionResult PopisStudija()
        {
            var studiji = baza.studiji.ToList();
            return View(studiji);
        }
        public ActionResult DodajStudij()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajStudij([Bind(Include = "ime")] Studiji s)
        {
            
            if (ModelState.IsValid)
            {
                
                baza.studiji.Add(s);
                baza.SaveChanges();
                return RedirectToAction("PopisStudija");
            }
            return View();
        }
        public ActionResult UrediStudij(int? id)
        {
            //osnovna zaštita
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            //preko lamda izraza

            var studij = baza.studiji.ToList().Find(x => x.id == id);
            if (studij != null)
            {
                return View(studij);
            }
            return RedirectToAction("PopisStudija");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrediStudij([Bind(Include = "id, ime")] Studiji s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Studiji studij = baza.studiji.SingleOrDefault(x => x.id == s.id);

                    if (studij == null)
                        return new HttpNotFoundResult();

                    studij.ime = s.ime;
                    baza.SaveChanges();
                    return RedirectToAction("PopisStudija");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Greška kod spremanja, pokušajte ponovno!");
            }
            return RedirectToAction("PopisStudija");
        }

        // Kolegiji
        public ActionResult PopisKolegija()
        {
            var kolegiji = (
                from k in baza.kolegiji
                join p in baza.korisnici on k.korisnik_id equals p.id
                join s in baza.studiji on k.studij_id equals s.id
                select new PopisKolegijaViewModel { kolegij = k, profesor = p, studij = s }
            ).ToList();

            return View(kolegiji);
        }
        public ActionResult DodajKolegij()
        {
            var profesori = baza.korisnici.Where(x => x.uloga == Korisnici.KORISNIK_PROFESOR).Select(k => new {
                id = k.id,
                name = k.ime + " " + k.prezime
            }).ToList();

            var studiji = baza.studiji.Select(s => new {
                id = s.id,
                name = s.ime
            }).ToList();

            ViewBag.Profesori = new SelectList(profesori, "id", "name");
            ViewBag.Studiji = new SelectList(studiji, "id", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajKolegij([Bind(Include = "ime, korisnik_id, studij_id")] Kolegiji k)
        {
            if (ModelState.IsValid)
            {
                baza.kolegiji.Add(k);
                baza.SaveChanges();
                return RedirectToAction("PopisKolegija");
            }
            return View();
        }
        public ActionResult UrediKolegij(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }
            var kolegiji = baza.kolegiji.ToList().Find(x => x.id == id);
            if (kolegiji != null)
            {
                var profesori = baza.korisnici.Where(x => x.uloga == Korisnici.KORISNIK_PROFESOR).Select(k => new {
                    id = k.id,
                    name = k.ime + " " + k.prezime
                }).ToList();

                var studiji = baza.studiji.Select(s => new {
                    id = s.id,
                    name = s.ime
                }).ToList();

                ViewBag.Profesori = new SelectList(profesori, "id", "name");
                ViewBag.Studiji = new SelectList(studiji, "id", "name");
                return View(kolegiji);
            }
            return RedirectToAction("PopisKolegija");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrediKolegij([Bind(Include = "id, ime, korisnik_id, studij_id")] Kolegiji k)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Kolegiji kolegij = baza.kolegiji.SingleOrDefault(x => x.id == k.id);

                    if (kolegij == null)
                        return new HttpNotFoundResult();

                    kolegij.ime = k.ime;
                    kolegij.korisnik_id = k.korisnik_id;
                    kolegij.studij_id = k.studij_id;
                    baza.SaveChanges();
                    return RedirectToAction("PopisKolegija");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Greška kod spremanja, pokušajte ponovno!");
            }
            return RedirectToAction("PopisKolegija");
        }

        // Ispiti
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
        public ActionResult DodajIspit()
        {
            var kolegiji = baza.kolegiji.Select(k => new {
                id = k.id,
                name = k.ime
            }).ToList();

            ViewBag.Kolegiji = new SelectList(kolegiji, "id", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajIspit([Bind(Include = "kolegij_id, datum, ak_godina, tip")] Ispiti i)
        {
            if (ModelState.IsValid)
            {
                Kolegiji kolegij = baza.kolegiji.SingleOrDefault(x => x.id == i.kolegij_id);

                if (!baza.examExists(kolegij.korisnik_id, i.datum))
                {
                    baza.ispiti.Add(i);
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
                    ViewBag.Error = "Odabrani profesor već ima ispit za odabrano vrijeme!";
                    return View(i);
                }
            }
            return View();
        }
        public ActionResult UrediIspit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }
            var ispiti = baza.ispiti.ToList().Find(x => x.id == id);
            if (ispiti != null)
            {
                var kolegiji = baza.kolegiji.Select(k => new {
                    id = k.id,
                    name = k.ime
                }).ToList();

                ViewBag.Kolegiji = new SelectList(kolegiji, "id", "name");
                return View(ispiti);
            }
            return RedirectToAction("PopisIspita");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrediIspit([Bind(Include = "id, kolegij_id, datum, ak_godina, tip")] Ispiti i)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Ispiti ispit = baza.ispiti.SingleOrDefault(x => x.id == i.id);

                    if (ispit == null)
                        return new HttpNotFoundResult();

                    //Kolegiji kolegij = baza.kolegiji.SingleOrDefault(x => x.id == i.kolegij_id);

                    //if(!baza.examExists(kolegij.korisnik_id, i.datum))
                    //{
                        ispit.kolegij_id = i.kolegij_id;
                        ispit.datum = i.datum;
                        ispit.ak_godina = i.ak_godina;
                        ispit.tip = i.tip;
                        baza.SaveChanges();
                        return RedirectToAction("PopisIspita");
                    /*} else {
                        var kolegiji = baza.kolegiji.Select(k => new {
                            id = k.id,
                            name = k.ime
                        }).ToList();

                        ViewBag.Kolegiji = new SelectList(kolegiji, "id", "name");
                        ViewBag.Error = "Odabrani profesor već ima ispit za odabrano vrijeme!";
                        return View(ispit);
                    }*/
                    
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Greška kod spremanja, pokušajte ponovno!");
            }
            return RedirectToAction("PopisIspita");
        }

        public ActionResult ObrisiStudij(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            baza.deleteStudy((int)Id);
            baza.SaveChanges();

            return RedirectToAction("PopisStudija");
        }

        public ActionResult ObrisiKolegij(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            baza.deleteCourse((int)Id);
            baza.SaveChanges();

            return RedirectToAction("PopisKolegija");
        }

        public ActionResult ObrisiIspit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            Ispiti ispit = baza.ispiti.Find(Id);

            if (ispit != null)
            {
                baza.ispiti.Remove(ispit);
                baza.SaveChanges();
            }

            return RedirectToAction("PopisIspita");
        }

        public ActionResult ObrisiKorisnika(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }

            Korisnici korisnik = baza.korisnici.Find(Id);

            if (korisnik != null)
            {
                baza.deleteUser((int)Id);
                baza.SaveChanges();
            }

            return RedirectToAction("PopisKorisnika");
        }

        public ActionResult UrediKorisnika(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }
            Korisnici korisnik = baza.korisnici.Find(Id);

            if (korisnik != null)
            {
                var edit = new UrediKorisnikaViewModel {
                    id = (int)Id,
                    username = korisnik.username,
                    email = korisnik.email,
                    name = korisnik.ime,
                    surname=korisnik.prezime,
                    role=korisnik.uloga
                };
                return View(edit);
            }
            return RedirectToAction("PopisKorisnika");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UrediKorisnika([Bind(Include = "id, username, email, name, surname, role")] UrediKorisnikaViewModel k)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Korisnici korisnik = baza.korisnici.Find(k.id);

                    if (korisnik == null)
                        return new HttpNotFoundResult();

                    korisnik.username = k.username;
                    korisnik.email = k.email;
                    korisnik.ime = k.name;
                    korisnik.prezime = k.surname;
                    korisnik.uloga = k.role;

                    baza.SaveChanges();
                    return RedirectToAction("PopisKorisnika");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Greška kod spremanja, pokušajte ponovno!");
            }
            return RedirectToAction("PopisKorisnika");
        }
    }
}