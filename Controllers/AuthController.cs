using HealthcareAPI.Data;
using HealthcareAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (
                string.IsNullOrWhiteSpace(request.Username)
                || string.IsNullOrWhiteSpace(request.Password)
            )
                return BadRequest(new { message = "Date invalide" });

            var doctor = _context
                .Doctors.AsNoTracking()
                .FirstOrDefault(d => d.Username.ToLower() == request.Username.ToLower());

            if (doctor == null)
                return NotFound(new { message = "Utilizatorul nu există" });

            if (!BCrypt.Net.BCrypt.Verify(request.Password, doctor.Password))
                return Unauthorized(new { message = "Parolă incorectă" });

            return Ok(
                new
                {
                    success = true,
                    user = new
                    {
                        id = doctor.Id,
                        username = doctor.Username,
                        fullname = doctor.FullName,
                        role = doctor.Role,
                        email = doctor.Email,
                    },
                }
            );
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (
                string.IsNullOrWhiteSpace(request.Username)
                || string.IsNullOrWhiteSpace(request.Password)
            )
                return BadRequest(new { message = "Date invalide" });

            var existingDoctor = _context.Doctors.FirstOrDefault(d =>
                d.Username == request.Username || d.Email == request.Email
            );

            if (existingDoctor != null)
                return BadRequest(new { message = "Username sau email deja există" });

            var doctor = new Doctor
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email,
                Role = "doctor",
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return Ok(new { message = "Cont creat!" });
        }
    }
}
