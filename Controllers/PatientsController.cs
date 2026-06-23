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

            patient.DeletedAt = null;
            patient.DeletedBy = null;

            _context.Patients.Add(patient);
            _context.SaveChanges();

            return Ok(
                new
                {
                    success = true,
                    patient = new
                    {
                        patient.Id,
                        patient.DoctorId,
                        patient.FirstName,
                        patient.LastName,
                        patient.Cnp,
                        patient.BirthDate,
                        patient.Age,
                        patient.Gender,
                        patient.Occupation,
                        patient.Email,
                        patient.Phone,
                        patient.County,
                        patient.City,
                        patient.Street,
                        patient.Number,
                        patient.Block,
                        patient.Apartment,
                        patient.Staircase,
                        patient.Floor,
                        patient.PostalCode,
                        patient.Weight,
                        patient.Height,
                        patient.BloodType,
                        patient.Rh,
                        patient.InsuranceCompany,
                        patient.InsuranceId,
                        patient.ChronicDiseases,
                        patient.Vaccinations,
                        patient.HereditaryDiseases,
                        patient.OtherDiseases,
                        patient.Diet,
                        patient.PhysicalActivity,
                        patient.Smoker,
                        patient.AlcoholConsumer,
                        patient.DrugConsumer,
                        patient.ProfileImage,
                        IsActive = true,
                    },
                }
            );
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

                    p.DeletedAt,
                    p.DeletedBy,
                    IsActive = p.DeletedAt == null,

                    Consultations = p
                        .Consultations.Where(c => c.DeletedAt == null)
                        .Select(c => new
                        {
                            c.Id,
                            c.Diagnosis,
                            c.Cnp,
                        })
                        .ToList(),

                    p.ProfileImage,
                })
                .ToList();

            return Ok(patients);
        }

        [HttpPut("update-patient/{id}")]
        public IActionResult UpdatePatient(int id, Patient updatedPatient)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);

            if (patient == null)
                return NotFound(new { message = "Patient not found" });

            patient.FirstName = updatedPatient.FirstName;
            patient.LastName = updatedPatient.LastName;
            patient.Cnp = updatedPatient.Cnp;
            patient.BirthDate = updatedPatient.BirthDate;
            patient.Age = updatedPatient.Age;
            patient.Gender = updatedPatient.Gender;
            patient.Occupation = updatedPatient.Occupation;

            patient.Email = updatedPatient.Email;
            patient.Phone = updatedPatient.Phone;

            patient.County = updatedPatient.County;
            patient.City = updatedPatient.City;
            patient.Street = updatedPatient.Street;
            patient.Number = updatedPatient.Number;
            patient.Block = updatedPatient.Block;
            patient.Apartment = updatedPatient.Apartment;
            patient.Staircase = updatedPatient.Staircase;
            patient.Floor = updatedPatient.Floor;
            patient.PostalCode = updatedPatient.PostalCode;

            patient.Weight = updatedPatient.Weight;
            patient.Height = updatedPatient.Height;
            patient.BloodType = updatedPatient.BloodType;
            patient.Rh = updatedPatient.Rh;

            patient.InsuranceCompany = updatedPatient.InsuranceCompany;
            patient.InsuranceId = updatedPatient.InsuranceId;

            patient.ChronicDiseases = updatedPatient.ChronicDiseases;
            patient.Vaccinations = updatedPatient.Vaccinations;
            patient.HereditaryDiseases = updatedPatient.HereditaryDiseases;
            patient.OtherDiseases = updatedPatient.OtherDiseases;

            patient.Diet = updatedPatient.Diet;
            patient.PhysicalActivity = updatedPatient.PhysicalActivity;
            patient.Smoker = updatedPatient.Smoker;
            patient.AlcoholConsumer = updatedPatient.AlcoholConsumer;
            patient.DrugConsumer = updatedPatient.DrugConsumer;

            patient.ProfileImage = updatedPatient.ProfileImage;

            _context.SaveChanges();

            return Ok(new { success = true, patient });
        }

        [HttpGet("get-patient/{cnp}")]
        public IActionResult GetPatientByCnp(string cnp)
        {
            var patient = _context
                .Patients.Where(p => p.Cnp == cnp)
                .Select(p => new
                {
                    p.Id,
                    p.DoctorId,

                    Doctor = new
                    {
                        p.Doctor.Id,
                        p.Doctor.FirstName,
                        p.Doctor.LastName,
                    },

                    p.FirstName,
                    p.LastName,
                    p.Cnp,
                    p.BirthDate,
                    p.Age,
                    p.Gender,
                    p.Occupation,

                    p.Email,
                    p.Phone,

                    p.County,
                    p.City,
                    p.Street,
                    p.Number,
                    p.Block,
                    p.Apartment,
                    p.Staircase,
                    p.Floor,
                    p.PostalCode,

                    p.Weight,
                    p.Height,
                    p.BloodType,
                    p.Rh,

                    p.InsuranceCompany,
                    p.InsuranceId,

                    p.ChronicDiseases,
                    p.Vaccinations,
                    p.HereditaryDiseases,
                    p.OtherDiseases,

                    p.Diet,
                    p.PhysicalActivity,
                    p.Smoker,
                    p.AlcoholConsumer,
                    p.DrugConsumer,

                    p.DeletedAt,
                    p.DeletedBy,
                    IsActive = p.DeletedAt == null,

                    Consultations = p
                        .Consultations.Where(c => c.DeletedAt == null)
                        .Select(c => new
                        {
                            c.Id,
                            c.Diagnosis,
                            c.Medication,
                            c.ConsultationDate,
                            c.Cnp,
                            c.Locations,
                            c.ConsultationNumber,
                        })
                        .ToList(),

                    p.ProfileImage,
                })
                .FirstOrDefault();

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id, [FromQuery] string doctorId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);

            if (patient == null)
                return NotFound("Patient not found");

            if (patient.DeletedAt != null)
                return BadRequest("Already deleted");

            patient.DeletedAt = DateTime.UtcNow;
            patient.DeletedBy = doctorId;

            _context.SaveChanges();

            return Ok(new { success = true });
        }

        [HttpPut("activate/{cnp}")]
        public IActionResult ActivatePatient(string cnp)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Cnp == cnp);

            if (patient == null)
                return NotFound("Patient not found");

            patient.DeletedAt = null;
            patient.DeletedBy = null;

            _context.SaveChanges();

            return Ok(new { success = true });
        }
    }
}
