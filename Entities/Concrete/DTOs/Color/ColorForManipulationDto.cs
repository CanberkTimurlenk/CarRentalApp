using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Color
{
    public record ColorForManipulationDto:IDto
    {
        public string ColorName { get; init; }

    }
}
