using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Department",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "shortName",
                table: "Department",
                newName: "ShortName");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Department",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Department",
                newName: "startDate");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Department",
                newName: "shortName");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "startDate",
                table: "Department",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
