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
    public class ProductViewModel
    {        
        const string url_get_all = "http://mssqltest1.incomm-poc/api/Product";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ProductViewModel(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger logger)
        {     
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;

            IEnumerable<Product> result = GetProducts().Result;
            this.Products = result;            
        }

        public IEnumerable<Product> Products {get; set;}
        public Product SelectedProduct {get; set;}    

        private async Task<IEnumerable<Product>> GetProducts()
        {                    
            var request = new HttpRequestMessage(HttpMethod.Get, url_get_all);                        
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);            

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(responseStream);
                return products;
            }  
            else
            {
                return null;
            }          
        }
    }
}
