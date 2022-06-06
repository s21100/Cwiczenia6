using cwiczenia6.Models;
using cwiczenia6.Models.DTO;
using cwiczenia6.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {

        private readonly IDbService _dbService;

        public MedicineController(IDbService dbService) {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("/api/doctors")]
        public async Task<IActionResult> GetDoctors() {
            
            var doctors = await _dbService.GetDoctors();
            return Ok(doctors);
        }

        [HttpDelete]
        [Route("/api/doctors/{idDoctor:int}")]
        public async Task<IActionResult> RemoveDoctor(int idDoctor) {
            var deleted = await _dbService.RemoveDoctor(idDoctor);

            if (!deleted) {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        [Route("/api/doctors")]
        public async Task<IActionResult> AddDoctor(SomeSortOfDoctor doctor) {

            var added = await _dbService.AddDoctor(doctor);
            return Ok(added);
        }

        [HttpPatch]
        [Route("/api/doctors/{idDoctor:int}")]
        public async Task<IActionResult> ModifyDoctor([FromRoute] int idDoctor, [FromBody] SomeSortOfDoctor doctor) {

            var modified = await _dbService.ModifyDoctor(idDoctor, doctor);

            if (modified == null)
            {
                return BadRequest(idDoctor);
            }

            return Ok(modified);
        }

        [HttpGet]
        [Route("/api/prescriptions/{idPrescription:int}")]
        public async Task<IActionResult> GetPrescription(int idPrescription) {
            var prescription = await _dbService.GetPrescription(idPrescription);

            if (prescription == null) {
                return BadRequest(idPrescription);
            }

            return Ok(prescription);
        }
    }
}
