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
        public IActionResult Add(CarDtoForManipulation carDtoForManipulation)
        {

            var result = _carService.Add(carDtoForManipulation);

            if (result.Success)
                return Ok(result);


            return BadRequest(result);

        }

        [HttpPut("update")]
        public IActionResult Update(int id, CarDtoForManipulation carDtoForManipulation)
        {
            var result = _carService.Update(id, carDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _carService.Delete(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getallcardetails")]
        public IActionResult GetAllCarDetails(CarParameters carParameters)
        {
            var pagedResult = _carService.GetAllCarDetails(carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

        [HttpGet("getcarsbycolorid")]
        public IActionResult GetCarsByColorId(int colorId, [FromQuery] CarParameters carParameters)
        {
            var pagedResult = _carService.GetCarsByColorId(colorId, carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

        [HttpGet("getcarsbybrandid")]
        public IActionResult GetCarsByBrandId(int brandId, [FromQuery] CarParameters carParameters)
        {
            var pagedResult = _carService.GetCarsByBrandId(brandId, carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] CarParameters carParameters)
        {
            var pagedResult = _carService.GetAll(carParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }
    }
}
