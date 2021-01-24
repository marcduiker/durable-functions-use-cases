using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net.Http;

namespace DurableFunctions.UseCases
{
    public static class NotifySupportHttpClient
    {
        [FunctionName(nameof(NotifySupportHttpClient))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethod.Post),
                Route = null)] HttpRequestMessage message,
            [DurableClient] IDurableClient client,
            ILogger logger)
        {
            var input = await message.Content.ReadAsAsync<NotifySupportInput>();

            string instanceId = await client.StartNewAsync(
               nameof(NotifySupportOrchestrator),
               input);

            return client.CreateCheckStatusResponse(message, instanceId);
        }
    }
}
