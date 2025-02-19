using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departement_Ministries_MinistryId",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartementMember_Departement_DepartementsId",
                table: "DepartementMember");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartementProgram_Departement_DepartementsId",
                table: "DepartementProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMembers_Departement_DepartementId",
                table: "DepartmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Departement_DepartementId",
                table: "ProgramDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departement",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameIndex(
                name: "IX_Departement_Name",
                table: "Departments",
                newName: "IX_Departments_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Departement_MinistryId",
                table: "Departments",
                newName: "IX_Departments_MinistryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartementMember_Departments_DepartementsId",
                table: "DepartementMember",
                column: "DepartementsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartementProgram_Departments_DepartementsId",
                table: "DepartementProgram",
                column: "DepartementsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMembers_Departments_DepartementId",
                table: "DepartmentMembers",
                column: "DepartementId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments",
                column: "MinistryId",
                principalTable: "Ministries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Departments_DepartementId",
                table: "ProgramDepartments",
                column: "DepartementId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartementMember_Departments_DepartementsId",
                table: "DepartementMember");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartementProgram_Departments_DepartementsId",
                table: "DepartementProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentMembers_Departments_DepartementId",
                table: "DepartmentMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Departments_DepartementId",
                table: "ProgramDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_Name",
                table: "Department",
                newName: "IX_Departement_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_MinistryId",
                table: "Department",
                newName: "IX_Departement_MinistryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departement",
                table: "Department",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departement_Ministries_MinistryId",
                table: "Department",
                column: "MinistryId",
                principalTable: "Ministries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartementMember_Departement_DepartementsId",
                table: "DepartementMember",
                column: "DepartementsId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartementProgram_Departement_DepartementsId",
                table: "DepartementProgram",
                column: "DepartementsId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentMembers_Departement_DepartementId",
                table: "DepartmentMembers",
                column: "DepartementId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Departement_DepartementId",
                table: "ProgramDepartments",
                column: "DepartementId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
