using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditorium.Pages
{
    using Auditorium.ViewModels;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorPage : ContentPage
    {
        public ErrorPage()
        {
            InitializeComponent();
        }

        public ErrorPage(string errorMessage)
        {
            InitializeComponent();
            BindingContext = new ErrorModel(errorMessage);
        }
    }
}