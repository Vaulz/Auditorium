using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditorium.Pages
{
    using System.Collections.ObjectModel;
    using Auditorium.Services;
    using Auditorium.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingPage : ContentPage
    {
        private readonly BookingService _bookingService;
        
        public ObservableCollection<BookingModel> BookingModels { get; set; }

        public AuditoryModel AuditoryModel { get; set; }

        public BookingPage()
        {
            InitializeComponent();
            
        }
        
        public BookingPage(AuditoryModel auditoryModel)
        {
            InitializeComponent();
            _bookingService = new BookingService();
            FillModels(auditoryModel);
        }

        public async void FillModels(AuditoryModel auditoryModel)
        {
            AuditoryModel = auditoryModel;
            BookingModels = new ObservableCollection<BookingModel>();
            BindingContext = this;
            var bookings = await _bookingService.GetAuditoryBookings(
                AuditoryModel.Id, DateTime.Now);
            BookingModels.AddRange(bookings);
        }

        private async void OnCreateBookingButtonClicked(object sender, EventArgs e)
        {
            var startTime = StartTime.Time;
            var endTime = EndTime.Time;
            var date = Date.Date;

            var bookingService = new BookingService();
            var operationResult = await bookingService.CreateBooking(
                AuditoryModel.Id, date.Add(startTime), date.Add(endTime));

            if (operationResult.IsSuccess)
            {
                await Shell.Current.GoToAsync("//UserBookingsPage");
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage(operationResult.Message));
            }
        }

        private async void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            var bookings = await _bookingService.GetAuditoryBookings(
                AuditoryModel.Id, e.NewDate);
            BookingModels.Clear();
            BookingModels.AddRange(bookings);
        }
    }
}