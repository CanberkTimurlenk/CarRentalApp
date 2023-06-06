using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Mapping
{
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BrandName)
                   .IsRequired()
                   .HasMaxLength(20);

            //  for testing purposes
            builder.HasData(
                new Brand { Id = 1, BrandName = "Toyota" },
                new Brand { Id = 2, BrandName = "Mercedes-Benz" },
                new Brand { Id = 3, BrandName = "Renault" },
                new Brand { Id = 4, BrandName = "Kia" }
                );

        }
    }
}
