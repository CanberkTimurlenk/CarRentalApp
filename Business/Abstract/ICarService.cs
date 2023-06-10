using Core.Business;
using Entities.Concrete.DTOs;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.RequestFeatures;
using Core.Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface ICarService : IBusinessRepository<CarDto,CarDtoForManipulation,CarParameters>
    {        
        (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByColorId(CarParameters carParameters, int colorId);
        (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByBrandId(CarParameters carParameters, int brandId);
        (IDataResult<IEnumerable<CarDetailDto>> result, MetaData metaData) GetAllCarDetails(CarParameters carParameters);        
    }

}
