using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabServices_PrgDates_PrgDateId",
                table: "TabServices");

            migrationBuilder.DropIndex(
                name: "IX_TabServices_PrgDateId",
                table: "TabServices");

            migrationBuilder.DropColumn(
                name: "PrgDateId",
                table: "TabServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrgDateId",
                table: "TabServices",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TabServices_PrgDateId",
                table: "TabServices",
                column: "PrgDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_TabServices_PrgDates_PrgDateId",
                table: "TabServices",
                column: "PrgDateId",
                principalTable: "PrgDates",
                principalColumn: "Id");
        }
    }
}
