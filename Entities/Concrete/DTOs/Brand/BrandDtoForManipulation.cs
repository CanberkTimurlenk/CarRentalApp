using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Brand
{
    public record BrandDtoForManipulation:IDto
    {
        public string BrandName { get; init; }

    }
}
