namespace Auditorium.Pages
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using Auditorium.Entities;
    using Auditorium.Models;
    using Auditorium.Services;
    using Auditorium.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Plugin.Fingerprint;
    using Plugin.Fingerprint.Abstractions;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using AppContext = Auditorium.AppContext;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
            BindingContext = new LogInModel();
        }

        private async void OnLogInButtonClicked(object sender, EventArgs e)
        {
            var logInModel = (LogInModel)BindingContext;

            var userService = new UserService();
            var operationResult = await userService.LogIn(logInModel);

            if (operationResult.IsSuccess)
            {
                Helper.CurrentUser = new CurrentUserModel
                {
                    Login = logInModel.Login,
                    Password = logInModel.Password,
                    Id = ((JsonElement)operationResult.Data).GetInt32()
                };
                
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage(operationResult.Message));
            }
        }

        private async void OnRegistrationButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void OnFingerPrintButtonClick(object sender, EventArgs e)
        {
            var isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync();

            if (isFingerprintAvailable)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync(
                    new AuthenticationRequestConfiguration("Войти по отпечатку пальца", string.Empty));

                if (auth.Authenticated)
                {
                    if (auth.Authenticated)
                    {
                        await using var db = new AppContext();
                        
                        var logInModel = await db.Users
                            .Select(x => new LogInModel
                            {
                                Login = x.Login,
                                Password = x.Password
                            })
                            .SingleOrDefaultAsync();

                        if (logInModel == null)
                        {
                            await Navigation.PushAsync(new ErrorPage("Отпечаток пальца не привязан к аккаунту"));
                        }
                        else
                        {
                            var userService = new UserService();
                            var operationResult = await userService.LogIn(logInModel);

                            if (operationResult.IsSuccess)
                            {
                                Helper.CurrentUser = new CurrentUserModel
                                {
                                    Login = logInModel.Login,
                                    Password = logInModel.Password,
                                    Id = ((JsonElement)operationResult.Data).GetInt32()
                                };
                
                                Application.Current.MainPage = new MainPage();
                            }
                        }
                    }
                }
            }
        }
    }
}