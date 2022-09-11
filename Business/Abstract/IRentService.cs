using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace Business.Abstract
{
     public interface IRentService
    {

        
        void Update(Car car);
        void Delete(Car car);
        List<Car> GetAll();
        List<Car> GetById(int id);
        void Add(Car car);


    }

}
