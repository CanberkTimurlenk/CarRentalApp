using Core.Entities;

namespace Entities.Concrete.DTOs.Customer
{
    public record CustomerDto :IDto
    {
        public int Id { get; init; }        
        public int UserId { get; init; }
        public string? CompanyName { get; init; }

    }
}
