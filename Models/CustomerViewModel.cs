using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webtest1.Data;

namespace webtest1.Models
{
    public class CustomerViewModel
    {        
        const string url_get_all = "https://mssqltest1.incomm-poc/api/Customer";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CustomerViewModel(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger logger)
        {     
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;

            IEnumerable<Customer> result = GetCustomers().Result;
            this.Customers = result;            
        }

        public IEnumerable<Customer> Customers {get; set;}
        public Customer SelectedCustomer {get; set;}    

        private async Task<IEnumerable<Customer>> GetCustomers()
        {                    
            var request = new HttpRequestMessage(HttpMethod.Get, url_get_all);                        
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);            

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var customers = await JsonSerializer.DeserializeAsync<IEnumerable<Customer>>(responseStream);
                return customers;
            }  
            else
            {
                return null;
            }          
        }
    }
}
