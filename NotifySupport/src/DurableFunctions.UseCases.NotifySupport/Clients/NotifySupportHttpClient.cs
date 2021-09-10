using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Net.Http;
using System;

namespace DurableFunctions.UseCases.NotifySupport
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
            var clientInput = await message.Content.ReadAsAsync<NotifySupportClientInput>();
            var waitTimeForEscalationInSeconds = int.TryParse(Environment.GetEnvironmentVariable("WaitTimeForEscalationInSeconds"), out int waitTime) ? waitTime : 60;
            var maxNotificationAttempts = int.TryParse(Environment.GetEnvironmentVariable("MaxNotificationAttempts"), out int maxAttempts) ? maxAttempts : 3;
            
            var orchestratorInput = NotifySupportOrchestratorInputBuilder.Build(
                clientInput,
                maxNotificationAttempts,
                waitTimeForEscalationInSeconds);

            string instanceId = await client.StartNewAsync(
               nameof(NotifySupportOrchestrator),
               orchestratorInput);

            return client.CreateCheckStatusResponse(message, instanceId);
        }
    }
}
