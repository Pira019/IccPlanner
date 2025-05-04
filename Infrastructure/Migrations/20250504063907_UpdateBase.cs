using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId",
                table: "DepartmentPrograms");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_Type",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "NbrServices",
                table: "PrgDepartmentInfos");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_Type",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_Type",
                table: "DepartmentPrograms");

            migrationBuilder.AddColumn<int>(
                name: "NbrServices",
                table: "PrgDepartmentInfos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_Type",
                table: "DepartmentPrograms",
                column: "Type");
        }
    }
}
