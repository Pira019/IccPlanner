using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionTypoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnty",
                table: "DepartmentMembers");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "FeedBacks",
                newName: "Rating");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEntry",
                table: "DepartmentMembers",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEntry",
                table: "DepartmentMembers");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "FeedBacks",
                newName: "rating");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEnty",
                table: "DepartmentMembers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
