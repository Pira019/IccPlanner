using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdPosteTable01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Poste",
                table: "Poste");

            migrationBuilder.RenameTable(
                name: "Poste",
                newName: "Postes");

            migrationBuilder.RenameIndex(
                name: "IX_Poste_Name",
                table: "Postes",
                newName: "IX_Postes_Name");

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Postes",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Postes",
                table: "Postes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Postes",
                table: "Postes");

            migrationBuilder.RenameTable(
                name: "Postes",
                newName: "Poste");

            migrationBuilder.RenameIndex(
                name: "IX_Postes_Name",
                table: "Poste",
                newName: "IX_Poste_Name");

            migrationBuilder.AlterColumn<string>(
                name: "ShortName",
                table: "Poste",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poste",
                table: "Poste",
                column: "Id");
        }
    }
}
