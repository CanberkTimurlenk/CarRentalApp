using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Brand
{
    public record BrandDto : IDto
    {
        public int Id { get; init; }
        public string BrandName { get; init; }

    }
}
