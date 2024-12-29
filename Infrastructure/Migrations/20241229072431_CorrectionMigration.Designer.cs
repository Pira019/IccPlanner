﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(IccPlannerContext))]
    [Migration("20241229072431_CorrectionMigration")]
    partial class CorrectionMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DepartementMember", b =>
                {
                    b.Property<int>("DepartementsId")
                        .HasColumnType("integer");

                    b.Property<Guid>("MembersId")
                        .HasColumnType("uuid");

                    b.HasKey("DepartementsId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("DepartementMember");
                });

            modelBuilder.Entity("DepartementProgram", b =>
                {
                    b.Property<int>("DepartementsId")
                        .HasColumnType("integer");

                    b.Property<int>("ProgramsId")
                        .HasColumnType("integer");

                    b.HasKey("DepartementsId", "ProgramsId");

                    b.HasIndex("ProgramsId");

                    b.ToTable("DepartementProgram");
                });

            modelBuilder.Entity("DepartmentMemberProgramDepartment", b =>
                {
                    b.Property<int>("DepartmentMembersId")
                        .HasColumnType("integer");

                    b.Property<int>("ProgramDepartmentsId")
                        .HasColumnType("integer");

                    b.HasKey("DepartmentMembersId", "ProgramDepartmentsId");

                    b.HasIndex("ProgramDepartmentsId");

                    b.ToTable("DepartmentMemberProgramDepartment");
                });

            modelBuilder.Entity("Domain.Entities.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DepartmentMemberId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer");

                    b.Property<int>("ProgramDepartmentId")
                        .HasColumnType("integer");

                    b.Property<int>("ProgramId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentMemberId");

                    b.HasIndex("ProgramDepartmentId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("Domain.Entities.Departement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MinistryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("shortName")
                        .HasMaxLength(55)
                        .HasColumnType("character varying(55)");

                    b.Property<DateOnly>("startDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("MinistryId");

                    b.ToTable("Departement");
                });

            modelBuilder.Entity("Domain.Entities.DepartmentMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("DateEnty")
                        .HasColumnType("date");

                    b.Property<int>("DepartementId")
                        .HasColumnType("integer");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.Property<string>("NickName")
                        .HasMaxLength(55)
                        .HasColumnType("character varying(55)");

                    b.Property<int>("Staus")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DepartementId");

                    b.HasIndex("MemberId");

                    b.ToTable("DepartmentMembers");
                });

            modelBuilder.Entity("Domain.Entities.FeedBack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DepartmentMemberId")
                        .HasColumnType("integer");

                    b.Property<int>("ProgramDepartmentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("SubmitAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("rating")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentMemberId");

                    b.HasIndex("ProgramDepartmentId");

                    b.ToTable("FeedBacks");
                });

            modelBuilder.Entity("Domain.Entities.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddedById")
                        .HasColumnType("uuid");

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("City")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateOnly?>("EntryDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Quarter")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Sexe")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AddedById");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Domain.Entities.Ministry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Ministries");
                });

            modelBuilder.Entity("Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Domain.Entities.Planning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailabilityId")
                        .HasColumnType("integer");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("MemberName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("PlanningType")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("ProgramDate")
                        .HasColumnType("date");

                    b.Property<Guid>("ProgrammedById")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityId");

                    b.HasIndex("ProgrammedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Plannings");
                });

            modelBuilder.Entity("Domain.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Domain.Entities.Program", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ShortName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Programs");
                });

            modelBuilder.Entity("Domain.Entities.ProgramDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<Guid>("CreateById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DepartementId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Localisation")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("ProgramId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UpdateById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("isRecurring")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CreateById");

                    b.HasIndex("DepartementId");

                    b.HasIndex("ProgramId");

                    b.HasIndex("UpdateById");

                    b.ToTable("ProgramDepartments");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MemberId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DepartementMember", b =>
                {
                    b.HasOne("Domain.Entities.Departement", null)
                        .WithMany()
                        .HasForeignKey("DepartementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DepartementProgram", b =>
                {
                    b.HasOne("Domain.Entities.Departement", null)
                        .WithMany()
                        .HasForeignKey("DepartementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Program", null)
                        .WithMany()
                        .HasForeignKey("ProgramsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DepartmentMemberProgramDepartment", b =>
                {
                    b.HasOne("Domain.Entities.DepartmentMember", null)
                        .WithMany()
                        .HasForeignKey("DepartmentMembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ProgramDepartment", null)
                        .WithMany()
                        .HasForeignKey("ProgramDepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Availability", b =>
                {
                    b.HasOne("Domain.Entities.DepartmentMember", "DepartmentMember")
                        .WithMany("Availabilities")
                        .HasForeignKey("DepartmentMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ProgramDepartment", "ProgramDepartment")
                        .WithMany("Availabilities")
                        .HasForeignKey("ProgramDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartmentMember");

                    b.Navigation("ProgramDepartment");
                });

            modelBuilder.Entity("Domain.Entities.Departement", b =>
                {
                    b.HasOne("Domain.Entities.Ministry", "Ministry")
                        .WithMany("Departements")
                        .HasForeignKey("MinistryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ministry");
                });

            modelBuilder.Entity("Domain.Entities.DepartmentMember", b =>
                {
                    b.HasOne("Domain.Entities.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departement");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Domain.Entities.FeedBack", b =>
                {
                    b.HasOne("Domain.Entities.DepartmentMember", "DepartmentMember")
                        .WithMany("FeedBacks")
                        .HasForeignKey("DepartmentMemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ProgramDepartment", "ProgramDepartment")
                        .WithMany("FeedBacks")
                        .HasForeignKey("ProgramDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DepartmentMember");

                    b.Navigation("ProgramDepartment");
                });

            modelBuilder.Entity("Domain.Entities.Member", b =>
                {
                    b.HasOne("Domain.Entities.Member", "AddedBy")
                        .WithMany()
                        .HasForeignKey("AddedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddedBy");
                });

            modelBuilder.Entity("Domain.Entities.Planning", b =>
                {
                    b.HasOne("Domain.Entities.Availability", "Availability")
                        .WithMany()
                        .HasForeignKey("AvailabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Member", "ProgrammedBy")
                        .WithMany()
                        .HasForeignKey("ProgrammedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Member", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Availability");

                    b.Navigation("ProgrammedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Domain.Entities.ProgramDepartment", b =>
                {
                    b.HasOne("Domain.Entities.Member", "CreateBy")
                        .WithMany()
                        .HasForeignKey("CreateById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Departement", "Departement")
                        .WithMany()
                        .HasForeignKey("DepartementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Program", "Program")
                        .WithMany()
                        .HasForeignKey("ProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Member", "UpdateBy")
                        .WithMany()
                        .HasForeignKey("UpdateById");

                    b.Navigation("CreateBy");

                    b.Navigation("Departement");

                    b.Navigation("Program");

                    b.Navigation("UpdateBy");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Member", "Member")
                        .WithOne("User")
                        .HasForeignKey("Domain.Entities.User", "MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Domain.Entities.DepartmentMember", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("FeedBacks");
                });

            modelBuilder.Entity("Domain.Entities.Member", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Ministry", b =>
                {
                    b.Navigation("Departements");
                });

            modelBuilder.Entity("Domain.Entities.ProgramDepartment", b =>
                {
                    b.Navigation("Availabilities");

                    b.Navigation("FeedBacks");
                });
#pragma warning restore 612, 618
        }
    }
}
