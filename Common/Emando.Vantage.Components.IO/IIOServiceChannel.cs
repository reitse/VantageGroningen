using System.Threading.Tasks;

namespace Emando.Vantage.Components.IO
{
    public interface IIOServiceChannel
    {
        Task SetAsync(int id, object value);
    }
}