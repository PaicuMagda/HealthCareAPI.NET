using HealthcareAPI.Data;
using HealthcareAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add-patient")]
        public IActionResult AddPatient(Patient patient)
        {
            var doctor = _context.Doctors.Find(patient.DoctorId);

            if (doctor == null)
                return BadRequest("Doctorul nu există");

            var newPatient = new Patient
            {
                DoctorId = patient.DoctorId,
                Nume = patient.Nume,
                Prenume = patient.Prenume,
                Cnp = patient.Cnp,
                Email = patient.Email,
                Telefon = patient.Telefon,
                Judet = patient.Judet,
                Oras = patient.Oras,
                GrupaSanguina = patient.GrupaSanguina,
                Rh = patient.Rh,
                BoliCronice = patient.BoliCronice,
            };

            _context.Patients.Add(newPatient);
            _context.SaveChanges();

            return Ok(new { success = true, patient = newPatient });
        }

        [HttpGet("{doctorId}/get-patients")]
        public IActionResult GetPatientsByDoctor(int doctorId)
        {
            var doctorExists = _context.Doctors.Any(d => d.Id == doctorId);

            if (!doctorExists)
                return NotFound(new { message = "Doctorul nu există" });

            var patients = _context
                .Patients.Where(p => p.DoctorId == doctorId)
                .Select(p => new
                {
                    p.Id,
                    p.DoctorId,
                    p.Nume,
                    p.Prenume,
                    p.Cnp,
                    p.Email,
                    p.Telefon,
                    p.Judet,
                    p.Oras,
                    p.GrupaSanguina,
                    p.Rh,
                    p.BoliCronice,
                })
                .ToList();

            return Ok(patients);
        }
    }
}
