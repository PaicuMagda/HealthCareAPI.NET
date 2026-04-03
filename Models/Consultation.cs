using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HealthcareAPI.Models;

public class Consultation
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Cnp { get; set; } = null!;

    [Required]
    public DateTime DataConsultatie { get; set; }

    [Required]
    public string Diagnostic { get; set; } = null!;

    public string? Medicamentatie { get; set; }

    public int NrConsultatie { get; set; }

    [JsonIgnore]
    public Patient Patient { get; set; } = null!;
}
