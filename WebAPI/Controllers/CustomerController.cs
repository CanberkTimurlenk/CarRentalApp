using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private readonly ICustomerService _customerService;
        
        public CustomerController(ICustomerService customerService)
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
        public IActionResult Add(Customer customer)
        {

            var result = _customerService.Add(customer);

            // gönderdiğimiz  obje business a gider eğer business da yazdığımız koşullara uyarsa
            // business Data access katmanındaki add methodunu çalıştırır 
            // SuccessResult, success durumu ve message içeren bir class döndürür
            // koşullar sağlanmazsa ErrorResult, success durumu (false) ve message içeren bir class döner

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }

        [HttpPost("update")]
        public IActionResult Update(Customer customer)
        {
            var result = _customerService.Update(customer);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete(Customer customer)
        {
            var result = _customerService.Delete(customer);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _customerService.GetById(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpGet("getall")]
        public IActionResult GetAll()

        {
            var result = _customerService.GetAll();

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }
    }
}
