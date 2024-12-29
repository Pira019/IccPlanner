using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramedById",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "AddedByMemberId",
                table: "Members");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Plannings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "DepartmentMembers",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "shortName",
                table: "Departement",
                type: "character varying(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDepartments_DepartementId",
                table: "ProgramDepartments",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDepartments_ProgramId",
                table: "ProgramDepartments",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Departement_DepartementId",
                table: "ProgramDepartments",
                column: "DepartementId",
                principalTable: "Departement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDepartments_Programs_ProgramId",
                table: "ProgramDepartments",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Departement_DepartementId",
                table: "ProgramDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDepartments_Programs_ProgramId",
                table: "ProgramDepartments");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDepartments_DepartementId",
                table: "ProgramDepartments");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDepartments_ProgramId",
                table: "ProgramDepartments");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Plannings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramedById",
                table: "Plannings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddedByMemberId",
                table: "Members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "NickName",
                table: "DepartmentMembers",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "shortName",
                table: "Departement",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55,
                oldNullable: true);
        }
    }
}
