

using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class IccPlannerContext : IdentityDbContext
    {
        public DbSet<Member> Members { get; set; } 
        public DbSet<Ministry> Ministries { get; set; } 
        public DbSet<Program> Programs { get; set; } 
        public DbSet<User> Users { get; set; } 
        public DbSet<ProgramDepartment> ProgramDepartments { get; set; } 
        public DbSet<DepartmentMember> DepartmentMembers { get; set; } 
        public DbSet<FeedBack> FeedBacks { get; set; } 
        public DbSet<Availability> Availabilities { get; set; } 
        public DbSet<Planning> Plannings { get; set; }  
        public DbSet<Role> Profiles { get; set; }  
        public DbSet<Permission> Permissions { get; set; }


        public IccPlannerContext(DbContextOptions<IccPlannerContext> options)
            : base (options)
        {
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            //// Appliquer toutes les configurations d'entité 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IccPlannerContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // Renommer les tables d'identité
            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        } 
    }
}
