using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases.NotifySupport
{
    public class SendNotificationActivity
    {
        [FunctionName(nameof(SendNotificationActivity))]
        public void Run(
            [ActivityTrigger] SendNotificationInput input,
            ILogger logger)
        {
            logger.LogInformation($"=== Calling {input.PhoneNumber}, Attempt={input.Attempt}, Message={input.Message} ===");

            return;
        }
    }
}