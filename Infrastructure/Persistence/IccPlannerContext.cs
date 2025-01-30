

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
        public DbSet<Role> Roles { get; set; }  
        public DbSet<Permission> Permissions { get; set; }


        public IccPlannerContext(DbContextOptions<IccPlannerContext> options)
            : base (options)
        {
        } 

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            //// Appliquer toutes les configurations d'entité 
            builder.ApplyConfigurationsFromAssembly(typeof(IccPlannerContext).Assembly);
            base.OnModelCreating(builder);

            // Renommer les tables d'identité
            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        } 
    }
}
