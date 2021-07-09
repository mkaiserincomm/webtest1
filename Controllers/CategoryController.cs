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
    public class CategoryController : Controller
    {
        const string url_get_all = "http://mssqltest1.incomm-poc/api/Category";
        private readonly ILogger<CategoryController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public CategoryController(ILogger<CategoryController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public IActionResult Index()
        {            
            return View(new DataViewModel<Category>(_clientFactory, _configuration, _logger, url_get_all));
        }
                
        public IActionResult CategoryList()
        {
            return View(new DataViewModel<Category>(_clientFactory, _configuration, _logger, url_get_all));
        }
        
    }
}
