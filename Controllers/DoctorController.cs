using HealthcareAPI.Data;
using HealthcareAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public DoctorsController(AppDbContext context)
    {
        _context = context;
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
                firstname = doctor.FirstName,
                lastname = doctor.LastName,
                fullname = doctor.FullName ?? "",
                email = doctor.Email,
                role = doctor.Role,
                cnp = doctor.Cnp,
                password = doctor.Password,
            }
        );
    }

    [HttpPut("update-doctor/{id}")]
    public async Task<IActionResult> UpdateDoctor(int id, [FromBody] Doctor dto)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
            return NotFound(new { message = "Doctorul nu a fost găsit" });

        doctor.Username = dto.Username;
        doctor.FirstName = dto.FirstName;
        doctor.LastName = dto.LastName;
        doctor.Email = dto.Email;
        doctor.Role = dto.Role;
        doctor.Cnp = dto.Cnp;

        if (!string.IsNullOrEmpty(dto.Password))
            doctor.Password = dto.Password;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Doctor actualizat cu succes" });
    }
}
