using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IspitniRokovi.Models
{
    [Table("ir_db.kolegiji")]
    public partial class Kolegiji
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Naziv kolegija")]
        public string ime { get; set; }

        [Required]
        [Display(Name = "Profesor")]
        public int korisnik_id { get; set; }

        [Required]
        [Display(Name = "Studij")]
        public int studij_id { get; set; }
    }
}