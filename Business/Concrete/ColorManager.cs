using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using DataAccess.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Entities.Concrete.Models;
using Entities.Concrete.DTOs.Color;
using Core.Business;
using AutoMapper;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;
        private readonly IMapper _mapper;
        public ColorManager(IColorDal colorDal, IMapper mapper)
        {
            _colorDal = colorDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IDataResult<int> Add(ColorDtoForManipulation colorDtoForManipulation)
        {
            var entity = _mapper.Map<Color>(colorDtoForManipulation);

            _colorDal.Add(entity);

            return new SuccessDataResult<int>(entity.Id, Messages.ColorAdded);

        }

        public IResult Delete(int id)
        {
            var entity = _colorDal.Get(c => c.Id == id);

            _colorDal.Delete(entity);

            return new SuccessResult(Messages.ColorDeleted);
        }

        public IDataResult<ColorDto> GetById(int id)
        {
            var result = _mapper.Map<ColorDto>(_colorDal.Get(c => c.Id == id));

            return new SuccessDataResult<ColorDto>(result, Messages.SuccessListedById);


        }

        public IDataResult<IEnumerable<ColorDto>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<ColorDto>>(_colorDal.GetAll());

            return new SuccessDataResult<IEnumerable<ColorDto>>(result, Messages.ColorsListed);

        }

        public IResult Update(int id, ColorDtoForManipulation colorDtoForManipulation)
        {
            var entity = _colorDal.Get(c => c.Id == id);

            var mappedEntity = _mapper.Map(colorDtoForManipulation, entity);

            _colorDal.Update(mappedEntity);

            return new SuccessResult(Messages.ColorUpdated);
        }


    }
}
