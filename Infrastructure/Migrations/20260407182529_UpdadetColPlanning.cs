using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdadetColPlanning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanningType",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PlanningPeriods");

            migrationBuilder.RenameColumn(
                name: "IsTraining",
                table: "Plannings",
                newName: "IndTraining");

            migrationBuilder.AddColumn<bool>(
                name: "IndObservation",
                table: "Plannings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IndArchived",
                table: "PlanningPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IndPublished",
                table: "PlanningPeriods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IndPlanning",
                table: "DepartmentMembers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndObservation",
                table: "Plannings");

            migrationBuilder.DropColumn(
                name: "IndArchived",
                table: "PlanningPeriods");

            migrationBuilder.DropColumn(
                name: "IndPublished",
                table: "PlanningPeriods");

            migrationBuilder.DropColumn(
                name: "IndPlanning",
                table: "DepartmentMembers");

            migrationBuilder.RenameColumn(
                name: "IndTraining",
                table: "Plannings",
                newName: "IsTraining");

            migrationBuilder.AddColumn<int>(
                name: "PlanningType",
                table: "Plannings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PlanningPeriods",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
