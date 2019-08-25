using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Emando.Vantage.Entities.Competitions;

namespace Emando.Vantage.Server.Services.Competitions
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/02/Server/Competitions")]
    public interface IDataService : IDisposable
    {
        [OperationContract]
        Task<Distance[]> GetDistancesAsync(Guid competitionId);
    }
}