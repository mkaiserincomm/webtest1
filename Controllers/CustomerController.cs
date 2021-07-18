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
    public class CustomerController : Controller
    {
        private readonly string _url_get_all;
        private readonly ILogger<CustomerController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public CustomerController(ILogger<CustomerController> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
            _url_get_all = _configuration.GetValue<string>("DAL:Customer");
        }

        public IActionResult Index()
        {            
            return View(new DataViewModel<Customer>(_clientFactory,  _logger, _url_get_all));
        }
        
        public IActionResult CustomerList()
        {
            return View(new DataViewModel<Customer>(_clientFactory,  _logger, _url_get_all));
        }

        public IActionResult GetCustomer(DataViewModel<Customer> customer)
        {
            switch (customer.Action)
            {
                case "updatedata":
                    // Save the data
                    return View("CustomerList",new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all));                     

                case "insertdata":
                    // Insert the data
                    return View("CustomerList", new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all));   

                case "deletedata":
                    // Delete the data
                    return View(new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all));                    

                case "edit":
                    return View("CustomerEdit", new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all, customer.Id));
                    
                case "insert":
                    return View("CustomerInsert", new DataViewModel<Customer>(_clientFactory,  _logger));

                case "delete":
                    return View("CustomerDelete", new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all, customer.Id));

                default:
                    return View("CustomerList", new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all));                    
            }
            
        }       
        
    }
}
