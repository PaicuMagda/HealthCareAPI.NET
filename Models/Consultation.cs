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
    public DateTime ConsultationDate { get; set; }

    [Required]
    public string Diagnosis { get; set; } = null!;

    public string? Medication { get; set; }

    public int ConsultationNumber { get; set; }

    [JsonIgnore]
    public Patient Patient { get; set; } = null!;
}
