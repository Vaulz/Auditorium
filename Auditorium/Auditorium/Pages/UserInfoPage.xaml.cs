using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditorium.Pages
{
    using Auditorium.Entities;
    using Auditorium.Services;
    using Microsoft.EntityFrameworkCore;
    using Plugin.Fingerprint;
    using Plugin.Fingerprint.Abstractions;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserInfoPage : ContentPage
    {
        public UserInfoPage()
        {
            InitializeComponent();
            FillModel();
        }

        private async void FillModel()
        {
            var userService = new UserService();
            BindingContext = await userService.GetUserInfo();
        }

        private async void OnSaveFingerPrintButtonClicked(object sender, EventArgs e)
        {
            var isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync();

            if (isFingerprintAvailable)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync(
                    new AuthenticationRequestConfiguration("Привязать отпечаток пальца", string.Empty));

                if (auth.Authenticated)
                {
                    await using var db = new AppContext();
                    if (await db.Users.AnyAsync())
                    {
                        await Navigation.PushAsync(new ErrorPage("Отпечаток пальца уже привязан"));
                    }

                    await db.Users.AddAsync(new User
                    {
                        Login = Helper.CurrentUser.Login,
                        Password = Helper.CurrentUser.Password
                    });

                    await db.SaveChangesAsync();
                }
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage("Вход по отпечатку пальца не доступен"));
            }
        }
    }
}