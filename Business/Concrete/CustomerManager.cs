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
using Entities.Concrete.DTOs.Customer;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDal customerDal, IMapper mapper)
        {
            _customerDal = customerDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IDataResult<int> Add(CustomerDtoForManipulation customerDtoForManipulation)
        {
            var entity = _mapper.Map<Customer>(customerDtoForManipulation);

            _customerDal.Add(entity);

            return new SuccessDataResult<int>(entity.Id,Messages.CustomerAdded);
        }

        public IResult Delete(int id)
        {
            var entity = _customerDal.Get(c => c.Id == id);

            _customerDal.Delete(entity);

            return new SuccessResult(Messages.CustomerDeleted);
        }

        public (IDataResult<IEnumerable<CustomerDto>> result, MetaData metaData) GetAll(CustomerParamaters customerParameters)
        {
            var customersWithMetaData = _customerDal.GetAll(customerParameters);
            var customers = _mapper.Map<IEnumerable<CustomerDto>>(customersWithMetaData);

            return (new SuccessDataResult<IEnumerable<CustomerDto>>(customers, Messages.CarsListed), customersWithMetaData.MetaData);

        }

        public IDataResult<CustomerDto> GetById(int id)
        {
            var entity = _customerDal.Get(c => c.Id == id);

            var result = _mapper.Map<CustomerDto>(entity);

            return new SuccessDataResult<CustomerDto>(result, Messages.SuccessListedById);
        }

        public IResult Update(int id, CustomerDtoForManipulation customerDtoForManipulation)
        {
            var entity = _customerDal.Get(c => c.Id == id);

            var mappedEntity = _mapper.Map(customerDtoForManipulation, entity);

            _customerDal.Update(mappedEntity);
            return new SuccessResult(Messages.CustomerUpdated);
        }

    }
}
