namespace AuditoriumWebHosting.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AuditoriumWebHosting.Entities;
    using AuditoriumWebHosting.Models;

    public class KeyJournalService
    {
        private readonly ApplicationContext _applicationContext;

        public KeyJournalService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<OperationResult> CreateKeyJournal(int bookingId)
        {
            try
            {
                var keyJounral = new KeyJournal
                {
                    Booking = _applicationContext.Bookings.Single(x => x.Id == bookingId),
                    StartDateTime = DateTime.Now
                };

                var saveResult = await _applicationContext.KeyJournals.AddAsync(keyJounral);
                await _applicationContext.SaveChangesAsync();

                return new OperationResult
                {
                    IsSuccess = true,
                    Data = saveResult.Entity.Id
                };
            }
            catch (Exception exception)
            {
                return new OperationResult(false, exception.Message);
            }
        }
        
        public async Task<OperationResult> UpdateKeyJournal(int bookingId)
        {
            try
            {
                var keyJounral = _applicationContext.KeyJournals.Single(x => x.Id == bookingId);
                
                keyJounral.EndDateTime = DateTime.Now;

                _applicationContext.Update(keyJounral);
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