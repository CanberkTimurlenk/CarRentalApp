using Business.Abstract;
using Core.Entities.Concrete;
using Core.Entities.Concrete.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (!userToLogin.Success) return BadRequest(userToLogin.Message);

            var result = _authService.CreateAccessToken(userToLogin.Data);

            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Data);


        }


        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = _authService.Register(userForRegisterDto);

            if (!registerResult.Success) return BadRequest(registerResult.Message);

            var result = _authService.CreateAccessToken(registerResult.Data);

            if (!result.Success) return BadRequest(result.Message);

            return Ok(result.Data);


        }
    }
}
