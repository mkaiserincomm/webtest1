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
    public class CategoryViewModel
    {        
        const string url_get_all = "http://mssqltest1.incomm-poc/api/Category";
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public CategoryViewModel(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger logger)
        {     
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;

            IEnumerable<Category> result = GetCategories().Result;
            this.Categories = result;            
        }

        public IEnumerable<Category> Categories {get; set;}
        public Category SelectedCategory {get; set;}    

        private async Task<IEnumerable<Category>> GetCategories()
        {                    
            var request = new HttpRequestMessage(HttpMethod.Get, url_get_all);                        
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);            

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var categories = await JsonSerializer.DeserializeAsync<IEnumerable<Category>>(responseStream);
                return categories;
            }  
            else
            {
                return null;
            }          
        }
    }
}
