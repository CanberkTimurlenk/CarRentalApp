using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //      api/Cars ile erişim sağlayacağım 

    public class CarsController : ControllerBase
    {

        //loosely coupled class
        private readonly ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;   //  Dependency Injection
        }

        /*
         * List of Operations
         * 
         * Add
         * Update
         * Delete
         * GetAllCarDetails
         * GetCarsByBrandId
         * GetCarsByColorId
         * GetById
         * GetAll
         * 
         */

        [HttpPost("add")]
        public IActionResult Add(Car car) 
        {

            var result = _carService.Add(car);

            // gönderdiğimiz car objesi business a gider eğer business da yazdığımız koşullara uyarsa
            // business Data access katmanındaki add methodunu çalıştırır 
            // SuccessResult, success durumu ve message içeren bir class döndürür
            // koşullar sağlanmazsa ErrorResult, success durumu (false) ve message içeren bir class döner

            if (result.Success)
            {
                return Ok(result);
            
            }
            
            return BadRequest(result);
        
        }
        
        [HttpPost("update")]
        public IActionResult Update(Car car) 
        { 
            var result = _carService.Update(car);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }
           
        [HttpPost("delete")]
        public IActionResult Delete(Car car) 
        {
            var result = _carService.Delete(car );

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }

        
        [HttpGet("getallcardetails")]
        public IActionResult GetAllCarDetails() 
        {
            var result = _carService.GetAllCarDetails();

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);


        }

        
        [HttpPost("getcarsbycolorid")]
        public IActionResult GetCarsByColorId(int id) 
        {
            var result = _carService.GetCarsByColorId(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }

        
        [HttpPost("getcarsbybrandid")]
        public IActionResult GetCarsByBrandId(int id) 
        { 
            var result = _carService.GetCarsByBrandId(id);

            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);        
        }

        
        [HttpPost("getbyid")]
        public IActionResult GetById(int id) 
        {
            var result = _carService.GetById(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }
    
        
        [HttpGet("getall")]
        public IActionResult GetAll()         
        
        {
            var result = _carService.GetAll();

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }    

    }
}
