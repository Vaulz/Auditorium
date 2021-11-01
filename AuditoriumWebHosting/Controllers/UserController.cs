namespace AuditoriumWebHosting.Controllers
{
    using System.Threading.Tasks;
    using AuditoriumWebHosting.Dto;
    using AuditoriumWebHosting.Services;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> GetUserInfo([FromBody] int userId)
        {
            return Ok(await _userService.GetUserInfo(userId));
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            return Ok(await _userService.RegisterUser(userDto));
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] AccountDataDto accountDataDto)
        {
            return Ok(await _userService.LogIn(accountDataDto));
        }
    }
}