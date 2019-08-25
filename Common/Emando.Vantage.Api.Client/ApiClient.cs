using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Emando.Vantage.Api.Client.Properties;

namespace Emando.Vantage.Api.Client
{
    public class ApiClient : IDisposable
    {
        private readonly OAuthApiClient oauthClient;
        private bool isDisposed;

        public ApiClient(Uri baseUri, OAuthApiClient oauthClient)
        {
            BaseUri = baseUri;
            this.oauthClient = oauthClient ?? throw new ArgumentNullException(nameof(oauthClient));

            if (oauthClient.Token != null)
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(oauthClient.Token.TokenType,
                    oauthClient.Token.AccessToken);
        }

        public ApiClient(Uri baseUri, string clientId, SecureString clientSecret = null)
            : this(baseUri, new OAuthApiClient(baseUri, clientId, clientSecret))
        {
        }

        public Uri BaseUri { get; }

        public TimeSpan Timeout
        {
            get { return Client.Timeout; }
            set { Client.Timeout = value; }
        }

        public string AcceptLanguage
        {
            get { return Client.DefaultRequestHeaders.AcceptLanguage.FirstOrDefault()?.Value; }
            set
            {
                Client.DefaultRequestHeaders.AcceptLanguage.Clear();
                Client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(value, 1));
            }
        }

        protected HttpClient Client { get; } = new HttpClient();

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        protected async Task<HttpContent> InvokeAsync(string relativeUri, Func<Uri, Task<HttpResponseMessage>> action)
        {
            var uri = new Uri(BaseUri, relativeUri);
            var response = await action(uri);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var token = await oauthClient.LoginOrRefreshTokenAsync();
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType,
                    token.AccessToken);
                response = await action(uri);
            }

            if (response.IsSuccessStatusCode)
                return response.Content;

            Trace.TraceWarning("API request to '{0}' failed with status code {1}.", relativeUri, response.StatusCode);
            switch (response.StatusCode)
            {
                case HttpStatusCode.MethodNotAllowed:
                    throw new ApiException(Resources.InvalidApiRequest);
                case HttpStatusCode.BadRequest:
                    var errorMessage = response.Content.ReadAsAsync<ErrorMessage>().Result;
                    if (errorMessage != null)
                        throw new ApiClientException(errorMessage.Message);
                    throw new ApiClientException(Resources.InvalidApiRequest);
                case HttpStatusCode.Unauthorized:
                    throw new AuthenticationException(Resources.ApiUnauthorized);
                case HttpStatusCode.NotFound:
                    throw new ApiResourceNotFoundException(Resources.ApiResourceNotFound);
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.ServiceUnavailable:
                default:
                    throw new ApiServerException(Resources.ApiServerError);
            }
        }

        protected async Task<TResponse> GetAsAsync<TResponse>(string relativeUri, CancellationToken cancellationToken)
        {
            var content = await InvokeAsync(relativeUri, u => Client.GetAsync(u, cancellationToken));
            return await content.ReadAsAsync<TResponse>(cancellationToken);
        }

        protected Task<Stream> GetStreamAsync(string relativeUri)
        {
            return Client.GetStreamAsync(new Uri(BaseUri, relativeUri));
        }

        protected Task PostAsync<T>(string relativeUri, T value, CancellationToken cancellationToken)
        {
            return InvokeAsync(relativeUri, u => Client.PostAsJsonAsync(u, value, cancellationToken));
        }

        protected async Task<TResponse> PostAsync<T, TResponse>(string relativeUri, T value,
            CancellationToken cancellationToken)
        {
            var content = await InvokeAsync(relativeUri, u => Client.PostAsJsonAsync(u, value, cancellationToken));
            return await content.ReadAsAsync<TResponse>(cancellationToken);
        }

        protected Task PutAsync(string relativeUri, CancellationToken cancellationToken)
        {
            return InvokeAsync(relativeUri, u => Client.PutAsync(u, null, cancellationToken));
        }

        protected async Task<TResponse> PutAsync<TResponse>(string relativeUri, CancellationToken cancellationToken)
        {
            var content = await InvokeAsync(relativeUri, u => Client.PutAsync(u, null, cancellationToken));
            return await content.ReadAsAsync<TResponse>(cancellationToken);
        }

        protected Task PutAsync<T>(string relativeUri, T value, CancellationToken cancellationToken)
        {
            return InvokeAsync(relativeUri, u => Client.PutAsJsonAsync(u, value, cancellationToken));
        }

        protected async Task<TResponse> PutAsync<T, TResponse>(string relativeUri, T value,
            CancellationToken cancellationToken)
        {
            var content = await InvokeAsync(relativeUri, u => Client.PutAsJsonAsync(u, value, cancellationToken));
            return await content.ReadAsAsync<TResponse>(cancellationToken);
        }

        protected Task DeleteAsync(string relativeUri, CancellationToken cancellationToken)
        {
            return InvokeAsync(relativeUri, u => Client.DeleteAsync(u, cancellationToken));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    Client.Dispose();

                isDisposed = true;
            }
        }

        ~ApiClient()
        {
            Dispose(false);
        }
    }
}