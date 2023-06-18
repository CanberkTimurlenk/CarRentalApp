using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs.Color
{
    public record ColorDto : IDto
    {
        public int Id { get; init; }
        public string ColorName { get; init; }

    }
}
