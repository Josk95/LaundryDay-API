using System;
using System.IdentityModel.Tokens.Jwt;
using LaundryWebApi.Dtos;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IJwtManager
    {
        string GenerateToken(User authUser);
        JwtSecurityToken Verify(string jwt);
    }
}
