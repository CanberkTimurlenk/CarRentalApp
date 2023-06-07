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
    public class CartItemMap : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(ci => ci.Car)
                   .WithMany(c => c.CartItems)
                   .HasForeignKey(ci => ci.CarId);

            builder.HasOne(ci => ci.Customer)
                   .WithMany(c => c.CartItems)
                   .HasForeignKey(ci => ci.CustomerId);

        }
    }
}
