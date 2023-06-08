using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Models;
using Entities.Concrete.DTOs.Car;

namespace Business.Abstract
{
    public interface ICarService : IBusinessRepository<CarDto,CarDtoForManipulation>
    {
        IDataResult<IEnumerable<CarDto>> GetCarsByColorId(int id);
        IDataResult<IEnumerable<CarDto>> GetCarsByBrandId(int id);
        IDataResult<IEnumerable<CarDetailDto>> GetAllCarDetails();

    }

}
