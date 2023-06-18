using Core.Business;
using Entities.Concrete.DTOs.Brand;
using Entities.Concrete.RequestFeatures;

namespace Business.Abstract
{
    public interface IBrandService : IBusinessRepository<BrandDto,BrandDtoForManipulation,BrandParameters>
    {
        

    }
}
