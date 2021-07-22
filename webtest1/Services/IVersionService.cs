using System.Threading.Tasks;

namespace webtest1.Services
{
    public interface IVersionService
    {
        Task<string> GetVersion();
    }
}