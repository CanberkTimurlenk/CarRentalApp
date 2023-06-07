using Core.Entities.Concrete;
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
    public class RentalMap : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.RentDate)
                   .IsRequired()
                   .HasDefaultValueSql("getdate()");

            builder.HasOne(r => r.Car)
                   .WithMany(c => c.Rentals)
                   .HasForeignKey(r => r.CarId);

            builder.HasOne(r => r.Customer)
                   .WithMany(c => c.Rentals)
                   .HasForeignKey(r => r.CustomerId);

        }
    }
}
