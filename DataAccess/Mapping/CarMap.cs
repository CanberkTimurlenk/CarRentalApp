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
    public class CarMap : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CarName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(c => c.CarName)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(c => c.ModelYear)
                   .IsRequired();

            builder.Property(c => c.DailyPrice)
                   .IsRequired();

            builder.HasOne(c => c.Brand)
                   .WithMany(b => b.Cars)
                   .HasForeignKey(c => c.BrandId);

            builder.HasOne(c => c.Color)
                   .WithMany(clr => clr.Cars)
                   .HasForeignKey(c => c.BrandId);



            //  for testing purposes
            builder.HasData(
                new Car { Id = 1, CarName = "Car 1", BrandId = 1, DailyPrice = 200, ColorId = 1, ModelYear = 1996, Description = "desc" },
                new Car { Id = 2, CarName = "Car 2", BrandId = 2, DailyPrice = 1225, ColorId = 2, ModelYear = 2005, Description = "desc" },
                new Car { Id = 3, CarName = "Car 3", BrandId = 3, DailyPrice = 3200, ColorId = 3, ModelYear = 2020, Description = "desc" },
                new Car { Id = 4, CarName = "Car 4", BrandId = 4, DailyPrice = 150, ColorId = 4, ModelYear = 1992, Description = "desc" },
                new Car { Id = 5, CarName = "Car 5", BrandId = 1, DailyPrice = 800, ColorId = 3, ModelYear = 2003, Description = "desc" }
                );
        }
    }
}
