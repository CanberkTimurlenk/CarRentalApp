using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete.DTOs.Rental;
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        /*
         * List of Operations
         * Add
         * Update
         * Delete
         * GetById
         * GetAll
         * GetAllRentalDetails
         * 
         */


        [HttpPost("add")]
        public IActionResult Add(RentalDtoForManipulation rentalDtoForManipulation)
        {

            var result = _rentalService.Add(rentalDtoForManipulation);

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
        public IActionResult Update(int id, RentalDtoForManipulation rentalDtoForManipulation)
        {
            var result = _rentalService.Update(id, rentalDtoForManipulation);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _rentalService.Delete(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _rentalService.GetById(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpGet("getall")]
        public IActionResult GetAll()

        {
            var result = _rentalService.GetAll();

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpGet("getallrentaldetails")]
        public IActionResult GetAllRentalDetails()
        {
            var result = _rentalService.GetAllRentalDetails();

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


    }
}
