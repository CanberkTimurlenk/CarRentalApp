using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental,CarAppContext> , IRentalDal
    {
        public IEnumerable<RentalDetailDto> GetAllRentalDetails()
        {
            using (CarAppContext context = new CarAppContext())
            {
                var result = from rental in context.Rentals 
                             join car in context.Cars on rental.CarId equals car.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             join customer in context.Customers on rental.CustomerId equals customer.Id
                             join user in context.Users on customer.userId equals user.Id
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
