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

    [Required]
    public string Status { get; set; } = "Draft";

    public DateTime? SignedAt { get; set; }
    public string? SignedBy { get; set; }

    public string? SignatureHash { get; set; }

    public string? Locations { get; set; }

    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [JsonIgnore]
    public Patient Patient { get; set; } = null!;
}
