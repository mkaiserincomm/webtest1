using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webtest1.Data;

namespace webtest1.Models
{
    public class DataViewModel<T>
    {        
        private string _url_get_all;
        private IHttpClientFactory _clientFactory;        
        private ILogger _logger;

        public DataViewModel()
        {
            
        }

        public DataViewModel(IHttpClientFactory clientFactory, ILogger logger)
        {     
            Attach(clientFactory, logger);
        }

        public DataViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all)
        {     
            Attach(clientFactory, logger, url_get_all);

            IEnumerable<T> result = Get().Result;
            this.Data = result;            
        }

        public DataViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all, string id)
        {                 
            Attach(clientFactory, logger, url_get_all, id);

            T result = GetById().Result;
            this.Current= result;            
        }
        
        public IEnumerable<T> Data {get; set;}
        public T Current {get; set;}    

        public string Action {get; set;}
        public string Id {get; set;}

        public void Attach(IHttpClientFactory clientFactory, ILogger logger, string url_get_all = "", string id = null) 
        {
            _clientFactory = clientFactory;    
            _logger = logger;
            _url_get_all = url_get_all;
            if(!string.IsNullOrWhiteSpace(id)) Id = id;
        }

        public async Task<IEnumerable<T>> Get()
        {                    
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _url_get_all);                        

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

        public async Task<T> GetById()
        {                    
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _url_get_all + "/" + this.Id);                                    
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
        public async Task<bool> Put()
        {                                
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var request = new HttpRequestMessage(HttpMethod.Put, _url_get_all + "/" + this.Id);                        
            request.Content = new StringContent(JsonSerializer.Serialize(this.Current), Encoding.UTF8, "application/json");
            
            var response = await client.SendAsync(request);            

            return response.IsSuccessStatusCode;            
        }

        public async Task<bool> Post()
        {                                
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var request = new HttpRequestMessage(HttpMethod.Post, _url_get_all);                      
            request.Content = new StringContent(JsonSerializer.Serialize(this.Current), Encoding.UTF8, "application/json");
            
            var response = await client.SendAsync(request);            

            return response.IsSuccessStatusCode;            
        }

        public async Task<bool> Delete()
        {                                
            var client = _clientFactory.CreateClient();                        
            var request = new HttpRequestMessage(HttpMethod.Delete, _url_get_all + "/" + this.Id);                                                
            var response = await client.SendAsync(request);            

            return response.IsSuccessStatusCode;            
        }
    }

    
}
