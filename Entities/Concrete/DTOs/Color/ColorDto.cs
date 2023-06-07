using Core.Entities;

namespace Entities.Concrete.DTOs.Color
{
    public record ColorDto : IDto
    {
        public int Id { get; set; }
        public string ColorName { get; set; }

    }
}
