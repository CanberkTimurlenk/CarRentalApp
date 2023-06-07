using Business.Abstract;
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

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
        public IActionResult Add([FromForm] IFormFile newFile, [FromForm] CarImage newCarImage)
        {

            if (newFile != null)
            {
                var result = _carImageService.Add(newFile, newCarImage);

                if (result.Success) return Ok();
                
            }

            return BadRequest();
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile newFile,[FromForm] CarImage updatedCarImage)
        {

            var result = _carImageService.Update(newFile , updatedCarImage);

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }       
        [HttpPost("delete")]
        public IActionResult Delete([FromForm] CarImage carImage)
        {

            var result = _carImageService.Delete(carImage);

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }                
        [HttpGet("getbycarid")]
        public IActionResult GetByCarId([FromForm] int carImageId)
        {

            var result = _carImageService.GetByCarId(carImageId);

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }
        [HttpGet("getbyid")]
        public IActionResult GetById([FromForm] int carImageId)
        {

            var result = _carImageService.GetById(carImageId);

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {

            var result = _carImageService.GetAll();

            if (result.Success)
            {
                return Ok(result);


            }

            return BadRequest(result);

        }


    }
}
