namespace HealthcareAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        // Personal Info
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Cnp { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? Occupation { get; set; }

        // Contact
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        // Address
        public string County { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Block { get; set; }
        public string? Apartment { get; set; }
        public string? Staircase { get; set; }
        public string? Floor { get; set; }
        public string? PostalCode { get; set; }

        // Medical Info
        public float Weight { get; set; }
        public float Height { get; set; }
        public string BloodType { get; set; } = null!;
        public string Rh { get; set; } = null!;

        // Insurance
        public string? InsuranceCompany { get; set; }
        public string? InsuranceId { get; set; }

        // Medical History
        public string? ChronicDiseases { get; set; }
        public string? Vaccinations { get; set; }
        public string? HereditaryDiseases { get; set; }
        public string? OtherDiseases { get; set; }

        // Lifestyle
        public string? Diet { get; set; }
        public string? PhysicalActivity { get; set; }
        public bool Smoker { get; set; }
        public bool AlcoholConsumer { get; set; }
        public bool DrugConsumer { get; set; }

        // Profile Image
        public string? ProfileImage { get; set; }
    }
}
