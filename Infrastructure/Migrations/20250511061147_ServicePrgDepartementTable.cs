using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ServicePrgDepartementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrgDepartmentInfoTabServices",
                columns: table => new
                {
                    PrgDepartmentInfosId = table.Column<int>(type: "integer", nullable: false),
                    TabServicesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrgDepartmentInfoTabServices", x => new { x.PrgDepartmentInfosId, x.TabServicesId });
                    table.ForeignKey(
                        name: "FK_PrgDepartmentInfoTabServices_PrgDepartmentInfos_PrgDepartme~",
                        column: x => x.PrgDepartmentInfosId,
                        principalTable: "PrgDepartmentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrgDepartmentInfoTabServices_TabServices_TabServicesId",
                        column: x => x.TabServicesId,
                        principalTable: "TabServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePrgDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TabServicesId = table.Column<int>(type: "integer", nullable: false),
                    PrgDepartmentInfoId = table.Column<int>(type: "integer", nullable: false),
                    ArrivalTimeOfMember = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Day = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePrgDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePrgDepartments_PrgDepartmentInfos_PrgDepartmentInfoId",
                        column: x => x.PrgDepartmentInfoId,
                        principalTable: "PrgDepartmentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePrgDepartments_TabServices_TabServicesId",
                        column: x => x.TabServicesId,
                        principalTable: "TabServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfoTabServices_TabServicesId",
                table: "PrgDepartmentInfoTabServices",
                column: "TabServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrgDepartments_PrgDepartmentInfoId",
                table: "ServicePrgDepartments",
                column: "PrgDepartmentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrgDepartments_TabServicesId",
                table: "ServicePrgDepartments",
                column: "TabServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrgDepartmentInfoTabServices");

            migrationBuilder.DropTable(
                name: "ServicePrgDepartments");
        }
    }
}
