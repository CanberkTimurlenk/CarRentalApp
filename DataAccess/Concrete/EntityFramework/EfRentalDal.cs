﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental,CarAppContext,RentalParameters> , IRentalDal
    {
        private readonly IDesignTimeDbContextFactory<CarAppContext> _contextFactory;
        public EfRentalDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public PagedList<RentalDetailDto> GetAllRentalDetails(RentalParameters rentalParameters)
        {
            using (var context = _contextFactory.CreateDbContext(new string[0]))
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

                return PagedList<RentalDetailDto>.ToPagedList(result, rentalParameters.PageNumber, rentalParameters.PageSize);

            }
            
           

        }

    
    }
}
