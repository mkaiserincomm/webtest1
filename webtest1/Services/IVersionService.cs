using System.Threading.Tasks;

public interface IVersionService
{
    Task<string> GetVersion();
}