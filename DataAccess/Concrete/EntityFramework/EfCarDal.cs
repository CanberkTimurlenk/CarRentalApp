using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;
using Entities.Concrete.DTOs;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework.Extensions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car>, ICarDal
    {

        public EfCarDal(CarAppContext context) : base(context)
        {

        }
        
        public PagedList<Car> GetAllWithSorting(CarParameters carParamaters, bool trackChanges)
        {
            var query = GetAllAsQueryable(trackChanges).Sort(carParamaters.OrderBy);

            return PagedList<Car>.ToPagedList(query, carParamaters.PageNumber, carParamaters.PageSize);
        }         
        public PagedList<CarDetailDto> GetAllCarDetails(CarParameters carParamaters, bool trackChanges)
        {
            var carDetails = GetAllCarDetailsAsQueryable(carParamaters, trackChanges);

            
            

            return PagedList<CarDetailDto>
                    .ToPagedList(carDetails, carParamaters.PageNumber, carParamaters.PageSize);

            

        }
        public PagedList<CarDetailDto> GetAllCarDetailsByCondition(Expression<Func<CarDetailDto, bool>> filter, CarParameters carParamaters, bool trackChanges)
        {
            var carDetails = GetAllCarDetailsAsQueryable(carParamaters, trackChanges).Where(filter);

            return PagedList<CarDetailDto>
                    .ToPagedList(carDetails, carParamaters.PageNumber, carParamaters.PageSize);

        }
        private IQueryable<CarDetailDto> GetAllCarDetailsAsQueryable(CarParameters carParameters, bool trackChanges)
        {
            var cars = _context.Set<Car>();
            var colors = _context.Set<Color>();
            var brands = _context.Set<Brand>();

            var query = from car in cars
                         join color in colors
                         on car.ColorId equals color.Id
                         join brand in brands
                         on car.BrandId equals brand.Id
                         select new CarDetailDto
                         {
                             CarName = car.CarName,
                             BrandName = brand.BrandName,
                             ColorName = color.ColorName,
                             DailyPrice = car.DailyPrice,

                         };

            return query = !trackChanges ? query.AsNoTracking() : query;
                      
        }

    }
}

