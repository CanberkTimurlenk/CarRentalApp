using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class ColorMap : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ColorName)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.HasData(
                new Color { Id = 1, ColorName = "Black" },
                new Color { Id = 2, ColorName = "White" },
                new Color { Id = 3, ColorName = "Red" },
                new Color { Id = 4, ColorName = "Blue" }
                );

        }
    }
}
