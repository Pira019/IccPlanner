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
            builder.HasIndex(x => x.PhoneNumber).IsUnique();

            builder.Property(m => m.Status)
                .HasConversion(
                    v => v.ToString().ToUpper(),
                    v => (MemberStatus)Enum.Parse(typeof(MemberStatus), v, true)
                    ).HasMaxLength(15);

            builder.HasOne(user => user.Member)
                .WithOne(member => member.User)
                .HasForeignKey<User>(u => u.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
