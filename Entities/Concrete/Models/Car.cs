using Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public string CarName { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string? Description { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public ICollection<CarImage> CarImages { get; set; }


    }
}
