using System;
using Newtonsoft.Json;

namespace Emando.Vantage.Data.Json
{
    public class TimeSpanTicksConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var time = value as TimeSpan?;
            serializer.Serialize(writer, time?.Ticks);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.TokenType != JsonToken.Null ? TimeSpan.FromTicks(serializer.Deserialize<long>(reader)) : new TimeSpan?();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TimeSpan).IsAssignableFrom(objectType) || typeof(TimeSpan?).IsAssignableFrom(objectType);
        }
    }
}