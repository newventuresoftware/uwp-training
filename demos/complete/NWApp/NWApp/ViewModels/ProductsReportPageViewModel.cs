using NWApp.Models;
using Prism.Windows.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace NWApp.ViewModels
{
    public class ProductsReportPageViewModel : ViewModelBase
    {
        public ProductsReportPageViewModel(Services.INorthwindService northwindService)
        {
            this.northwindService = northwindService;

            northwindService.GetProductsAsync(10)
                .ContinueWith(antc => this.Products = antc.Result.OrderBy(c => c.UnitPrice).ToArray(),
                System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());
        }

        private Services.INorthwindService northwindService;
        private IList<Product> products;

        public IList<Product> Products { get => products; private set => SetProperty(ref products, value); }
    }
}
