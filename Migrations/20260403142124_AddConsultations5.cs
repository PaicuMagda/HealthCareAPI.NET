using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultations5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NrConsultatie",
                table: "Consultations",
                newName: "ConsultationNumber");

            migrationBuilder.RenameColumn(
                name: "Medicamentatie",
                table: "Consultations",
                newName: "Medication");

            migrationBuilder.RenameColumn(
                name: "Diagnostic",
                table: "Consultations",
                newName: "Diagnosis");

            migrationBuilder.RenameColumn(
                name: "DataConsultatie",
                table: "Consultations",
                newName: "ConsultationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Medication",
                table: "Consultations",
                newName: "Medicamentatie");

            migrationBuilder.RenameColumn(
                name: "Diagnosis",
                table: "Consultations",
                newName: "Diagnostic");

            migrationBuilder.RenameColumn(
                name: "ConsultationNumber",
                table: "Consultations",
                newName: "NrConsultatie");

            migrationBuilder.RenameColumn(
                name: "ConsultationDate",
                table: "Consultations",
                newName: "DataConsultatie");
        }
    }
}
