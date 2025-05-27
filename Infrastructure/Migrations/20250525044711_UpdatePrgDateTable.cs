using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePrgDateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TabServicePrgs_PrgDateId",
                table: "TabServicePrgs");

            migrationBuilder.CreateIndex(
                name: "IX_TabServicePrgs_PrgDateId_TabServicesId",
                table: "TabServicePrgs",
                columns: new[] { "PrgDateId", "TabServicesId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TabServicePrgs_PrgDateId_TabServicesId",
                table: "TabServicePrgs");

            migrationBuilder.CreateIndex(
                name: "IX_TabServicePrgs_PrgDateId",
                table: "TabServicePrgs",
                column: "PrgDateId");
        }
    }
}
