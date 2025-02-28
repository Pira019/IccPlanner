using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatProgrem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentProgram_Departments_DepartementsId",
                table: "DepartmentProgram");

            migrationBuilder.RenameColumn(
                name: "DepartementsId",
                table: "DepartmentProgram",
                newName: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentProgram_Departments_DepartmentsId",
                table: "DepartmentProgram",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentProgram_Departments_DepartmentsId",
                table: "DepartmentProgram");

            migrationBuilder.RenameColumn(
                name: "DepartmentsId",
                table: "DepartmentProgram",
                newName: "DepartementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentProgram_Departments_DepartementsId",
                table: "DepartmentProgram",
                column: "DepartementsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
