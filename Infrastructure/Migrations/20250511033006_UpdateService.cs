using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "TabServices",
                type: "character varying(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_TabServices_StartTime_EndTime_DisplayName",
                table: "TabServices",
                columns: new[] { "StartTime", "EndTime", "DisplayName" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TabServices_StartTime_EndTime_DisplayName",
                table: "TabServices");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "TabServices",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(55)",
                oldMaxLength: 55);
        }
    }
}
