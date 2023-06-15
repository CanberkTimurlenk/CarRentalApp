using DataAccess.Abstract;
using DataAccess.Abstract.RepositoryManager;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;

namespace DataAccess.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly CarAppContext _context;
        private readonly Lazy<IBrandDal> _brandDal;
        private readonly Lazy<ICarDal> _carDal;
        private readonly Lazy<ICarImageDal> _carImageDal;
        private readonly Lazy<ICartItemDal> _cartItemDal;
        private readonly Lazy<IColorDal> _colorDal;
        private readonly Lazy<ICustomerDal> _customerDal;
        private readonly Lazy<IRentalDal> _rentalDal;
        private readonly Lazy<IUserDal> _userDal;

        public RepositoryManager(CarAppContext context)
        {
            _context = context;
            _brandDal = new Lazy<IBrandDal>(() => new EfBrandDal(_context));
            _carDal = new Lazy<ICarDal>(() => new EfCarDal(_context));
            _carImageDal = new Lazy<ICarImageDal>(() => new EfCarImageDal(_context));
            _cartItemDal = new Lazy<ICartItemDal>(() => new EfCartItemDal(_context));
            _colorDal = new Lazy<IColorDal>(() => new EfColorDal(_context));
            _customerDal = new Lazy<ICustomerDal>(() => new EfCustomerDal(_context));
            _rentalDal = new Lazy<IRentalDal>(() => new EfRentalDal(_context));
            _userDal = new Lazy<IUserDal>(() => new EfUserDal(_context));

        }

        public IBrandDal Brand => _brandDal.Value;
        public ICarDal Car => _carDal.Value;
        public ICarImageDal CarImage => _carImageDal.Value;
        public ICartItemDal CartItem => _cartItemDal.Value;
        public IColorDal Color => _colorDal.Value;
        public ICustomerDal Customer => _customerDal.Value;
        public IRentalDal Rental => _rentalDal.Value;
        public IUserDal User => _userDal.Value;

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
