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
                return BadRequest(new { message = "Doctor not found" });

            _context.Patients.Add(patient);
            _context.SaveChanges();

            return Ok(new { success = true, patient });
        }

        [HttpGet("{doctorId}/get-patients")]
        public IActionResult GetPatientsByDoctor(int doctorId)
        {
            var doctorExists = _context.Doctors.Any(d => d.Id == doctorId);

            if (!doctorExists)
                return NotFound(new { message = "Doctor not found" });

            var patients = _context
                .Patients.Where(p => p.DoctorId == doctorId)
                .Select(p => new
                {
                    p.Id,
                    p.DoctorId,

                    // Personal Info
                    p.FirstName,
                    p.LastName,
                    p.Cnp,
                    p.BirthDate,
                    p.Age,
                    p.Gender,
                    p.Occupation,

                    // Contact
                    p.Email,
                    p.Phone,

                    // Address
                    p.County,
                    p.City,
                    p.Street,
                    p.Number,
                    p.Block,
                    p.Apartment,
                    p.Staircase,
                    p.Floor,
                    p.PostalCode,

                    // Medical Info
                    p.Weight,
                    p.Height,
                    p.BloodType,
                    p.Rh,

                    // Insurance
                    p.InsuranceCompany,
                    p.InsuranceId,

                    // Medical History
                    p.ChronicDiseases,
                    p.Vaccinations,
                    p.HereditaryDiseases,
                    p.OtherDiseases,

                    // Lifestyle
                    p.Diet,
                    p.PhysicalActivity,
                    p.Smoker,
                    p.AlcoholConsumer,
                    p.DrugConsumer,

                    // Image
                    p.ProfileImage,
                })
                .ToList();

            return Ok(patients);
        }
    }
}
