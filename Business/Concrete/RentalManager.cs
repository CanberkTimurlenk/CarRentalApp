using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Rental;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentDal;
        private readonly IMapper _mapper;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentDal = rentalDal;

        }

        [ValidationAspect(typeof(RentalValidator))]
        public IDataResult<int> Add(RentalDtoForManipulation rentalDtoForManipulation)
        {

            //  CheckIfAlreadyRented
            var result = _rentDal.Get(r => r.CarId == rentalDtoForManipulation.CarId && r.ReturnDate == null);

            if (result is null)
            {
                var mappedEntity = _mapper.Map<Rental>(rentalDtoForManipulation);
                _rentDal.Add(mappedEntity);
                return new SuccessDataResult<int>(mappedEntity.Id,Messages.RentalAdded);
            }

            
            return new ErrorDataResult<int>(Messages.InvalidRentalAdd);

        }

        public IResult Delete(int id)
        {
            var entity = _rentDal.Get(r => r.Id == id);

            _rentDal.Delete(entity);

            return new SuccessResult(Messages.RentalDeleted);

        }


        [CacheAspect]
        public (IDataResult<IEnumerable<RentalDto>> result, MetaData metaData) GetAll(RentalParameters rentalParameters)
        {
            var rentalsWithMetaData = _rentDal.GetAll(rentalParameters);
            var rentals = _mapper.Map<IEnumerable<RentalDto>>(rentalsWithMetaData);

            return (new SuccessDataResult<IEnumerable<RentalDto>>(rentals, Messages.CarsListed), rentalsWithMetaData.MetaData);

        }

        public IDataResult<RentalDto> GetById(int id)
        {
            var entity = _rentDal.Get(r => r.Id == id);

            var result = _mapper.Map<RentalDto>(entity);

            return new SuccessDataResult<RentalDto>(result, Messages.SuccessListedById);
        }

        public IResult Update(int id, RentalDtoForManipulation rentalDtoForManipulation)
        {

            var entity = _rentDal.Get(r => r.Id == id);

            var mappedEntity = _mapper.Map(rentalDtoForManipulation, entity);

            _rentDal.Update(mappedEntity);

            return new SuccessResult(Messages.RentalUpdated);
        }
        
        [CacheAspect]
        public (IDataResult<IEnumerable<RentalDetailDto>> result, MetaData metaData) GetAllRentalDetails(RentalParameters rentalParameters) 
        {
            var rentalDetailsWithMetaData = _rentDal.GetAllRentalDetails(rentalParameters);

            var rentalDetails = _mapper.Map<IEnumerable<RentalDetailDto>>(rentalDetailsWithMetaData);

            return (new SuccessDataResult<IEnumerable<RentalDetailDto>>(rentalDetails), rentalDetailsWithMetaData.MetaData);

        }


    }
}
