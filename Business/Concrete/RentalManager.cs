using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Cache;
using Core.Aspects.Autofac.Validation;
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
        public async Task<IDataResult<int>> AddAsync(RentalForManipulationDto rentalDtoForManipulation)
        {
            //  CheckIfAlreadyRented
            var result = await _manager.Rental.GetAsync(r => r.CarId == rentalDtoForManipulation.CarId && r.ReturnDate == null, false);

            if (result is null)
            {
                var mappedEntity = _mapper.Map<Rental>(rentalDtoForManipulation);

                await _manager.Rental.AddAsync(mappedEntity);
                await _manager.SaveAsync();

                return new SuccessDataResult<int>(mappedEntity.Id, Messages.RentalAdded);
            }

            return new ErrorDataResult<int>(Messages.InvalidRentalAdd);

        }
        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Rental.GetAsync(r => r.Id == id, trackChanges);

            _manager.Rental.Delete(entity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.RentalDeleted);

        }
        [CacheAspect]
        public async Task<(IDataResult<IEnumerable<RentalDto>> result, MetaData metaData)> GetAllAsync(RentalParameters rentalParameters, bool trackChanges)
        {
            var rentalsWithMetaData = await _manager.Rental.GetAllWithSorting(rentalParameters, trackChanges);

            var rentals = _mapper.Map<IEnumerable<RentalDto>>(rentalsWithMetaData);

            return (new SuccessDataResult<IEnumerable<RentalDto>>(rentals, Messages.CarsListed), rentalsWithMetaData.MetaData);

        }
        public async Task<IDataResult<RentalDto>> GetByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Rental.GetAsync(r => r.Id == id, trackChanges);

            var result = _mapper.Map<RentalDto>(entity);

            return new SuccessDataResult<RentalDto>(result, Messages.SuccessListedById);
        }
        public async Task<IResult> UpdateAsync(int id, RentalForManipulationDto rentalDtoForManipulation, bool trackChanges)
        {
            var entity = await _manager.Rental.GetAsync(r => r.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(rentalDtoForManipulation, entity);

            _manager.Rental.Update(mappedEntity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.RentalUpdated);
        }
        [CacheAspect]
        public async Task<(IDataResult<IEnumerable<RentalDetailDto>> result, MetaData metaData)> GetAllRentalDetailsAsync(RentalParameters rentalParameters, bool trackChanges)
        {
            var rentalDetailsWithMetaData = await _manager.Rental.GetAllRentalDetails(rentalParameters, trackChanges);

            var rentalDetails = _mapper.Map<IEnumerable<RentalDetailDto>>(rentalDetailsWithMetaData);

            return (new SuccessDataResult<IEnumerable<RentalDetailDto>>(rentalDetails), rentalDetailsWithMetaData.MetaData);

        }


    }
}
