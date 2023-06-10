using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        [HttpPost("add")]
        public IActionResult Add(UserDtoForManipulation userDtoForManipulation)
        {
            var result = _userService.Add(userDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }
        [HttpPost("update")]
        public IActionResult Update(int id,UserDtoForManipulation userDtoForManipulation)
        {
            var result = _userService.Update(id,userDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }
        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);

            if (result.Success)
                 return Ok(result);

            return BadRequest(result);

        }
        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }
        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] UserParameters userParameters)
        {
            var pagedResult = _userService.GetAll(userParameters);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);
        }
    
    }
}
