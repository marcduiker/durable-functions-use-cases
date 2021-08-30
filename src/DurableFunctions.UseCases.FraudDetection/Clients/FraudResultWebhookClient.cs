using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DurableFunctions.UseCases.Entities;
using DurableFunctions.UseCases.FraudDetection.Models;

namespace DurableFunctions.UseCases
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
                    "FraudResult",
                    fraudResult.IsSuspiciousTransaction);
            }
            else
            {
                log.LogError($"Entity {entityId} does not exist");
            }

            return new AcceptedResult();
        }
    }
}
