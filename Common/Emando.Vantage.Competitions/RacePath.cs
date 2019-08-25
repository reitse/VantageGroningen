using System;
using System.Runtime.Serialization;

namespace Emando.Vantage.Competitions
{
    [DataContract(Namespace = "http://emandovantage.com/2014/02/Competitions")]
    public struct RacePath
    {
        public RacePath(int distance, int round, int heat, int lane) : this()
        {
            Distance = distance;
            Round = round;
            Heat = heat;
            Lane = lane;
        }

        [DataMember]
        public int Distance { get; set; }

        [DataMember]
        public int Round { get; set; }

        [DataMember]
        public int Heat { get; set; }

        [DataMember]
        public int Lane { get; set; }

        public override string ToString()
        {
            return $"{Distance}/{Round}/{Heat}/{Lane}";
        }

        public static RacePath Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            var fields = s.Split('.', '/');
            if (fields.Length != 4)
                throw new FormatException();

            var distance = int.Parse(fields[0]);
            var round = int.Parse(fields[1]);
            var heat = int.Parse(fields[2]);
            var lane = int.Parse(fields[3]);
            return new RacePath(distance, round, heat, lane);
        }
    }
}