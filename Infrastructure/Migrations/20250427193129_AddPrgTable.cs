// Ignore Spelling: Prg

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrgTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurring",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "Localisation",
                table: "DepartmentPrograms");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DepartmentPrograms",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DepartmentInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentProgramId = table.Column<int>(type: "integer", nullable: false),
                    Dates = table.Column<List<DateOnly>>(type: "date[]", nullable: true),
                    Days = table.Column<List<string>>(type: "text[]", nullable: true),
                    NbrServices = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                        column: x => x.DepartmentProgramId,
                        principalTable: "DepartmentPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_Type",
                table: "DepartmentPrograms",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentInfos_DepartmentProgramId",
                table: "DepartmentInfos",
                column: "DepartmentProgramId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentInfos");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_Type",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DepartmentPrograms");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurring",
                table: "DepartmentPrograms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Localisation",
                table: "DepartmentPrograms",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
