using Core.Entities;

namespace Entities.Concrete.DTOs.Color
{
    public record ColorDtoForManipulation:IDto
    {
        public string ColorName { get; set; }

    }
}
