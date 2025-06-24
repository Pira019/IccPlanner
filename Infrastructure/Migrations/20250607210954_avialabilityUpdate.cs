using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class avialabilityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentMemberTabServicePrg",
                columns: table => new
                {
                    DepartmentMembersId = table.Column<int>(type: "integer", nullable: false),
                    TabServicePrgId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMemberTabServicePrg", x => new { x.DepartmentMembersId, x.TabServicePrgId });
                    table.ForeignKey(
                        name: "FK_DepartmentMemberTabServicePrg_DepartmentMembers_DepartmentM~",
                        column: x => x.DepartmentMembersId,
                        principalTable: "DepartmentMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMemberTabServicePrg_TabServicePrgs_TabServicePrgId",
                        column: x => x.TabServicePrgId,
                        principalTable: "TabServicePrgs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMemberTabServicePrg_TabServicePrgId",
                table: "DepartmentMemberTabServicePrg",
                column: "TabServicePrgId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentMemberTabServicePrg");
        }
    }
}
