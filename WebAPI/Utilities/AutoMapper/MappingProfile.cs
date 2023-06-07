using AutoMapper;
using Entities.Concrete.DTOs.Brand;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.DTOs.CarImage;
using Entities.Concrete.DTOs.CartItem;
using Entities.Concrete.DTOs.Color;
using Entities.Concrete.DTOs.Customer;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.Rental;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.DTOs.UserOperationClaim;
using Entities.Concrete.Models;

namespace WebAPI.Utilities.AutoMapper
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<CarDtoForManipulation, Car>();

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<BrandDtoForManipulation, Brand>();

            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<ColorDtoForManipulation, Color>();

            CreateMap<CarImage, CarImageDto>().ReverseMap();
            CreateMap<CarImageDtoForManipulation, CarImage>();

            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<CartItemDtoForManipulation, CartItem>();

            CreateMap<Rental, RentalDto>().ReverseMap();
            CreateMap<RentalDtoForManipulation, Rental>();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerDtoForManipulation, Customer>();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserDtoForManipulation, User>();

            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
            CreateMap<OperationClaimDtoForManipulation, OperationClaim>();

            CreateMap<UserOperationClaim, UserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaimDtoForManipulation, UserOperationClaim>();

        }
    }
}

