using Business.Abstract;
using Core.Entities.Concrete.DTOs.Token;
using Core.Entities.Concrete.DTOs.User;
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
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);

            if (!userToLogin.Success)
                return BadRequest(userToLogin.Message);

            var result = await _authService.CreateToken(userToLogin.Data,true);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var registerResult = await _authService.Register(userForRegisterDto);

            if (!registerResult.Success)
                return BadRequest(registerResult.Message);

            var result = await _authService.CreateToken(registerResult.Data,true);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);

        }

        [HttpPost("refresh")]
        public  async Task<IActionResult> Refresh([FromBody] TokenForRefreshDto refreshTokenDto)
        {
            var tokenDtoToReturn = await _authService
                .RefreshToken(refreshTokenDto);

            return Ok(tokenDtoToReturn);
        }

    }
}
