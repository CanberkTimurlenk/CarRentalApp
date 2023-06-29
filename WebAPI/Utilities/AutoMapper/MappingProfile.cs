using AutoMapper;
using Core.Entities.Concrete.DTOs.User;
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
            CreateMap<CarForManipulationDto, Car>();

            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<BrandForManipulationDto, Brand>();

            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<ColorForManipulationDto, Color>();

            CreateMap<CarImage, CarImageDto>().ReverseMap();
            CreateMap<CarImageForManipulationDto, CarImage>();

            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<CartItemForManipulationDto, CartItem>();

            CreateMap<Rental, RentalDto>().ReverseMap();
            CreateMap<RentalForManipulationDto, Rental>();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CustomerForManipulationDto, Customer>();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserForManipulationDto, User>().ReverseMap();

            CreateMap<OperationClaim, OperationClaimDto>().ReverseMap();
            CreateMap<OperationClaimDtoForManipulation, OperationClaim>();

            CreateMap<UserOperationClaim, UserOperationClaimDto>().ReverseMap();
            CreateMap<UserOperationClaimDtoForManipulation, UserOperationClaim>();

            CreateMap<UserForManipulationDto, UserDto>();

        }
    }
}

