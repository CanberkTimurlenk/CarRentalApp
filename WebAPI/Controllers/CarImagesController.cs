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
        public async Task<IActionResult> Add([FromForm] IFormFile newFile, [FromForm] CarImageForManipulationDto carImageDtoForManipulation)
        {

            if (newFile is not null)
            {
                var result = await _carImageService.AddAsync(newFile, carImageDtoForManipulation);

                if (result.Success)
                    return Ok();

            }

            return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] IFormFile newFile, int id, [FromForm] CarImageForManipulationDto carImageDtoForManipulation)
        {

            var result = await _carImageService.UpdateAsync(newFile, id, carImageDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var result = await _carImageService.DeleteAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbycarid")]
        public async Task<IActionResult> GetByCarId([FromForm] int carImageId, [FromQuery] CarImageParameters carImageParameters)
        {
            var pagedResult = await _carImageService.GetByCarIdAsync(carImageParameters, carImageId, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromForm] int carImageId)
        {
            var result = await _carImageService.GetByIdAsync(carImageId, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] CarImageParameters carImageParameters)
        {
            var pagedResult = await _carImageService.GetAllAsync(carImageParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
