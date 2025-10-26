using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCol1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrgDepartmentInfos_TabDays_DayId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropIndex(
                name: "IX_PrgDepartmentInfos_DayId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId_DayId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "Dates",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "PrgDepartmentInfos");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrgDepartmentInfoTabDays");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Dates",
                table: "PrgDepartmentInfos",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "PrgDepartmentInfos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfos_DayId",
                table: "PrgDepartmentInfos",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId_DayId",
                table: "PrgDepartmentInfos",
                columns: new[] { "DepartmentProgramId", "DayId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrgDepartmentInfos_TabDays_DayId",
                table: "PrgDepartmentInfos",
                column: "DayId",
                principalTable: "TabDays",
                principalColumn: "Id");
        }
    }
}
