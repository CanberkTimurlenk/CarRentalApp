using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Color
{
    public record ColorDtoForManipulation:IDto
    {
        public string ColorName { get; init; }

    }
}
