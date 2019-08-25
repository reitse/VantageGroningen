namespace Emando.Vantage.Entities.Competitions
{
    //[DataContract(Namespace = "http://emandovantage.com/2014/02/Entities/Competitions")]
    //public class RaceComparand
    //{
    //    public RaceComparand(RaceComparandType type, Guid? id, string shortDisplayName, string fullDisplayName, IEnumerable<IReadOnlyRaceLap> laps, IEnumerable<IReadOnlyRacePassing> passings)
    //    {
    //        if (laps == null)
    //            throw new ArgumentNullException("laps");
    //        if (passings == null)
    //            throw new ArgumentNullException("passings");

    //        Type = type;
    //        Id = id;
    //        ShortDisplayName = shortDisplayName;
    //        FullDisplayName = fullDisplayName;
    //        Laps = laps.Select(l => new Lap
    //        {
    //            Time = l.Time,
    //            Points = l.Points
    //        }).ToList();
    //        Passings = passings.Select(p => new Passing
    //        {
    //            Time = p.Time,
    //            Passed = p.Passed,
    //            Speed = p.Speed
    //        }).ToList();
    //    }

    //    [DataMember]
    //    public RaceComparandType Type { get; private set; }

    //    [DataMember]
    //    public Guid? Id { get; private set; }

    //    [DataMember]
    //    public string ShortName { get; set; }

    //    [DataMember]
    //    public string FullName { get; set; }

    //    [DataMember]
    //    public IList<Lap> Laps { get; private set; }

    //    [DataMember]
    //    public IList<Passing> Passings { get; private set; }
    //}
}