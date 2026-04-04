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
        };

        _context.Consultations.Add(newConsultation);
        await _context.SaveChangesAsync();

        return Ok(newConsultation);
    }

    [HttpGet("{cnp}")]
    public async Task<IActionResult> GetConsultations(string cnp)
    {
        var consultations = await _context.Consultations.Where(c => c.Cnp == cnp).ToListAsync();

        return Ok(new { consultations });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConsultation(int id, UpdateConsultationDto dto)
    {
        var consultation = await _context.Consultations.FindAsync(id);

        if (consultation == null)
            return NotFound();

        consultation.ConsultationDate = dto.ConsultationDate;
        consultation.Diagnosis = dto.Diagnosis;
        consultation.Medication = dto.Medication;

        await _context.SaveChangesAsync();

        return Ok(new { success = true });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConsultation(int id)
    {
        var consultation = await _context.Consultations.FirstOrDefaultAsync(c =>
            c.ConsultationNumber == id
        );

        if (consultation == null)
            return NotFound("Consultation not found");

        _context.Consultations.Remove(consultation);
        await _context.SaveChangesAsync();

        return Ok(new { success = true });
    }
}
