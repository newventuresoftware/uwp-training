using System;
using System.Collections.Generic;
using System.Windows.Input;
using NWApp.Models;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace NWApp.ViewModels
{
    public class CustomerDetailsPageViewModel : ViewModelBase
    {
        public CustomerDetailsPageViewModel(Services.INorthwindService northwindService, INavigationService navigationService)
        {
            this.northwindService = northwindService;
            this.navigationService = navigationService;

            acceptCommand = new DelegateCommand(OnAcceptEdit);
            cancelCommand = new DelegateCommand(OnCancelEdit);
        }

        private Customer _originalCustomer, _customer;
        private Services.INorthwindService northwindService;
        private INavigationService navigationService;
        private DelegateCommand acceptCommand, cancelCommand;

        public ICommand AcceptEditComand => acceptCommand;
        public ICommand CancelEditCommand => cancelCommand;

        public Customer Customer
        {
            get => _customer;
            private set => SetProperty(ref _customer, value);
        }

        public async override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            string customerId = e.Parameter as string;
            if (string.IsNullOrEmpty(customerId))
            {
                Customer = new Customer();
            }
            else
            {
                _originalCustomer = await northwindService.GetCustomerAsync(customerId);
                Customer = _originalCustomer.Clone();
            }
        }

        private async void OnAcceptEdit()
        {
            if (_customer.HasErrors)
                return;

            bool result;
            if (_originalCustomer == null)
                result = await northwindService.InsertCustomer(_customer);
            else
                result = await northwindService.UpdateCustomer(_customer);

            if (!result)
                return;

            _originalCustomer?.CopyFrom(_customer);

            if (navigationService.CanGoBack())
                navigationService.GoBack();
        }

        private void OnCancelEdit()
        {
            Customer = _originalCustomer?.Clone();
            if (navigationService.CanGoBack())
                navigationService.GoBack();
        }
    }
}
