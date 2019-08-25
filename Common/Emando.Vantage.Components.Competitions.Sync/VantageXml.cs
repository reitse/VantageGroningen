using System.IO;
using System.Runtime.Serialization;

namespace Emando.Vantage.Components.Competitions.Sync
{
    public static class VantageXml
    {
        private static readonly DataContractSerializer Serializer = new DataContractSerializer(typeof(VantageSyncData), new DataContractSerializerSettings
        {
            PreserveObjectReferences = true
        });

        public static void Save(VantageSyncData data, Stream stream)
        {
            using (stream)
            using (var memoryStream = new MemoryStream())
            {
                Serializer.WriteObject(memoryStream, data);
                memoryStream.Position = 0;
                memoryStream.CopyTo(stream);
                stream.Flush();
            }
        }

        public static VantageSyncData Load(Stream stream)
        {
            return (VantageSyncData)Serializer.ReadObject(stream);
        }
    }
}