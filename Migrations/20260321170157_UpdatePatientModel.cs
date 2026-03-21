using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoliCronice",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "Patients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Prenume",
                table: "Patients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Poza",
                table: "Patients",
                newName: "Vaccinations");

            migrationBuilder.RenameColumn(
                name: "Oras",
                table: "Patients",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Nume",
                table: "Patients",
                newName: "County");

            migrationBuilder.RenameColumn(
                name: "Judet",
                table: "Patients",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "GrupaSanguina",
                table: "Patients",
                newName: "BloodType");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AlcoholConsumer",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Apartment",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Block",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChronicDiseases",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diet",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DrugConsumer",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Floor",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Patients",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "HereditaryDiseases",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceCompany",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsuranceId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDiseases",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalActivity",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Smoker",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Staircase",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Patients",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AlcoholConsumer",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Apartment",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Block",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ChronicDiseases",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Diet",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DrugConsumer",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "HereditaryDiseases",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "InsuranceCompany",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "OtherDiseases",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PhysicalActivity",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Smoker",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Staircase",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Vaccinations",
                table: "Patients",
                newName: "Poza");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Patients",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patients",
                newName: "Prenume");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Patients",
                newName: "Oras");

            migrationBuilder.RenameColumn(
                name: "County",
                table: "Patients",
                newName: "Nume");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Patients",
                newName: "Judet");

            migrationBuilder.RenameColumn(
                name: "BloodType",
                table: "Patients",
                newName: "GrupaSanguina");

            migrationBuilder.AddColumn<string>(
                name: "BoliCronice",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
