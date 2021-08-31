using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DurableFunctions.UseCases.FraudDetection.Builders;

namespace DurableFunctions.UseCases.FraudDetection.Clients
{
    public static class FraudDetectionClient
    {
        [FunctionName(nameof(FraudDetectionClient))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request,
            [DurableClient] IDurableClient client,
            ILogger log)
        {
            // Creating a random transaction, normally the transaction would be in the body of the request.
            var fakeTransaction = FakeTransactionBuilder.Create();

            var instanceId = await client.StartNewAsync(
                nameof(FraudDetectionOrchestrator), 
                fakeTransaction);

            return client.CreateCheckStatusResponse(request, instanceId);
        }
    }
}
