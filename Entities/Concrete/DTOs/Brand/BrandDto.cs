using Core.Entities;

namespace Entities.Concrete.DTOs.Brand
{
    public record BrandDto : IDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

    }
}
