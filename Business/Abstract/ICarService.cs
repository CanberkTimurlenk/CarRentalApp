using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.Business;
using Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract
{
     public interface ICarService : IBusinessRepository<Car>
    {
        IDataResult<List<Car>> GetCarsByColorId(int id);
        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetAllCarDetails();

    }

}
