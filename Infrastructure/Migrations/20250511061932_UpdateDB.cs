using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrgDepartmentInfoTabServices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrgDepartmentInfoTabServices",
                columns: table => new
                {
                    PrgDepartmentInfosId = table.Column<int>(type: "integer", nullable: false),
                    TabServicesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrgDepartmentInfoTabServices", x => new { x.PrgDepartmentInfosId, x.TabServicesId });
                    table.ForeignKey(
                        name: "FK_PrgDepartmentInfoTabServices_PrgDepartmentInfos_PrgDepartme~",
                        column: x => x.PrgDepartmentInfosId,
                        principalTable: "PrgDepartmentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrgDepartmentInfoTabServices_TabServices_TabServicesId",
                        column: x => x.TabServicesId,
                        principalTable: "TabServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrgDepartmentInfoTabServices_TabServicesId",
                table: "PrgDepartmentInfoTabServices",
                column: "TabServicesId");
        }
    }
}
