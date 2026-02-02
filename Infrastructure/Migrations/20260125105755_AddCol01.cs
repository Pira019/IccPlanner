using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCol01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId",
                table: "PrgDepartmentInfos");

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                column: "DepartmentProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId",
                table: "PrgDepartmentInfos");

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                column: "DepartmentProgramId",
                unique: true);
        }
    }
}
