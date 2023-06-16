using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs.Car
{
    public record CarDto : IDto
    {
        public int Id { get; init; }
        public int BrandId { get; init; }
        public int ColorId { get; init; }
        public string CarName { get; init; }
        public int ModelYear { get; init; }
        public decimal DailyPrice { get; init; }
        public string? Description { get; init; }

    }
}
