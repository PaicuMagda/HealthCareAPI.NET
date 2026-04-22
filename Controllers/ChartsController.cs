using HealthcareAPI.Data;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ChartsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ChartsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("diagnostics")]
    public IActionResult GetDiagnosticsStats([FromQuery] int? doctorId, [FromQuery] string role)
    {
        var query = _context.Consultations.Where(c => c.DeletedAt == null).AsQueryable();

        if (role != "admin")
        {
            query = query.Where(c => c.Patient.DoctorId == doctorId);
        }

        var result = query
            .GroupBy(c => c.Diagnosis)
            .Select(g => new
            {
                boala = g.Key,
                numar_pacienti = g.Select(x => x.Cnp).Distinct().Count(),
            })
            .ToList();

        return Ok(result);
    }
}
