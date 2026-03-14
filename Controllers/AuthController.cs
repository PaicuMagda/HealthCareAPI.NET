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
                return BadRequest(new { message = "Utilizatorul nu există" });

            if (doctor.Password != request.Password)
                return BadRequest(new { message = "Parolă incorectă" });

            return Ok(new { success = true, user = doctor });
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDoctorRequest request)
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
                Name = request.Name,
                Email = request.Email,
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return Ok(new { message = "Doctor înregistrat cu succes", doctor = doctor });
        }
    }
}
