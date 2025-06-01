using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvailability001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_DepartmentProgramId",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_DepartmentProgramId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "DepartmentProgramId",
                table: "Availabilities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentProgramId",
                table: "Availabilities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_DepartmentProgramId",
                table: "Availabilities",
                column: "DepartmentProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_DepartmentProgramId",
                table: "Availabilities",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id");
        }
    }
}
