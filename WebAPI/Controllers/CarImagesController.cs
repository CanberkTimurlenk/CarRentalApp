using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class Image
    {
        public IFormFile file { get; set; }

    }

   


    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly ICarImageService _carImageService;
        public CarImagesController(ICarImageService carImageService)
        {

            _carImageService = carImageService;
           

        }


        [HttpPost("add")]
        public IActionResult Add([FromForm] Image file)
        {
            if (file != null)
            {
                return Ok();
            }

            return BadRequest();
        }

        /*

        [HttpPost("delete")]
        public IActionResult Delete(CarImage carImage)
        {

            var result = _carImageService.Delete(carImage);

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }

        [HttpPost("update")]
        public IActionResult Update(CarImage carImage)
        {

            var result = _carImageService.Update(carImage);

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }

        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int id)
        {

            var result = _carImageService.GetByCarId(id);

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }

        */




    }
}
