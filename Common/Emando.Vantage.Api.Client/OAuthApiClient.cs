using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Emando.Vantage.Components;

namespace Emando.Vantage.Api.Client
{
    public class OAuthApiClient : IDisposable
    {
        private readonly Uri baseUri;
        private readonly HttpClient client = new HttpClient();
        private readonly string clientId;
        private readonly SecureString clientSecret;
        private bool isDisposed;
        private Task<Token> refreshTokenTask;

        public OAuthApiClient(Uri baseUri, string clientId, SecureString clientSecret = null)
        {
            this.baseUri = baseUri;
            this.clientId = clientId;
            this.clientSecret = clientSecret;
        }

        internal Token Token { get; private set; }

        public string UserName => Token?.UserName;

        public string GivenName => Token?.GivenName;

        public string Name => Token?.Name;

        private async Task<Token> LoginAsync()
        {
            var formData = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", clientSecret.ToUnsecureString() }
            };
            var content = new FormUrlEncodedContent(formData);
            var response = await client.PostAsync(new Uri(baseUri, "token"), content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Token>();
        }

        public async Task LoginAsync(string userName, SecureString password)
        {
            var formData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", clientId },
                { "client_secret", clientSecret.ToUnsecureString() },
                { "username", userName },
                { "password", password.ToUnsecureString() }
            };
            var content = new FormUrlEncodedContent(formData);
            var response = await client.PostAsync(new Uri(baseUri, "token"), content);
            response.EnsureSuccessStatusCode();

            Token = await response.Content.ReadAsAsync<Token>();
        }

        internal Task<Token> LoginOrRefreshTokenAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<Token>();
            var task = Interlocked.CompareExchange(ref refreshTokenTask, taskCompletionSource.Task, null);
            if (task != null)
                return task;

            var loginTask = Token != null ? RefreshTokenAsync() : LoginAsync();
            loginTask.ContinueWith(t =>
            {
                switch (t.Status)
                {
                    case TaskStatus.RanToCompletion:
                        Token = t.Result;
                        taskCompletionSource.SetResult(Token);
                        break;
                    case TaskStatus.Canceled:
                        taskCompletionSource.SetCanceled();
                        break;
                    case TaskStatus.Faulted:
                        taskCompletionSource.SetException(t.Exception);
                        break;
                }
                Interlocked.CompareExchange(ref refreshTokenTask, null, taskCompletionSource.Task);
            });

            return taskCompletionSource.Task;
        }

        private async Task<Token> RefreshTokenAsync()
        {
            if (Token == null)
                throw new InvalidOperationException();

            var formData = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", clientId },
                { "client_secret", clientSecret.ToUnsecureString() },
                { "refresh_token", Token.RefreshToken }
            };
            var content = new FormUrlEncodedContent(formData);
            var response = await client.PostAsync(new Uri(baseUri, "token"), content);
            if (response.StatusCode == HttpStatusCode.BadRequest)
                return await LoginAsync();

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Token>();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~OAuthApiClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                    client.Dispose();

                isDisposed = true;
            }
        }
    }
}