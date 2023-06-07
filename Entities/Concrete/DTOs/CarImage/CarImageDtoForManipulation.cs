using Core.Entities;

namespace Entities.Concrete.DTOs.CarImage
{
    public record CarImageDtoForManipulation : IDto
    {
        public int CarId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}
