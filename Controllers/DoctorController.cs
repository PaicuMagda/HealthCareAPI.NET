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
            }
        );
    }

    [HttpPut("update-doctor/{id}")]
    public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDto dto)
    {
        var doctor = await _context.Doctors.FindAsync(id);

        if (doctor == null)
            return NotFound(new { message = "Doctorul nu a fost găsit" });

        doctor.Username = dto.Username ?? doctor.Username;
        doctor.FirstName = dto.FirstName ?? doctor.FirstName;
        doctor.LastName = dto.LastName ?? doctor.LastName;
        doctor.Email = dto.Email ?? doctor.Email;
        doctor.Cnp = dto.Cnp ?? doctor.Cnp;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Doctor actualizat cu succes" });
    }

    // [HttpPut("change-password/{id}")]
    // public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDto dto)
    // {
    //     var doctor = await _context.Doctors.FindAsync(id);

    //     if (doctor == null)
    //         return NotFound();

    //     if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, doctor.Password))
    //         return BadRequest(new { message = "Parola veche e greșită" });

    //     doctor.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

    //     await _context.SaveChangesAsync();

    //     return Ok(new { message = "Parola schimbată" });
    // }
}
