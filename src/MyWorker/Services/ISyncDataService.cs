using System.Threading.Tasks;

namespace MyWorker.Services
{
    public interface ISyncDataService
    {
        Task StartAsync();
    }
}
