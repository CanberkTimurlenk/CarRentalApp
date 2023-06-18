using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.EntityFramework.Extensions;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentalParameters>, IRentalDal
    {
        public EfRentalDal(CarAppContext context) : base(context)
        {

        }

        public PagedList<Rental> GetAllWithSorting(RentalParameters rentalParameters, bool trackChanges)
        {
            var query = GetAllAsQueryable(trackChanges).Sort(rentalParameters.OrderBy);

            return PagedList<Rental>.ToPagedList(query, rentalParameters.PageNumber, rentalParameters.PageSize);
        }
        public PagedList<RentalDetailDto> GetAllRentalDetails(RentalParameters rentalParameters, bool trackChanges)
        {
            var rentalDetails = GetAllRentalDetailsAsQueryable(rentalParameters, trackChanges);

            return PagedList<RentalDetailDto>
                    .ToPagedList(rentalDetails, rentalParameters.PageNumber, rentalParameters.PageSize);

        }
        public PagedList<RentalDetailDto> GetAllRentalDetailsByCondition(Expression<Func<RentalDetailDto, bool>> filter, RentalParameters rentalParameters, bool trackChanges)
        {
            var rentalDetails = GetAllRentalDetailsAsQueryable(rentalParameters, trackChanges).Where(filter);

            return PagedList<RentalDetailDto>
                    .ToPagedList(rentalDetails, rentalParameters.PageNumber, rentalParameters.PageSize);
        }
        private IQueryable<RentalDetailDto> GetAllRentalDetailsAsQueryable(RentalParameters rentalParameters, bool trackChanges)
        {
            var rentals = _context.Set<Rental>();
            var brands = _context.Set<Brand>();
            var customers = _context.Set<Customer>();
            var cars = _context.Set<Car>();
            var users = _context.Set<User>();


            var query = from rental in rentals
                        join car in cars on rental.Id equals car.Id
                        join brand in brands on car.BrandId equals brand.Id
                        join customer in customers on rental.CustomerId equals customer.Id
                        join user in users on customer.Id equals user.Id
                        select new RentalDetailDto()
                        {

                            Id = rental.Id,
                            BrandName = brand.BrandName,
                            CustomerFirstName = user.FirstName,
                            CustomerLastName = user.LastName,
                            RentDate = rental.RentDate,
                            ReturnDate = rental.ReturnDate

                        };

            return !trackChanges ? query.AsNoTracking() : query;

        }

    }
}


