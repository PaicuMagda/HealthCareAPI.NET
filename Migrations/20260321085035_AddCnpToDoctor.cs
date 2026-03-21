using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCnpToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cnp",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cnp",
                table: "Doctors");
        }
    }
}
