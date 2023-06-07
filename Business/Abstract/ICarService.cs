﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Business;
using Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.Models;

namespace Business.Abstract
{
    public interface ICarService : IBusinessRepository<Car>
    {
        IDataResult<IEnumerable<Car>> GetCarsByColorId(int id);
        IDataResult<IEnumerable<Car>> GetCarsByBrandId(int id);
        IDataResult<IEnumerable<CarDetailDto>> GetAllCarDetails();

    }

}
