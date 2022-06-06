using cwiczenia6.Models;
using cwiczenia6.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia6.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<bool> RemoveDoctor(int id);
        Task<Doctor> AddDoctor(SomeSortOfDoctor doctor);
        Task<Doctor> ModifyDoctor(int id, SomeSortOfDoctor doctor);
        Task<SomeSortOfPrescription> GetPrescription(int id);
    }
}
