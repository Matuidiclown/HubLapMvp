using HubLap.Business.Interfaces;
using HubLap.Data.Interfaces;
using HubLap.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubLap.Business.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task CreateBooking(BookingHeader booking)
        {
            // Validamos la fecha de inicio contra el servidor (UTC o Local según tu BD)
            if (booking.BookingStart < DateTime.Now)
            {
                throw new ArgumentException("No se puede reservar en una fecha pasada.");
            }

            // Validamos que no sea al revés
            if (booking.BookingEnd <= booking.BookingStart)
            {
                throw new ArgumentException("La fecha de fin debe ser posterior a la de inicio.");
            }

            // Validamos duración (Máximo 5 horas)
            var duration = booking.BookingEnd - booking.BookingStart;
            if (duration.TotalHours > 5)
            {
                throw new ArgumentException("Las reservas no pueden exceder las 5 horas.");
            }

            await _bookingRepository.CreateBooking(booking);
        }

        // DENTRO DE BookingService.cs
        public async Task<IEnumerable<BookingHeader>> GetAllBookings()
        {
            // El Service le pide los datos al Repository, NO a la DB directamente
            return await _bookingRepository.GetAllBookings();
        }
        public async Task<BookingHeader?> GetBookingById(int id)
        {
            return await _bookingRepository.GetBookingById(id);
        }

        public async Task UpdateBooking(BookingHeader booking)
        {
            if (booking.BookingStart < DateTime.Now)
                throw new ArgumentException("No puedes mover una reserva al pasado.");

            if (booking.BookingEnd <= booking.BookingStart)
                throw new ArgumentException("La fecha de fin debe ser posterior a la de inicio.");

            await _bookingRepository.UpdateBooking(booking);
        }

        public async Task DeleteBooking(int id)
        {
            await _bookingRepository.DeleteBooking(id);
        }
    }
}
