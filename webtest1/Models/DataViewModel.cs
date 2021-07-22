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
        public IEnumerable<T> Data {get; set;}
        public T Current {get; set;}    

        public string Action {get; set;}
        public string Id {get; set;}
    }
}
