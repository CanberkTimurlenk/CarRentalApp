using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Entities.Concrete.Models;
using Entities.Concrete.DTOs.Color;
using AutoMapper;
using Core.Entities.Concrete.RequestFeatures;
using Entities.Concrete.RequestFeatures;
using DataAccess.Abstract.RepositoryManager;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public ColorManager(IRepositoryManager manager, IMapper mapper)
        {
            _manager = manager;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public async Task<IDataResult<int>> AddAsync(ColorForManipulationDto colorDtoForManipulation)
        {
            var entity = _mapper.Map<Color>(colorDtoForManipulation);

            await _manager.Color.AddAsync(entity);
            await _manager.SaveAsync();

            return new SuccessDataResult<int>(entity.Id, Messages.ColorAdded);

        }
        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Color.GetAsync(c => c.Id == id, trackChanges);

            _manager.Color.Delete(entity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.ColorDeleted);
        }
        public async Task<IDataResult<ColorDto>> GetByIdAsync(int id, bool trackChanges)
        {
            var entity = _mapper.Map<ColorDto>
                (await _manager.Color.GetAsync(c => c.Id == id, trackChanges));

            return new SuccessDataResult<ColorDto>(entity, Messages.SuccessListedById);

        }
        public async Task<(IDataResult<IEnumerable<ColorDto>> result, MetaData metaData)> GetAllAsync(ColorParameters colorParameters, bool trackChanges)
        {
            var colorsWithMetaData = await _manager.Color.GetAllAsync(colorParameters, trackChanges);

            var colors = _mapper.Map<IEnumerable<ColorDto>>(colorsWithMetaData);

            return (new SuccessDataResult<IEnumerable<ColorDto>>(colors, Messages.CarsListed), colorsWithMetaData.MetaData);

        }
        public async Task<IResult> UpdateAsync(int id, ColorForManipulationDto colorDtoForManipulation, bool trackChanges)
        {
            var entity = await _manager.Color.GetAsync(c => c.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(colorDtoForManipulation, entity);

            _manager.Color.Update(mappedEntity);
            await _manager.SaveAsync();

            return new SuccessResult(Messages.ColorUpdated);

        }

    }
}
