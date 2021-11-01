using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditorium.Pages
{
    using System.Collections.ObjectModel;
    using Auditorium.Services;
    using Auditorium.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuditoriesPage : ContentPage
    {
        public ObservableCollection<AuditoryModel> AuditoryModels { get; set; }

        public AuditoriesPage()
        {
            InitializeComponent();
            AuditoryModels = new ObservableCollection<AuditoryModel>();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            FillModels();
            base.OnAppearing();
        }

        private async void FillModels()
        {
            var auditoryService = new AuditoryService();
            var auditories = await auditoryService.GetAuditories();
            AuditoryModels.Clear();
            AuditoryModels.AddRange(auditories);
        }

        private async void OnAuditorySelect(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new BookingPage((AuditoryModel)e.Item));
        }
    }
}