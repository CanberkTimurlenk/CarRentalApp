using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.CarImage
{
    public record CarImageForManipulationDto : IDto
    {
        public int CarId { get; init; }
        public string ImagePath { get; init; }
        public DateTime Date { get; init; }
    }
}
