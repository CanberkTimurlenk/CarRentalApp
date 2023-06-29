using Business.Abstract;
using Entities.Concrete.DTOs;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Aspects.Autofac.Cache;
using Entities.Concrete.Models;
using AutoMapper;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.RequestFeatures;
using Core.Entities.Concrete.RequestFeatures;
using DataAccess.Abstract.RepositoryManager;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CarManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        //[CacheAspect]
        public (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetAll(CarParameters carParameters, bool trackChanges)
        {
            var carsWithMetaData = _manager.Car.GetAllWithSorting(carParameters, trackChanges);
            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars, Messages.CarsListed), carsWithMetaData.MetaData);

        }
        public IDataResult<CarDto> GetById(int id, bool trackChanges)
        {
            var entity = _manager.Car.Get(c => c.Id == id, trackChanges);

            return new SuccessDataResult<CarDto>(_mapper.Map<CarDto>(entity), Messages.SuccessListedById);
        }

        //[SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IDataResult<int> Add(CarForManipulationDto carDtoForManipulation)
        {
            var entity = _mapper.Map<Car>(carDtoForManipulation);

            _manager.Car.Add(entity);
            _manager.Save();

            return new SuccessDataResult<int>(entity.Id, Messages.CarAdded);
        }

        public IResult Update(int id, CarForManipulationDto carDtoForManipulation, bool trackChanges)
        {
            var entity = _manager.Car.Get((c => c.Id == id), trackChanges);
            var mappedEntity = _mapper.Map(carDtoForManipulation, entity);

            _manager.Car.Update(mappedEntity);
            _manager.Save();

            return new SuccessResult(Messages.CarUpdated);

        }

        public IResult Delete(int id, bool trackChanges)
        {
            var entity = _manager.Car.Get(c => c.Id == id, trackChanges);

            _manager.Car.Delete(entity);
            _manager.Save();

            return new SuccessResult(Messages.CarDeleted);

        }

        public (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByColorId(int colorId, CarParameters carParameters, bool trackChanges)
        {
            var carsWithMetaData = _manager.Car.GetAllByCondition(c => c.ColorId == colorId, carParameters, trackChanges);

            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars), carsWithMetaData.MetaData);

        }

        public (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByBrandId(int brandId, CarParameters carParameters, bool trackChanges)
        {
            var carsWithMetaData = _manager.Car.GetAllByCondition(c => c.BrandId == brandId, carParameters, trackChanges);

            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars), carsWithMetaData.MetaData);

        }

        //[CacheAspect]
        public (IDataResult<IEnumerable<CarDetailDto>> result, MetaData metaData) GetAllCarDetails(CarParameters carParameters, bool trackChanges)
        {
            var carDetailsWithMetaData = _manager.Car.GetAllCarDetails(carParameters, trackChanges);

            var carDetails = _mapper.Map<IEnumerable<CarDetailDto>>(carDetailsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDetailDto>>(carDetails), carDetailsWithMetaData.MetaData);

        }
    }
}
