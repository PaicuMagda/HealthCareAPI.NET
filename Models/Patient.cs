namespace HealthcareAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public string Nume { get; set; } = null!;
        public string Prenume { get; set; } = null!;
        public string Cnp { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Telefon { get; set; } = null!;

        public string Judet { get; set; } = null!;
        public string Oras { get; set; } = null!;

        public string GrupaSanguina { get; set; } = null!;
        public string Rh { get; set; } = null!;

        public string BoliCronice { get; set; } = null!;
    }
}
