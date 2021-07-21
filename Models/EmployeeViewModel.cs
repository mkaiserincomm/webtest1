using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using webtest1.Data;
using System.Linq;

namespace webtest1.Models
{
    public class EmployeeViewModel : DataViewModel<Employee>
    {

        public SelectList EmployeeList {get; set;}

        public EmployeeViewModel() : base() { }

        public EmployeeViewModel(IHttpClientFactory clientFactory, ILogger logger) : base (clientFactory, logger) { }        

        public EmployeeViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all) : base (clientFactory, logger, url_get_all) { }
        
        public EmployeeViewModel(IHttpClientFactory clientFactory, ILogger logger, string url_get_all, string id) : base (clientFactory, logger, url_get_all, id) { }        

        public void LoadEmployeeList()
        {
            var result = this.Get().Result.Where(a => a.employeeId != this.Current?.employeeId).Select(a => new {employeeId = a.employeeId, fullName = a.lastName + ", " + a.firstName}).OrderBy(a => a.fullName).ToList();            
            EmployeeList = new SelectList(result, "employeeId", "fullName");
        }
    }
}