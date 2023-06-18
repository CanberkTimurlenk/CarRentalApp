using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.CarImage
{
    public record CarImageDto : IDto
    {
        public int Id { get; init; }
        public int CarId { get; init; }
        public string ImagePath { get; init; }
        public DateTime Date { get; init; }
    }
}
