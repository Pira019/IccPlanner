using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    /// <summary>
    ///   Permet de faire la configuration des utilisateur
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(m => m.Status)
                .HasConversion(
                    v => v.ToString().ToUpper(),
                    v => (MemberStatusEnum)Enum.Parse(typeof(MemberStatusEnum), v, true)
                    ) .HasMaxLength(15);
        } 
    }
}
