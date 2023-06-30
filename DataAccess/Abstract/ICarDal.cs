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
        Task<PagedList<CarDetailDto>> GetAllCarDetails(CarParameters carParameters, bool trackChanges);

        Task<PagedList<Car>> GetAllWithSorting(CarParameters carParamaters, bool trackChanges);

    }
}
