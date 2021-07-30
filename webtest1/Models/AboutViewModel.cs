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
using System.Collections.Generic;

namespace webtest1.Models
{
    public class AboutViewModel
    {        
        public string DataVersion {get; set; }
        public List<KeyValuePair<string, string>> Cookies {get; set; }
        public string CurrentEnvironment 
        {
            get {
                return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            }
        }
        public string Version { 
            get {
                return Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            }
        }        
    }
}
