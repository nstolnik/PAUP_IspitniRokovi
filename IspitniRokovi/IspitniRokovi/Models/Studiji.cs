using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IspitniRokovi.Models
{
    [Table("ir_db.studiji")]
    public partial class Studiji
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Naziv studija")]
        public string ime { get; set; }
    }
}