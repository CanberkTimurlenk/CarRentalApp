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

        /*
        * List of Operations
        * Add
        * Update
        * Delete
        * GetById
        * GetAll
        * 
        */


        [HttpPost("add")]
        public IActionResult Add(CustomerDtoForManipulation customerDtoForManipulation)
        {
            var result = _customerService.Add(customerDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public IActionResult Update(int id, CustomerDtoForManipulation customerDtoForManipulation)
        {
            var result = _customerService.Update(id, customerDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _customerService.Delete(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetById(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public IActionResult GetAll(CustomerParamaters customerParameters)
        {
            var pagedResult = _customerService.GetAll(customerParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }
    }
}
