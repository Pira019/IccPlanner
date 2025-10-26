using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "PrgDates");

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId_DayId",
                table: "PrgDepartmentInfos",
                columns: new[] { "DepartmentProgramId", "DayId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId_DayId",
                table: "PrgDepartmentInfos");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "PrgDates",
                type: "text",
                nullable: true);
        }
    }
}
