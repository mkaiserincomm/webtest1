using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace webtest1.Models
{
    public class AboutViewModel
    {
        private readonly string _url_get;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public string DataVersion;
        public string CurrentEnvironment;

        public AboutViewModel(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger logger, string url_get)
        {     
            _clientFactory = clientFactory;
            _configuration = configuration;
            _logger = logger;
            _url_get = url_get;

            string result = GetVersion().Result;
            this.DataVersion = result;            

            CurrentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public string Version { 
            get {
                return Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            }
        }

        private async Task<string> GetVersion()
        {                    
            var request = new HttpRequestMessage(HttpMethod.Get, _url_get);                        
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
    }
}
