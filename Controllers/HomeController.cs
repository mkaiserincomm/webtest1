using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webtest1.Models;

namespace webtest1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation(new EventId(9999, "Debug"), @"*********************WOOHOO*****************************" );
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://mssqltest1.incomm-poc/health/live"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("{apiResponse}", apiResponse );
                    //reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
