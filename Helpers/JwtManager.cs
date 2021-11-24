using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LaundryWebApi.Helpers
{
    public class JwtManager : IJwtManager
    {


        private readonly AppSettings _appSettings;


        public JwtManager(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;       
        }

        public string GenerateToken(User authUser)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);


            var header = new JwtHeader(credentials);

            var payLoad = new JwtPayload(authUser.Id.ToString(), null, null, null, DateTime.Today.AddDays(1));

            var securityToken = new JwtSecurityToken(header, payLoad);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
         
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret));

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = secretKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken) validatedToken;
        }

        
    }
}
