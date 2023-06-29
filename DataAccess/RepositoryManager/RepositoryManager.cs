using Core.Entities.Abstract;
using DataAccess.Abstract;
using DataAccess.Abstract.RepositoryManager;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CarAppContext _context;
        private readonly IBrandDal _brandDal;
        private readonly ICarDal _carDal;
        private readonly ICarImageDal _carImageDal;
        private readonly ICartItemDal _cartItemDal;
        private readonly IColorDal _colorDal;
        private readonly ICustomerDal _customerDal;
        private readonly IRentalDal _rentalDal;
        private readonly IUserDal _userDal;

        public RepositoryManager(CarAppContext context,
            IBrandDal brandDal,
            ICarDal carDal,
            ICarImageDal carImageDal,
            ICartItemDal cartItemDal,
            IColorDal colorDal,
            ICustomerDal customerDal,
            IRentalDal rentalDal,
            IUserDal userDal)
        {
            _context = context;
            _brandDal = brandDal;
            _carDal = carDal;
            _carImageDal = carImageDal;
            _cartItemDal = cartItemDal;
            _colorDal = colorDal;
            _customerDal = customerDal;
            _rentalDal = rentalDal;
            _userDal = userDal;
        }

        public IBrandDal Brand => _brandDal;
        public ICarDal Car => _carDal;
        public ICarImageDal CarImage => _carImageDal;
        public ICartItemDal CartItem => _cartItemDal;
        public IColorDal Color => _colorDal;
        public ICustomerDal Customer => _customerDal;
        public IRentalDal Rental => _rentalDal;
        public IUserDal User => _userDal;

        public void Save()
        {
            _context.SaveChanges();
            

        }

    }
}
