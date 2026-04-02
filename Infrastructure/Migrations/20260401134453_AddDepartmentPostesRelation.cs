using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentPostesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentPostes",
                columns: table => new
                {
                    DepartmentsId = table.Column<int>(type: "integer", nullable: false),
                    PostesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentPostes", x => new { x.DepartmentsId, x.PostesId });
                    table.ForeignKey(
                        name: "FK_DepartmentPostes_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentPostes_Postes_PostesId",
                        column: x => x.PostesId,
                        principalTable: "Postes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentPostes_PostesId",
                table: "DepartmentPostes",
                column: "PostesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentPostes");
        }
    }
}
