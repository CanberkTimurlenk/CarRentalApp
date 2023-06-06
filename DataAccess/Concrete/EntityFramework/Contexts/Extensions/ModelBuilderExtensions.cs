using DataAccess.Mapping;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess.Concrete.EntityFramework.Contexts.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllEntityConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandMap());
            modelBuilder.ApplyConfiguration(new CarMap());
            modelBuilder.ApplyConfiguration(new CarImageMap());
            modelBuilder.ApplyConfiguration(new CartItemMap());
            modelBuilder.ApplyConfiguration(new ColorMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new OperationClaimMap());
            modelBuilder.ApplyConfiguration(new RentalMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserOperationClaimMap());


        }

    }
}
