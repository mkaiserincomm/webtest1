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
    public class CategoryController : Controller
    {        
        private readonly ILogger<CategoryController> _logger;                
        private readonly IDataService<Category> _dataService;

        public CategoryController(ILogger<CategoryController> logger, IDataService<Category> dataService)
        {
            _logger = logger;
            _dataService = dataService;            
        }

        public async Task<IActionResult> Index()
        {
            return await CategoryList();
        }
        
        public async Task<IActionResult> CategoryList()
        {
            var viewModel = NewViewModel();
            viewModel.Data = await _dataService.Get();
            return View("List", viewModel);
        }

        public async Task<IActionResult> GetCategory(DataViewModel<Category> model)
        {
            switch (model.Action)
            {
                case "updatedata": return await UpdateData(model);                                                            
                case "insertdata": return await InsertData(model);                                                                                        
                case "edit": return await Edit(model);                                        
                case "insert": return Insert();                    
                case "delete": return await Delete(model);                             
                default: return await CategoryList();
            }
            
        }       
        
        private DataViewModel<Category> NewViewModel()
        {        
            return new DataViewModel<Category>();            
        }    

        private async Task<IActionResult> UpdateData(DataViewModel<Category> model)
        {            
            if (ModelState.IsValid && await _dataService.Put(model.Id, model.Current))
            {                            
                return await CategoryList();
            }
            else
            {
                return View("Edit", model);                                
            }
        }

        private async Task<IActionResult> InsertData(DataViewModel<Category> model)
        {            
            if (ModelState.IsValid && await _dataService.Post(model.Current))
            {                            
                return await CategoryList();
            }
            else
            {
                return View("Insert", model);                                
            }
        }

        private async Task<IActionResult> Edit(DataViewModel<Category> model)
        {  
            var viewModel = NewViewModel();
            viewModel.Current = await _dataService.GetById(model.Id);
            return View("Edit", viewModel);                     
        }

        private IActionResult Insert()
        {                        
            return View("Insert", NewViewModel());
        }

        private async Task<IActionResult> Delete(DataViewModel<Category> model)
        {                        
            await _dataService.Delete(model.Id);
            return await CategoryList();
        }

    }
}
