using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete.DTOs;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.CrossCuttingConcerns.Validation;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{


    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;

        //newlemek yerine dependency injection yapıyoruz


        public CarManager(ICarDal carDal)  //constructor oluşturduk
        {
            _carDal = carDal;   //newlemek yerine dependency injection yapıyoruz

        }



        public IDataResult<List<Car>> GetAll()
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
            //return _carDal.GetAll();

        }

        public IDataResult<Car> GetById(int id)
        {


            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id), Messages.SuccessListedById);
            //return _carDal.Get(c => c.CarId == id);

            //  Burda paramatre olarak belirttiğimiz id aslında primary key yani o id ye sahip
            //  tek bir car mevcut, lambda expression olarak bunu bulduruyoruz
            //  return type Car olarak verdim, çünkü tek bir Car arıyoruz !! birden fazla olmadığı için
            //  listeye gerek yok..

        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {

            //  business codes..
            
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);




        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);



        }


        public IResult Delete(Car car)
        {

            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);


        }


        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id).ToList());

        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id).ToList());

        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetails()
        {

            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails());

        }


    }
}
