namespace Auditorium.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Auditorium.Models;
    using Auditorium.ViewModels;

    public class UserService
    {
        private const string _baseUrl = Helper.BaseUrl + "/User/";
        
        public async Task<UserInfoModel> GetUserInfo()
        {
            var httpClient = new HttpClient();
            var response = await Helper.GetResponse(typeof(UserInfoModel), await httpClient.PostAsync(
                _baseUrl + "GetUserInfo", Helper.GetHttpContent(Helper.CurrentUser.Id)));

            return (UserInfoModel)response;
        }
        
        public async Task<OperationResult> RegisterUser(RegistrationModel registrationModel)
        {
            var httpClient = new HttpClient();
            var response = await Helper.GetResponse(typeof(OperationResult), await httpClient.PostAsync(
                _baseUrl + "RegisterUser", Helper.GetHttpContent(registrationModel)));

            return (OperationResult)response;
        }
        
        public async Task<OperationResult> LogIn(LogInModel logInModel)
        {
            var httpClient = new HttpClient();
            var response = await Helper.GetResponse(typeof(OperationResult), await httpClient.PostAsync(
                _baseUrl + "LogIn", Helper.GetHttpContent(logInModel)));

            return (OperationResult)response;
        }
    }
}