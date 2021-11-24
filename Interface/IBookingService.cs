using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaundryWebApi.Dtos;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IBookingService
    {
        Booking CreateBooking(BookingDto dto, int authUserId);

        IEnumerable<Booking> GetTimeSlots();

        Booking DeleteBooking(int userID, BookingDto dto);

        
    }
}
