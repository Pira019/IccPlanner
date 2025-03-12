using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_ProgramDepartments_ProgramDepartmentId",
                table: "Availabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_ProgramDepartments_ProgramDepartmentId",
                table: "FeedBacks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Departments_DepartmentId",
                table: "ProgramDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Members_CreateById",
                table: "ProgramDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Members_UpdateById",
                table: "ProgramDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Programs_ProgramId",
                table: "ProgramDepartments");

            migrationBuilder.DropTable(
                name: "DepartmentMemberProgramDepartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramDepartments",
                table: "ProgramDepartments");

            migrationBuilder.RenameTable(
                name: "ProgramDepartments",
                newName: "DepartmentPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDepartments_UpdateById",
                table: "DepartmentPrograms",
                newName: "IX_DepartmentPrograms_UpdateById");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDepartments_ProgramId_DepartmentId_StartAt",
                table: "DepartmentPrograms",
                newName: "IX_DepartmentPrograms_ProgramId_DepartmentId_StartAt");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDepartments_DepartmentId",
                table: "DepartmentPrograms",
                newName: "IX_DepartmentPrograms_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramDepartments_CreateById",
                table: "DepartmentPrograms",
                newName: "IX_DepartmentPrograms_CreateById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentPrograms",
                table: "DepartmentPrograms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DepartmentMemberDepartmentProgram",
                columns: table => new
                {
                    DepartmentMembersId = table.Column<int>(type: "integer", nullable: false),
                    ProgramDepartmentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMemberDepartmentProgram", x => new { x.DepartmentMembersId, x.ProgramDepartmentsId });
                    table.ForeignKey(
                        name: "FK_DepartmentMemberDepartmentProgram_DepartmentMembers_Departm~",
                        column: x => x.DepartmentMembersId,
                        principalTable: "DepartmentMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMemberDepartmentProgram_DepartmentPrograms_Progra~",
                        column: x => x.ProgramDepartmentsId,
                        principalTable: "DepartmentPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMemberDepartmentProgram_ProgramDepartmentsId",
                table: "DepartmentMemberDepartmentProgram",
                column: "ProgramDepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_ProgramDepartmentId",
                table: "Availabilities",
                column: "ProgramDepartmentId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPrograms_Departments_DepartmentId",
                table: "DepartmentPrograms",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPrograms_Members_CreateById",
                table: "DepartmentPrograms",
                column: "CreateById",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPrograms_Members_UpdateById",
                table: "DepartmentPrograms",
                column: "UpdateById",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPrograms_Programs_ProgramId",
                table: "DepartmentPrograms",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_DepartmentPrograms_ProgramDepartmentId",
                table: "FeedBacks",
                column: "ProgramDepartmentId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Availabilities_DepartmentPrograms_ProgramDepartmentId",
                table: "Availabilities");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPrograms_Departments_DepartmentId",
                table: "DepartmentPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPrograms_Members_CreateById",
                table: "DepartmentPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPrograms_Members_UpdateById",
                table: "DepartmentPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPrograms_Programs_ProgramId",
                table: "DepartmentPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_DepartmentPrograms_ProgramDepartmentId",
                table: "FeedBacks");

            migrationBuilder.DropTable(
                name: "DepartmentMemberDepartmentProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentPrograms",
                table: "DepartmentPrograms");

            migrationBuilder.RenameTable(
                name: "DepartmentPrograms",
                newName: "ProgramDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentPrograms_UpdateById",
                table: "ProgramDepartments",
                newName: "IX_ProgramDepartments_UpdateById");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_StartAt",
                table: "ProgramDepartments",
                newName: "IX_ProgramDepartments_ProgramId_DepartmentId_StartAt");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentPrograms_DepartmentId",
                table: "ProgramDepartments",
                newName: "IX_ProgramDepartments_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentPrograms_CreateById",
                table: "ProgramDepartments",
                newName: "IX_ProgramDepartments_CreateById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramDepartments",
                table: "ProgramDepartments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DepartmentMemberProgramDepartment",
                columns: table => new
                {
                    DepartmentMembersId = table.Column<int>(type: "integer", nullable: false),
                    ProgramDepartmentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMemberProgramDepartment", x => new { x.DepartmentMembersId, x.ProgramDepartmentsId });
                    table.ForeignKey(
                        name: "FK_DepartmentMemberProgramDepartment_DepartmentMembers_Departm~",
                        column: x => x.DepartmentMembersId,
                        principalTable: "DepartmentMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMemberProgramDepartment_ProgramDepartments_Progra~",
                        column: x => x.ProgramDepartmentsId,
                        principalTable: "ProgramDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMemberProgramDepartment_ProgramDepartmentsId",
                table: "DepartmentMemberProgramDepartment",
                column: "ProgramDepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Availabilities_ProgramDepartments_ProgramDepartmentId",
                table: "Availabilities",
                column: "ProgramDepartmentId",
                principalTable: "ProgramDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_ProgramDepartments_ProgramDepartmentId",
                table: "FeedBacks",
                column: "ProgramDepartmentId",
                principalTable: "ProgramDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Departments_DepartmentId",
                table: "ProgramDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Members_CreateById",
                table: "ProgramDepartments",
                column: "CreateById",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Members_UpdateById",
                table: "ProgramDepartments",
                column: "UpdateById",
                principalTable: "Members",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Programs_ProgramId",
                table: "ProgramDepartments",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
