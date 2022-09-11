using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class InMemoryRentDal : IRentDal

    {


        List<Car> _car;     //Global değişken naming convention da böyle tanımlanır. veritabanını simüle etmek için bir car listesi oluşturduk
        //_car

        public InMemoryRentDal () //constructor olarak tanımlandı, bir listeyle geliyor.. ctor yazabilir constructor oluştururz
            //bellekte newlendiği (referans aldığı zaman çalışacak kısım)
        {
             
            _car = new List<Car>
            {
                

                new Car{Id = 1 , BrandId = 1 , ColorId = 1 , DailyPrice = 1000 , ModelYear = 1990 , Description = "New"},
                new Car{Id = 2 , BrandId = 1 , ColorId = 2 , DailyPrice = 2000 , ModelYear = 2000 , Description = "New"},
                new Car{Id = 3 , BrandId = 2 , ColorId = 3 , DailyPrice = 3000 , ModelYear = 2005 , Description = "New"},
                new Car{Id = 4 , BrandId = 2 , ColorId = 4 , DailyPrice = 4000 , ModelYear = 2010 , Description = "New"},
                new Car{Id = 5 , BrandId = 3 , ColorId = 5 , DailyPrice = 5000 , ModelYear = 2020 , Description = "New"}
              

               

            };

        }
        

        public List<Car> GetById(int Id)
        {
            return _car.Where(c => c.Id == Id).ToList();

        }

        public List<Car> GetAll()
        {
            return _car; //method list return etmeliydi, oluşturduğumuz fake veritabanını aynen return ediyor, o da zaten listti.
        }

        public void Add(Car car)
        {
            _car.Add(car); // car nesnesi listeye ekleniyor add komutuyla
        }

        public void Update(Car car)
        {
            Car carToUpdate = _car.SingleOrDefault(c => c.Id == car.Id);

            _car.Remove(carToUpdate); // class ın kendisini yazdık

        }

        public void Delete(Car car)
        { 

            Car carToDelete = _car.SingleOrDefault(c => c.Id == car.Id);

            _car.Remove(carToDelete); // class ın kendisini yazdık

        }


    }
}
