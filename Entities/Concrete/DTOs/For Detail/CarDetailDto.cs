using Core.Entities.Abstract;

namespace Entities.Concrete.DTOs
{
    public record CarDetailDto:IDto
    {
        public string CarName { get; init; }
        public string BrandName { get; init; }
        public string ColorName { get; init; }
        public decimal DailyPrice { get; init; }

    }
}
