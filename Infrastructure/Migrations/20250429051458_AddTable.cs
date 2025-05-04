using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "DepartmentInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentInfos",
                table: "DepartmentInfos");

            migrationBuilder.RenameTable(
                name: "DepartmentInfos",
                newName: "PrgDepartmentInfos");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentInfos_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                newName: "IX_PrgDepartmentInfos_DepartmentProgramId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PrgDepartmentInfos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PrgDepartmentInfos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrgDepartmentInfos",
                table: "PrgDepartmentInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PrgDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    PrgDepartmentInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrgDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrgDates_PrgDepartmentInfos_PrgDepartmentInfoId",
                        column: x => x.PrgDepartmentInfoId,
                        principalTable: "PrgDepartmentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrgDates_Date",
                table: "PrgDates",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_PrgDates_PrgDepartmentInfoId",
                table: "PrgDates",
                column: "PrgDepartmentInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrgDepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropTable(
                name: "PrgDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrgDepartmentInfos",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PrgDepartmentInfos");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PrgDepartmentInfos");

            migrationBuilder.RenameTable(
                name: "PrgDepartmentInfos",
                newName: "DepartmentInfos");

            migrationBuilder.RenameIndex(
                name: "IX_PrgDepartmentInfos_DepartmentProgramId",
                table: "DepartmentInfos",
                newName: "IX_DepartmentInfos_DepartmentProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentInfos",
                table: "DepartmentInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentInfos_DepartmentPrograms_DepartmentProgramId",
                table: "DepartmentInfos",
                column: "DepartmentProgramId",
                principalTable: "DepartmentPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
