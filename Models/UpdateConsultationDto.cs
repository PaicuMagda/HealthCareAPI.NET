public class UpdateConsultationDto
{
    public DateTime ConsultationDate { get; set; }
    public string Diagnosis { get; set; } = null!;
    public string? Medication { get; set; }

    public List<string>? Locations { get; set; }
}
