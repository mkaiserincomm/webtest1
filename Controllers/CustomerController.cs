using System.Net.Http;
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
            return View("List", NewViewModel(_clientFactory,  _logger, _url_get_all));
        }
        
        public IActionResult CustomerList()
        {
            return View("List", NewViewModel(_clientFactory,  _logger, _url_get_all));
        }

        public IActionResult GetCustomer(DataViewModel<Customer> model)
        {
            switch (model.Action)
            {
                case "updatedata":                                        
                    model.Attach(_clientFactory, _logger, _url_get_all);
                    if (ModelState.IsValid && model.Put().Result)
                    {                                            
                        return View("List", NewViewModel(_clientFactory, _logger, _url_get_all));                     
                    }
                    else
                    {
                        return View("Edit", model);                                
                    }
                    
                case "insertdata":                    
                    model.Attach(_clientFactory, _logger, _url_get_all);
                    if (ModelState.IsValid && model.Post().Result)                                                
                    {
                        return View("List", NewViewModel(_clientFactory, _logger, _url_get_all));                     
                    }
                    else
                    {
                        return View("Insert", model);                                
                    }                    
                
                case "edit":
                    return View("Edit", NewViewModel(_clientFactory, _logger, _url_get_all, model.Id));
                    
                case "insert":
                    return View("Insert", NewViewModel(_clientFactory,  _logger));

                case "delete":                    
                    model.Attach(_clientFactory, _logger, _url_get_all);
                    var dummy = model.Delete().Result;
                    return View("List", NewViewModel(_clientFactory, _logger, _url_get_all));                    

                default:
                    return View("List", NewViewModel(_clientFactory, _logger, _url_get_all));                    
            }
            
        }       
        
        private DataViewModel<Customer> NewViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all = "", string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all);
            }
            else
            {
                return new DataViewModel<Customer>(_clientFactory, _logger, _url_get_all, id);
            }
            
        }

    }
}
