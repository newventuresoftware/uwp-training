using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace XamlPerf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            GetOrders().ContinueWith(antc => this.Orders = antc.Result, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private List<Models.Order> orders;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Models.Order> Orders
        {
            get => orders;
            private set
            {
                this.orders = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Orders)));
            }
        }

        public async Task<List<Models.Order>> GetOrders()
        {
            List<Models.Order> orders = new List<Models.Order>();
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/SampleData.csv"));
            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    string[] tokens = line.Split(',');
                    Models.Order order = new Models.Order()
                    {
                        OrderDate = DateTime.Parse(tokens[0], CultureInfo.InvariantCulture),
                        Region = tokens[1],
                        Rep = tokens[2],
                        Item = tokens[3],
                        Units = int.Parse(tokens[4], CultureInfo.InvariantCulture),
                        UnitCost = decimal.Parse(tokens[5], CultureInfo.InvariantCulture),
                        Total = decimal.Parse(tokens[6], CultureInfo.InvariantCulture)
                    };
                    orders.Add(order);
                }
            }
            return orders;
        }
    }
}
