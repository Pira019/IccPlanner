using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueColumn01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentProgramId",
                table: "PrgDepartmentInfos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentProgramId",
                table: "PrgDepartmentInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
