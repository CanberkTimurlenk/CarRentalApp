﻿using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService : IBusinessFileRepository<IFormFile,CarImage>
    {

        
        IDataResult<CarImage> GetByCarId(int carId);




    }
}