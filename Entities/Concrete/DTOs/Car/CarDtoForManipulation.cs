using Core.Entities;

namespace Entities.Concrete.DTOs.Car
{
    public record CarDtoForManipulation : IDto
    {        
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string CarName { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string? Description { get; set; }
    }
}
