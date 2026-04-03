public class CreateConsultationDto
{
    public string Cnp { get; set; } = null!;
    public DateTime DataConsultatie { get; set; }
    public string Diagnostic { get; set; } = null!;
    public string? Medicamentatie { get; set; }
}
