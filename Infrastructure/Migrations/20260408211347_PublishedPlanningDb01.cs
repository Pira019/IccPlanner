using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PublishedPlanningDb01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PublishedPlannings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlanningPeriodId = table.Column<int>(type: "integer", nullable: false),
                    SourcePlanningId = table.Column<int>(type: "integer", nullable: false),
                    MemberName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ProgramName = table.Column<string>(type: "text", nullable: false),
                    ProgramShortName = table.Column<string>(type: "text", nullable: true),
                    ServiceName = table.Column<string>(type: "text", nullable: false),
                    PosteName = table.Column<string>(type: "text", nullable: true),
                    IndTraining = table.Column<bool>(type: "boolean", nullable: false),
                    IndObservation = table.Column<bool>(type: "boolean", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedPlannings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishedPlannings_PlanningPeriods_PlanningPeriodId",
                        column: x => x.PlanningPeriodId,
                        principalTable: "PlanningPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublishedPlannings_PlanningPeriodId",
                table: "PublishedPlannings",
                column: "PlanningPeriodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublishedPlannings");
        }
    }
}
