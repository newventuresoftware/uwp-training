using Newtonsoft.Json;
using NWApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NWApp.Services
{
    public interface INorthwindService
    {
        Task<IList<Customer>> GetCustomersAsync(int? limit = null);
        Task<Customer> GetCustomerAsync(string id);
        Task<IList<Product>> GetProductsAsync(int? limit = null);
        Task<IList<Order>> GetOrdersAsync(int? limit = null);
        Task<bool> DeleteCustomer(Models.Customer customer);
        Task<bool> UpdateCustomer(Models.Customer customer);
        Task<bool> InsertCustomer(Models.Customer customer);
    }

    public class NorthwindService : INorthwindService
    {
        public NorthwindService()
        {
            client = new HttpClient();
        }

        private HttpClient client;
        private static string baseServiceUrl = "localhost:3000";
        private HashSet<string> registeredCustomerIds;
        private ObservableCollection<Customer> cachedCustomers;

        public async Task<IList<Models.Customer>> GetCustomersAsync(int? limit = null)
        {
            if (this.cachedCustomers != null)
                return cachedCustomers;

            string requestUri = CreateGetRequest("customers", limit);
            var content = await ExecuteGetRequest(requestUri);
            if (content == null)
                return null;

            List<Models.Customer> data = JsonConvert.DeserializeObject<List<Models.Customer>>(content);
            registeredCustomerIds = new HashSet<string>(data.Select(c => c.ID));
            cachedCustomers = new ObservableCollection<Customer>(data);
            return cachedCustomers;
        }

        public async Task<Customer> GetCustomerAsync(string id)
        {
            if (registeredCustomerIds.Contains(id))
                return cachedCustomers.SingleOrDefault(c => c.ID == id);

            string requestUri = $"http://{baseServiceUrl}/customers/{id}";
            var content = await ExecuteGetRequest(requestUri);
            if (content == null)
                return null;

            return JsonConvert.DeserializeObject<Customer>(content);

        }

        public async Task<IList<Models.Product>> GetProductsAsync(int? limit = null)
        {
            string requestUri = CreateGetRequest("products", limit);
            var content = await ExecuteGetRequest(requestUri);
            if (content == null)
                return null;

            List<Models.Product> data = JsonConvert.DeserializeObject<List<Models.Product>>(content);
            return data;
        }

        public async Task<IList<Models.Order>> GetOrdersAsync(int? limit = null)
        {
            string requestUri = CreateGetRequest("orders", limit);
            var content = await ExecuteGetRequest(requestUri);
            if (content == null)
                return null;

            List<Models.Order> data = JsonConvert.DeserializeObject<List<Models.Order>>(content);
            return data;
        }

        public async Task<bool> DeleteCustomer(Customer customer)
        {
            if (!registeredCustomerIds.Contains(customer.ID))
                return false;

            string requestUri = $"http://{baseServiceUrl}/customers/{customer.ID}";
            HttpResponseMessage response = null;
            try
            {
                response = await client.DeleteAsync(requestUri);
            }
            catch
            {
                return false;
            }

            if (response.IsSuccessStatusCode)
                this.cachedCustomers.Remove(customer);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> InsertCustomer(Customer customer)
        {
            if (registeredCustomerIds.Contains(customer.ID))
                return false;

            string requestUri = $"http://{baseServiceUrl}/customers/";

            string customerJson = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await client.PostAsync(requestUri, content);
            }
            catch
            {
                return false;
            }

            if (response.IsSuccessStatusCode)
                this.cachedCustomers.Add(customer);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            if (!registeredCustomerIds.Contains(customer.ID))
                return false;

            string requestUri = $"http://{baseServiceUrl}/customers/{customer.ID}";

            string customerJson = JsonConvert.SerializeObject(customer);
            StringContent content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            try
            {
                response = await client.PutAsync(requestUri, content);
            }
            catch
            {
                return false;
            }

            return response.IsSuccessStatusCode;
        }

        private string CreateGetRequest(string resource, int? limit)
        {
            string filter = string.Empty;
            if (limit != null)
                filter = $"?_limit={limit.Value}";
            string requestUrl = $"http://{baseServiceUrl}/{resource}{filter}";
            return requestUrl;
        }

        private async Task<string> ExecuteGetRequest(string requestUri)
        {
            HttpResponseMessage response = null;
            try
            {
                response = await client.GetAsync(requestUri);
            }
            catch
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStringAsync();
        }
    }
}
