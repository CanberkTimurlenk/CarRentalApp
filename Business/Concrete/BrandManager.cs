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
        public async Task<IDataResult<int>> AddAsync(BrandForManipulationDto brandDtoForManipulation)
        {
            //Thread.Sleep(2000); to test PerformanceAspect

            var entity = _mapper.Map<Brand>(brandDtoForManipulation);

            await _manager.Brand.AddAsync(entity);
            await _manager.SaveAsync();

            return new SuccessDataResult<int>(entity.Id, Messages.BrandAdded);

        }
        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Brand.GetAsync(b => b.Id == id, trackChanges);

            _manager.Brand.Delete(entity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.BrandDeleted);

        }
        public async Task<IDataResult<BrandDto>> GetByIdAsync(int brandId, bool trackChanges)
        {
            var entity = await _manager.Brand.GetAsync(b => b.Id == brandId, trackChanges);
            var result = _mapper.Map<BrandDto>(entity);

            return new SuccessDataResult<BrandDto>(result, Messages.SuccessListedById);

        }
        public async Task<(IDataResult<IEnumerable<BrandDto>> result, MetaData metaData)> GetAllAsync(BrandParameters brandParameters, bool trackChanges)
        {
            var brandsWithMetaData = await _manager.Brand.GetAllAsync(brandParameters, trackChanges);
            var brands = _mapper.Map<IEnumerable<BrandDto>>(brandsWithMetaData);

            return (new SuccessDataResult<IEnumerable<BrandDto>>(brands, Messages.BrandsListed), brandsWithMetaData.MetaData);

        }
        public async Task<IResult> UpdateAsync(int id, BrandForManipulationDto brandDtoForManipulation, bool trackChanges)
        {
            var entity = await _manager.Brand.GetAsync(b => b.Id == id, trackChanges);
            var mappedEntity = _mapper.Map(brandDtoForManipulation, entity);

            _manager.Brand.Update(mappedEntity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.BrandUpdated);
        }

    }
}
