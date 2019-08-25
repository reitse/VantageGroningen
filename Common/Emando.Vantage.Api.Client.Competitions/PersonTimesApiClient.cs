using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Models.Competitions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Emando.Vantage.Api.Client.Competitions
{
    public class PersonTimesApiClient : ApiClient
    {
        public PersonTimesApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
            Timeout = TimeSpan.FromMinutes(10);
        }

        public PersonTimesApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
            Timeout = TimeSpan.FromMinutes(10);
        }

        public Task<HistoricalTimeViewModel[]> GetPersonTimesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAsAsync<HistoricalTimeViewModel[]>("people/times", cancellationToken);
        }

        public IEnumerable<HistoricalTimeViewModel> GetPersonTimes(CancellationToken cancellationToken)
        {
            var serializer = JsonSerializer.CreateDefault();
            using (var stream = GetStreamAsync("people/times").Result)
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                if (!jsonReader.Read() || jsonReader.TokenType != JsonToken.StartArray)
                    throw new FormatException();

                while (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartObject)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return (HistoricalTimeViewModel)serializer.Deserialize(jsonReader, typeof(HistoricalTimeViewModel));
                }
            }
        } 
    }
}