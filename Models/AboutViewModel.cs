using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace webtest1.Models
{
    public class AboutViewModel
    {
        const string url_get = "http://mssqltest1.incomm-poc/api/Version";        
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public string DataVersion;

        public AboutViewModel(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger logger)
        {     
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;

            string result = GetVersion().Result;
            this.DataVersion = result;            
        }

        public string Version { 
            get {
                return Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            }
        }

        private async Task<string> GetVersion()
        {                    
            var request = new HttpRequestMessage(HttpMethod.Get, url_get);                        
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);            

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();                
                var version = responseStream.ToString();
                return version;
            }  
            else
            {
                return null;
            }          
        }
    }
}
