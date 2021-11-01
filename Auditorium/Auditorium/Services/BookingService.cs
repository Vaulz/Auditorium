namespace Auditorium.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Auditorium.Models;
    using Auditorium.ViewModels;

    public class BookingService
    {
        private const string _baseUrl = Helper.BaseUrl + "/Booking/";

        public async Task<List<BookingModel>> GetAuditoryBookings(
            int auditoryId,
            DateTime bookingDate)
        {
            var httpClient = new HttpClient();
            var parameters = new
            {
                AuditoryId = auditoryId,
                StartDateTime = bookingDate
            };

            var response = await Helper.GetResponse(typeof(List<BookingModel>), await httpClient.PostAsync(
                _baseUrl + "GetAuditoryBookings", Helper.GetHttpContent(parameters)));

            return (List<BookingModel>)response;
        }
        
        public async Task<List<UserBookingModel>> GetUserBookings()
        {
            var httpClient = new HttpClient();

            var response = await Helper.GetResponse(typeof(List<UserBookingModel>), await httpClient.PostAsync(
                _baseUrl + "GetUserBookings", Helper.GetHttpContent(Helper.CurrentUser.Id)));

            return (List<UserBookingModel>)response;
        }
        
        public async Task<OperationResult> CreateBooking(
            int auditoryId, DateTime startDateTime, DateTime endDateTime)
        {
            var httpClient = new HttpClient();
            var parameters = new
            {
                UserId = Helper.CurrentUser.Id,
                AuditoryId = auditoryId,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };

            var response = await Helper.GetResponse(typeof(OperationResult), await httpClient.PostAsync(
                _baseUrl + "CreateBooking", Helper.GetHttpContent(parameters)));

            return (OperationResult)response;
        }
        
        public async Task<OperationResult> GetAuditoryKey(int bookingId)
        {
            var httpClient = new HttpClient();

            var response = await Helper.GetResponse(typeof(OperationResult), await httpClient.PostAsync(
                _baseUrl + "GetAuditoryKey", Helper.GetHttpContent(bookingId)));

            return (OperationResult)response;
        }
        
        public async Task<OperationResult> DeleteBooking(int bookingId)
        {
            var httpClient = new HttpClient();

            var response = await Helper.GetResponse(typeof(OperationResult), await httpClient.PostAsync(
                _baseUrl + "DeleteBooking", Helper.GetHttpContent(bookingId)));

            return (OperationResult)response;
        }
    }
}