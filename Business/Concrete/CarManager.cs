using Business.Abstract;
using DataAccess.Abstract;
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
using Core.Business;
using Entities.Concrete.RequestFeatures;
using Entities.Concrete;
using Core.Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IMapper _mapper;


        public CarManager(ICarDal carDal, IMapper mapper)
        {
            _carDal = carDal;
            _mapper = mapper;
        }


        [CacheAspect]
        public (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetAll(CarParameters carParameters)
        {
            var carsWithMetaData = _carDal.GetAll(requestParameters: carParameters);
            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars, Messages.CarsListed), carsWithMetaData.MetaData);

        }

        public IDataResult<CarDto> GetById(int id)
        {
            var entity = _carDal.Get(c => c.Id == id);

            return new SuccessDataResult<CarDto>(_mapper.Map<CarDto>(entity), Messages.SuccessListedById);
        }

        //[SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IDataResult<int> Add(CarDtoForManipulation carDtoForManipulation)
        {
            var entity = _mapper.Map<Car>(carDtoForManipulation);

            _carDal.Add(entity);

            return new SuccessDataResult<int>(entity.Id, Messages.CarAdded);
        }

        public IResult Update(int id, CarDtoForManipulation carDtoForManipulation)
        {
            var entity = _carDal.Get(c => c.Id == id);

            var mappedEntity = _mapper.Map(carDtoForManipulation, entity);

            _carDal.Update(mappedEntity);

            return new SuccessResult(Messages.CarUpdated);

        }

        public IResult Delete(int id)
        {

            var entity = _carDal.Get(c => c.Id == id);

            _carDal.Delete(entity);

            return new SuccessResult(Messages.CarDeleted);


        }

        public (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByColorId(CarParameters carParameters, int colorId)
        {
            var carsWithMetaData = _carDal.GetAll(carParameters, c => c.ColorId == colorId);

            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars), carsWithMetaData.MetaData);

        }

        public (IDataResult<IEnumerable<CarDto>> result, MetaData metaData) GetCarsByBrandId(CarParameters carParameters, int brandId)
        {
            var carsWithMetaData = _carDal.GetAll(carParameters, c => c.BrandId == brandId);

            var cars = _mapper.Map<IEnumerable<CarDto>>(carsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDto>>(cars), carsWithMetaData.MetaData);

        }

        //[CacheAspect]
        public (IDataResult<IEnumerable<CarDetailDto>> result, MetaData metaData) GetAllCarDetails(CarParameters carParameters)
        {
            var carDetailsWithMetaData = _carDal.GetAll(carParameters);

            var carDetails = _mapper.Map<IEnumerable<CarDetailDto>>(carDetailsWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarDetailDto>>(carDetails), carDetailsWithMetaData.MetaData);

        }
    }
}
