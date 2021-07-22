using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using webtest1.Data;
using webtest1.Models;
using webtest1.Services;

namespace webtest1.Controllers
{
    public class CustomerController : Controller
    {        
        private readonly ILogger<CustomerController> _logger;                
        private readonly IDataService<Customer> _dataService;

        public CustomerController(ILogger<CustomerController> logger, IDataService<Customer> dataService)
        {
            _logger = logger;
            _dataService = dataService;            
        }

        public async Task<IActionResult> Index()
        {
            return await CustomerList();
        }
        
        public async Task<IActionResult> CustomerList()
        {
            var viewModel = NewViewModel();
            viewModel.Data = await _dataService.Get();
            return View("List", viewModel);
        }

        public async Task<IActionResult> GetCustomer(DataViewModel<Customer> model)
        {
            switch (model.Action)
            {
                case "updatedata": return await UpdateData(model);                                                            
                case "insertdata": return await InsertData(model);                                                                                        
                case "edit": return await Edit(model);                                        
                case "insert": return Insert();                    
                case "delete": return await Delete(model);                             
                default: return await CustomerList();
            }
            
        }       
        
        private DataViewModel<Customer> NewViewModel()
        {        
            return new DataViewModel<Customer>();            
        }    

        private async Task<IActionResult> UpdateData(DataViewModel<Customer> model)
        {            
            if (ModelState.IsValid && await _dataService.Put(model.Id, model.Current))
            {                            
                return await CustomerList();
            }
            else
            {
                return View("Edit", model);                                
            }
        }

        private async Task<IActionResult> InsertData(DataViewModel<Customer> model)
        {            
            if (ModelState.IsValid && await _dataService.Post(model.Current))
            {                            
                return await CustomerList();
            }
            else
            {
                return View("Insert", model);                                
            }
        }

        private async Task<IActionResult> Edit(DataViewModel<Customer> model)
        {  
            var viewModel = NewViewModel();
            viewModel.Current = await _dataService.GetById(model.Id);
            return View("Edit", viewModel);                     
        }

        private IActionResult Insert()
        {                        
            return View("Insert", NewViewModel());
        }

        private async Task<IActionResult> Delete(DataViewModel<Customer> model)
        {                        
            await _dataService.Delete(model.Id);
            return await CustomerList();
        }

    }
}
