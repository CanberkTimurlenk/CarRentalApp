using Business.Abstract;
using DataAccess.Abstract;
using Business.Constants;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Aspects.Autofac.Validation;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Performance;
using Entities.Concrete.Models;
using AutoMapper;
using Entities.Concrete.DTOs.Brand;
using Core.Entities.Concrete.RequestFeatures;
using Entities.Concrete.RequestFeatures;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
        private readonly IMapper _mapper;

        public BrandManager(IBrandDal brandDal, IMapper mapper)
        {
            _brandDal = brandDal;
            _mapper = mapper;

        }

        //[SecuredOperation("brand.add,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [PerformanceAspect(5)]
        public IDataResult<int> Add(BrandDtoForManipulation brandDtoForManipulation)
        {
            //Thread.Sleep(2000); to test PerformanceAspect

            var entity = _mapper.Map<Brand>(brandDtoForManipulation);

            _brandDal.Add(entity);

            return new SuccessDataResult<int>(entity.Id, Messages.BrandAdded);

        }

        public IResult Delete(int id)
        {
            var entity = _brandDal.Get(b => b.Id == id);

            _brandDal.Delete(entity);

            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<BrandDto> GetById(int brandId)
        {
            var entity = _mapper.Map<BrandDto>(_brandDal.Get(b => b.Id == brandId));

            return new SuccessDataResult<BrandDto>(entity, Messages.SuccessListedById);

        }

        public (IDataResult<IEnumerable<BrandDto>> result, MetaData metaData) GetAll(BrandParameters brandParameters)
        {
            var brandsWithMetaData = _brandDal.GetAll(brandParameters);
            var brands = _mapper.Map<IEnumerable<BrandDto>>(brandsWithMetaData);

            return (new SuccessDataResult<IEnumerable<BrandDto>>(brands, Messages.BrandsListed), brandsWithMetaData.MetaData);

        }

        public IResult Update(int id,BrandDtoForManipulation brandDtoForManipulation)
        {
            var entity = _brandDal.Get(b => b.Id == id);
            var mappedEntity = _mapper.Map(brandDtoForManipulation, entity);

            _brandDal.Update(mappedEntity);

            return new SuccessResult(Messages.BrandUpdated);
        }

        
    }
}
