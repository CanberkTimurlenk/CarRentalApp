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
using DataAccess.Abstract.RepositoryManager;
using Entities.Concrete.DTOs;
using Entities.Concrete.DTOs.Rental;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public RentalManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;

        }

        [ValidationAspect(typeof(RentalValidator))]
        public IDataResult<int> Add(RentalDtoForManipulation rentalDtoForManipulation)
        {
            //  CheckIfAlreadyRented
            var result = _manager.Rental.Get(r => r.CarId == rentalDtoForManipulation.CarId && r.ReturnDate == null, false);

            if (result is null)
            {
                var mappedEntity = _mapper.Map<Rental>(rentalDtoForManipulation);

                _manager.Rental.Add(mappedEntity);
                _manager.Save();

                return new SuccessDataResult<int>(mappedEntity.Id, Messages.RentalAdded);
            }

            return new ErrorDataResult<int>(Messages.InvalidRentalAdd);

        }
        public IResult Delete(int id, bool trackChanges)
        {
            var entity = _manager.Rental.Get(r => r.Id == id, trackChanges);

            _manager.Rental.Delete(entity);
            _manager.Save();

            return new SuccessResult(Messages.RentalDeleted);

        }        
        [CacheAspect]
        public (IDataResult<IEnumerable<RentalDto>> result, MetaData metaData) GetAll(RentalParameters rentalParameters, bool trackChanges)
        {
            var rentalsWithMetaData = _manager.Rental.GetAllWithSorting(rentalParameters, trackChanges);

            var rentals = _mapper.Map<IEnumerable<RentalDto>>(rentalsWithMetaData);

            return (new SuccessDataResult<IEnumerable<RentalDto>>(rentals, Messages.CarsListed), rentalsWithMetaData.MetaData);

        }
        public IDataResult<RentalDto> GetById(int id, bool trackChanges)
        {
            var entity = _manager.Rental.Get(r => r.Id == id, trackChanges);

            var result = _mapper.Map<RentalDto>(entity);

            return new SuccessDataResult<RentalDto>(result, Messages.SuccessListedById);
        }
        public IResult Update(int id, RentalDtoForManipulation rentalDtoForManipulation, bool trackChanges)
        {
            var entity = _manager.Rental.Get(r => r.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(rentalDtoForManipulation, entity);

            _manager.Rental.Update(mappedEntity);
            _manager.Save();

            return new SuccessResult(Messages.RentalUpdated);
        }        
        [CacheAspect]
        public (IDataResult<IEnumerable<RentalDetailDto>> result, MetaData metaData) GetAllRentalDetails(RentalParameters rentalParameters, bool trackChanges)
        {
            var rentalDetailsWithMetaData = _manager.Rental.GetAllRentalDetails(rentalParameters, trackChanges);

            var rentalDetails = _mapper.Map<IEnumerable<RentalDetailDto>>(rentalDetailsWithMetaData);

            return (new SuccessDataResult<IEnumerable<RentalDetailDto>>(rentalDetails), rentalDetailsWithMetaData.MetaData);

        }


    }
}
