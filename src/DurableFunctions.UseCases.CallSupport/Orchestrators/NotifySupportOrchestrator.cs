using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases
{
    public class NotifySupportOrchestrator
    {
        [FunctionName(nameof(NotifySupportOrchestrator))]
        public async Task Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {
            var input = context.GetInput<NotifySupportInput>();

            var retryNotificationInput = new RetryNotificationInput { 
                    MaxNotificationAttempts = input.MaxNotificationAttempts,
                    Message = input.Message,
                    NotificationAttemptCount = 1,
                    PhoneNumber = input.PhoneNumbers[input.PhoneNumberIndex],
                    WaitTimeForEscalation = input.WaitTimeForEscalation };
            
            var notificationResult = await context.CallSubOrchestratorAsync<RetryNotificationResult>(
                    nameof(RetryNotificationOrchestrator),
                    retryNotificationInput);
            
            if (!notificationResult.CallBackReceived && input.PhoneNumbers.Last() != retryNotificationInput.PhoneNumber)
            {
                // Calls have not been answered, let's try the next number.
                input.PhoneNumberIndex++;
                logger.LogInformation($"=== Next Number={input.PhoneNumbers[input.PhoneNumberIndex]} ===");
                context.ContinueAsNew(input);
            }
            else
            {
                logger.LogInformation($"=== Completed {nameof(NotifySupportOrchestrator)} for {notificationResult.PhoneNumber} with callback received={notificationResult.CallBackReceived} on attempt={notificationResult.Attempt}. ===");
            }
        }
        
    }
}