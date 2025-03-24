using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class DepartmentMemberConfiguration : IEntityTypeConfiguration<DepartmentMember>
    {
        public void Configure(EntityTypeBuilder<DepartmentMember> builder)
        {
            builder.Property(m => m.Status)
               .HasConversion(
                   v => v.ToString().ToUpper(),
                   v => (MemberStatus)Enum.Parse(typeof(MemberStatus), v, true)
                   ).HasMaxLength(15);

            builder.HasIndex(x => new {  x.DepartmentId, x.MemberId}).IsUnique();
        }
    }
}
