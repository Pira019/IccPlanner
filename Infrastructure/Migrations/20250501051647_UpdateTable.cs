using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrgDates_PrgDepartmentInfoId",
                table: "PrgDates");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_StartAt",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "DepartmentPrograms");

            migrationBuilder.CreateIndex(
                name: "IX_PrgDates_PrgDepartmentInfoId",
                table: "PrgDates",
                column: "PrgDepartmentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PrgDates_PrgDepartmentInfoId",
                table: "PrgDates");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId",
                table: "DepartmentPrograms");

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartAt",
                table: "DepartmentPrograms",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_PrgDates_PrgDepartmentInfoId",
                table: "PrgDates",
                column: "PrgDepartmentInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_StartAt",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId", "StartAt" },
                unique: true);
        }
    }
}
