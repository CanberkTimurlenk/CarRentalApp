using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;

namespace Entities.Concrete.Models
{
    public class Brand : IEntity
    {
        public int Id { get; set; }
        public string BrandName { get; set; }

        public ICollection<Car> Cars { get; set; }  //  Collection Navigation Property
    }
}
