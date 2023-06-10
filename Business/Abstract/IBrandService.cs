using Core.Business;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.Brand;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface IBrandService : IBusinessRepository<BrandDto,BrandDtoForManipulation,BrandParameters>
    {
        

    }
}
