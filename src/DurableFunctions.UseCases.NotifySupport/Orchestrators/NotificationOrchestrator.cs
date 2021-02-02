using System;
using System.Threading.Tasks;
using DurableFunctions.UseCases.NotifySupport;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases.NotifySupport
{
    public class NotificationOrchestrator
    {
        [FunctionName(nameof(NotificationOrchestrator))]
        public async Task<NotificationOrchestratorResult> Run(
          [OrchestrationTrigger] IDurableOrchestrationContext context,
          ILogger logger)
        {
            var input = context.GetInput<NotificationOrchestratorInput>();
            var waitTimeBetweenRetry = TimeSpan.FromSeconds(input.WaitTimeForEscalationInSeconds / input.MaxNotificationAttempts);
            
            var activityInput = new SendNotificationInput { 
                Attempt = input.NotificationAttemptCount,
                Message = input.Message,
                PhoneNumber = input.SupportContact.PhoneNumber};
            // Use Durable Entity to store instance based on phonenumber
            var entityId = new EntityId(nameof(NotificationOrchestratorInstanceEntity), input.SupportContact.PhoneNumber);
            context.SignalEntity(
                entityId,
                nameof(NotificationOrchestratorInstanceEntity.Set),
                context.InstanceId);

            await context.CallActivityAsync(nameof(SendNotificationActivity), activityInput);
            
            // Orchestrator will wait until the event is received or waitTimeBetweenRetry is passed (not very accurate), defaults to false.
            var callBackResult = await context.WaitForExternalEvent<bool>(EventNames.CallBack, waitTimeBetweenRetry, false);
            if (!callBackResult && input.NotificationAttemptCount < input.MaxNotificationAttempts)
            {
                // Call has not been answered, let's try again!
                input.NotificationAttemptCount++;
                context.ContinueAsNew(input);
            }

            var result = new NotificationOrchestratorResult {
                Attempt  = input.NotificationAttemptCount,
                PhoneNumber = input.SupportContact.PhoneNumber,
                CallBackReceived = callBackResult };

            return result;
        }
    }
}