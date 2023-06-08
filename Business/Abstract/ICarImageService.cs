using Core.Business;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.CarImage;
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService : IBusinessFileRepository<IFormFile,CarImageDto,CarImageDtoForManipulation>
    {


        IDataResult<IEnumerable<CarImageDto>> GetByCarId(int carId);




    }
}
