using Core.Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class Color : IEntity
    {
        public int Id { get; set; }
        public string ColorName { get; set; }

        public ICollection<Car> Cars { get; set; }

    }
}
