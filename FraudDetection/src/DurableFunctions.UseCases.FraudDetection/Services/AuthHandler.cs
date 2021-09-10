using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DurableFunctions.UseCases.FraudDetection.Services
{
    public class AuthHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(
                        $"{Environment.GetEnvironmentVariable("GitHubOwner")}:{Environment.GetEnvironmentVariable("GitHubWebhookPAT")}"));
            request.Headers.Add("User-Agent", "webhook-playground");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}