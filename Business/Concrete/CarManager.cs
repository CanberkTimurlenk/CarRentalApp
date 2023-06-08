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


        //[CacheAspect]
        public IDataResult<IEnumerable<CarDto>> GetAll()
        {            
            var result = _mapper.Map<IEnumerable<CarDto>>(_carDal.GetAll()); 



            return new SuccessDataResult<IEnumerable<CarDto>>(result, Messages.CarsListed);
            //return _carDal.GetAll();

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

            return new SuccessDataResult<int>(entity.Id,Messages.CarAdded);
        }

        public IResult Update(int id,CarDtoForManipulation carDtoForManipulation)
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

        public IDataResult<IEnumerable<CarDto>> GetCarsByColorId(int colorId)
        {
            var entity = _carDal.GetAll(c => c.ColorId == colorId).ToList();
            
            return new SuccessDataResult<IEnumerable<CarDto>>(_mapper.Map<IEnumerable<CarDto>>(entity));

        }

        public IDataResult<IEnumerable<CarDto>> GetCarsByBrandId(int brandId)
        {
            var entity = _carDal.GetAll(c => c.BrandId == brandId).ToList();
            
            return new SuccessDataResult<IEnumerable<CarDto>>(_mapper.Map<IEnumerable<CarDto>>(entity));

        }

        //[CacheAspect]
        public IDataResult<IEnumerable<CarDetailDto>> GetAllCarDetails()
        {
            var entity = _carDal.GetAllCarDetails();

            return new SuccessDataResult<IEnumerable<CarDetailDto>>(_mapper.Map<IEnumerable<CarDetailDto>>(entity));

        }

        

    }
}
