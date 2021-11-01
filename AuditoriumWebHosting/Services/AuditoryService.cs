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

    public class AuditoryService
    {
        private readonly ApplicationContext _applicationContext;

        public AuditoryService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<OperationResult> CreateAuditory(AuditoryDto auditoryDto)
        {
            try
            {
                ValidateAuditory(auditoryDto);

                var auditory = new Auditory
                {
                    Number = auditoryDto.Number,
                    Capacity = auditoryDto.Capacity,
                    AdditionalInfo = auditoryDto.AdditionalInfo
                };

                await _applicationContext.Auditories.AddAsync(auditory);
                await _applicationContext.SaveChangesAsync();

                return new OperationResult(true);
            }
            catch (Exception exception)
            {
                return new OperationResult(false, exception.Message);
            }
        }

        public async Task<OperationResult> DeleteAuditory(int auditoryId)
        {
            try
            {
                _applicationContext.Auditories.Remove(_applicationContext.Auditories.Single(x => x.Id == auditoryId));
                await _applicationContext.SaveChangesAsync();

                return new OperationResult(true);
            }
            catch (Exception exception)
            {
                return new OperationResult(false, exception.Message);
            }
        }

        public async Task<List<AuditoryListDto>> GetAuditories()
        {
            var auditories = await _applicationContext.Auditories
                .Select(x => new AuditoryListDto
                {
                    Id = x.Id,
                    Number = x.Number,
                    Capacity = x.Capacity,
                    AdditionalInfo = x.AdditionalInfo
                })
                .ToListAsync();

            return auditories;
        }

        private void ValidateAuditory(AuditoryDto auditoryDto)
        {
            if (_applicationContext.Auditories.Any(x => x.Number == auditoryDto.Number))
            {
                throw new ValidationException("Аудитория с таким номером уже существует");
            }
        }
    }
}