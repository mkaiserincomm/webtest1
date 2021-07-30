using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webtest1.Options;

namespace webtest1.Services
{
    public class DataService<T> : IDataService<T>
    {    
        private readonly IHttpClientFactory _clientFactory;        
        private readonly ILogger _logger;
        private readonly IOptions<DALOptions> _options;

        private string _url_get_all;

        public DataService (IHttpClientFactory clientFactory, ILogger<DataService<T>> logger, IOptions<DALOptions> options)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _options = options;
                            
            _url_get_all = _options.Value.GetValue(typeof(T).Name);        
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var client = _clientFactory.CreateClient();                        
                var request = new HttpRequestMessage(HttpMethod.Delete, _url_get_all + "/" + id);                                                
                var response = await client.SendAsync(request);            

                return response.IsSuccessStatusCode;  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {method} for {Type}", "Delete", typeof(T).Name);
                return false;
            }
        }

        public async Task<IEnumerable<T>> Get()
        {
            try
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
                    return new List<T>();
                }   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {method} for {Type}", "Get", typeof(T).Name);
                return new List<T>();
            }
        }

        public async Task<T> GetById(string id)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Get, _url_get_all + "/" + id);                                    
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {method} for {Type}", "GetById", typeof(T).Name);
                return default;
            }
        }

        public async Task<bool> Post(T model)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var request = new HttpRequestMessage(HttpMethod.Post, _url_get_all);                      
                request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                
                var response = await client.SendAsync(request);            

                return response.IsSuccessStatusCode;   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {method} for {Type}", "Post", typeof(T).Name);
                return false;
            }
        }

        public async Task<bool> Put(string id, T model)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var request = new HttpRequestMessage(HttpMethod.Put, _url_get_all + "/" + id);                        
                request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                
                var response = await client.SendAsync(request);            

                return response.IsSuccessStatusCode;  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {method} for {Type}", "Put", typeof(T).Name);
                return false;
            }
        }
    }
}