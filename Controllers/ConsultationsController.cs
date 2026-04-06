using System.Security.Cryptography;
using System.Text;
using HealthcareAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ConsultationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ConsultationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddConsultation(CreateConsultationDto dto)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Cnp == dto.Cnp);

        if (patient == null)
            return NotFound("Patient does not exist!");

        var lastNumber = await _context
            .Consultations.Where(c => c.Cnp == dto.Cnp)
            .OrderByDescending(c => c.ConsultationNumber)
            .Select(c => c.ConsultationNumber)
            .FirstOrDefaultAsync();

        var newConsultation = new Consultation
        {
            Cnp = dto.Cnp,
            ConsultationDate = dto.ConsultationDate,
            Diagnosis = dto.Diagnosis,
            Medication = dto.Medication,
            ConsultationNumber = lastNumber + 1,
            Status = "Draft",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Consultations.Add(newConsultation);
        await _context.SaveChangesAsync();

        return Ok(newConsultation);
    }

    [HttpGet("{cnp}")]
    public async Task<IActionResult> GetConsultations(string cnp)
    {
        var consultations = await _context
            .Consultations.Where(c => c.Cnp == cnp)
            .OrderByDescending(c => c.ConsultationDate)
            .Select(c => new
            {
                c.Id,
                c.Cnp,
                c.ConsultationNumber,
                c.ConsultationDate,
                c.Diagnosis,
                c.Medication,
                c.Status,
                c.CreatedAt,
                c.UpdatedAt,
                c.DeletedAt,
                c.DeletedBy,
            })
            .ToListAsync();

        return Ok(new { consultations });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConsultation(
        int id,
        UpdateConsultationDto dto,
        [FromQuery] string doctorId
    )
    {
        var consultation = await _context.Consultations.FindAsync(id);

        if (consultation == null)
            return NotFound();

        if (consultation.Status == "Finalized")
            return BadRequest("Consultation is signed and cannot be modified.");

        if (consultation.DeletedAt != null)
            return BadRequest("Consultation is deleted.");

        consultation.ConsultationDate = dto.ConsultationDate;
        consultation.Diagnosis = dto.Diagnosis;
        consultation.Medication = dto.Medication;
        consultation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(new { success = true });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsultation(int id, [FromQuery] string doctorId)
    {
        var consultation = await _context.Consultations.FirstOrDefaultAsync(c => c.Id == id);

        if (consultation == null)
            return NotFound("Consultation not found");

        if (consultation.DeletedAt != null)
            return BadRequest("Already deleted");

        consultation.DeletedAt = DateTime.UtcNow;
        consultation.DeletedBy = doctorId;

        await _context.SaveChangesAsync();

        return Ok(new { success = true });
    }

    [HttpPost("sign/{id}")]
    public async Task<IActionResult> SignConsultation(int id)
    {
        var consultation = await _context.Consultations.FindAsync(id);

        if (consultation == null)
            return NotFound();

        if (consultation.Status == "Finalized")
            return BadRequest("Already signed");

        if (consultation.DeletedAt != null)
            return BadRequest("Cannot sign a deleted consultation");

        consultation.Status = "Finalized";
        consultation.SignedAt = DateTime.UtcNow;
        consultation.SignedBy = "doctor_id";

        var content =
            $"{consultation.Cnp}{consultation.ConsultationDate}{consultation.Diagnosis}{consultation.Medication}";
        consultation.SignatureHash = ComputeHash(content);

        await _context.SaveChangesAsync();

        return Ok(new { success = true });
    }

    private string ComputeHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
