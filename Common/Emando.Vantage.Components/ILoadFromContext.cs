using System.Threading.Tasks;

namespace Emando.Vantage.Components
{
    public interface ILoadFromContext<in TContext>
    {
        Task LoadAsync(TContext context);
    }
}