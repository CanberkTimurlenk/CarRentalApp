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
using Core.Aspects.Autofac.Performance;
using Business.BusinessAspects.Autofac;

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
        [PerformanceAspect(2)]
        public async Task<(IDataResult<IEnumerable<CarDto>> result, MetaData metaData)> GetAllAsync(CarParameters carParameters, bool trackChanges)
        {
            Thread.Sleep(3000);
            var carsWithMetaData = await _manager.Car.GetAllWithSorting(carParameters, trackChanges);
            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars, Messages.CarsListed), carsWithMetaData.MetaData);

        }
        public async Task<IDataResult<CarDto>> GetByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Car.GetAsync(c => c.Id == id, trackChanges);

            return new SuccessDataResult<CarDto>(_mapper.Map<CarDto>(entity), Messages.SuccessListedById);
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public async Task<IDataResult<int>> AddAsync(CarForManipulationDto carDtoForManipulation)
        {
            var entity = _mapper.Map<Car>(carDtoForManipulation);

            await _manager.Car.AddAsync(entity);
            await _manager.SaveAsync();

            return new SuccessDataResult<int>(entity.Id, Messages.CarAdded);
        }

        public async Task<IResult> UpdateAsync(int id, CarForManipulationDto carDtoForManipulation, bool trackChanges)
        {
            var entity = await _manager.Car.GetAsync((c => c.Id == id), trackChanges);
            var mappedEntity = _mapper.Map(carDtoForManipulation, entity);

            _manager.Car.Update(mappedEntity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.CarUpdated);

        }

        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Car.GetAsync(c => c.Id == id, trackChanges);

            _manager.Car.Delete(entity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.CarDeleted);

        }

        public async Task<(IDataResult<IEnumerable<CarDto>> result, MetaData metaData)> GetCarsByColorIdAsync(int colorId, CarParameters carParameters, bool trackChanges)
        {
            var carsWithMetaData = await _manager.Car.GetAllByConditionAsync(c => c.ColorId == colorId, carParameters, trackChanges);

            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars), carsWithMetaData.MetaData);

        }

        public async Task<(IDataResult<IEnumerable<CarDto>> result, MetaData metaData)> GetCarsByBrandIdAsync(int brandId, CarParameters carParameters, bool trackChanges)
        {
            var carsWithMetaData = await _manager.Car.GetAllByConditionAsync(c => c.BrandId == brandId, carParameters, trackChanges);

            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars), carsWithMetaData.MetaData);

        }

        //[CacheAspect]
        public async Task<(IDataResult<IEnumerable<CarDetailDto>> result, MetaData metaData)> GetAllCarDetailsAsync(CarParameters carParameters, bool trackChanges)
        {
            var carDetailsWithMetaData = await _manager.Car.GetAllCarDetails(carParameters, trackChanges);

            var carDetails = _mapper.Map<IEnumerable<CarDetailDto>>(carDetailsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDetailDto>>(carDetails), carDetailsWithMetaData.MetaData);

        }
    }
}
