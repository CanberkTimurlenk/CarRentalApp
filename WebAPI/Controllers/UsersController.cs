using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
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
        public IActionResult Add(UserDtoForManipulation userDtoForManipulation)
        {

            var result = _userService.Add(userDtoForManipulation);

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
        public IActionResult Update(int id,UserDtoForManipulation userDtoForManipulation)
        {
            var result = _userService.Update(id,userDtoForManipulation);

            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpGet("getall")]
        public IActionResult GetAll()

        {
            var result = _userService.GetAll();

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }
    }
}
