using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenommerTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUtilisateur");

            migrationBuilder.CreateTable(
                name: "RoleUsers",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsers", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUsers_Profiles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsers_UsersId",
                table: "RoleUsers",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUsers");

            migrationBuilder.CreateTable(
                name: "RoleUtilisateur",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUtilisateur", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUtilisateur_Profiles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUtilisateur_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUtilisateur_UsersId",
                table: "RoleUtilisateur",
                column: "UsersId");
        }
    }
}
