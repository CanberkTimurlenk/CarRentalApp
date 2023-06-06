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
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Cache;

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


        [CacheAspect]
        public IDataResult<IEnumerable<Car>> GetAll()
        {

            return new SuccessDataResult<IEnumerable<Car>>(_carDal.GetAll(), Messages.CarsListed);
            //return _carDal.GetAll();

        }

        public IDataResult<Car> GetById(int Id)
        {


            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == Id), Messages.SuccessListedById);
            //return _carDal.Get(c => c.Id == id);

            //  Burda paramatre olarak belirttiğimiz id aslında primary key yani o id ye sahip
            //  tek bir car mevcut, lambda expression olarak bunu bulduruyoruz
            //  return type Car olarak verdim, çünkü tek bir Car arıyoruz !! birden fazla olmadığı için
            //  listeye gerek yok..

        }
        
        //[SecuredOperation("car.add,admin")]
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


        public IDataResult<IEnumerable<Car>> GetCarsByColorId(int colorId)
        {

            return new SuccessDataResult<IEnumerable<Car>>(_carDal.GetAll(c => c.ColorId == colorId).ToList());

        }

        public IDataResult<IEnumerable<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<IEnumerable<Car>>(_carDal.GetAll(c => c.BrandId == brandId).ToList());

        }

        [CacheAspect]
        public IDataResult<IEnumerable<CarDetailDto>> GetAllCarDetails()
        {

            return new SuccessDataResult<IEnumerable<CarDetailDto>>(_carDal.GetAllCarDetails());

        }


    }
}
