using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Add(CartItem cartItem)
        {

            var result = _cartItemService.Add(cartItem);

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
        public IActionResult Update(CartItem cartItem)
        {
            var result = _cartItemService.Update(cartItem);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete(CartItem cartItem)
        {
            var result = _cartItemService.Delete(cartItem);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpPost("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _cartItemService.GetById(id);

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


        [HttpGet("getall")]
        public IActionResult GetAll()

        {
            var result = _cartItemService.GetAll();

            if (result.Success)
            {
                return Ok(result);

            }

            return BadRequest(result);

        }


    }
}
