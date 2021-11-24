using System;
namespace LaundryWebApi.Dtos
{
    public class RegisterDto
    {
        public string AppartmentNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
