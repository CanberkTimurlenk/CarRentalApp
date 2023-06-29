using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Core.Utilities.Security.Encryption;
using System.Security.Claims;
using Core.Extensions;
using Microsoft.Extensions.Configuration;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.DTOs.OperationClaim;
using System.Security.Cryptography;
using Core.Entities.Concrete.DTOs.Token;

namespace Core.Utilities.Security.Jwt
{

    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); // in case of missing reference, install Microsoft.Extensions.Configuration.Binder with nuget

        }

        public TokenDto CreateToken(UserDto userDto, IEnumerable<OperationClaimDto> operationClaims, DateTime userRefreshTokenExp, bool populateRefreshTokenExp)
        {            
            var accessToken = CreateAccessToken(userDto, operationClaims);
            var refreshToken = CreateRefreshToken();

            if (populateRefreshTokenExp)
                userRefreshTokenExp = DateTime.Now.AddDays(3);

            return new TokenDto
            {
                AccessToken = new AccessToken() { Token = accessToken.Token, Expiration = accessToken.Expiration },
                RefreshToken = new RefreshToken() { Token = refreshToken, Expiration = userRefreshTokenExp },
            };

        }

        private AccessToken CreateAccessToken(UserDto userDto, IEnumerable<OperationClaimDto> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, userDto, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Expiration = _accessTokenExpiration,
                Token = token
            };
        }

        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public bool CheckRefreshTokenIsValid(string providedRefreshToken, RefreshToken storedRefreshToken)            
        {
            
            if (storedRefreshToken is null ||
                storedRefreshToken.Token != providedRefreshToken ||
                storedRefreshToken.Expiration <= DateTime.Now)
            {
                return false;
            }

            return true;
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, UserDto userDto, SigningCredentials signingCredentials, IEnumerable<OperationClaimDto> operationClaims)
        {

            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                claims: SetClaims(userDto, operationClaims),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                expires: _accessTokenExpiration

                );

            return jwt;

        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
        {                        
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _tokenOptions.Issuer,
                ValidAudience = _tokenOptions.Audience,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken is null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                     StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }

        private IEnumerable<Claim> SetClaims(UserDto userDto, IEnumerable<OperationClaimDto> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(userDto.Id.ToString());
            claims.AddEmail(userDto.Email);
            claims.AddName($"{userDto.FirstName} {userDto.LastName}");
            var x = operationClaims.Select(c => c.OperationClaimName).ToArray();
            claims.AddRoles(operationClaims.Select(c => c.OperationClaimName).ToArray());

            return claims;
        }
       
    }
}
