using Business.Abstract;
using Business.Constants;
using Business.Constants.StoragePaths;
using Core.Entities;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Business;
using Entities.Concrete.Models;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService

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


            var ruleCheck = BusinessRules.Run(
                CheckIfCarImageLimitExceed(addedFileEntity.CarId)

                );

            if (ruleCheck == null)
            {

                addedFileEntity.Date = DateTime.Now;
                addedFileEntity.ImagePath = _fileHelper.UploadFile(file, Paths.CarImageFolder).Data;
                _carImageDal.Add(addedFileEntity);

                return new SuccessResult();
            }


            return new ErrorResult();

        }
        

        public IResult Update(IFormFile file, CarImage updatedFileEntity)   // updatedFileEntity indicates the old image
        {



            var carImage = _carImageDal.Get(c => c.Id == updatedFileEntity.Id);

            if (carImage != null )
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
        

        public IResult Delete(CarImage carImage)
        {
            var deletedImage = _carImageDal.Get(c => c.Id == carImage.Id);
            var deletedImagePath = deletedImage.ImagePath;

            var result = _fileHelper.DeleteFile(Path.Combine(Paths.CarImageFolder, deletedImagePath));

            _carImageDal.Delete(deletedImage);

            if (result.Success) return new SuccessResult(Messages.CarImageDeleted);
            return new ErrorResult();


        }
        
        public IDataResult<IEnumerable<CarImage>> GetByCarId(int carId)    //  If there is not such an image, it returns ErrorResult
        {
            var ruleCheck = BusinessRules.Run(
                CheckIfMentionedCarHaveAnyImage(carId)
                );

            IEnumerable<CarImage> result = new List<CarImage>();

            if (ruleCheck != null)
            {
                result = new List<CarImage> { GetDefaultImage(carId).Data };
                return new ErrorDataResult<IEnumerable<CarImage>>(result, Messages.EmptyImage);

            }

            result = _carImageDal.GetAll(c => c.CarId == carId);
            return new SuccessDataResult<IEnumerable<CarImage>>(result, Messages.SuccessListedById);

        }
        
        public IDataResult<CarImage> GetById(int carImageId)
        {
            var result = _carImageDal.Get(c => c.Id == carImageId);

            return new SuccessDataResult<CarImage>(result, Messages.SuccessListedById);

        }
        
        public IDataResult<IEnumerable<CarImage>> GetAll()
        {
            var result = _carImageDal.GetAll();

            return new SuccessDataResult<IEnumerable<CarImage>>(result, Messages.CarImagesListed);
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
