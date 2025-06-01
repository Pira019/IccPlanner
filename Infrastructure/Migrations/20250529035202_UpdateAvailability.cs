using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_ProgramDepartmentId",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_ProgramDepartmentId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "ProgramDepartmentId",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                table: "Availabilities",
                newName: "TabServicePrgId");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Availabilities",
                newName: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentProgramId",
                table: "Availabilities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_DepartmentProgramId",
                table: "Availabilities",
                column: "DepartmentProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_TabServicePrgId",
                table: "Availabilities",
                column: "TabServicePrgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_DepartmentProgramId",
                table: "Availabilities",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_TabServicePrgs_TabServicePrgId",
                table: "Availabilities",
                column: "TabServicePrgId",
                principalTable: "TabServicePrgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_DepartmentProgramId",
                table: "Availabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_TabServicePrgs_TabServicePrgId",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_DepartmentProgramId",
                table: "Availabilities");

            migrationBuilder.DropIndex(
                name: "IX_Availabilities_TabServicePrgId",
                table: "Availabilities");

            migrationBuilder.DropColumn(
                name: "DepartmentProgramId",
                table: "Availabilities");

            migrationBuilder.RenameColumn(
                name: "TabServicePrgId",
                table: "Availabilities",
                newName: "ProgramId");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Availabilities",
                newName: "Comment");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Availabilities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Availabilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProgramDepartmentId",
                table: "Availabilities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_ProgramDepartmentId",
                table: "Availabilities",
                column: "ProgramDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_ProgramDepartmentId",
                table: "Availabilities",
                column: "ProgramDepartmentId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
