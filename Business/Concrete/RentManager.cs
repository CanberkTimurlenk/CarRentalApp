using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;

namespace Business.Concrete
{


    public class RentManager : IRentService
    {
        private readonly IRentDal _rentDal;

        //newlemek yerine dependency injection yapıyoruz
        

        public RentManager(IRentDal rentDal)  //constructor oluşturduk
        { 
            _rentDal = rentDal;   //newlemek yerine dependency injection yapıyoruz

        }

       

        public List<Car> GetAll()
        {
            return _rentDal.GetAll();

        }

        public List<Car> GetById(int Id)
        {
            return _rentDal.GetById(Id);

        }

        public void Add(Car car)
        {

            _rentDal.Add(car);


        }

        public void Update(Car car)
        {

            _rentDal.Update(car);


        }


        public void Delete(Car car)
        {

            _rentDal.Delete(car);


        }




    }
}
