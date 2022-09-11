using System;
using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;


namespace Program;

 class Class1
{
     static void Main(string[] args)
    {

        RentManager rentManager =  new RentManager(new InMemoryRentDal());


        //inMeMoryRentDal ın referansını gönderiyoruz

        foreach (var item in rentManager.GetAll())
        {
            Console.WriteLine(item.ModelYear);
        }


    }




}

