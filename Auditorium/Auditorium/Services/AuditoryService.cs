namespace Auditorium.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Auditorium.ViewModels;

    public class AuditoryService
    {
        private const string _baseUrl = Helper.BaseUrl + "/Auditory/";
        
        public async Task<List<AuditoryModel>> GetAuditories()
        {
            var httpClient = new HttpClient();
            var auditories = await Helper.GetResponse(
                typeof(List<AuditoryModel>), await httpClient.GetAsync(_baseUrl + "GetAuditories"));

            return (List<AuditoryModel>)auditories;
        }
    }
}