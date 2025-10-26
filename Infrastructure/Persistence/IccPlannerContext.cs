


// Ignore Spelling: Prg Prgs

using System.Reflection.Emit;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence
{
    public class IccPlannerContext : IdentityDbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Ministry> Ministries { get; set; }
        public DbSet<DepartmentMemberPost> DepartmentMemberPosts { get; set; }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PrgDate> PrgDates { get; set; }

        /// <summary>
        /// Detail de programme dans un département
        /// </summary>
        public DbSet<PrgDepartmentInfo> PrgDepartmentInfos { get; set; }
        public DbSet<Program> Programs { get; set; }
#pragma warning disable CS0114 // Un membre masque un membre hérité ; le mot clé override est manquant
        public DbSet<User> Users { get; set; }
#pragma warning restore CS0114 // Un membre masque un membre hérité ; le mot clé override est manquant
        public DbSet<DepartmentProgram> DepartmentPrograms { get; set; }
        public DbSet<DepartmentMember> DepartmentMembers { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Planning> Plannings { get; set; }
#pragma warning disable CS0114 // Un membre masque un membre hérité ; le mot clé override est manquant
        public DbSet<Role> Roles { get; set; }
#pragma warning restore CS0114 // Un membre masque un membre hérité ; le mot clé override est manquant
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<TabServices> TabServices { get; set; }
        public DbSet<TabServicePrg> TabServicePrgs { get; set; }

        /// <summary>
        ///     Ress
        /// </summary>
        public DbSet<RessDay> RessDays { get; set; }

        /// <summary>
        ///    Table programmes recuurents
        /// </summary>
        public DbSet<PrgRecDay> PrgRecDays { get; set; }


        public IccPlannerContext(DbContextOptions<IccPlannerContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //// Appliquer toutes les configurations t'entité 
            builder.ApplyConfigurationsFromAssembly(typeof(IccPlannerContext).Assembly);
            base.OnModelCreating(builder);

            

            //map ProgmamDepartments
            builder.Entity<Program>()
               .HasMany(p => p.Departments)
               .WithMany(d => d.Programs)
               .UsingEntity<DepartmentProgram>(
                    r => r.HasOne<Department>(d => d.Department).WithMany(dp => dp.DepartmentPrograms).HasForeignKey(dp => dp.DepartmentId),
                    l => l.HasOne<Program>(p => p.Program).WithMany(dp => dp.ProgramDepartments).HasForeignKey(dp => dp.ProgramId)
                );

            //map Departments
            builder.Entity<Member>()
               .HasMany(p => p.Departments)
               .WithMany(d => d.Members)
               .UsingEntity<DepartmentMember>(
                    r => r.HasOne<Department>(d => d.Department).WithMany(dm => dm.DepartmentMembers).HasForeignKey(d => d.DepartmentId),
                    l => l.HasOne<Member>(d => d.Member).WithMany(dm => dm.DepartmentMembers).HasForeignKey(d => d.MemberId)
                );

            //map Departments
            builder.Entity<TabServicePrg>()
               .HasMany(d => d.DepartmentMembers)
               .WithMany(t => t.TabServicePrgs)
               .UsingEntity<Availability>(
                    r => r.HasOne<DepartmentMember>(d => d.DepartmentMember).WithMany(dm => dm.Availabilities).HasForeignKey(a => a.DepartmentMemberId),
                    l => l.HasOne<TabServicePrg>(t => t.TabServicePrg).WithMany(a => a.Availabilities).HasForeignKey(a => a.TabServicePrgId)
                );

            // Renommer les tables t'identité
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>()
            .ToTable("UserRoles")               // Utiliser la table existante
            .HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}
