using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityConfigurations
{
    public class PrgDepartmentInfoConfiguration : IEntityTypeConfiguration<PrgDepartmentInfo>
    {
        public void Configure(EntityTypeBuilder<PrgDepartmentInfo> builder)
        {
          /*  // Conversion de List<DateOnly> en chaîne séparée par des virgules
            builder.Property(p => p.Dates)
             .HasConversion(
                 v => string.Join(",", v.Select(d => d.ToString("yyyy-MM-dd"))),  // Conversion en chaîne de dates
                 v => !string.IsNullOrEmpty(v)
                     ? v.Split(',', StringSplitOptions.RemoveEmptyEntries)  // Ignore les éléments vides
                         .Select(date => DateOnly.Parse(date.Trim())) // Enlever les espaces autour des dates
                         .ToList()
                     : new List<DateOnly>());

            // Conversion de List<string> en chaîne séparée par des virgules
            builder.Property(p => p.Days)
                .HasConversion(
                    v => string.Join(",", v),  // Conversion en chaîne de jours
                    v => !string.IsNullOrEmpty(v)
                        ? v.Split(',', StringSplitOptions.RemoveEmptyEntries)  // Ignore les éléments vides
                            .Select(day => day.Trim())  // Enlever les espaces autour des jours
                            .ToList()
                        : new List<string>());*/
        }
    }
} 
