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
        private readonly IConfiguration _configuration;

        public AboutController(ILogger<AboutController> logger, IVersionService versionService, IConfiguration configuration)
        {
            _logger = logger;            
            _versionService = versionService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {            
            var model = new AboutViewModel();            
            try { model.CategoryVersion = await _versionService.GetVersionCategory(); } catch(Exception) {}
            try { model.CustomerVersion = await _versionService.GetVersionCustomer(); } catch(Exception) {}
            try { model.EmployeeVersion = await _versionService.GetVersionEmployee(); } catch(Exception) {}
            try { model.ProductVersion = await _versionService.GetVersionProduct(); } catch(Exception) {}
            model.Message = _configuration["Message"];

            if (Request.Cookies != null) model.Cookies = Request.Cookies.ToList();            
            return View(model);
        }
                        
        public async Task<IActionResult> About()
        {            
            return await Index();
        }        
        
    }
}
