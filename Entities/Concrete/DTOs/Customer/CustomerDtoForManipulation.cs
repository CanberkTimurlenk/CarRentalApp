using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Customer
{
    public record CustomerDtoForManipulation : IDto
    {        
        public int UserId { get; init; }
        public string? CompanyName { get; init; }        

    }
}
