
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Core.Utilities.Security.Encryption;
using System.Security.Claims;
using Core.Extensions;
using Microsoft.Extensions.Configuration;
using Core.Entities.Concrete;
using Entities.Concrete.Models;
using Entities.Concrete.DTOs.User;

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

        public AccessToken CreateToken(UserDto userDto, IEnumerable<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, userDto, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken {
                Expiration = _accessTokenExpiration,
                Token = token
            };


        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, UserDto userDto, SigningCredentials signingCredentials, IEnumerable<OperationClaim> operationClaims)
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



        private IEnumerable<Claim> SetClaims(UserDto userDto, IEnumerable<OperationClaim> operationClaims)
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
