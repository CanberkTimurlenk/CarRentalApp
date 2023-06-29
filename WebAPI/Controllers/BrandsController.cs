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
        public IActionResult Add(BrandForManipulationDto brandDtoForManipulation)
        {
            var result = _brandService.Add(brandDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public IActionResult Update(int id, BrandForManipulationDto brandDtoForManipulation)
        {
            var result = _brandService.Update(id, brandDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _brandService.Delete(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int brandId)
        {
            var result = _brandService.GetById(brandId, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] BrandParameters brandParameters)
        {
            var pagedResult = _brandService.GetAll(brandParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
