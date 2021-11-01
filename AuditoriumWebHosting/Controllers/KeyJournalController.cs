namespace AuditoriumWebHosting.Controllers
{
    using System.Threading.Tasks;
    using AuditoriumWebHosting.Services;
    using Microsoft.AspNetCore.Mvc;

    public class KeyJournalController : Controller
    {
        private readonly KeyJournalService _keyJournalService;

        public KeyJournalController(KeyJournalService keyJournalService)
        {
            _keyJournalService = keyJournalService;
        }

        public async Task<IActionResult> CreateKeyJournal([FromBody] int bookingId)
        {
            return Ok(await _keyJournalService.CreateKeyJournal(bookingId));
        }

        public async Task<IActionResult> UpdateKeyJournal([FromBody] int keyJournalId)
        {
            return Ok(await _keyJournalService.UpdateKeyJournal(keyJournalId));
        }
    }
}