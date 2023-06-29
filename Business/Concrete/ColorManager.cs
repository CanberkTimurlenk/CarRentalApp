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
        public IDataResult<int> Add(ColorForManipulationDto colorDtoForManipulation)
        {
            var entity = _mapper.Map<Color>(colorDtoForManipulation);

            _manager.Color.Add(entity);
            _manager.Save();

            return new SuccessDataResult<int>(entity.Id, Messages.ColorAdded);

        }
        public IResult Delete(int id, bool trackChanges)
        {
            var entity = _manager.Color.Get(c => c.Id == id, trackChanges);

            _manager.Color.Delete(entity);
            _manager.Save();

            return new SuccessResult(Messages.ColorDeleted);
        }
        public IDataResult<ColorDto> GetById(int id, bool trackChanges)
        {
            var entity = _mapper.Map<ColorDto>
                (_manager.Color.Get(c => c.Id == id, trackChanges));

            return new SuccessDataResult<ColorDto>(entity, Messages.SuccessListedById);

        }
        public (IDataResult<IEnumerable<ColorDto>> result, MetaData metaData) GetAll(ColorParameters colorParameters, bool trackChanges)
        {
            var colorsWithMetaData = _manager.Color.GetAll(colorParameters, trackChanges);

            var colors = _mapper.Map<IEnumerable<ColorDto>>(colorsWithMetaData);

            return (new SuccessDataResult<IEnumerable<ColorDto>>(colors, Messages.CarsListed), colorsWithMetaData.MetaData);

        }
        public IResult Update(int id, ColorForManipulationDto colorDtoForManipulation, bool trackChanges)
        {
            var entity = _manager.Color.Get(c => c.Id == id, trackChanges);

            var mappedEntity = _mapper.Map(colorDtoForManipulation, entity);

            _manager.Color.Update(mappedEntity);
            _manager.Save();

            return new SuccessResult(Messages.ColorUpdated);

        }

    }
}
