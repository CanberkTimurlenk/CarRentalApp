using Business.Abstract;
using Entities.Concrete.DTOs.Rental;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {

        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("add")]
        public IActionResult Add(RentalForManipulationDto rentalDtoForManipulation)
        {
            var result = _rentalService.Add(rentalDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public IActionResult Update(int id, RentalForManipulationDto rentalDtoForManipulation)
        {
            var result = _rentalService.Update(id, rentalDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _rentalService.Delete(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _rentalService.GetById(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] RentalParameters rentalParameters)
        {
            var pagedResult = _rentalService.GetAll(rentalParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

        [HttpGet("getallrentaldetails")]
        public IActionResult GetAllRentalDetails([FromQuery] RentalParameters rentalParameters)
        {
            var pagedResult = _rentalService.GetAllRentalDetails(rentalParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
