using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanningPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlanningPeriodId",
                table: "Plannings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlanningPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PublishedById = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningPeriods_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanningPeriods_Members_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "Members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_PlanningPeriodId",
                table: "Plannings",
                column: "PlanningPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningPeriods_DepartmentId_Month_Year",
                table: "PlanningPeriods",
                columns: new[] { "DepartmentId", "Month", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanningPeriods_PublishedById",
                table: "PlanningPeriods",
                column: "PublishedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Plannings_PlanningPeriods_PlanningPeriodId",
                table: "Plannings",
                column: "PlanningPeriodId",
                principalTable: "PlanningPeriods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plannings_PlanningPeriods_PlanningPeriodId",
                table: "Plannings");

            migrationBuilder.DropTable(
                name: "PlanningPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Plannings_PlanningPeriodId",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "PlanningPeriodId",
                table: "Plannings");
        }
    }
}
