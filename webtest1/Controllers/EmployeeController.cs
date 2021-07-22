using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webtest1.Data;
using webtest1.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace webtest1.Controllers
{
    public class EmployeeController : Controller
    {        
        private readonly ILogger<EmployeeController> _logger;                
        private readonly IDataService<Employee> _dataService;

        public EmployeeController(ILogger<EmployeeController> logger, IDataService<Employee> dataService)
        {
            _logger = logger;
            _dataService = dataService;            
        }

        public async Task<IActionResult> Index()
        {
            return await EmployeeList();
        }
        
        public async Task<IActionResult> EmployeeList()
        {
            var viewModel = NewViewModel();
            viewModel.Data = await _dataService.Get();
            return View("List", viewModel);
        }

        public async Task<IActionResult> GetEmployee(EmployeeViewModel model)
        {
            switch (model.Action)
            {
                case "updatedata": return await UpdateData(model);                                                            
                case "insertdata": return await InsertData(model);                                                                                        
                case "edit": return await Edit(model);                                        
                case "insert": return await Insert();                    
                case "delete": return await Delete(model);                             
                default: return await EmployeeList();
            }
            
        }       
        
        private EmployeeViewModel NewViewModel()
        {        
            return new EmployeeViewModel();            
        }    

        private async Task<IActionResult> UpdateData(EmployeeViewModel model)
        {            
            if (ModelState.IsValid && await _dataService.Put(model.Id, model.Current))
            {                            
                return await EmployeeList();
            }
            else
            {
                return View("Edit", model);                                
            }
        }

        private async Task<IActionResult> InsertData(EmployeeViewModel model)
        {            
            if (ModelState.IsValid && await _dataService.Post(model.Current))
            {                            
                return await EmployeeList();
            }
            else
            {
                return View("Insert", model);                                
            }
        }

        private async Task<IActionResult> Edit(EmployeeViewModel model)
        {  
            var viewModel = NewViewModel();
            viewModel.Current = await _dataService.GetById(model.Id);
            var result = (await _dataService.Get()).Where(a => a.employeeId != model.Current?.employeeId).Select(a => new {employeeId = a.employeeId, fullName = a.lastName + ", " + a.firstName}).OrderBy(a => a.fullName).ToList();                        
            viewModel.EmployeeList = new SelectList(result, "employeeId", "fullName");
            return View("Edit", viewModel);                     
        }

        private async Task<IActionResult> Insert()
        {                        
            var viewModel = NewViewModel();
            var result = (await _dataService.Get()).Select(a => new {employeeId = a.employeeId, fullName = a.lastName + ", " + a.firstName}).OrderBy(a => a.fullName).ToList();                        
            viewModel.EmployeeList = new SelectList(result, "employeeId", "fullName");
            return View("Insert", viewModel);
        }

        private async Task<IActionResult> Delete(EmployeeViewModel model)
        {                        
            await _dataService.Delete(model.Id);
            return await EmployeeList();
        }

    }
}
