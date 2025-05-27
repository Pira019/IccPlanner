// Ignore Spelling: Prg

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class TabServicePrgConfiguration : IEntityTypeConfiguration<TabServicePrg>
    {
        public void Configure(EntityTypeBuilder<TabServicePrg> builder)
        {
            builder.HasIndex(x => new { x.PrgDateId, x.TabServicesId }).IsUnique();
        }
    }
}
