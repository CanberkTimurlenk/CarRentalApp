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

        /*
        * List of Operations
        * 
        * Add
        * Update
        * Delete
        * GetById
        * GetAll
        * 
        */


        [HttpPost("add")]
        public IActionResult Add(CartItemDtoForManipulation cartItemDtoForManipulation)
        {
            var result = _cartItemService.Add(cartItemDtoForManipulation);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpPut("update")]
        public IActionResult Update(int id, CartItemDtoForManipulation cartItemDtoForManipulation)
        {
            var result = _cartItemService.Update(id, cartItemDtoForManipulation, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var result = _cartItemService.Delete(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _cartItemService.GetById(id, false);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet("getall")]
        public IActionResult GetAll([FromQuery] CartItemParameters cartItemParameters)
        {
            var pagedResult = _cartItemService.GetAll(cartItemParameters, false);

            Response.Headers
                    .Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            if (pagedResult.result.Success)
                return Ok(pagedResult.result);

            return BadRequest(pagedResult.result);

        }

    }
}
