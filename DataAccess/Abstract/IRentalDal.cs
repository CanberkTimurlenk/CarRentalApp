using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IRepositoryBase<Rental, RentalParameters>
    {
        PagedList<Rental> GetAllWithSorting(RentalParameters rentalParameters, bool trackChanges);
        PagedList<RentalDetailDto> GetAllRentalDetails(RentalParameters rentalParameters, bool trackChanges);
        PagedList<RentalDetailDto> GetAllRentalDetailsByCondition(Expression<Func<RentalDetailDto, bool>> filter, RentalParameters rentalParameters, bool trackChanges);

    }
}
