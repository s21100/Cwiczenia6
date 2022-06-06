using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia6.Models
{
    public class MainDbContext : DbContext
    {

        protected MainDbContext()
        {
        }
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

        /*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
                    optionsBuilder.UseSqlServer("Server=localhost,1433; Database=Master; User Id=SA;Password =< YourStrong@Passw0rd >");
                }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasData(
                        new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", BirthDate = DateTime.Parse("2000-01-01") },
                        new Patient { IdPatient = 2, FirstName = "Adam", LastName = "Nowak", BirthDate = DateTime.Parse("2001-01-01") }
                    );
            });

            modelBuilder.Entity<Doctor>(p =>
            {
                p.HasData(
                        new Doctor { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan@kowalski.com" },
                        new Doctor { IdDoctor = 2, FirstName = "Adam", LastName = "Nowak", Email = "adam@nowak.com" }
                    );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasData(
                        new Prescription { IdPrescription = 1, Date = DateTime.Parse("2022-01-01"), DueDate = DateTime.Parse("2022-03-01"), IdDoctor = 1, IdPatient = 2 },
                        new Prescription { IdPrescription = 2, Date = DateTime.Parse("2022-01-01"), DueDate = DateTime.Parse("2023-03-01"), IdDoctor = 2, IdPatient = 1 }
                    );
            });

            modelBuilder.Entity<Medicament>(p =>
            {
                p.HasData(
                        new Medicament { IdMedicament = 1, Name = "Test_Medicament", Description = "Test_Description", Type = "Test_Type" },
                        new Medicament { IdMedicament = 2, Name = "Test_Medicament2", Description = "Test_Description2", Type = "Test_Type2" }
                    );
            });

            modelBuilder.Entity<Prescription_Medicament>(p =>
            {
                p.HasKey(e => new { e.IdMedicament, e.IdPrescription });
                p.HasData(
                        new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "Test_Details"},
                        new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Dose = null, Details = "Test_Details2"}
                    );
            });
        }
    }
}
