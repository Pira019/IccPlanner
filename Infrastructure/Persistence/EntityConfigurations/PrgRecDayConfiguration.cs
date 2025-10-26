using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class PrgRecDayConfiguration : IEntityTypeConfiguration<PrgRecDay>
    {
        public void Configure(EntityTypeBuilder<PrgRecDay> builder)
        {
            builder.HasIndex(x => new { x.Day, x.PrgDepartmentInfoId }).IsUnique();
        }
    }
}
