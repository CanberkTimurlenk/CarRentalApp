using Entities.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mapping
{
    public class CarImageMap : IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Date)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(ci => ci.Car)
                   .WithMany(c => c.CarImages)
                   .HasForeignKey(ci => ci.CarId);

        }
    }
}
