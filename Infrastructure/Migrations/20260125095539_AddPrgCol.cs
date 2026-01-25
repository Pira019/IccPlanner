using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrgCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPrograms_Members_CreateById",
                table: "DepartmentPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPrograms_Members_UpdateById",
                table: "DepartmentPrograms");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_CreateById",
                table: "DepartmentPrograms");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_Type",
                table: "DepartmentPrograms");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_UpdateById",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "UpdateById",
                table: "DepartmentPrograms");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "PrgDepartmentInfos",
                type: "character varying(1)",
                maxLength: 1,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "DepartmentPrograms",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateF",
                table: "DepartmentPrograms",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateS",
                table: "DepartmentPrograms",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IndRecurent",
                table: "DepartmentPrograms",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdateBy",
                table: "DepartmentPrograms",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_IndRecurent",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId", "IndRecurent" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_IndRecurent",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "DateF",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "DateS",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "IndRecurent",
                table: "DepartmentPrograms");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "DepartmentPrograms");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "PrgDepartmentInfos",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateById",
                table: "DepartmentPrograms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DepartmentPrograms",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateById",
                table: "DepartmentPrograms",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_CreateById",
                table: "DepartmentPrograms",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_ProgramId_DepartmentId_Type",
                table: "DepartmentPrograms",
                columns: new[] { "ProgramId", "DepartmentId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPrograms_UpdateById",
                table: "DepartmentPrograms",
                column: "UpdateById");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPrograms_Members_CreateById",
                table: "DepartmentPrograms",
                column: "CreateById",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPrograms_Members_UpdateById",
                table: "DepartmentPrograms",
                column: "UpdateById",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
