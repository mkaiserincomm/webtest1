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
using webtest1.Services;

namespace webtest1.Controllers
{
    public class AboutController : Controller
    {        
        private readonly ILogger<AboutController> _logger;     
        private readonly IVersionService _versionService;

        public AboutController(ILogger<AboutController> logger, IVersionService versionService)
        {
            _logger = logger;            
            _versionService = versionService;
        }

        public async Task<IActionResult> Index()
        {            
            var model = new AboutViewModel();
            model.CategoryVersion = await _versionService.GetVersionCategory();
            model.CustomerVersion = await _versionService.GetVersionCustomer();
            model.EmployeeVersion = await _versionService.GetVersionEmployee();
            model.ProductVersion = await _versionService.GetVersionProduct();

            if (Request.Cookies != null) model.Cookies = Request.Cookies.ToList();            
            return View(model);
        }
                        
        public async Task<IActionResult> About()
        {            
            return await Index();
        }        
        
    }
}
