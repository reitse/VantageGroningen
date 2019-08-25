using System.Threading.Tasks;

namespace Emando.Vantage.Server.Services.IO
{
    public interface IIOService
    {
        Task SetAsync(int id, object value);
    }
}