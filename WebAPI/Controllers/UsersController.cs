using Business.Abstract;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.RequestFeatures;
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
        public async Task<IActionResult> Add(UserForManipulationDto userDtoForManipulation)
        {
            var result = await _userService.AddAsync(userDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, UserForManipulationDto userDtoForManipulation)
        {
            var result = await _userService.UpdateAsync(id, userDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] UserParameters userParameters)
        {
            var pagedResult = await _userService.GetAllAsync(userParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
