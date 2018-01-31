using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IspitniRokovi.Models
{
    [Table("ir_db.ispiti")]
    public partial class Ispiti
    {
        public static int ISPIT_REDOVNI = 0;
        public static int ISPIT_IZVANREDNI = 1;
        public static string[] typesList = {
            "Redovni",
            "Izvanredni"
        };

        public int id { get; set; }

        [Required]
        [Display(Name = "Kolegij")]
        public int kolegij_id { get; set; }

        [Required]
        [Display(Name = "Vrijeme održavanja")]
        public DateTime datum { get; set; }

        [Required]
        [Display(Name = "Akademska godina")]
        public string ak_godina { get; set; }

        [Required]
        [Display(Name = "Tip")]
        public int tip { get; set; }
    }
}