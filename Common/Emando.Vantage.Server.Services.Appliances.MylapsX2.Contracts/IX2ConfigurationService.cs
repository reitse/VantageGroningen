using System.ServiceModel;
using System.Threading.Tasks;
using Emando.Vantage.Server.Entities.Appliances.MylapsX2;

namespace Emando.Vantage.Server.Services.Appliances.MylapsX2
{
    [ServiceContract(Namespace = "http://emandovantage.com/2014/10/Appliances/MYLAPSX2")]
    public interface IX2ConfigurationService
    {
        #region Instances

        [OperationContract]
        Task<X2Instance[]> GetInstancesAsync(string venueCode, string discipline);

        [OperationContract]
        Task<X2Instance> GetInstanceAsync(string venueCode, string discipline, string name);

        [OperationContract]
        Task<X2Instance> AddInstanceAsync(X2Instance instance);

        [OperationContract]
        Task DeleteInstanceAsync(X2Instance instance);

        [OperationContract]
        Task<X2Instance> UpdateInstanceAsync(X2Instance instance, string name, string host, string userName, string password);

        #endregion

        #region Auxiliary Channels

        [OperationContract]
        Task<X2AuxiliaryChannel[]> GetAuxiliaryChannelsAsync(string venueCode, string discipline);

        [OperationContract]
        Task<X2AuxiliaryChannel> AddAuxiliaryChannelAsync(X2AuxiliaryChannel channel);

        [OperationContract]
        Task DeleteAuxiliaryChannelAsync(X2AuxiliaryChannel channel);

        [OperationContract]
        Task<X2AuxiliaryChannel> UpdateAuxiliaryChannelAsync(X2AuxiliaryChannel channel, bool isStart, string device, long @where);

        #endregion

        #region Venue Loops

        [OperationContract]
        Task<X2VenueLaneLocations[]> GetVenueLoopssAsync(string venueCode, string discipline);

        [OperationContract]
        Task<X2VenueLaneLocations> AddVenueLoopsAsync(X2VenueLaneLocations venueLaneLocations);

        [OperationContract]
        Task DeleteVenueLoopsAsync(X2VenueLaneLocations venueLaneLocations);

        [OperationContract]
        Task<X2VenueLaneLocations> UpdateVenueLoopsAsync(X2VenueLaneLocations venueLaneLocations, long startLoop, long finishLoop);

        #endregion
    }
}