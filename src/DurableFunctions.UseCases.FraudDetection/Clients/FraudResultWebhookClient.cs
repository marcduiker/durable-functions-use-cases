using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DurableFunctions.UseCases.FraudDetection.Models;
using DurableFunctions.UseCases.FraudDetection.Entities;
using System.Net.Http;

namespace DurableFunctions.UseCases.FraudDetection.Clients
{
    public static class FraudResultWebhookClient
    {
        [FunctionName(nameof(FraudResultWebhookClient))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] FraudResult fraudResult,
            [DurableClient] IDurableClient client,
            ILogger log)
        {
            var entityId = new EntityId(
                nameof(FraudDetectionOrchestratorEntity),
                fraudResult.RecordId);

            var entityStateResponse = await client.ReadEntityStateAsync<FraudDetectionOrchestratorEntity>(entityId);
            if (entityStateResponse.EntityExists)
            {
                await client.RaiseEventAsync(
                    entityStateResponse.EntityState.InstanceId,
                    Constants.FraudResultCompletedEvent,
                    fraudResult.IsSuspiciousTransaction);
                return new AcceptedResult();
            }
            else
            {
                return new BadRequestObjectResult($"Entity {entityId} does not exist.");
            }
        }
    }
}
