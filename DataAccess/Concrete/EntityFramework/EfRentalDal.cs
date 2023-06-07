using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental,CarAppContext> , IRentalDal
    {
        public EfRentalDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {

        }
        public IEnumerable<RentalDetailDto> GetAllRentalDetails()
        {
            using (CarAppContext context = new CarAppContext())
            {
                var result = from rental in context.Rentals 
                             join car in context.Cars on rental.Id equals car.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join customer in context.Customers on rental.CustomerId equals customer.Id
                             join user in context.Users on customer.Id equals user.Id
                             select new RentalDetailDto(){

                                 Id = rental.Id,
                                 BrandName = brand.BrandName,
                                 CustomerFirstName = user.FirstName,
                                 CustomerLastName = user.LastName,                                 
                                 RentDate = rental.RentDate,
                                 ReturnDate = rental.ReturnDate
                                 

                             };



                return result.ToList();


            }
            
           

        }

    
    }
}
