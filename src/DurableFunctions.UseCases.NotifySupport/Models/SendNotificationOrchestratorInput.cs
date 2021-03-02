namespace DurableFunctions.UseCases.NotifySupport
{
    public class SendNotificationOrchestratorInput
    {
        public int MaxNotificationAttempts { get; set; }
        public string Message { get; set; }
        public int NotificationAttemptCount { get; set; }
        public SupportContactEntity SupportContact { get; set; }
        public int WaitTimeForEscalationInSeconds { get; set; }
    }
}