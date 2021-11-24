using System;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using LaundryWebApi.Extensions;
using System.IdentityModel.Tokens.Jwt;

namespace LaundryWebApi.Controllers
{
    
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;



public AuthController(IUserService userService)
        {
            _userService = userService;
          
        }


        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {


            return Created("Sucess", _userService.Create(dto));
        }

  
        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            
            var jwt = _userService.Authenticate(dto);

            if (_userService.Authenticate(dto) == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            Response.Cookies.Append("next-auth.session-token", jwt, new CookieOptions
            {
                HttpOnly = true
            });

           

            var user = _userService.VerifyUser(jwt);
            user.Token = jwt;

            return Ok(user);
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try {



                var jwt = Request.Cookies["next-auth.session-token"];

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(jwt);
                var tokenS = jsonToken as JwtSecurityToken;

                var jti = tokenS.Claims.First(claim => claim.Type == "accessToken").Value;

                

                return Ok(_userService.VerifyUser(jti));

            }
            catch (Exception e)
            {
                return Unauthorized();
            }
           
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("next-auth.session-token");

            return Ok(new { messag = "Sucess" });
        }




    }
}
