using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaundryWebApi.Dtos;
using LaundryWebApi.Models;

namespace LaundryWebApi.Interface
{
    public interface IBookingRepository
    {
        Booking SaveBooking(Booking booking);

        Booking GetBookingsById(BookingDto dto, int id);

        List<Booking> GetBookings();

        Booking Delete(int id, BookingDto dto);
        
    }
}
