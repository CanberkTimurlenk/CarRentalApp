using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IColorService : IBusinessRepository<Color>
    {

    }
}
