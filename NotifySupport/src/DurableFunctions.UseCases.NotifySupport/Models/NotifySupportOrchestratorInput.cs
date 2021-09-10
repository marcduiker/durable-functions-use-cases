namespace DurableFunctions.UseCases.NotifySupport
{
    public class NotifySupportOrchestratorInput
    {
        public int MaxNotificationAttempts { get; set; }
        public string Message { get; set; }
        public int Severity { get; set; }
        public int SupportContactIndex { get; set; }
        public SupportContactEntity[] SupportContacts { get; set; }
        public int WaitTimeForEscalationInSeconds { get; set; }
    }
}