using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpDateDepartmentMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DepartmentMembers_DepartementId",
                table: "DepartmentMembers");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentMemberId",
                table: "Postes",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DepartmentMembers",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "DepartmentMemberPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentMemberId = table.Column<int>(type: "integer", nullable: false),
                    PosteId = table.Column<int>(type: "integer", nullable: false),
                    StartAt = table.Column<DateOnly>(type: "date", nullable: true),
                    EndAt = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMemberPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentMemberPosts_DepartmentMembers_DepartmentMemberId",
                        column: x => x.DepartmentMemberId,
                        principalTable: "DepartmentMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMemberPosts_Postes_PosteId",
                        column: x => x.PosteId,
                        principalTable: "Postes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postes_DepartmentMemberId",
                table: "Postes",
                column: "DepartmentMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMembers_DepartementId_MemberId",
                table: "DepartmentMembers",
                columns: new[] { "DepartementId", "MemberId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMemberPosts_DepartmentMemberId",
                table: "DepartmentMemberPosts",
                column: "DepartmentMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMemberPosts_PosteId",
                table: "DepartmentMemberPosts",
                column: "PosteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_DepartmentMembers_DepartmentMemberId",
                table: "Postes",
                column: "DepartmentMemberId",
                principalTable: "DepartmentMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postes_DepartmentMembers_DepartmentMemberId",
                table: "Postes");

            migrationBuilder.DropTable(
                name: "DepartmentMemberPosts");

            migrationBuilder.DropIndex(
                name: "IX_Postes_DepartmentMemberId",
                table: "Postes");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentMembers_DepartementId_MemberId",
                table: "DepartmentMembers");

            migrationBuilder.DropColumn(
                name: "DepartmentMemberId",
                table: "Postes");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "DepartmentMembers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMembers_DepartementId",
                table: "DepartmentMembers",
                column: "DepartementId");
        }
    }
}
