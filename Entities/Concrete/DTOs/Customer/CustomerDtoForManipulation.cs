using Core.Entities;

namespace Entities.Concrete.DTOs.Customer
{
    public record CustomerDtoForManipulation : IDto
    {        
        public int UserId { get; set; }
        public string? CompanyName { get; set; }        

    }
}
