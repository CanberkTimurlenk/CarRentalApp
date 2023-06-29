using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Customer
{
    public record CustomerForManipulationDto : IDto
    {        
        public int UserId { get; init; }
        public string? CompanyName { get; init; }        

    }
}
