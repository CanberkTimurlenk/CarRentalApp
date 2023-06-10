using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IRepositoryBase<Rental, RentalParameters>
    {
        PagedList<RentalDetailDto> GetAllRentalDetails(RentalParameters rentalParameters);

    }
}
