using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditorium.Pages
{
    using System.Text.Json;
    using Auditorium.Services;
    using Plugin.NFC;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserBookingPage : ContentPage
    {
        private readonly int _bookingId;
        
        public UserBookingPage(int bookingId)
        {
            InitializeComponent();
            _bookingId = bookingId;
        }

        private async void OnOpenAuditoryButtonClicked(object sender, EventArgs e)
        {
            var bookingService = new BookingService();

            var operationResult = await bookingService.GetAuditoryKey(_bookingId);

            if (operationResult.IsSuccess)
            {
                var record = new NFCNdefRecord {
                    TypeFormat = NFCNdefTypeFormat.Mime,
                    MimeType = "application/auditorium",
                    Payload = NFCUtils.EncodeToByteArray(((JsonElement)operationResult.Data).GetGuid().ToString())
                };
            
                CrossNFC.Current.StartPublishing();
                CrossNFC.Current.PublishMessage(new TagInfo
                {
                    Records = new[]{record}
                });
            
                CrossNFC.Current.OnMessagePublished += (info =>
                {
                    CrossNFC.Current.StopPublishing();
                });
            }
            else
            {
                await Navigation.PushAsync(new ErrorPage(operationResult.Message));
            }
        }

        private async void OnDeleteBookingButtonClicked(object sender, EventArgs e)
        {
            var bookingService = new BookingService();
            var operationResult = await bookingService.DeleteBooking(_bookingId);

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