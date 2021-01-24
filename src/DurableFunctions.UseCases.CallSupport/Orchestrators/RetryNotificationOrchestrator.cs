using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases
{
    public class RetryNotificationOrchestrator
    {
        [FunctionName(nameof(RetryNotificationOrchestrator))]
        public async Task<RetryNotificationResult> Run(
          [OrchestrationTrigger] IDurableOrchestrationContext context,
          ILogger logger)
        {
            var input = context.GetInput<RetryNotificationInput>();
            var waitTimeBetweenRetry = TimeSpan.FromSeconds(input.WaitTimeForEscalation / input.MaxNotificationAttempts);
            var activityInput = new SendNotificationInput { 
                Attempt = input.NotificationAttemptCount,
                Message = input.Message,
                OrchestratorInstanceId = context.InstanceId,
                PhoneNumber = input.PhoneNumber};
            await context.CallActivityAsync(nameof(SendNotificationActivity), activityInput);
            
            // Orchestrator will wait until the event is received or waitTimeBetweenRetry is passed (not very accurate), defaults to false.
            var callBackResult = await context.WaitForExternalEvent<bool>(EventNames.CallBack, waitTimeBetweenRetry, false);
            if (!callBackResult && input.NotificationAttemptCount < input.MaxNotificationAttempts)
            {
                // Call has not been answered, let's try again!
                input.NotificationAttemptCount += 1;
                context.ContinueAsNew(input);
            }

            var result = new RetryNotificationResult {
                Attempt  = input.NotificationAttemptCount,
                PhoneNumber = input.PhoneNumber,
                CallBackReceived = callBackResult };

            return result;
        }

        private TimeSpan GetWaitTime(RetryNotificationInput input)
        {
            if (input.MaxNotificationAttempts == 1)
            {
                return TimeSpan.FromSeconds(input.WaitTimeForEscalation);
            }
            else
            {
                return TimeSpan.FromSeconds(input.WaitTimeForEscalation / (input.MaxNotificationAttempts - 1));
            }
        }
    }
}