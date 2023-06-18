using Core.Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class CarImage : IImageEntity
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }



    }
}
