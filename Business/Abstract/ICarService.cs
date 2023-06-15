using Core.Business;
using Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.RequestFeatures;
using Core.Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface ICarService : IBusinessRepository<CarDto, CarDtoForManipulation, CarParameters>
    {
        (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByColorId(int colorId, CarParameters carParameters, bool trackChanges);
        (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByBrandId(int brandId, CarParameters carParameters, bool trackChanges);
        (IDataResult<IEnumerable<CarDetailDto>> result, MetaData metaData) GetAllCarDetails(CarParameters carParameters, bool trackChanges);
    }

}
