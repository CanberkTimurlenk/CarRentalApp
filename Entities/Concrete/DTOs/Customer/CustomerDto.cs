using Core.Entities;

namespace Entities.Concrete.DTOs.Customer
{
    public record CustomerDto :IDto
    {
        public int Id { get; set; }        
        public int UserId { get; set; }
        public string? CompanyName { get; set; }

    }
}
