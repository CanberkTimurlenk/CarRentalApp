using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Car
{
    public record CarForManipulationDto : IDto
    {        
        public int BrandId { get; init; }
        public int ColorId { get; init; }
        public string CarName { get; init; }
        public int ModelYear { get; init; }
        public decimal DailyPrice { get; init; }
        public string? Description { get; init; }
    }
}
