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
        public IDataResult<int> Add(CustomerForManipulationDto customerDtoForManipulation)
        {
            var entity = _mapper.Map<Customer>(customerDtoForManipulation);

            _manager.Customer.Add(entity);
            _manager.Save();

            return new SuccessDataResult<int>(entity.Id, Messages.CustomerAdded);
        }
        public IResult Delete(int id, bool trackChanges)
        {
            var entity = _manager.Customer.Get(c => c.Id == id, trackChanges);

            _manager.Customer.Delete(entity);
            _manager.Save();

            return new SuccessResult(Messages.CustomerDeleted);
        }
        public (IDataResult<IEnumerable<CustomerDto>> result, MetaData metaData) GetAll(CustomerParamaters customerParameters, bool trackChanges)
        {
            var customersWithMetaData = _manager.Customer.GetAll(customerParameters, trackChanges);

            var customers = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetaData);

            return (new SuccessDataResult<IEnumerable<CustomerDto>>(customers, Messages.CarsListed), customersWithMetaData.MetaData);

        }
        public IDataResult<CustomerDto> GetById(int id, bool trackChanges)
        {
            var entity = _manager.Customer.Get(c => c.Id == id, trackChanges);

            var result = _mapper.Map<CustomerDto>(entity);

            return new SuccessDataResult<CustomerDto>(result, Messages.SuccessListedById);
        }
        public IResult Update(int id, CustomerForManipulationDto customerDtoForManipulation, bool trackChanges)
        {
            var entity = _manager.Customer.Get(c => c.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(customerDtoForManipulation, entity);

            _manager.Customer.Update(mappedEntity);
            _manager.Save();

            return new SuccessResult(Messages.CustomerUpdated);
        }

    }
}
