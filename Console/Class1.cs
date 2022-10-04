using System;
using Business.Concrete;
using Business.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework;


namespace Program;

 class Class1
{
    
    static void Main(string[] args)
    {
        
        Car car1 = new Car
        {
            CarId = 1,
            CarName = "first car",
            BrandId = 1,
            ColorId = 1,
            DailyPrice = 1,
            ModelYear = 1,
            Description = "text 1 "

        };

        Car car2 = new Car
        {
            CarId = 2,
            CarName = "second car",
            BrandId = 2,
            ColorId = 2,
            DailyPrice = 2,
            ModelYear = 1,
            Description = "text 2 "

        };

        Car car3 = new Car
        {
            CarId = 3,
            CarName = "third car",
            BrandId = 1,
            ColorId = 1,
            DailyPrice = 23,
            ModelYear = 1,
            Description = "text 3 "

        };

        Color color1 = new Color
        {
            ColorId = 1,
            ColorName = "Color a"
        };

        Brand brand1 = new Brand
        {
            BrandId = 1,
            BrandName = "Brand a"

        };
        
        /*
        AddCarTest(car1);
        AddCarTest(car2);
        AddCarTest(car3);

        */
      
        
        

        CarManager carManager = new CarManager(new EfCarDal());
        BrandManager brandManager = new BrandManager(new EfBrandDal());
        ColorManager colorManager = new ColorManager(new EfColorDal());

      
        
        
        /*

        foreach (var car in carManager.GetAllCarDetails())
        {
            Console.WriteLine("{0} / {1} / {2} / {3}" , car.CarName , car.BrandName , car.ColorName , car.DailyPrice);
        }
        
        */

}

    private static void AddCarTest(Car car)
    {
        CarManager carManager = new CarManager(new EfCarDal());
        carManager.Add(car);
    }


    private static void AddBrandTest(Brand brand)
    {
        BrandManager brandManager = new BrandManager(new EfBrandDal());
        brandManager.Add(brand);
    }

    private static void AddColorTest(Color color)
    {
        ColorManager colorManager = new ColorManager(new EfColorDal());
        colorManager.Add(color);
    }

   
}

