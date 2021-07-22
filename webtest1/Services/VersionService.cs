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

        private string _url_get;

        public VersionService (IHttpClientFactory clientFactory, ILogger<VersionService> logger, IOptions<DALOptions> options)
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _options = options;
                    
            _url_get = _options.Value.About;
        }
        
        public async Task<string> GetVersion()
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