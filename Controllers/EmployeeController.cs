using System.Net.Http;
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
            return View("List", NewViewModel(_clientFactory,  _logger, _url_get_all));
        }
        
        public IActionResult EmployeeList()
        {
            return View("List", NewViewModel(_clientFactory,  _logger, _url_get_all));
        }

        public IActionResult GetEmployee(EmployeeViewModel model)
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
                        model.LoadEmployeeList();
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
                        model.LoadEmployeeList();
                        return View("Insert", model);                                
                    }                    
                
                case "edit":
                {
                    var newModel = NewViewModel(_clientFactory,  _logger, _url_get_all, model.Id);
                    newModel.LoadEmployeeList();
                    return View("Edit", newModel);
                }                    
                    
                case "insert":
                {
                    var newModel = NewViewModel(_clientFactory,  _logger);
                    newModel.Attach(_clientFactory, _logger, _url_get_all);
                    newModel.LoadEmployeeList();
                    return View("Insert", newModel);
                }

                case "delete":                    
                    model.Attach(_clientFactory, _logger, _url_get_all);
                    var dummy = model.Delete().Result;
                    return View("List", NewViewModel(_clientFactory, _logger, _url_get_all));                    

                default:
                    return View("List", NewViewModel(_clientFactory, _logger, _url_get_all));                    
            }
            
        }       
        
        private EmployeeViewModel NewViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all = "", string id = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new EmployeeViewModel(_clientFactory, _logger, _url_get_all);
            }
            else
            {
                return new EmployeeViewModel(_clientFactory, _logger, _url_get_all, id);
            }
            
        }

    }
}
