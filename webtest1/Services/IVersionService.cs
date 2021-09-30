using System.Threading.Tasks;

namespace webtest1.Services
{
    public interface IVersionService
    {
        Task<string> GetVersionCategory();
        Task<string> GetVersionCustomer();
        Task<string> GetVersionEmployee();
        Task<string> GetVersionProduct();
    }
}