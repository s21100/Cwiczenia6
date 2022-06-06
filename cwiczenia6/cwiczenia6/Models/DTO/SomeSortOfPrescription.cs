using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia6.Models.DTO
{
    public class SomeSortOfPrescription
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public SomeSortOfDoctor Doctor { get; set; }
        public SomeSortOfPatient Patient { get; set; }
        public IEnumerable<SomeSortOfMedicament> Medicaments { get; set; }
    }
}
