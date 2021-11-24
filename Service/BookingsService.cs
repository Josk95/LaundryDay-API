using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LaundryWebApi.Dtos;
using LaundryWebApi.Interface;
using LaundryWebApi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace LaundryWebApi.Service
{
    public class BookingsService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public BookingsService(IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;  
    }

        public Booking CreateBooking(BookingDto dto, int authUserId)
        {
            Booking newBooking = new Booking();

            var booking = _bookingRepository.GetBookingsById(dto, authUserId);

            if (booking == null) {

                newBooking.UserId = authUserId;
                newBooking.Start = dto.Start;
                newBooking.End = dto.End;


                _bookingRepository.SaveBooking(newBooking);
            }

            return newBooking;
        }

        public Booking DeleteBooking(int userID, BookingDto dto)
        {

            User user = _userRepository.GetById(userID);

            

            _userRepository.Update(user);

            return _bookingRepository.Delete(userID, dto);
        }

        public IEnumerable<Booking> GetTimeSlots()
        {

            List<Booking> bookings = new List<Booking>();

            bookings = _bookingRepository.GetBookings();


            return bookings;

        }
    }

}

