using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCartItemDal : EfEntityRepositoryBase<CartItem>, ICartItemDal
    {

        public EfCartItemDal(CarAppContext context) : base(context)
        {

        }

        public PagedList<CartItemDetailDto> GetAllCartItemDetails(CartItemParameters cartItemParameters, bool trackChanges)
        {
            var cartItemDetails = GetAllCartItemDetailsAsQueryable(trackChanges);

            return PagedList<CartItemDetailDto>
                    .ToPagedList(cartItemDetails, cartItemParameters.PageNumber, cartItemParameters.PageSize);

        }
        public PagedList<CartItemDetailDto> GetAllCartItemDetailsByCondition(Expression<Func<CartItemDetailDto, bool>> filter, CartItemParameters cartItemParameters, bool trackChanges)
        {
            var cartItemDetails = GetAllCartItemDetailsAsQueryable(trackChanges).Where(filter);

            return PagedList<CartItemDetailDto>
                    .ToPagedList(cartItemDetails, cartItemParameters.PageNumber, cartItemParameters.PageSize);
        }
        private IQueryable<CartItemDetailDto> GetAllCartItemDetailsAsQueryable(bool trackChanges)
        {
            var cartItems = _context.Set<CartItem>();
            var customers = _context.Set<Customer>();
            var cars = _context.Set<Car>();

            var query = from cartItem in cartItems

                        join customer in customers
                        on cartItem.CustomerId equals customer.Id

                        join car in cars
                        on cartItem.CarId equals car.Id

                        select new CartItemDetailDto
                        {
                            CarId = car.Id,
                            CustomerId = customer.Id,
                            TotalAmount = cartItem.TotalAmount
                        };

            return !trackChanges ? query.AsNoTracking() : query;

        }
    }
}

