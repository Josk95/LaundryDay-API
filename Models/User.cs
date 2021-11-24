using System;
using System.Text.Json.Serialization;

namespace LaundryWebApi.Models
{
    public class User
    {

        public int Id { get; set; }
        public string AppartmentNumber { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string  Password { get; set; }
        public string Token { get; set; }
        

    }
}
