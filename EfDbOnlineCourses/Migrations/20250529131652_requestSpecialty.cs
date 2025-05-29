using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDbOnlineCourses.Migrations
{
    /// <inheritdoc />
    public partial class requestSpecialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SpecialtyId",
                table: "Requests",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Specialties_SpecialtyId",
                table: "Requests",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Specialties_SpecialtyId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_SpecialtyId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Requests");
        }
    }
}
