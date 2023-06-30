using Core.Business;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.CarImage;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService : IBusinessFileRepository<IFormFile, CarImageDto, CarImageForManipulationDto, CarImageParameters>
    {
        Task<(IDataResult<IEnumerable<CarImageDto>> result, MetaData metaData)> GetByCarIdAsync(CarImageParameters carImageParameters, int carId, bool trackChanges);

    }
}
