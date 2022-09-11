using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;

namespace DataAccess.Abstract
{
     public interface IRentDal
    {
        List <Car>GetById(int Id); // Return edecek, bir çok car objesi, sonrasında car.id ile print edeceğiz
        List <Car> GetAll(); // Return edecek, bir çok car objesi, sonrasında isteğe göre print edeceğiz
        void Add(Car car); //void olacak,   parametre geçmeliyiz  
        void Update(Car car); //void olacak,   parametre geçmeliyiz  
        void Delete(Car car); //void olacak,   parametre geçmeliyiz


    }
}
