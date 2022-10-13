using Business.Abstract;
using Business.Constants;
using Business.Constants.StoragePaths;
using Core.Entities;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CarImageManager // : ICarImageService

    {
        private readonly ICarImageDal _carImageDal;
        private readonly IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        public IResult Add(IFormFile file, CarImage addedFileEntity)
        {
            _fileHelper.UploadFile(file, Paths.CarImageFolder);

            var ruleCheck = BusinessRules.Run(
                CheckIfCarImageLimitExceed(addedFileEntity.CarId)

                );

            if (ruleCheck != null)
            {
                addedFileEntity.ImagePath = file.FileName;
                addedFileEntity.Date = DateTime.Now;
                _carImageDal.Add(addedFileEntity);



                return new SuccessResult();
            }


            return new ErrorResult();

        }
        public IResult Update(IFormFile file, CarImage updatedFileEntity)   // updatedFileEntity indicates the old image
        {

            var oldImageRelativePath = _carImageDal.Get(c => c.CarImageId == updatedFileEntity.CarImageId).ImagePath;

            var result = _fileHelper.UpdateFile(file, Path.Combine(Paths.CarImageFolder, oldImageRelativePath));

            if (result.Success) return new SuccessResult();

            return new ErrorResult();




        }
        public IResult Delete(CarImage carImage)
        {

            var result = _fileHelper.DeleteFile(Path.Combine(Paths.CarImageFolder, carImage.ImagePath));

            if (result.Success) return new SuccessResult(Messages.CarImageDeleted);
            return new ErrorResult();


        }
        public IDataResult<List<CarImage>> GetByCarId(int carId)    //  If there is not such an image, it returns ErrorResult
        {
            var ruleCheck = BusinessRules.Run(
                CheckIfMentionedCarHaveAnyImage(carId)
                );

            List<CarImage> result = new List<CarImage>();

            if(!ruleCheck.Success)
            {
                result = new List<CarImage> { GetDefaultImage(carId).Data };
                return new ErrorDataResult<List<CarImage>>(result, Messages.EmptyImage);

            }

            result = _carImageDal.GetAll(c => c.CarId == carId);
            return new SuccessDataResult<List<CarImage>>(result, Messages.SuccessListedById);
            
        }
        public IDataResult<CarImage> GetById(int carImageId)
        {
            var result = _carImageDal.Get(c => c.CarImageId == carImageId);

            return new SuccessDataResult<CarImage>(result, Messages.SuccessListedByCarId);

        }
        public IDataResult<List<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();

            return new SuccessDataResult<List<CarImage>>(result, Messages.CarImagesListed);
        }
        /*
         *      Logics..
         * 
         */
        private IResult CheckIfCarImageLimitExceed(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count();

            if (result > 5) return new ErrorResult(Messages.CarImageLimitExceed);

            return new SuccessResult();

        }
        private IResult CheckIfMentionedCarHaveAnyImage(int carId)
        
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId);

            if(!result.Any())
            {
                return new ErrorResult();

            }

            return new SuccessResult();

        }
        private IDataResult<CarImage> GetDefaultImage(int carId)
        {

            return new SuccessDataResult<CarImage>(new CarImage
            {
                
                CarId = carId,
                Date = DateTime.Now,
                ImagePath = "Default.jpg"
                
            });
        }
    }
}
