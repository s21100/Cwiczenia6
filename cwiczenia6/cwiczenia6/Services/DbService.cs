using cwiczenia6.Models;
using cwiczenia6.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace cwiczenia6.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _dbContext;

        public DbService(MainDbContext mainDbContext) {
            _dbContext = mainDbContext;
        }

        public async Task<Doctor> AddDoctor(SomeSortOfDoctor doctor)
        {
            Doctor doctorToAdd = new Doctor
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            };
            _dbContext.Add(doctorToAdd);
            await _dbContext.SaveChangesAsync();

            return doctorToAdd;
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _dbContext.Doctors.Select(e => e).ToListAsync();
        }

        public async Task<SomeSortOfPrescription> GetPrescription(int id)
        {
            var selectedPrescription = await _dbContext.Prescriptions.Select(e => e).Where(e => e.IdPrescription == id).FirstOrDefaultAsync();

            if (selectedPrescription == null) {
                return null;
            }

            return await _dbContext.Prescriptions
                .Select(e => new SomeSortOfPrescription
                {
                    Date = e.Date,
                    DueDate = e.DueDate,
                    Doctor = new SomeSortOfDoctor { FirstName = e.Doctor.FirstName, LastName = e.Doctor.LastName, Email = e.Doctor.Email},
                    Patient = new SomeSortOfPatient { FirstName = e.Patient.FirstName, LastName = e.Patient.LastName, BirthDate = e.Patient.BirthDate},
                    Medicaments = e.Prescription_Medicaments.Select(e => new SomeSortOfMedicament { Name = e.Medicament.Name, Description = e.Medicament.Description, Type = e.Medicament.Type})
                }).FirstOrDefaultAsync();
        }

        public async Task<Doctor> ModifyDoctor(int id, SomeSortOfDoctor doctor)
        {
            var doctorToEdit = await _dbContext.Doctors.Where(e => e.IdDoctor == id).FirstOrDefaultAsync();

            if (doctorToEdit == null) {
                return null;
            }

            if (doctor.FirstName != null)
                doctorToEdit.FirstName = doctor.FirstName;
            if (doctor.LastName != null)
                doctorToEdit.LastName = doctor.LastName;
            if (doctor.Email != null)
                doctorToEdit.Email = doctor.Email;

            await _dbContext.SaveChangesAsync();

            return doctorToEdit;
        
        }

        public async Task<bool> RemoveDoctor(int id)
        {
            var doctor = await _dbContext.Doctors.Select(e => e).Where(e => e.IdDoctor == id).FirstOrDefaultAsync();

            if (doctor == null) {
                return false;
            }

            _dbContext.Remove(doctor);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
