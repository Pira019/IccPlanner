using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class PosteConfiguration : IEntityTypeConfiguration<Poste>
    {
        public void Configure(EntityTypeBuilder<Poste> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
