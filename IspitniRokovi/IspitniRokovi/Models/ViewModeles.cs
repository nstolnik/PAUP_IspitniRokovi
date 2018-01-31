using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IspitniRokovi.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "Korisničko ime")]
        public string username { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Lozinka")]
        public string password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "Korisničko ime")]
        public string username { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Lozinka")]
        public string password { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Ponovi lozinku")]
        public string confirmPassword { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Ime")]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Prezime")]
        public string surname { get; set; }
    }

    public class UrediKorisnikaViewModel
    {
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Korisničko ime")]
        public string username { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Ime")]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Prezime")]
        public string surname { get; set; }

        [Required]
        [Display(Name = "Uloga")]
        public sbyte role { get; set; }
    }

    public class PopisKolegijaViewModel
    {
        public Kolegiji kolegij;
        public Korisnici profesor;
        public Studiji studij;
    }

    public class PopisIspitaViewModel
    {
        public Ispiti ispit;
        public Kolegiji kolegij;
        public Korisnici profesor;
        public Studiji studij;
    }

    public class IspitiViewModel
    {
        public Kolegiji kolegij;
        public Ispiti ispit;
        public Studiji studij;
    }
}