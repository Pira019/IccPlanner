

using Domain.Entities;
using Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class IccPlannerContext : DbContext
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
        public DbSet<Profile> Profiles { get; set; }  
        public DbSet<Permission> Permissions { get; set; }


        public IccPlannerContext(DbContextOptions<IccPlannerContext> options)
            : base (options)
        {
        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Appliquer toutes les configurations d'entité 
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(IccPlannerContext).Assembly); 
        } 
    }
}
