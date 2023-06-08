using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public static readonly string CarImageAdded = "CarImage Added.";
        public static readonly string CartItemAdded = "CartItem Added.";

        public static readonly string DescriptionLengthException = "Description must contain at least 2 characters";
        public static readonly string DailyPriceException = "Daily price must be greater than zero";
        public static readonly string DailyPriceDescriptionException = "Description must contain at least 2 characters, daily price must be greater than zero";

        public static readonly string CarUpdated = "Car updated.";
        public static readonly string BrandUpdated = "Brand updated.";
        public static readonly string ColorUpdated = "Color updated.";
        public static readonly string RentalUpdated = "Color updated.";
        public static readonly string UserUpdated = "Color updated.";
        public static readonly string CustomerUpdated = "Customer updated.";
        public static readonly string CarImageUpdated = "CarImage updated.";


        public static readonly string CarDeleted = "Car deleted.";
        public static readonly string BrandDeleted = "Brand deleted.";
        public static readonly string ColorDeleted = "Color deleted.";
        public static readonly string RentalDeleted = "Rental deleted.";
        public static readonly string UserDeleted = "User deleted.";
        public static readonly string CustomerDeleted = "Customer deleted.";
        public static readonly string CarImageDeleted = "CarImage deleted.";



        public static readonly string CarsListed = "Cars listed.";
        public static readonly string BrandsListed = "Brands listed.";
        public static readonly string ColorsListed = "Colors listed.";
        public static readonly string RentalsListed = "Rentals listed.";
        public static readonly string UsersListed = "Users listed.";
        public static readonly string CustomersListed = "Customers listed.";
        public static readonly string CarImagesListed = "CarImages listed.";
        public static readonly string CartItemsListed = "CartItems listed.";

        public static readonly string EmptyImage = "Since there is not such an image, default image will be displayed.";




        public static readonly string SuccessListedById = "Object matched with the related Id was fetched.";

        public static readonly string InvalidRentalAdd = "The car have already been rented.";

        public static readonly string CarImageLimitExceed = "Image limit exceed for the car. You could only add 5 image per Car. ";
        
        public static readonly string SuccessListedByCarId = "Images listed by carId";
        public static readonly string UserOperationClaimNotFound = "User does not have an operation Claim";
        public static readonly string DoesNotMatchAnUserAccount = "Does not match with an user account.";
        public static readonly string UserNotExist ="User does not exist";
        public static readonly string CartItemNotExist = "Cart item does not exist.";


        public static readonly string UserExists ="User exists";
        public static readonly string WrongPassword = "Wrong Password";
        public static readonly string SuccessfullLogin = "Correct Password, login successfully";
        public static readonly string UserRegistered = "User registered successfully";
        public static readonly string AccessTokenCreated = "Access Token Created.";
        public static readonly string AuthorizationDenied ="Authorization denied.";
        public static readonly string SuccessListedRentals = "Rentals listed.";
    }
}
