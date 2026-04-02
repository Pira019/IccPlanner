using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColonne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop index only if it exists
            migrationBuilder.Sql("DROP INDEX IF EXISTS \"IX_Users_PhoneNumber\";");

            migrationBuilder.Sql("DROP INDEX IF EXISTS \"IX_Plannings_AvailabilityId\";");

            migrationBuilder.DropColumn(
                name: "Dates",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "PrgDates");

            migrationBuilder.AddColumn<bool>(
                name: "IsTraining",
                table: "Plannings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PosteId",
                table: "Plannings",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_AvailabilityId",
                table: "Plannings",
                column: "AvailabilityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_PosteId",
                table: "Plannings",
                column: "PosteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plannings_Postes_PosteId",
                table: "Plannings",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plannings_Postes_PosteId",
                table: "Plannings");

            migrationBuilder.DropIndex(
                name: "IX_Plannings_AvailabilityId",
                table: "Plannings");

            migrationBuilder.DropIndex(
                name: "IX_Plannings_PosteId",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "IsTraining",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "PosteId",
                table: "Plannings");

            migrationBuilder.AddColumn<List<DateOnly>>(
                name: "Dates",
                table: "PrgDepartmentInfos",
                type: "date[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "PrgDates",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_AvailabilityId",
                table: "Plannings",
                column: "AvailabilityId");
        }
    }
}
