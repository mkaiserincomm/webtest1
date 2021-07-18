using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webtest1.Data;
using webtest1.Models;

namespace webtest1.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _url_get_all;
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public ProductController(ILogger<ProductController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _url_get_all = _configuration.GetValue<string>("DAL:Product");
        }

        public IActionResult Index()
        {            
            return View(new DataViewModel<Product>(_clientFactory, _logger, _url_get_all));
        }

        public IActionResult ProductList()
        {
            return View(new DataViewModel<Product>(_clientFactory, _logger, _url_get_all));
        }
                
    }
}
