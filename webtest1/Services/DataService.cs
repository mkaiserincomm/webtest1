using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class DataService<T> : IDataService<T>
{    
    private readonly IHttpClientFactory _clientFactory;        
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    private string _url_get_all;

    public DataService (IHttpClientFactory clientFactory, ILogger<DataService<T>> logger, IConfiguration configuration)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _configuration = configuration;
                
        _url_get_all = _configuration.GetValue<string>("DAL:" + typeof(T).Name);
    }

    public async Task<bool> Delete(string id)
    {
        var client = _clientFactory.CreateClient();                        
        var request = new HttpRequestMessage(HttpMethod.Delete, _url_get_all + "/" + id);                                                
        var response = await client.SendAsync(request);            

        return response.IsSuccessStatusCode;  
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

    public async Task<T> GetById(string id)
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

    public async Task<bool> Post(T model)
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

    public async Task<bool> Put(string id, T model)
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
}