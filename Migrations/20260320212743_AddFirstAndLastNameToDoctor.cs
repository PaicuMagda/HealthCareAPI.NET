using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstAndLastNameToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Doctors",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Doctors",
                newName: "Name");
        }
    }
}
