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
    public class EmployeeController : Controller
    {
        private readonly string _url_get_all;

        private readonly ILogger<EmployeeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public EmployeeController(ILogger<EmployeeController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _url_get_all = _configuration.GetValue<string>("DAL:Employee");
        }

        public IActionResult Index()
        {            
            return View(new DataViewModel<Employee>(_clientFactory, _logger, _url_get_all));
        }
                
        public IActionResult EmployeeList()
        {
            return View(new DataViewModel<Employee>(_clientFactory, _logger, _url_get_all));
        }

        public IActionResult GetEmployee(DataViewModel<Employee> employee)
        {
            return View(employee);
        }        
        
    }
}
