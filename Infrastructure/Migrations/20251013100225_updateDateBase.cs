using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDateBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE ""PrgDepartmentInfos""
                  ALTER COLUMN ""Dates"" TYPE date USING ""Dates""[1];"
            );

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_Type",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DepartmentPrograms");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Dates",
                table: "PrgDepartmentInfos",
                type: "date",
                nullable: true,
                oldClrType: typeof(List<DateOnly>),
                oldType: "date[]",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEnD",
                table: "PrgDepartmentInfos",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateStart",
                table: "PrgDepartmentInfos",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "PrgDepartmentInfos",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IndAct",
                table: "PrgDepartmentInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEnd",
                table: "DepartmentPrograms",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IndActiv",
                table: "DepartmentPrograms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IndRecurent",
                table: "DepartmentPrograms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfos_DayId",
                table: "PrgDepartmentInfos",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_IndRecurent",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId", "IndRecurent" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrgDepartmentInfos_TabDays_DayId",
                table: "PrgDepartmentInfos",
                column: "DayId",
                principalTable: "TabDays",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrgDepartmentInfos_TabDays_DayId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropTable(
                name: "TabDays");

            migrationBuilder.DropIndex(
                name: "IX_PrgDepartmentInfos_DayId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_IndRecurent",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "DateEnD",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "IndAct",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "IndActiv",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "IndRecurent",
                table: "DepartmentPrograms");

            migrationBuilder.AlterColumn<List<DateOnly>>(
                name: "Dates",
                table: "PrgDepartmentInfos",
                type: "date[]",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "PrgDepartmentInfos",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DepartmentPrograms",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_Type",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId", "Type" },
                unique: true);
        }
    }
}
