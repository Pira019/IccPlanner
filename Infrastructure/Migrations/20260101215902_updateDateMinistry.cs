using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDateMinistry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "MinistryId",
                table: "Departments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments",
                column: "MinistryId",
                principalTable: "Ministries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "MinistryId",
                table: "Departments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Ministries_MinistryId",
                table: "Departments",
                column: "MinistryId",
                principalTable: "Ministries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
