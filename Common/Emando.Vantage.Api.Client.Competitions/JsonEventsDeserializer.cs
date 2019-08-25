using System;
using System.Collections.Generic;
using Emando.Vantage.Data.Json;
using Emando.Vantage.Models.Competitions.Events;
using Emando.Vantage.Models.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Emando.Vantage.Api.Client.Competitions
{
    public static class JsonEventsDeserializer
    {
        private static readonly IDictionary<string, Type> EventTypes = new Dictionary<string, Type>
        {
            { "InstanceSwitchedEvent", typeof(InstanceSwitchedEventViewModel) },
            { "CompetitionActivatedEvent", typeof(CompetitionActivatedEventViewModel) },
            { "DistanceActivatedEvent", typeof(DistanceActivatedEventViewModel) },
            { "DistanceDeactivatedEvent", typeof(DistanceDeactivatedEventViewModel) },
            { "HeatActivatedEvent", typeof(HeatActivatedEventViewModel) },
            { "HeatClearedEvent", typeof(HeatClearedEventViewModel) },
            { "HeatStartedEvent", typeof(HeatStartedEventViewModel) },
            { "HeatCommittedEvent", typeof(HeatCommittedEventViewModel) },
            { "HeatDeactivatedEvent", typeof(HeatDeactivatedEventViewModel) },
            { "RacePassingAddedEvent", typeof(RacePassingAddedEventViewModel) },
            { "RacePassingUpdatedEvent", typeof(RacePassingUpdatedEventViewModel) },
            { "RaceLapAddedEvent", typeof(RaceLapAddedEventViewModel) },
            { "RaceLapUpdatedEvent", typeof(RaceLapUpdatedEventViewModel) },
            { "LastPresentedRaceLapChangedEvent", typeof(LastPresentedRaceLapChangedEventViewModel) }
        };

        private static readonly JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters =
            {
                new TimeSpanTicksConverter()
            }
        });

        public static bool TryDeserialize(JToken token, out EventViewModelBase @event)
        {
            @event = null;
            var type = token.Value<string>("typeName");
            if (!EventTypes.ContainsKey(type))
                return false;

            @event = (EventViewModelBase)token.ToObject(EventTypes[type], Serializer);
            return true;
        }
    }
}