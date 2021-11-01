namespace Auditorium.Pages
{
    using System;
    using Auditorium.Services;
    using Auditorium.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new RegistrationModel();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var userService = new UserService();
            var operationResult = await userService.RegisterUser((RegistrationModel)BindingContext);
            if (operationResult.IsSuccess)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage(operationResult.Message));
            }
        }
    }
}