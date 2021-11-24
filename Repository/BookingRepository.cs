using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaundryWebApi.Data;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;

namespace LaundryWebApi.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly UserContext _userContext;

        public BookingRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
       /*
        public List<Booking> GetBookingsByDate(DateTime date)
        {
           return _userContext.bookings.Where(u => u.Start == date).ToList();
        }
       */

        public Booking GetBookingsById(BookingDto dto, int id)
        {
            var date = dto.Start;
            Console.WriteLine(date);
            Console.WriteLine(dto.Start);
            Console.WriteLine(dto.End);

            return (Booking)_userContext.bookings.Where(i => i.UserId == id && i.Start == date);

        }

        public List<Booking> GetBookings()
        {
            return _userContext.bookings.ToList();
        }

        public Booking SaveBooking(Booking booking)
        {
            _userContext.bookings.Add(booking);
            _userContext.SaveChanges();

            return booking;
        }

        public Booking Delete(int id, BookingDto dto)
        {
            /*
            var booking = _userContext.bookings
                .Where(d => d.Date == dto.Date)
                .Where(s => s.StartTime == dto.StartTime)
                .Where(e => e.EndTime == dto.EndTime)
                .Where(u => u.UserId == id)
                .First();

            if (booking != null) {
                _userContext.bookings.Remove(booking);
                _userContext.SaveChanges();
                return booking;
            }
            */
            return null;
            
        }

        
    }
}
