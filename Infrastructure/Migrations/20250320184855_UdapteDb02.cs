using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UdapteDb02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMember_Departments_DepartementsId",
                table: "DepartmentMember");

            migrationBuilder.RenameColumn(
                name: "DepartementsId",
                table: "DepartmentMember",
                newName: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMember_Departments_DepartmentsId",
                table: "DepartmentMember",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMember_Departments_DepartmentsId",
                table: "DepartmentMember");

            migrationBuilder.RenameColumn(
                name: "DepartmentsId",
                table: "DepartmentMember",
                newName: "DepartementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMember_Departments_DepartementsId",
                table: "DepartmentMember",
                column: "DepartementsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
