using Business.Abstract;
using Business.Constants;
using Business.Constants.StoragePaths;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Business;
using Entities.Concrete.DTOs.CarImage;
using AutoMapper;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Core.Entities.Concrete.RequestFeatures;
using DataAccess.Abstract.RepositoryManager;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly IRepositoryManager _manager;
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;

        public CarImageManager(IRepositoryManager manager, IFileHelper fileHelper, IMapper mapper)
        {
            _manager = manager;
            _fileHelper = fileHelper;
            _mapper = mapper;
        }

        public async Task<IResult> AddAsync(IFormFile file, CarImageForManipulationDto carImageForManipulation)
        {
            var ruleCheck = BusinessRules.Run(
                await CheckIfCarImageLimitExceed(carImageForManipulation.CarId)
                );

            if (ruleCheck is null)
            {
                var entity = _mapper.Map<CarImage>(carImageForManipulation);

                entity.Date = DateTime.Now;
                entity.ImagePath = (await _fileHelper.UploadFile(file, Paths.CarImageFolder)).Data;

                await _manager.CarImage.AddAsync(entity);
                await _manager.SaveAsync();

                return new SuccessResult();
            }

            return new ErrorResult();

        }
        public async Task<IResult> UpdateAsync(IFormFile file, int id, CarImageForManipulationDto carImageForManipulation, bool trackChanges)
        {
            var entity = await _manager.CarImage.GetAsync(c => c.Id == id, trackChanges);

            var carImage = _mapper.Map(carImageForManipulation, entity);

            if (entity is not null)
            {
                var oldImageRelativePath = carImage.ImagePath;

                var result = await _fileHelper.UpdateFile(file, Path.Combine(Paths.CarImageFolder, oldImageRelativePath));

                carImage.Date = DateTime.Now;

                carImage.ImagePath = result.Data;

                _manager.CarImage.Update(carImage);
                await _manager.SaveAsync();

                if (result.Success)
                    return new SuccessResult();
            }

            return new ErrorResult();
        }
        public async Task<IResult> DeleteAsync(int id, bool trackChanges)
        {
            var entity = await _manager.CarImage.GetAsync(c => c.Id == id, trackChanges);

            var deletedImagePath = entity.ImagePath;

            var result = _fileHelper.DeleteFile
                (Path.Combine(
                        Paths.CarImageFolder, deletedImagePath));

            _manager.CarImage.Delete(entity);
            await _manager.SaveAsync();

            if (result.Success)
                return new SuccessResult(Messages.CarImageDeleted);

            return new ErrorResult();

        }
        public async Task<(IDataResult<IEnumerable<CarImageDto>> result, MetaData metaData)> GetByCarIdAsync(CarImageParameters carImageParameters, int carId, bool trackChanges)    //  If the car do not have an image, it returns ErrorResult
        {
            var ruleCheck = BusinessRules.Run(
                await CheckIfMentionedCarHaveAnyImage(carId)
                );

            IEnumerable<CarImageDto> result = new List<CarImageDto>();

            if (ruleCheck is not null)
            {
                result = new List<CarImageDto> { GetDefaultImage(carId).Data };
                return (new ErrorDataResult<IEnumerable<CarImageDto>>(result, Messages.EmptyImage), null);

            }

            var entitiesByCarId = await _manager.CarImage.GetAllByConditionAsync(c => c.CarId == carId, carImageParameters, trackChanges);

            result = _mapper.Map<IEnumerable<CarImageDto>>(entitiesByCarId);

            return (new SuccessDataResult<IEnumerable<CarImageDto>>(result, Messages.SuccessListedById), entitiesByCarId.MetaData);

        }
        public async Task<IDataResult<CarImageDto>> GetByIdAsync(int carImageId, bool trackChanges)
        {
            var entity = await _manager.CarImage.GetAsync(c => c.Id == carImageId, trackChanges);

            var result = _mapper.Map<CarImageDto>(entity);

            return new SuccessDataResult<CarImageDto>(result, Messages.SuccessListedById);

        }
        public async Task<(IDataResult<IEnumerable<CarImageDto>> result, MetaData metaData)> GetAllAsync(CarImageParameters carImageParameters, bool trackChanges)
        {
            var carImagesWithMetaData = await _manager.CarImage.GetAllAsync(carImageParameters, trackChanges);

            var carImages = _mapper.Map<IEnumerable<CarImageDto>>(carImagesWithMetaData);

            return (new SuccessDataResult<IEnumerable<CarImageDto>>(carImages), carImagesWithMetaData.MetaData);
        }


        private async Task<IResult> CheckIfCarImageLimitExceed(int carId)
        {
            var result = (await _manager.CarImage.GetAllByConditionAsEnumerableAsync
                                                    (c => c.CarId == carId, false))
                                                    .Count();

            if (result >= Restrictions.CarImageLimit)
                return new ErrorResult(Messages.CarImageLimitExceed);

            return new SuccessResult();

        }
        private async Task<IResult> CheckIfMentionedCarHaveAnyImage(int carId)
        {
            var result = await _manager.CarImage.GetAllByConditionAsEnumerableAsync(c => c.CarId == carId, false);

            if (!result.Any())
                return new ErrorResult();

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
