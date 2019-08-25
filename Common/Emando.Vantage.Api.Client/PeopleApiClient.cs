using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Models;
using Newtonsoft.Json;

namespace Emando.Vantage.Api.Client
{
    public class PeopleApiClient : ApiClient
    {
        public PeopleApiClient(Uri baseUri, OAuthApiClient oauthClient) : base(baseUri, oauthClient)
        {
        }

        public PeopleApiClient(Uri baseUri, string clientId, SecureString clientSecret = null) : base(baseUri, clientId, clientSecret)
        {
        }

        public IEnumerable<PersonDetailsViewModel> Get(CancellationToken cancellationToken = default(CancellationToken))
        {
            var serializer = JsonSerializer.CreateDefault();
            using (var stream = GetStreamAsync("people").Result)
            using (var reader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(reader))
            {
                if (!jsonReader.Read() || jsonReader.TokenType != JsonToken.StartArray)
                    throw new FormatException();

                while (jsonReader.Read() && jsonReader.TokenType == JsonToken.StartObject)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    yield return (PersonDetailsViewModel)serializer.Deserialize(jsonReader, typeof(PersonDetailsViewModel));
                }
            }
        }
    }
}