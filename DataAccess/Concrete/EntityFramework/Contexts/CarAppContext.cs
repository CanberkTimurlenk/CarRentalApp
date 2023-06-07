using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts.Extensions;
using Entities.Concrete.Models;

namespace DataAccess.Concrete.EntityFramework.Contexts
{

    public class CarAppContext : DbContext
    {
        public CarAppContext(DbContextOptions options)
            : base(options)
        {

        }

        public CarAppContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllEntityConfigurations();
        }

        public DbSet<Car>? Cars { get; set; }
        public DbSet<Brand>? Brands { get; set; }
        public DbSet<Color>? Colors { get; set; }
        public DbSet<Rental>? Rentals { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<CarImage>? CarImages { get; set; }
        public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }
        public DbSet<OperationClaim>? OperationClaims { get; set; }
        public DbSet<CartItem>? CartItems { get; set; }



    }
}
