using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telerik.Data.Core;

namespace NWApp.Models
{
    public class Customer : BindableBase, ISupportEntityValidation
    {
        public Customer()
        {
            this.errorsContainer = new ErrorsContainer<string>(RaiseErrorsChanged);
        }

        private const string Group_Address = "Address";
        private const string Group_ContactInfo = "Contact Information";
        private ErrorsContainer<string> errorsContainer;

        private string _address, _city, _companyName, _contactName, _contactTitle,
            _country, _id, _fax, _phone, _postalCode, _region;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        [Display("ID")]
        public string ID { get => _id; set => SetProperty(ref _id, value); }

        [Display("Contact Name")]
        public string ContactName { get => _contactName; set => SetProperty(ref _contactName, value); }

        [Display("Contact Title")]
        public string ContactTitle { get => _contactTitle; set => SetProperty(ref _contactTitle, value); }

        [Display("Company Name")]
        public string CompanyName { get => _companyName; set => SetProperty(ref _companyName, value); }

        [Display("Phone", Group = Group_ContactInfo)]
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }

        [Display("Fax", Group = Group_ContactInfo)]
        public string Fax { get => _fax; set => SetProperty(ref _fax, value); }

        [Display("Address", Group = Group_Address)]
        public string Address { get => _address; set => SetProperty(ref _address, value); }

        [Display("PO Code", Group = Group_Address)]
        public string PostalCode { get => _postalCode; set => SetProperty(ref _postalCode, value); }

        [Display("City", Group = Group_Address)]
        public string City { get => _city; set => SetProperty(ref _city, value); }

        [Display("Country", Group = Group_Address)]
        public string Country { get => _country; set => SetProperty(ref _country, value); }

        [Display("Region", Group = Group_Address)]
        public string Region { get => _region; set => SetProperty(ref _region, value); }

        public bool HasErrors => this.errorsContainer.HasErrors;

        public Customer Clone()
        {
            return new Customer()
            {
                Address = this.Address,
                City = this.City,
                CompanyName = this.CompanyName,
                ContactName = this.ContactName,
                ContactTitle = this.ContactTitle,
                Country = this.Country,
                ID = this.ID,
                Fax = this.Fax,
                Phone = this.Phone,
                PostalCode = this.PostalCode,
                Region = this.Region,
            };
        }

        public void CopyFrom(Customer other)
        {
            Address = other.Address;
            City = other.City;
            CompanyName = other.CompanyName;
            ContactName = other.ContactName;
            ContactTitle = other.ContactTitle;
            Country = other.Country;
            ID = other.ID;
            Fax = other.Fax;
            Phone = other.Phone;
            PostalCode = other.PostalCode;
            Region = other.Region;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   Address == customer.Address &&
                   City == customer.City &&
                   CompanyName == customer.CompanyName &&
                   ContactName == customer.ContactName &&
                   ContactTitle == customer.ContactTitle &&
                   Country == customer.Country &&
                   ID == customer.ID &&
                   Fax == customer.Fax &&
                   Phone == customer.Phone &&
                   PostalCode == customer.PostalCode &&
                   Region == customer.Region;
        }

        public override int GetHashCode()
        {
            var hashCode = -526004057;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(City);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CompanyName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ContactName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ContactTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Fax);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Phone);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PostalCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Region);
            return hashCode;
        }

        public bool Validate()
        {
            ValidateId(ID);
            ValidateContactName(ContactName);
            ValidateCompanyName(CompanyName);

            return !HasErrors;
        }

        public Task ValidatePropertyAsync(Entity entity, string propertyName)
        {
            EntityProperty property = entity.GetEntityProperty(propertyName);
            switch (propertyName)
            {
                case (nameof(ID)):
                    property.IsValid = ValidateId(property.PropertyValue as string);
                    break;
                case (nameof(ContactName)):
                    property.IsValid = ValidateContactName(property.PropertyValue as string);
                    break;
                case (nameof(CompanyName)):
                    property.IsValid = ValidateCompanyName(property.PropertyValue as string);
                    break;
            }

            return Task.CompletedTask;
        }

        public IEnumerable GetErrors(string propertyName) => this.errorsContainer.GetErrors(propertyName);

        private void RaiseErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private bool ValidateId(string value)
        {
            if (value == null || !Regex.IsMatch(value, "^[A-Z]{5}$"))
            {
                errorsContainer.SetErrors(() => ID, new string[]
                {
                    "ID must be exactly 5 uppercase symbols long."
                });
                return false;
            }
            else
            {
                errorsContainer.ClearErrors(() => ID);
                return true;
            }
        }

        public bool ValidateCompanyName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                errorsContainer.SetErrors(() => CompanyName, new string[]
                {
                    "Company Name should not be empty."
                });
                return true;
            }
            else
            {
                errorsContainer.ClearErrors(() => CompanyName);
                return false;
            }
        }

        public bool ValidateContactName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                errorsContainer.SetErrors(() => ContactName, new string[]
                {
                    "Contact Name should not be empty."
                });
                return true;
            }
            else
            {
                errorsContainer.ClearErrors(() => ContactName);
                return false;
            }
        }
    }
}
