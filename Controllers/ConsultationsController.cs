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
            return NotFound("Pacientul nu există!");

        var lastNumber = await _context
            .Consultations.Where(c => c.Cnp == dto.Cnp)
            .OrderByDescending(c => c.NrConsultatie)
            .Select(c => c.NrConsultatie)
            .FirstOrDefaultAsync();

        var newConsultation = new Consultation
        {
            Cnp = dto.Cnp,
            DataConsultatie = dto.DataConsultatie,
            Diagnostic = dto.Diagnostic,
            Medicamentatie = dto.Medicamentatie,
            NrConsultatie = lastNumber + 1,
        };

        _context.Consultations.Add(newConsultation);
        await _context.SaveChangesAsync();

        return Ok(newConsultation);
    }

    [HttpGet("{cnp}")]
    public async Task<IActionResult> GetConsultations(string cnp)
    {
        var consultations = await _context.Consultations.Where(c => c.Cnp == cnp).ToListAsync();

        return Ok(new { consultatii = consultations });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateConsultation(int id, Consultation updated)
    {
        var consultation = await _context.Consultations.FindAsync(id);

        if (consultation == null)
            return NotFound();

        consultation.Diagnostic = updated.Diagnostic;
        consultation.Medicamentatie = updated.Medicamentatie;
        consultation.DataConsultatie = updated.DataConsultatie;

        await _context.SaveChangesAsync();

        return Ok(new { success = true });
    }
}
