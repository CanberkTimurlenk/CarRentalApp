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
        IRentDal _RentDal;
        //newlemek yerine dependency injection yapıyoruz
        

        public RentManager(IRentDal RentDal)
        {
            _RentDal = RentDal;  

        }

        //constructor oluşturduk

        public List<Car> GetAll()
        {
            return _RentDal.GetAll();

        }

        public List<Car> GetById(int Id)
        {
            return _RentDal.GetById(Id);

        }

        public void Add(Car car)
        {

            _RentDal.Add(car);


        }

        public void Update(Car car)
        {

            _RentDal.Update(car);


        }


        public void Delete(Car car)
        {

            _RentDal.Delete(car);


        }




    }
}
