using Business.Abstract;
using Entities.Concrete.DTOs.CartItem;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(CartItemForManipulationDto cartItemDtoForManipulation)
        {
            var result = await _cartItemService.AddAsync(cartItemDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, CartItemForManipulationDto cartItemDtoForManipulation)
        {
            var result = await _cartItemService.UpdateAsync(id, cartItemDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cartItemService.DeleteAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cartItemService.GetByIdAsync(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] CartItemParameters cartItemParameters)
        {
            var pagedResult = await _cartItemService.GetAllAsync(cartItemParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
