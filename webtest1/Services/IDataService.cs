using System.Collections.Generic;
using System.Threading.Tasks;

namespace webtest1.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> Get();        
        Task<T> GetById(string id);
        Task<bool> Put(string id, T model);    
        Task<bool> Post(T model);
        Task<bool> Delete(string id);
    }
}