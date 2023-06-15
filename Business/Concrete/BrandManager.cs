using Business.Abstract;
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
using DataAccess.Abstract.RepositoryManager;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public BrandManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;

        }

        //[SecuredOperation("brand.add,admin")]
        [ValidationAspect(typeof(BrandValidator))]
        [PerformanceAspect(5)]
        public IDataResult<int> Add(BrandDtoForManipulation brandDtoForManipulation)
        {
            //Thread.Sleep(2000); to test PerformanceAspect

            var entity = _mapper.Map<Brand>(brandDtoForManipulation);

            _manager.Brand.Add(entity);
            _manager.Save();

            return new SuccessDataResult<int>(entity.Id, Messages.BrandAdded);

        }
        public IResult Delete(int id, bool trackChanges)
        {
            var entity = _manager.Brand.Get(b => b.Id == id, trackChanges);

            _manager.Brand.Delete(entity);
            _manager.Save();

            return new SuccessResult(Messages.BrandDeleted);

        }
        public IDataResult<BrandDto> GetById(int brandId, bool trackChanges)
        {
            var entity = _manager.Brand.Get(b => b.Id == brandId, trackChanges);
            var result = _mapper.Map<BrandDto>(entity);

            return new SuccessDataResult<BrandDto>(result, Messages.SuccessListedById);

        }
        public (IDataResult<IEnumerable<BrandDto>> result, MetaData metaData) GetAll(BrandParameters brandParameters, bool trackChanges)
        {
            var brandsWithMetaData = _manager.Brand.GetAll(brandParameters, trackChanges);
            var brands = _mapper.Map<IEnumerable<BrandDto>>(brandsWithMetaData);

            return (new SuccessDataResult<IEnumerable<BrandDto>>(brands, Messages.BrandsListed), brandsWithMetaData.MetaData);

        }
        public IResult Update(int id, BrandDtoForManipulation brandDtoForManipulation, bool trackChanges)
        {
            var entity = _manager.Brand.Get(b => b.Id == id, trackChanges);
            var mappedEntity = _mapper.Map(brandDtoForManipulation, entity);

            _manager.Brand.Update(mappedEntity);
            _manager.Save();

            return new SuccessResult(Messages.BrandUpdated);
        }

    }
}
