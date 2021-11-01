namespace AuditoriumWebHosting.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AuditoriumWebHosting.Dto;
    using AuditoriumWebHosting.Services;
    using Microsoft.AspNetCore.Mvc;

    public class AuditoryController : Controller
    {
        private readonly AuditoryService _auditoryService;

        public AuditoryController(AuditoryService auditoryService)
        {
            _auditoryService = auditoryService;
        }

        public async Task<IActionResult> CreateAuditory([FromBody] AuditoryDto auditoryDto)
        {
            return Ok(await _auditoryService.CreateAuditory(auditoryDto));
        }

        public async Task<IActionResult> DeleteAuditory(int auditoryId)
        {
            return Ok(await _auditoryService.DeleteAuditory(auditoryId));
        }

        public async Task<List<AuditoryListDto>> GetAuditories()
        {
            return await _auditoryService.GetAuditories();
        }
    }
}