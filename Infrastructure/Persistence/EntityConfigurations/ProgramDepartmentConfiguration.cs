using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class ProgramDepartmentConfiguration : IEntityTypeConfiguration<DepartmentProgram>
    {
        public void Configure(EntityTypeBuilder<DepartmentProgram> builder)
        {
            builder.HasIndex(x => new { x.ProgramId, x.DepartmentId, x.IndRecurent }).IsUnique();  
        }
    }
}
