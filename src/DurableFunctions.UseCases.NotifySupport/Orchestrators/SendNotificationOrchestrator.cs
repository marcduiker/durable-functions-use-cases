using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases.NotifySupport
{
    public class SendNotificationOrchestrator
    {
        [FunctionName(nameof(SendNotificationOrchestrator))]
        public async Task<SendNotificationOrchestratorResult> Run(
          [OrchestrationTrigger] IDurableOrchestrationContext context,
          ILogger logger)
        {
            var input = context.GetInput<SendNotificationOrchestratorInput>();
            var waitTimeBetweenRetry = TimeSpan.FromSeconds(input.WaitTimeForEscalationInSeconds / input.MaxNotificationAttempts);

            // Use Durable Entity to store orchestrator instanceId based on phonenumber
            var entityId = new EntityId(nameof(NotificationOrchestratorInstanceEntity), input.SupportContact.PhoneNumber);
            context.SignalEntity(
                entityId,
                nameof(NotificationOrchestratorInstanceEntity.Set),
                context.InstanceId);

            var activityInput = new SendNotificationActivityInput { 
                Attempt = input.NotificationAttemptCount,
                Message = input.Message,
                PhoneNumber = input.SupportContact.PhoneNumber};
            await context.CallActivityAsync(nameof(SendNotificationActivity), activityInput);
            
            // Orchestrator will wait until the event is received or waitTimeBetweenRetry is passed, defaults to false.
            var callBackResult = await context.WaitForExternalEvent<bool>(EventNames.Callback, waitTimeBetweenRetry, false);
            if (!callBackResult && input.NotificationAttemptCount < input.MaxNotificationAttempts)
            {
                // Call has not been answered, let's try again!
                input.NotificationAttemptCount++;
                context.ContinueAsNew(input);
            }

            // Call has been answered or MaxNotificationAttempts has been reached.
            var result = new SendNotificationOrchestratorResult {
                Attempt  = input.NotificationAttemptCount,
                PhoneNumber = input.SupportContact.PhoneNumber,
                CallbackReceived = callBackResult };

            return result;
        }
    }
}