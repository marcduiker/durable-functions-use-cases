using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases.NotifySupport
{
    public class NotifySupportOrchestrator
    {
        [FunctionName(nameof(NotifySupportOrchestrator))]
        public async Task Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {
            var input = context.GetInput<NotifySupportOrchestratorInput>();

            if (input.SupportContacts == null)
            {
                // Let's get the support contacts from storage.
                var supportContacts = await context.CallActivityAsync<IEnumerable<SupportContactEntity>>(
                    nameof(GetSupportContactActivity),
                    "A");

                input.SupportContacts = supportContacts.ToArray();
            }

            var notificationOrchestratorInput = SendNotificationOrchestratorInputBuilder.Build(input);
            
            var notificationResult = await context.CallSubOrchestratorAsync<SendNotificationOrchestratorResult>(
                    nameof(SendNotificationOrchestrator),
                    notificationOrchestratorInput);
            
            if (!notificationResult.CallbackReceived &&
                notificationOrchestratorInput.SupportContact != input.SupportContacts.Last())
            {
                // Calls have not been answered, let's try the next contact.
                input.SupportContactIndex++;
                logger.LogInformation($"=== Next Contact={input.SupportContacts[input.SupportContactIndex].PhoneNumber} ===");
                context.ContinueAsNew(input);
            }
            else
            {
                logger.LogInformation($"=== Completed {nameof(NotifySupportOrchestrator)} for {notificationResult.PhoneNumber} with callback received={notificationResult.CallbackReceived} on attempt={notificationResult.Attempt}. ===");
            }
        }
    }
}