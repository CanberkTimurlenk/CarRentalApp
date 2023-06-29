using Core.DataAccess;
using Core.Entities.Concrete.RequestFeatures;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Abstract
{
    public interface ICarDal : IRepositoryBase<Car>
    {
        PagedList<CarDetailDto> GetAllCarDetails(CarParameters carParameters, bool trackChanges);

        PagedList<Car> GetAllWithSorting(CarParameters carParamaters, bool trackChanges);

    }
}
