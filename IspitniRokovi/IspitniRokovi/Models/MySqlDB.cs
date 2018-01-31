namespace IspitniRokovi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MySqlDB : DbContext
    {
        public virtual DbSet<Korisnici> korisnici { get; set; }
        public virtual DbSet<Studiji> studiji { get; set; }
        public virtual DbSet<Kolegiji> kolegiji { get; set; }
        public virtual DbSet<Ispiti> ispiti { get; set; }

        public bool examExists(int _profesor,DateTime _datum)
        {
            var ispitiList = (
                from i in ispiti
                join k in kolegiji on i.kolegij_id equals k.id
                where i.datum.Equals(_datum) && k.korisnik_id == _profesor
                select new { ispit = i, kolegij = k }
            ).ToList();

            if(ispitiList.Count > 0)
            {
                return true;
            }
            return false;
        }

        public void deleteCourse(int _id)
        {
            Kolegiji kolegij = kolegiji.Find(_id);

            if (kolegij != null)
            {
                var ispitiList = (from i in ispiti
                                  where i.kolegij_id == _id
                                  select i).ToList();

                if (ispitiList != null)
                {
                    foreach (Ispiti ispit in ispitiList)
                    {
                        ispiti.Remove(ispit);
                    }
                }

                kolegiji.Remove(kolegij);
            }
        }

        public void deleteStudy(int _id)
        {
            Studiji studij = studiji.Find(_id);

            if (studij != null)
            {
                var kolegijiList = (from i in kolegiji
                                    where i.studij_id == _id
                                    select i).ToList();

                if (kolegijiList != null)
                {
                    foreach (Kolegiji kolegij in kolegijiList)
                    {
                        deleteCourse(kolegij.id);
                    }
                }

                studiji.Remove(studij);
            }
        }

        public void deleteUser(int _id)
        {
            Korisnici korisnik = korisnici.Find(_id);

            if (korisnik != null)
            {
                var kolegijiList = (from i in kolegiji
                                    where i.korisnik_id == _id
                                    select i).ToList();

                if (kolegijiList != null)
                {
                    foreach (Kolegiji kolegij in kolegijiList)
                    {
                        deleteCourse(kolegij.id);
                    }
                }

                korisnici.Remove(korisnik);
            }
        }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<korisnici>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<korisnici>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<korisnici>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<korisnici>()
                .Property(e => e.ime)
                .IsUnicode(false);

            modelBuilder.Entity<korisnici>()
                .Property(e => e.prezime)
                .IsUnicode(false);
        }*/
    }
}
