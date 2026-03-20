using HealthcareAPI.Data;
using HealthcareAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Username == request.Username);

            if (doctor == null)
                return Ok(new { success = false, message = "Utilizatorul nu există" });

            if (doctor.Password != request.Password)
                return Ok(new { success = false, message = "Parolă incorectă" });

            return Ok(
                new
                {
                    success = true,
                    user = new
                    {
                        id = doctor.Id,
                        username = doctor.Username,
                        nume = doctor.Name,
                        role = doctor.Role,
                        email = doctor.Email,
                    },
                }
            );
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var existingDoctor = _context.Doctors.FirstOrDefault(d =>
                d.Username == request.Username
            );

            if (existingDoctor != null)
                return BadRequest(new { message = "Username deja există" });

            var doctor = new Doctor
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                Role = "doctor",
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return Ok(
                new
                {
                    message = "Contul a fost adăugat cu succes!",
                    user = new
                    {
                        id = doctor.Id,
                        username = doctor.Username,
                        role = doctor.Role,
                        email = doctor.Email,
                    },
                }
            );
        }

        [HttpGet("get-doctor-by-id/{id}")]
        public IActionResult GetDoctorById(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);

            if (doctor == null)
                return NotFound(new { message = "Doctorul nu a fost găsit" });

            return Ok(
                new
                {
                    id = doctor.Id,
                    username = doctor.Username,
                    nume = doctor.Name ?? "",
                    email = doctor.Email,
                    role = doctor.Role,
                }
            );
        }
    }
}
