using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init01Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AddedById = table.Column<Guid>(type: "uuid", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Sexe = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Tel = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    Quarter = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_Members_AddedById",
                        column: x => x.AddedById,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ministries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ministries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MinistryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    shortName = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departement_Ministries_MinistryId",
                        column: x => x.MinistryId,
                        principalTable: "Ministries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartementMember",
                columns: table => new
                {
                    DepartementsId = table.Column<int>(type: "integer", nullable: false),
                    MembersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartementMember", x => new { x.DepartementsId, x.MembersId });
                    table.ForeignKey(
                        name: "FK_DepartementMember_Departement_DepartementsId",
                        column: x => x.DepartementsId,
                        principalTable: "Departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartementMember_Members_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartementProgram",
                columns: table => new
                {
                    DepartementsId = table.Column<int>(type: "integer", nullable: false),
                    ProgramsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartementProgram", x => new { x.DepartementsId, x.ProgramsId });
                    table.ForeignKey(
                        name: "FK_DepartementProgram_Departement_DepartementsId",
                        column: x => x.DepartementsId,
                        principalTable: "Departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartementProgram_Programs_ProgramsId",
                        column: x => x.ProgramsId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartementId = table.Column<int>(type: "integer", nullable: false),
                    NickName = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: true),
                    DateEntry = table.Column<DateOnly>(type: "date", nullable: true),
                    Staus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentMembers_Departement_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartementId = table.Column<int>(type: "integer", nullable: false),
                    ProgramId = table.Column<int>(type: "integer", nullable: false),
                    StartAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Localisation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreateById = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateById = table.Column<Guid>(type: "uuid", nullable: true),
                    isRecurring = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgramDepartments_Departement_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramDepartments_Members_CreateById",
                        column: x => x.CreateById,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramDepartments_Members_UpdateById",
                        column: x => x.UpdateById,
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProgramDepartments_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramId = table.Column<int>(type: "integer", nullable: false),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    ProgramDepartmentId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentMemberId = table.Column<int>(type: "integer", nullable: false),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availabilities_DepartmentMembers_DepartmentMemberId",
                        column: x => x.DepartmentMemberId,
                        principalTable: "DepartmentMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Availabilities_ProgramDepartments_ProgramDepartmentId",
                        column: x => x.ProgramDepartmentId,
                        principalTable: "ProgramDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentMemberProgramDepartment",
                columns: table => new
                {
                    DepartmentMembersId = table.Column<int>(type: "integer", nullable: false),
                    ProgramDepartmentsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMemberProgramDepartment", x => new { x.DepartmentMembersId, x.ProgramDepartmentsId });
                    table.ForeignKey(
                        name: "FK_DepartmentMemberProgramDepartment_DepartmentMembers_Departm~",
                        column: x => x.DepartmentMembersId,
                        principalTable: "DepartmentMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMemberProgramDepartment_ProgramDepartments_Progra~",
                        column: x => x.ProgramDepartmentsId,
                        principalTable: "ProgramDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedBacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentMemberId = table.Column<int>(type: "integer", nullable: false),
                    ProgramDepartmentId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: true),
                    SubmitAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedBacks_DepartmentMembers_DepartmentMemberId",
                        column: x => x.DepartmentMemberId,
                        principalTable: "DepartmentMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedBacks_ProgramDepartments_ProgramDepartmentId",
                        column: x => x.ProgramDepartmentId,
                        principalTable: "ProgramDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plannings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AvailabilityId = table.Column<int>(type: "integer", nullable: false),
                    MemberName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProgramDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ProgrammedById = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    PlanningType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plannings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plannings_Availabilities_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "Availabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plannings_Members_ProgrammedById",
                        column: x => x.ProgrammedById,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plannings_Members_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_DepartmentMemberId",
                table: "Availabilities",
                column: "DepartmentMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_ProgramDepartmentId",
                table: "Availabilities",
                column: "ProgramDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departement_MinistryId",
                table: "Departement",
                column: "MinistryId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartementMember_MembersId",
                table: "DepartementMember",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartementProgram_ProgramsId",
                table: "DepartementProgram",
                column: "ProgramsId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMemberProgramDepartment_ProgramDepartmentsId",
                table: "DepartmentMemberProgramDepartment",
                column: "ProgramDepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMembers_DepartementId",
                table: "DepartmentMembers",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMembers_MemberId",
                table: "DepartmentMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_DepartmentMemberId",
                table: "FeedBacks",
                column: "DepartmentMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_ProgramDepartmentId",
                table: "FeedBacks",
                column: "ProgramDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_AddedById",
                table: "Members",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_AvailabilityId",
                table: "Plannings",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_ProgrammedById",
                table: "Plannings",
                column: "ProgrammedById");

            migrationBuilder.CreateIndex(
                name: "IX_Plannings_UpdatedById",
                table: "Plannings",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_Name",
                table: "Profiles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDepartments_CreateById",
                table: "ProgramDepartments",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDepartments_DepartementId",
                table: "ProgramDepartments",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDepartments_ProgramId",
                table: "ProgramDepartments",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDepartments_UpdateById",
                table: "ProgramDepartments",
                column: "UpdateById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_MemberId",
                table: "Users",
                column: "MemberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartementMember");

            migrationBuilder.DropTable(
                name: "DepartementProgram");

            migrationBuilder.DropTable(
                name: "DepartmentMemberProgramDepartment");

            migrationBuilder.DropTable(
                name: "FeedBacks");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Plannings");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "DepartmentMembers");

            migrationBuilder.DropTable(
                name: "ProgramDepartments");

            migrationBuilder.DropTable(
                name: "Departement");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Ministries");
        }
    }
}
