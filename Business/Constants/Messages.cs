using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        
        public static readonly string CarAdded = "Car Added.";
        public static readonly string BrandAdded = "Brand Added.";
        public static readonly string ColorAdded = "Color Added.";
        public static readonly string RentalAdded = "Rental Added.";
        public static readonly string UserAdded = "User Added.";
        public static readonly string CustomerAdded = "Customer Added.";

        public static readonly string DescriptionLengthException = "Description must contain at least 2 characters";
        public static readonly string DailyPriceException = "Daily price must be greater than zero";
        public static readonly string DailyPriceDescriptionException = "Description must contain at least 2 characters, daily price must be greater than zero";

        public static readonly string CarUpdated = "Car updated.";
        public static readonly string BrandUpdated = "Brand updated.";
        public static readonly string ColorUpdated = "Color updated.";
        public static readonly string RentalUpdated = "Color updated.";
        public static readonly string UserUpdated = "Color updated.";
        public static readonly string CustomerUpdated = "Customer updated.";


        public static readonly string CarDeleted = "Car deleted.";
        public static readonly string BrandDeleted = "Brand deleted.";
        public static readonly string ColorDeleted = "Color deleted.";
        public static readonly string RentalDeleted = "Rental deleted.";
        public static readonly string UserDeleted = "User deleted.";
        public static readonly string CustomerDeleted = "Customer deleted.";



        public static readonly string CarsListed = "Cars listed.";
        public static readonly string BrandListed = "Brands listed.";
        public static readonly string ColorListed = "Colors listed.";
        public static readonly string RentalsListed = "Rentals listed.";
        public static readonly string UsersListed = "Users listed.";
        public static readonly string CustomersListed = "Customers listed.";


        public static readonly string SuccessListedById = "Object matched with the related Id was fetched.";

        public static readonly string InvalidRentalAdd = "The car have already been rented.";
    }
}
