using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JointableDepartementMember01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentMemberDepartmentProgram");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
