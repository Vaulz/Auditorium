namespace AuditoriumWebHosting.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AuditoriumWebHosting.Dto;
    using AuditoriumWebHosting.Models;
    using AuditoriumWebHosting.Services;
    using Microsoft.AspNetCore.Mvc;

    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<IActionResult> CreateBooking([FromBody] BookingDto bookingDto)
        {
            return Ok(await _bookingService.CreateBooking(bookingDto));
        }

        public async Task<IActionResult> DeleteBooking([FromBody] int bookingId)
        {
            return Ok(await _bookingService.DeleteBooking(bookingId));
        }

        public async Task<List<BookingInfoListDto>> GetAuditoryBookings([FromBody] BookingDto bookingDto)
        {
            return await _bookingService.GetAuditoryBookings(bookingDto);
        }

        public async Task<List<UserBookingDto>> GetUserBookings([FromBody] int userId)
        {
            return await _bookingService.GetUserBookings(userId);
        }

        public async Task<OperationResult> GetAuditoryKey([FromBody] int bookingId)
        {
            return await _bookingService.GetAuditoryKey(bookingId);
        }
    }
}