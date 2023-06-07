﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCartItemDal : EfEntityRepositoryBase<CartItem, CarAppContext>, ICartItemDal
    {
        public EfCartItemDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {

        }
        
        public IEnumerable<CartItemDetailDto> GetAllCartItemDetails(Expression<Func<CartItemDetailDto,bool>>filter = null)
        {
            
            
            using (CarAppContext context = new CarAppContext())
            {
                var result = from cartItem in context.CartItems

                             join customer in context.Customers
                             on cartItem.CustomerId equals customer.Id

                             join car in context.Cars
                             on cartItem.CarId equals car.Id

                             select new CartItemDetailDto
                             {
                                 CarId = car.Id,
                                 CustomerId = customer.Id,
                                 TotalAmount = cartItem.TotalAmount
                             };



                return result.ToList();
            } 
         

    }
          
    }
}
