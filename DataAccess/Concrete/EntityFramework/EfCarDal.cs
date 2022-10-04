using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarAppContext>, ICarDal
    //  EfCarDal : Entity Framework Data Access Layer
    //  EfEntityRepositoryBase EntityFramework için yaratılan base class..
    //  EntityFramework methodlarını tutar (Repository)
    //  EfCarDal inherits EfEntityRepositoryBase with "Car" and "CarpAppContext" and implements "ICarDal"

    {
        public List<CarDetailDto> GetAllCarDetails()
        {
            using (CarAppContext context = new CarAppContext())
            {
                var result = from car in context.Cars join color in context.Colors
                             on car.ColorId equals color.ColorId
                             join brand in context.Brands
                             on car.BrandId equals brand.BrandId
                             select new CarDetailDto
                             {
                                 CarName = car.CarName,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,

                             };
                // şuan elimizde bir sürü CarDetailDto var 
                return result.ToList();
                //şuan result bir IQuayrable olduğu için onu listeye çevirmemiz gerekli 
            }            
        }
    }
}
