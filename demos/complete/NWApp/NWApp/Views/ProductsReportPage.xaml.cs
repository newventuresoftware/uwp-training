using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NWApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductsReportPage : Page
    {
        public ProductsReportPage()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => this.ViewModel = e.NewValue as ViewModels.ProductsReportPageViewModel;
        }

        public ViewModels.ProductsReportPageViewModel ViewModel { get; set; }
    }
}
