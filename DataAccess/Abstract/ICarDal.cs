using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using Core.DataAccess;
using Entities.Concrete.DTOs;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        public List<CarDetailDto> GetAllCarDetails();
        // yeni oluşturduğumuz CarDetailDto isimli entity class ı istediğimiz verileri prop olarak tutuyor
        // return type ı bu class ait objectler olan bir list 
        // parametre almıyor tüm araçların detayını dökecek.
        // ICarDal , car ile alakalı tüm işlemlerin signature (imzalarının) yazıldığı interface
        // Entity framework ya da çalışılan platform her neyse bu methodun içeriğini ona göre dolduracak
        // bu method özünde sadece car ile alakalı olduğu için sadece buraya yazdık :) !!

    }
}
