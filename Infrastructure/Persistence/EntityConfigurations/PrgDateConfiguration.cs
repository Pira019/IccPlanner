using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class PrgDateConfiguration : IEntityTypeConfiguration<PrgDate>
    {
        public void Configure(EntityTypeBuilder<PrgDate> builder)
        {
            builder.HasIndex(x => x.Date);
        }
    }
}
