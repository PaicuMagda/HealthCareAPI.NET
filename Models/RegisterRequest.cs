namespace HealthcareAPI.Models
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = "doctor";
    }
}
