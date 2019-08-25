using System.Runtime.Serialization;

namespace Emando.Vantage.Entities.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    public class Weather
    {
        [DataMember]
        public double? AirTemperature { get; set; }
        [DataMember]
        public double? TrackTemperature { get; set; }
        [DataMember]
        public double? WindSpeed { get; set; }
        [DataMember]
        public double? Humidity { get; set; }
        [DataMember]
        public double? AirPressure { get; set; }
    }
}