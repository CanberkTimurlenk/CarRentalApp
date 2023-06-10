using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;
using Entities.Concrete.DTOs;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarAppContext,CarParameters>, ICarDal
    {
        private readonly IDesignTimeDbContextFactory<CarAppContext> _contextFactory;
        public EfCarDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public PagedList<CarDetailDto> GetAllCarDetails(CarParameters carParameters)
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

                return PagedList<CarDetailDto>.ToPagedList(
                    result, carParameters.PageNumber, carParameters.PageSize
                    );
            }

        }
       
    }
}
