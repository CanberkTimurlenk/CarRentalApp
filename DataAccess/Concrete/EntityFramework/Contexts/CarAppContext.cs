using Microsoft.EntityFrameworkCore;
using Entities.Concrete.Models;
using System.Reflection;

namespace DataAccess.Concrete.EntityFramework.Contexts
{

    public class CarAppContext : DbContext
    {
        public CarAppContext(DbContextOptions options)
            : base(options)
        {

        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
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
