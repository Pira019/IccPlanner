using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Availabilities_DepartmentMemberId",
                table: "Availabilities");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_DepartmentMemberId_TabServicePrgId",
                table: "Availabilities",
                columns: new[] { "DepartmentMemberId", "TabServicePrgId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Availabilities_DepartmentMemberId_TabServicePrgId",
                table: "Availabilities");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_DepartmentMemberId",
                table: "Availabilities",
                column: "DepartmentMemberId");
        }
    }
}
