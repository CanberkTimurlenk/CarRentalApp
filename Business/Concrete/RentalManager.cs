using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentDal = rentalDal;

        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental addedItem)
        {
            //_rentDal.Add(addedItem);
            //return new SuccessResult(Messages.RentalAdded);


            var result = _rentDal.Get(r => r.CarId == addedItem.CarId && r.ReturnDate == null);

            // result class olarak döner, (araç halihazırda kiralanmışsa returnDate= null)
            // result null olarak döner, (henüz kiralanmamış ve eşleşen iD ye sahip araba varsa)
            
            if (result == null) //ekleme yaparız, returndate nulldan farklı olduğu için result null oldu
            {
                _rentDal.Add(addedItem);
                return new SuccessResult(Messages.RentalAdded);
            }

            //else if (result !=null)   araç şuan zaten kirada, ekleme yapmayız... bu if i gizledim
            // result == null çalışmazsa result null a zaten eşit değil demektir o yüzden else if yazmadım
            {
                return new ErrorResult(Messages.InvalidRentalAdd);

            }
           

        }

        public IResult Delete(Rental deletedItem)
        {
            _rentDal.Delete(deletedItem);
            return new SuccessResult(Messages.RentalDeleted);

        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentDal.GetAll(), Messages.RentalsListed);
        }

        public IDataResult<Rental> GetById(int id)
        {

            return new SuccessDataResult<Rental>(_rentDal.Get(r => r.RentalId == id), Messages.SuccessListedById);
        }

        public IResult Update(Rental updatedItem)
        {
            _rentDal.Update(updatedItem);
            return new SuccessResult(Messages.RentalUpdated);
        }



    }
}
