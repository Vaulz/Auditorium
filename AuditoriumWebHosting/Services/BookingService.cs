namespace AuditoriumWebHosting.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using AuditoriumWebHosting.Dto;
    using AuditoriumWebHosting.Entities;
    using AuditoriumWebHosting.Models;
    using Microsoft.EntityFrameworkCore;

    public class BookingService
    {
        private readonly ApplicationContext _applicationContext;

        public BookingService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<OperationResult> CreateBooking(BookingDto bookingDto)
        {
            try
            {
                await ValidateBooking(bookingDto);
                var booking = new Booking
                {
                    Auditory = _applicationContext.Auditories.Single(x => x.Id == bookingDto.AuditoryId),
                    User = _applicationContext.Users.Single(x => x.Id == bookingDto.UserId),
                    StartDateTime = bookingDto.StartDateTime,
                    EndDateTime = bookingDto.EndDateTime
                };

                await _applicationContext.AddAsync(booking);
                await _applicationContext.SaveChangesAsync();

                return new OperationResult(true);
            }
            catch (Exception exception)
            {
                return new OperationResult(false, exception.Message);
            }
        }

        private async Task ValidateBooking(BookingDto bookingDto)
        {
            if (await _applicationContext.Bookings
                .Where(x => x.Auditory.Id == bookingDto.AuditoryId)
                .AnyAsync(x => x.StartDateTime <= bookingDto.StartDateTime && x.EndDateTime >= bookingDto.StartDateTime
                    || x.StartDateTime <= bookingDto.EndDateTime && x.EndDateTime >= bookingDto.EndDateTime))
            {
                throw new ValidationException("Аудитория уже забронирована на данное время");
            }
        }

        public async Task<List<BookingInfoListDto>> GetAuditoryBookings(BookingDto bookingDto)
        {
            return await _applicationContext.Bookings
                .Include(x => x.Auditory)
                .Include(x => x.User)
                .Where(x => x.Auditory.Id == bookingDto.AuditoryId)
                .Where(x => x.StartDateTime.Date == bookingDto.StartDateTime.Date)
                .OrderBy(x => x.StartDateTime)
                .Select(x => new BookingInfoListDto
                {
                    Time = $"{x.StartDateTime.ToShortTimeString()} - {x.EndDateTime.ToShortTimeString()}",
                    UserFullName = $"{x.User.LastName} {x.User.FirstName} {x.User.MiddleName}"
                })
                .ToListAsync();
        }
        
        public async Task<List<UserBookingDto>> GetUserBookings(int userId)
        {
            return await _applicationContext.Bookings
                .Include(x => x.Auditory)
                .Include(x => x.User)
                .Where(x => x.User.Id == userId)
                .OrderBy(x => x.StartDateTime)
                .Select(x => new UserBookingDto
                {
                    Id = x.Id,
                    AuditoryNumber = x.Auditory.Number,
                    Date = x.StartDateTime.ToLongDateString(),
                    Time = $"{x.StartDateTime.ToShortTimeString()} - {x.EndDateTime.ToShortTimeString()}"
                })
                .ToListAsync();
        }
        
        public async Task<OperationResult> GetAuditoryKey(int bookingId)
        {
            var booking = await _applicationContext.Bookings
                .Where(x => x.Id == bookingId)
                .Select(x => new
                {
                    AuditoryKey = x.Auditory.Key,
                    x.StartDateTime
                })
                .SingleAsync();

            if (booking.StartDateTime.AddMinutes(-10) > DateTime.Now)
            {
                return new OperationResult(
                    false,
                    "Пока невозможно открыть аудиторию. Аудиторию можно открыть за 10 минут до начала бронирования");
            }

            return new OperationResult(true, booking.AuditoryKey);
        }

        public async Task<OperationResult> DeleteBooking(int bookingId)
        {
            try
            {
                _applicationContext.Bookings.Remove(_applicationContext.Bookings.Single(x => x.Id == bookingId));
                await _applicationContext.SaveChangesAsync();

                return new OperationResult(true);
            }
            catch (Exception exception)
            {
                return new OperationResult(false, exception.Message);
            }
        }
    }
}