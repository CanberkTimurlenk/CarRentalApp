using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.RepositoryManager
{
    public interface IRepositoryManager
    {
        IBrandDal Brand { get; }
        ICarDal Car { get; }
        ICarImageDal CarImage { get; }
        ICartItemDal CartItem { get; }
        IColorDal Color { get; }
        ICustomerDal Customer { get; }
        IRentalDal Rental { get; }
        IUserDal User { get; }        
        Task SaveAsync();

    }
}
