namespace HealthcareAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "doctor";
        public string Cnp { get; set; } = null!;
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
