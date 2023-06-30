using Business.Abstract;
using Entities.Concrete.DTOs.Customer;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CustomerForManipulationDto customerDtoForManipulation)
        {
            var result = await _customerService.AddAsync(customerDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, CustomerForManipulationDto customerDtoForManipulation)
        {
            var result = await _customerService.UpdateAsync(id, customerDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customerService.GetByIdAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(CustomerParamaters customerParameters)
        {
            var pagedResult = await _customerService.GetAllAsync(customerParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }
    }
}
