using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Abstract;


namespace Entities.Concrete.Models
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        public ICollection<Car> Cars { get; set; }  //  Collection Navigation Property
    }
}
