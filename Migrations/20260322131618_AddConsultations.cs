using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cnp",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Patients_Cnp",
                table: "Patients",
                column: "Cnp");

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnp = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataConsultatie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicamentatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrConsultatie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultations_Patients_Cnp",
                        column: x => x.Cnp,
                        principalTable: "Patients",
                        principalColumn: "Cnp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Cnp",
                table: "Patients",
                column: "Cnp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_Cnp",
                table: "Consultations",
                column: "Cnp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Patients_Cnp",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Cnp",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "Cnp",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
