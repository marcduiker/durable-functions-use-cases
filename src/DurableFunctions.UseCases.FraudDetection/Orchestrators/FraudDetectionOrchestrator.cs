namespace DurableFunctions.UseCases.FraudDetection
{
    public class FraudDetectionOrchestrator
    {
        [FunctionName(nameof(FraudDetectionOrchestrator))]
        public async Task Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {

        }
    }
}