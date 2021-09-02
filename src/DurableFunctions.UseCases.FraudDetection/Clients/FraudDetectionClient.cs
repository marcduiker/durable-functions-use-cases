using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DurableFunctions.UseCases.FraudDetection.Models;
using System.Net.Http;

namespace DurableFunctions.UseCases.FraudDetection.Clients
{
    public static class FraudDetectionClient
    {
        [FunctionName(nameof(FraudDetectionClient))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestMessage message,
            [DurableClient] IDurableClient client,
            ILogger log)
        {
            var transaction = await message.Content.ReadAsAsync<Transaction>();
            var instanceId = await client.StartNewAsync(
                nameof(FraudDetectionOrchestrator), 
                transaction);

            return client.CreateCheckStatusResponse(message, instanceId);
        }
    }
}
