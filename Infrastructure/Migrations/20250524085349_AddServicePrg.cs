using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddServicePrg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicePrgDepartments_PrgDepartmentInfos_PrgDepartmentInfoId",
                table: "ServicePrgDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServicePrgDepartments_TabServices_TabServicesId",
                table: "ServicePrgDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicePrgDepartments",
                table: "ServicePrgDepartments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ServicePrgDepartments");

            migrationBuilder.RenameTable(
                name: "ServicePrgDepartments",
                newName: "TabServicePrgs");

            migrationBuilder.RenameColumn(
                name: "PrgDepartmentInfoId",
                table: "TabServicePrgs",
                newName: "PrgDateId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicePrgDepartments_TabServicesId",
                table: "TabServicePrgs",
                newName: "IX_TabServicePrgs_TabServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_ServicePrgDepartments_PrgDepartmentInfoId",
                table: "TabServicePrgs",
                newName: "IX_TabServicePrgs_PrgDateId");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "PrgDates",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Days",
                table: "TabServicePrgs",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TabServicePrgs",
                table: "TabServicePrgs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TabServicePrgs_PrgDates_PrgDateId",
                table: "TabServicePrgs",
                column: "PrgDateId",
                principalTable: "PrgDates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TabServicePrgs_TabServices_TabServicesId",
                table: "TabServicePrgs",
                column: "TabServicesId",
                principalTable: "TabServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TabServicePrgs_PrgDates_PrgDateId",
                table: "TabServicePrgs");

            migrationBuilder.DropForeignKey(
                name: "FK_TabServicePrgs_TabServices_TabServicesId",
                table: "TabServicePrgs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TabServicePrgs",
                table: "TabServicePrgs");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "PrgDates");

            migrationBuilder.RenameTable(
                name: "TabServicePrgs",
                newName: "ServicePrgDepartments");

            migrationBuilder.RenameColumn(
                name: "PrgDateId",
                table: "ServicePrgDepartments",
                newName: "PrgDepartmentInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_TabServicePrgs_TabServicesId",
                table: "ServicePrgDepartments",
                newName: "IX_ServicePrgDepartments_TabServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_TabServicePrgs_PrgDateId",
                table: "ServicePrgDepartments",
                newName: "IX_ServicePrgDepartments_PrgDepartmentInfoId");

            migrationBuilder.AlterColumn<string>(
                name: "Days",
                table: "ServicePrgDepartments",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "ServicePrgDepartments",
                type: "date",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicePrgDepartments",
                table: "ServicePrgDepartments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePrgDepartments_PrgDepartmentInfos_PrgDepartmentInfoId",
                table: "ServicePrgDepartments",
                column: "PrgDepartmentInfoId",
                principalTable: "PrgDepartmentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServicePrgDepartments_TabServices_TabServicesId",
                table: "ServicePrgDepartments",
                column: "TabServicesId",
                principalTable: "TabServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
