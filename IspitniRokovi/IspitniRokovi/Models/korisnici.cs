namespace IspitniRokovi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ir_db.korisnici")]
    public partial class Korisnici
    {
        public static int KORISNIK_PROFESOR = 0;
        public static int KORISNIK_ADMIN = 1;
        public static string[] rolesList = {
            "Profesor",
            "Administrator"
        };

        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string username { get; set; }

        [Required]
        [StringLength(40)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [Required]
        [StringLength(20)]
        public string ime { get; set; }

        [Required]
        [StringLength(20)]
        public string prezime { get; set; }

        public sbyte uloga { get; set; }
    }
}
