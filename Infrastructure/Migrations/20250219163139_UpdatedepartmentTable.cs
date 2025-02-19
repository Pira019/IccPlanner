using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedepartmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartementMember");

            migrationBuilder.DropTable(
                name: "DepartementProgram");

            migrationBuilder.CreateTable(
                name: "DepartmentMember",
                columns: table => new
                {
                    DepartementsId = table.Column<int>(type: "integer", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMember", x => new { x.DepartementsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_DepartmentMember_Departments_DepartementsId",
                        column: x => x.DepartementsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentProgram",
                columns: table => new
                {
                    DepartementsId = table.Column<int>(type: "integer", nullable: false),
                    ProgramsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentProgram", x => new { x.DepartementsId, x.ProgramsId });
                    table.ForeignKey(
                        name: "FK_DepartmentProgram_Departments_DepartementsId",
                        column: x => x.DepartementsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentProgram_Programs_ProgramsId",
                        column: x => x.ProgramsId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMember_MembersId",
                table: "DepartmentMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentProgram_ProgramsId",
                table: "DepartmentProgram",
                column: "ProgramsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentMember");

            migrationBuilder.DropTable(
                name: "DepartmentProgram");

            migrationBuilder.CreateTable(
                name: "DepartementMember",
                columns: table => new
                {
                    DepartementsId = table.Column<int>(type: "integer", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartementMember", x => new { x.DepartementsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_DepartementMember_Departments_DepartementsId",
                        column: x => x.DepartementsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartementMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartementProgram",
                columns: table => new
                {
                    DepartementsId = table.Column<int>(type: "integer", nullable: false),
                    ProgramsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartementProgram", x => new { x.DepartementsId, x.ProgramsId });
                    table.ForeignKey(
                        name: "FK_DepartementProgram_Departments_DepartementsId",
                        column: x => x.DepartementsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartementProgram_Programs_ProgramsId",
                        column: x => x.ProgramsId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartementMember_MembersId",
                table: "DepartementMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartementProgram_ProgramsId",
                table: "DepartementProgram",
                column: "ProgramsId");
        }
    }
}
