using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete.DTOs;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Entities.Concrete.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarAppContext>, ICarDal
    {
        private readonly IDesignTimeDbContextFactory<CarAppContext> _contextFactory;
        public EfCarDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<CarDetailDto> GetAllCarDetails()
        {
            using (var context = _contextFactory.CreateDbContext(new string[0]))
            {
                var result = from car in context.Cars
                             join color in context.Colors
                             on car.ColorId equals color.Id
                             join brand in context.Brands
                             on car.BrandId equals brand.Id
                             select new CarDetailDto
                             {
                                 CarName = car.CarName,
                                 BrandName = brand.BrandName,
                                 ColorName = color.ColorName,
                                 DailyPrice = car.DailyPrice,

                             };
                return result.ToList();
            }
        }
    }
}
