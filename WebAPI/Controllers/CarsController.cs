using Business.Abstract;
using Entities.Concrete.DTOs.Car;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]



    public class CarsController : ControllerBase
    {

        //loosely coupled class
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

        [HttpPost("update")]
        public IActionResult Update(int id, CarDtoForManipulation carDtoForManipulation)
        {
            var result = _carService.Update(id, carDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _carService.Delete(id);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }


        [HttpGet("getallcardetails")]
        public IActionResult GetAllCarDetails(CarParameters carParameters)
        {
            var pagedResult = _carService.GetAllCarDetails(carParameters);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }


        [HttpPost("getcarsbycolorid")]
        public IActionResult GetCarsByColorId([FromQuery] CarParameters carParameters, int colorId)
        {
            var pagedResult = _carService.GetCarsByColorId(carParameters, colorId);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }


        [HttpPost("getcarsbybrandid")]
        public IActionResult GetCarsByBrandId([FromQuery] CarParameters carParameters, int brandId)
        {
            var pagedResult = _carService.GetCarsByBrandId(carParameters,brandId);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);
        }


        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }


        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] CarParameters carParameters)
        {
            var pagedResult = _carService.GetAll(carParameters);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }
    }
}
