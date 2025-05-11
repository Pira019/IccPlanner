using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class TabServiceConfiguration : IEntityTypeConfiguration<TabServices>
    {
        public void Configure(EntityTypeBuilder<TabServices> builder)
        {
            builder.HasIndex(x => new { x.StartTime, x.EndTime, x.DisplayName }).IsUnique();  
        }
    }
}
