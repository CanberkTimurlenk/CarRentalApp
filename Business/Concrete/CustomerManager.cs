using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Abstract.RepositoryManager;
using Entities.Concrete.DTOs.Customer;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public CustomerManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public async Task<IDataResult<int>> AddAsync(CustomerForManipulationDto customerDtoForManipulation)
        {
            var entity = _mapper.Map<Customer>(customerDtoForManipulation);

            await _manager.Customer.AddAsync(entity);
            await _manager.SaveAsync();

            return new SuccessDataResult<int>(entity.Id, Messages.CustomerAdded);
        }
        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Customer.GetAsync(c => c.Id == id, trackChanges);

            _manager.Customer.Delete(entity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.CustomerDeleted);
        }
        public async Task<(IDataResult<IEnumerable<CustomerDto>> result, MetaData metaData)> GetAllAsync(CustomerParamaters customerParameters, bool trackChanges)
        {
            var customersWithMetaData = await _manager.Customer.GetAllAsync(customerParameters, trackChanges);

            var customers = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetaData);

            return (new SuccessDataResult<IEnumerable<CustomerDto>>(customers, Messages.CarsListed), customersWithMetaData.MetaData);

        }
        public async Task<IDataResult<CustomerDto>> GetByIdAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Customer.GetAsync(c => c.Id == id, trackChanges);

            var result = _mapper.Map<CustomerDto>(entity);

            return new SuccessDataResult<CustomerDto>(result, Messages.SuccessListedById);
        }
        public async Task<IResult> UpdateAsync(int id, CustomerForManipulationDto customerDtoForManipulation, bool trackChanges)
        {
            var entity = await _manager.Customer.GetAsync(c => c.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(customerDtoForManipulation, entity);

            _manager.Customer.Update(mappedEntity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.CustomerUpdated);
        }

    }
}
