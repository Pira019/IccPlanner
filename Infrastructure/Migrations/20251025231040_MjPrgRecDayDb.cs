using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MjPrgRecDayDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropTable(
                name: "PrgDepartmentInfoTabDays");

            migrationBuilder.DropTable(
                name: "TabDays");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentProgramId",
                table: "PrgDepartmentInfos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PrgRecDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    PrgDepartmentInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrgRecDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrgRecDays_PrgDepartmentInfos_PrgDepartmentInfoId",
                        column: x => x.PrgDepartmentInfoId,
                        principalTable: "PrgDepartmentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrgRecDays_Day_PrgDepartmentInfoId",
                table: "PrgRecDays",
                columns: new[] { "Day", "PrgDepartmentInfoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrgRecDays_PrgDepartmentInfoId",
                table: "PrgRecDays",
                column: "PrgDepartmentInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropTable(
                name: "PrgRecDays");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentProgramId",
                table: "PrgDepartmentInfos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "TabDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrgDepartmentInfoTabDays",
                columns: table => new
                {
                    PrgDepartmentInfosId = table.Column<int>(type: "integer", nullable: false),
                    TabDaysId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrgDepartmentInfoTabDays", x => new { x.PrgDepartmentInfosId, x.TabDaysId });
                    table.ForeignKey(
                        name: "FK_PrgDepartmentInfoTabDays_PrgDepartmentInfos_PrgDepartmentIn~",
                        column: x => x.PrgDepartmentInfosId,
                        principalTable: "PrgDepartmentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrgDepartmentInfoTabDays_TabDays_TabDaysId",
                        column: x => x.TabDaysId,
                        principalTable: "TabDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfoTabDays_TabDaysId",
                table: "PrgDepartmentInfoTabDays",
                column: "TabDaysId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id");
        }
    }
}
