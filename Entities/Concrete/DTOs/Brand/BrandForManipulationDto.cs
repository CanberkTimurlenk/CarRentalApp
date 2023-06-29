using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Brand
{
    public record BrandForManipulationDto:IDto
    {
        public string BrandName { get; init; }

    }
}
