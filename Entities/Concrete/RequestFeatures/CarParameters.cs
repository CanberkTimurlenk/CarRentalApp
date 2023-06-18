using Core.Entities.Concrete.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.RequestFeatures
{
    public class CarParameters : RequestParameters
    {
        public string? OrderBy { get; set; }

        public CarParameters()
        {
            OrderBy = "id asc";
        }
    }
}
