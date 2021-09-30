using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using webtest1.Options;

namespace webtest1.Services
{
    public class VersionService : IVersionService
    {
        private readonly IHttpClientFactory _clientFactory;        
        private readonly ILogger _logger;
        private readonly IOptions<DALOptions> _options;
        
        private string _url_get_category;
        private string _url_get_customer;
        private string _url_get_employee;
        private string _url_get_product;

        public VersionService (IHttpClientFactory clientFactory, ILogger<VersionService> logger, IOptions<DALOptions> options)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _options = options;
                                
            _url_get_category = _options.Value.CategoryAbout;
            _url_get_customer = _options.Value.CustomerAbout;
            _url_get_employee = _options.Value.EmployeeAbout;
            _url_get_product = _options.Value.ProductAbout;
        }

        public async Task<string> GetVersionCategory()
        {
            return await PrivateGetVersion(_url_get_category);
        }

        public async Task<string> GetVersionCustomer()
        {
            return await PrivateGetVersion(_url_get_customer);
        }

        public async Task<string> GetVersionEmployee()
        {
            return await PrivateGetVersion(_url_get_employee);
        }

        public async Task<string> GetVersionProduct()
        {
            return await PrivateGetVersion(_url_get_product);
        }
        
        private async Task<string> PrivateGetVersion(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);                        
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);            
                
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();      
                    using var sr = new StreamReader(responseStream);
                    var version = await sr.ReadToEndAsync();
                    return version;
                }  
                else
                {
                    return null;
                }  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {method}", "GetVersion");
                return null;
            }
        }
    }
}