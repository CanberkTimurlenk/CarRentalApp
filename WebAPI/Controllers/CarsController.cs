using Business.Abstract;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CarForManipulationDto carDtoForManipulation)
        {

            var result = await _carService.AddAsync(carDtoForManipulation);

            if (result.Success)
                return Ok(result);


            return BadRequest(result);

        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, CarForManipulationDto carDtoForManipulation)
        {
            var result = await _carService.UpdateAsync(id, carDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _carService.DeleteAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getallcardetails")]
        public async Task<IActionResult> GetAllCarDetails(CarParameters carParameters)
        {
            var pagedResult = await _carService.GetAllCarDetailsAsync(carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

        [HttpGet("getcarsbycolorid")]
        public async Task<IActionResult> GetCarsByColorId(int colorId, [FromQuery] CarParameters carParameters)
        {
            var pagedResult = await _carService.GetCarsByColorIdAsync(colorId, carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

        [HttpGet("getcarsbybrandid")]
        public async Task<IActionResult> GetCarsByBrandId(int brandId, [FromQuery] CarParameters carParameters)
        {
            var pagedResult = await _carService.GetCarsByBrandIdAsync(brandId, carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _carService.GetByIdAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] CarParameters carParameters)
        {
            var pagedResult = await _carService.GetAllAsync(carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }
    }
}
