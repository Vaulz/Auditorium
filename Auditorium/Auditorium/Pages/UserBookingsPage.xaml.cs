using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditorium.Pages
{
    using System.Collections.ObjectModel;
    using Auditorium.Services;
    using Auditorium.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserBookingsPage : ContentPage
    {
        public ObservableCollection<UserBookingModel> UserBookingModels { get; set; }
        
        public UserBookingsPage()
        {
            InitializeComponent();
            UserBookingModels = new ObservableCollection<UserBookingModel>();
            BindingContext = this;
        }
        
        protected override void OnAppearing()
        {
            FillModels();
            base.OnAppearing();
        }

        private async void FillModels()
        {
            var bookingService = new BookingService();
            var userBookings = await bookingService.GetUserBookings();
            UserBookingModels.Clear();
            UserBookingModels.AddRange(userBookings);
        }

        private void OnBookingClick(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new UserBookingPage(((UserBookingModel)e.Item).Id));
        }
    }
}