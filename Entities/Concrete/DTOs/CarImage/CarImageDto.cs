using Core.Entities;

namespace Entities.Concrete.DTOs.CarImage
{
    public record CarImageDto : IDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}
