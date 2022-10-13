using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : Db tabloları ile proje classlarını bağlamaya yarar.

    public class CarAppContext:DbContext
    {

        //override yazıp enter sonrasında boşluk bırakıp onConfiguring enter yapıyoruz
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //  burda veritabanının yerini belirttik.
            
            optionsBuilder.UseSqlServer(@"Server=CANBERK\SQLEXPRESS;Database = CarAppDB;Trusted_Connection=true");  
            
        }

        // hangi nesnemiz veritabanında hangi nesneye karşılık geliyor onu tanımlıyoruz.
        
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CarImage> CarImages { get; set; }



    }
}
