using Business.Abstract;
using Entities.Concrete.DTOs.CarImage;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public IActionResult Add([FromForm] IFormFile newFile, [FromForm] CarImageDtoForManipulation carImageDtoForManipulation)
        {

            if (newFile is not null)
            {
                var result = _carImageService.Add(newFile, carImageDtoForManipulation);

                if (result.Success)
                    return Ok();

            }

            return BadRequest();
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm] IFormFile newFile, int id, [FromForm] CarImageDtoForManipulation carImageDtoForManipulation)
        {

            var result = _carImageService.Update(newFile, id, carImageDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromForm] int id)
        {
            var result = _carImageService.Delete(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbycarid")]
        public IActionResult GetByCarId([FromForm] int carImageId, [FromQuery] CarImageParameters carImageParameters)
        {
            var pagedResult = _carImageService.GetByCarId(carImageParameters, carImageId, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromForm] int carImageId)
        {
            var result = _carImageService.GetById(carImageId, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] CarImageParameters carImageParameters)
        {
            var pagedResult = _carImageService.GetAll(carImageParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
