namespace AuditoriumWebHosting.Services
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using AuditoriumWebHosting;
    using AuditoriumWebHosting.Dto;
    using AuditoriumWebHosting.Entities;
    using AuditoriumWebHosting.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserService
    {
        private readonly ApplicationContext _applicationContext;

        public UserService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<UserDto> GetUserInfo(int userId)
        {
            return await _applicationContext.Users
                .Where(x => x.Id == userId)
                .Select(x => new UserDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName
                })
                .SingleOrDefaultAsync();
        }

        public async Task<OperationResult> RegisterUser(UserDto userDto)
        {
            try
            {
                await ValidateUserModel(userDto);
            
                var accountData = new AccountData
                {
                    Login = userDto.Login,
                    Password = userDto.Password
                }; 

                await _applicationContext.AccountDatas.AddAsync(accountData);

                var user = new User
                {
                    AccountData = accountData,
                    FirstName = userDto.FirstName,
                    MiddleName = userDto.MiddleName,
                    LastName = userDto.LastName
                };

                await _applicationContext.Users.AddAsync(user);
                await _applicationContext.SaveChangesAsync();

                return new OperationResult(true, user.Id);
            }
            catch (Exception exception)
            {
                return new OperationResult(false, exception.Message);
            }
        }

        public async Task<OperationResult> LogIn(AccountDataDto accountDataDto)
        {
            var userId = await _applicationContext.AccountDatas
                .Where(x => x.Login == accountDataDto.Login)
                .Where(x => x.Password == accountDataDto.Password)
                .Select(x => x.Id)
                .SingleOrDefaultAsync();
            
            if (userId != default)
            {
                return new OperationResult(true, userId);
            }

            return new OperationResult(false, "Не удалось найти пользователя с указанными данными");
        }

        private async Task ValidateUserModel(UserDto userDto)
        {
            if (await _applicationContext.AccountDatas.AnyAsync(x => x.Login == userDto.Login))
            {
                throw new ValidationException("Пользователь с таким логином уже существует");
            }

            if (await _applicationContext.Users
                .Where(x => x.FirstName == userDto.FirstName)
                .Where(x => x.MiddleName == userDto.MiddleName)
                .AnyAsync(x => x.LastName == userDto.LastName))
            {
                throw new ValidationException("Пользователь с таким ФИО уже существует");
            }
        }
    }
}