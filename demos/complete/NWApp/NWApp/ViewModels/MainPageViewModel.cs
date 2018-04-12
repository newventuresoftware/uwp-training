using NWApp.Models;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Input;

namespace NWApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(Services.INorthwindService northwindService, INavigationService navigationService)
        {
            this.northwindService = northwindService;
            this.navigationService = navigationService;

            northwindService.GetCustomersAsync()
                .ContinueWith(antc => this.Customers = antc.Result,
                System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());

            addNewCommand = new DelegateCommand(AddNewCustomer);
            editCustomerCommand = new DelegateCommand(EditCustomer, IsCustomerAvailable);
            editCustomerCommand.ObservesProperty(() => this.SelectedCustomer);
            deleteCustomerCommand = new DelegateCommand(DeleteCustomer, IsCustomerAvailable);
            deleteCustomerCommand.ObservesProperty(() => this.SelectedCustomer);
            detailsCommand = new DelegateCommand(ShowDetails);
        }

        private IList<Customer> customers;
        private Customer selectedCustomer;
        private Services.INorthwindService northwindService;
        private INavigationService navigationService;
        private DelegateCommand addNewCommand, editCustomerCommand, deleteCustomerCommand, detailsCommand;

        public ICommand AddNewCustomerCommand => this.addNewCommand;
        public ICommand EditCustomerCommand => this.editCustomerCommand;
        public ICommand DeleteCustomerCommand => this.deleteCustomerCommand;
        public ICommand CustomerDetailsCommand => this.detailsCommand;

        public Customer SelectedCustomer
        {
            get => this.selectedCustomer;
            set => SetProperty(ref this.selectedCustomer, value);
        }

        public IList<Customer> Customers { get => customers; set => SetProperty(ref customers, value); }

        private bool IsCustomerAvailable()
        {
            return selectedCustomer != null;
        }

        private void AddNewCustomer()
        {
            this.navigationService.Navigate("CustomerDetails", null);
        }

        private void EditCustomer()
        {
            this.navigationService.Navigate("CustomerDetails", this.selectedCustomer.ID);
        }

        private void DeleteCustomer()
        {
            this.northwindService.DeleteCustomer(this.selectedCustomer);
        }

        private void ShowDetails()
        {
            this.navigationService.Navigate("ProductsReport", null);
        }
    }
}
