using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webtest1.Data;

namespace webtest1.Models
{
    public class DataViewModel<T>
    {        
        private readonly string _url_get_all;
        private readonly IHttpClientFactory _clientFactory;        
        private readonly ILogger _logger;

        public DataViewModel()
        {
            
        }

        public DataViewModel(IHttpClientFactory clientFactory, ILogger logger)
        {     
            _clientFactory = clientFactory;
            _logger = logger;
            _url_get_all = "";
        }

        public DataViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all)
        {     
            _clientFactory = clientFactory;            
            _logger = logger;
            _url_get_all = url_get_all;

            IEnumerable<T> result = Get().Result;
            this.Data = result;            
        }

        public DataViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all, string id)
        {     
            _clientFactory = clientFactory;    
            _logger = logger;
            _url_get_all = url_get_all;

            T result = Get(id).Result;
            this.Current= result;            
        }

        public IEnumerable<T> Data {get; set;}
        public T Current {get; set;}    

        public string Action {get; set;}
        public string Id {get; set;}

        private async Task<IEnumerable<T>> Get()
        {                    
            var request = new HttpRequestMessage(HttpMethod.Get, _url_get_all);                        
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);            

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<IEnumerable<T>>(responseStream);
                return result;
            }  
            else
            {
                return null;
            }          
        }

        private async Task<T> Get(string id)
        {                    
            var request = new HttpRequestMessage(HttpMethod.Get, _url_get_all + "/" + id);                        
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);            

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<T>(responseStream);
                return result;
            }  
            else
            {
                return default;
            }          
        }
    }

    
}
