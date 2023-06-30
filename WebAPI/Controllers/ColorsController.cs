using Business.Abstract;
using Entities.Concrete.DTOs.Color;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ColorForManipulationDto colorDtoForManipulation)
        {
            var result = await _colorService.AddAsync(colorDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, ColorForManipulationDto colorDtoForManipulation)
        {
            var result = await _colorService.UpdateAsync(id, colorDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _colorService.DeleteAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _colorService.GetByIdAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] ColorParameters colorParameters)
        {
            var pagedResult = await _colorService.GetAllAsync(colorParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
