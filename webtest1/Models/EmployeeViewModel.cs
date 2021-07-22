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
    }
}