using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases.NotifySupport
{
    public static class CallbackHttpClient
    {
        [FunctionName(nameof(CallbackHttpClient))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(
                AuthorizationLevel.Function,
                nameof(HttpMethod.Post),
                Route = null)] HttpRequestMessage message,
            [DurableClient] IDurableClient client,
            ILogger logger)
        {
            var phoneNumber = await message.Content.ReadAsAsync<string>();
            var entityId = new EntityId(nameof(NotificationOrchestratorInstanceEntity), phoneNumber);
            var instanceEntity = await client.ReadEntityStateAsync<NotificationOrchestratorInstanceEntity>(entityId);
            if (instanceEntity.EntityExists)
            {
                await client.RaiseEventAsync(
                    instanceEntity.EntityState.InstanceId,
                    EventNames.Callback,
                    true);
            }
            else
            {
                logger.LogError($"=== No instanceId found for {phoneNumber}. ===");
            }
            
            return new AcceptedResult();
        }
    }
}