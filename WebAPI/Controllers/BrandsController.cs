using Business.Abstract;
using Entities.Concrete.DTOs.Brand;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(BrandForManipulationDto brandDtoForManipulation)
        {
            var result = await _brandService.AddAsync(brandDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, BrandForManipulationDto brandDtoForManipulation)
        {
            var result = await _brandService.UpdateAsync(id, brandDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _brandService.DeleteAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int brandId)
        {
            var result = await _brandService.GetByIdAsync(brandId, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] BrandParameters brandParameters)
        {
            var pagedResult = await _brandService.GetAllAsync(brandParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
