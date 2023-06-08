using Business.Abstract;
using Business.Constants;
using Business.Constants.StoragePaths;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Business;
using Entities.Concrete.DTOs.CarImage;
using AutoMapper;
using Entities.Concrete.Models;
using Core.Business;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService


    {
        private readonly ICarImageDal _carImageDal;
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper, IMapper mapper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
            _mapper = mapper;
        }

        public IResult Add(IFormFile file, CarImageDtoForManipulation carImageForManipulation)
        {
            var ruleCheck = BusinessRules.Run(
                CheckIfCarImageLimitExceed(carImageForManipulation.CarId)
                );

            if (ruleCheck == null)
            {
                var entity = _mapper.Map<CarImage>(carImageForManipulation);

                entity.Date = DateTime.Now;
                entity.ImagePath = _fileHelper.UploadFile(file, Paths.CarImageFolder).Data;
                _carImageDal.Add(entity);

                return new SuccessResult();
            }
            return new ErrorResult();

        }


        public IResult Update(IFormFile file, int id, CarImageDtoForManipulation carImageForManipulation)   // updatedFileEntity indicates the old image
        {
            var entity = _carImageDal.Get(c => c.Id == id);

            var carImage = _mapper.Map(carImageForManipulation, entity);

            if (entity != null)
            {
                var oldImageRelativePath = carImage.ImagePath;

                var result = _fileHelper.UpdateFile(file, Path.Combine(Paths.CarImageFolder, oldImageRelativePath));

                carImage.Date = DateTime.Now;

                carImage.ImagePath = result.Data;

                _carImageDal.Update(carImage);

                // veritabanındaki ef car image daki değişime bak, resmi değiştiriyorum ama veritabanındaki bilgiler değişiyor mu ?
                if (result.Success) return new SuccessResult();
            }

            return new ErrorResult();
        }

        public IResult Delete(int id)
        {
            var entity = _carImageDal.Get(c => c.Id == id);

            var deletedImagePath = entity.ImagePath;

            var result = _fileHelper.DeleteFile(Path.Combine(Paths.CarImageFolder, deletedImagePath));

            _carImageDal.Delete(entity);

            if (result.Success) return new SuccessResult(Messages.CarImageDeleted);
            return new ErrorResult();

        }

        public IDataResult<IEnumerable<CarImageDto>> GetByCarId(int carId)    //  If there is not such an image, it returns ErrorResult
        {
            var ruleCheck = BusinessRules.Run(
                CheckIfMentionedCarHaveAnyImage(carId)
                );

            IEnumerable<CarImageDto> result = new List<CarImageDto>();

            if (ruleCheck is not null)
            {
                result = new List<CarImageDto> { GetDefaultImage(carId).Data };
                return new ErrorDataResult<IEnumerable<CarImageDto>>(result, Messages.EmptyImage);

            }

            var entitiesByCarId = _carImageDal.GetAll(c => c.CarId == carId);

            result = _mapper.Map<IEnumerable<CarImageDto>>(entitiesByCarId);

            return new SuccessDataResult<IEnumerable<CarImageDto>>(result, Messages.SuccessListedById);

        }

        public IDataResult<CarImageDto> GetById(int carImageId)
        {
            var entity = _carImageDal.Get(c => c.Id == carImageId);

            var result = _mapper.Map<CarImageDto>(entity);

            return new SuccessDataResult<CarImageDto>(result, Messages.SuccessListedById);

        }

        public IDataResult<IEnumerable<CarImageDto>> GetAll()
        {
            var result = _mapper.Map<IEnumerable<CarImageDto>>(_carImageDal.GetAll());

            return new SuccessDataResult<IEnumerable<CarImageDto>>(result, Messages.CarImagesListed);
        }


        /*
         *      More logics..
         * 
         */

        private IResult CheckIfCarImageLimitExceed(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count();

            if (result >= Restrictions.CarImageLimit) return new ErrorResult(Messages.CarImageLimitExceed);

            return new SuccessResult();

        }
        private IResult CheckIfMentionedCarHaveAnyImage(int carId)

        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);

            if (!result.Any())
            {
                return new ErrorResult();

            }

            return new SuccessResult();

        }
        private IDataResult<CarImageDto> GetDefaultImage(int carId)
        {

            return new SuccessDataResult<CarImageDto>(new CarImageDto
            {

                CarId = carId,
                Date = DateTime.Now,
                ImagePath = "Default.jpg"

            });
        }

   
    }
}
