using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using LaundryWebApi.Dtos;
using LaundryWebApi.Extensions;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using LaundryWebApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LaundryWebApi.Controllers
{
 
    [Route("api")]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingsService;
        private readonly IUserService _userService;

        public BookingsController(IBookingService bookingsService, IUserService userService)
        {
            _bookingsService = bookingsService;
            _userService = userService;
        }
       

        [HttpPost("book")]
        public IActionResult BookTime([FromBody]BookingDto dto)
        {


            var jwt = Request.Cookies["next-auth.session-token"];

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(jwt);
            var tokenS = jsonToken as JwtSecurityToken;

            var jti = tokenS.Claims.First(claim => claim.Type == "accessToken").Value;



            User user = _userService.VerifyUser(jti);


            return Created("Sucess", _bookingsService.CreateBooking(dto, user.Id));


        }


        [HttpGet("bookings")]
        public IEnumerable<Booking> GetAllBookings()

        {
            try
            {
                var jwt = Request.Cookies["next-auth.session-token"];

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(jwt);
                var tokenS = jsonToken as JwtSecurityToken;

                var jti = tokenS.Claims.First(claim => claim.Type == "accessToken").Value;

                User user = _userService.VerifyUser(jti);

                return _bookingsService.GetTimeSlots();

            }
            catch (Exception e)
            {
                return null;
            }
          

            
        }

        [HttpDelete("delete")]
        public IActionResult DeleteBooking([FromBody] BookingDto dto) {

               try
               {
                   var jwt = Request.Cookies["next-auth.session-token"];

                   var handler = new JwtSecurityTokenHandler();
                   var jsonToken = handler.ReadJwtToken(jwt);
                   var tokenS = jsonToken as JwtSecurityToken;

                   var jti = tokenS.Claims.First(claim => claim.Type == "accessToken").Value;

                   User user = _userService.VerifyUser(jti);




                return Ok(_bookingsService.DeleteBooking(user.Id, dto));

               }
               catch {
                   return null;
               }
            
        }

     

    }
}
