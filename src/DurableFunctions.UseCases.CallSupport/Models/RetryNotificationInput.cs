namespace DurableFunctions.UseCases
{
    public class RetryNotificationInput
    {
        public int MaxNotificationAttempts { get; set; }
        public string  Message { get; set; }
        public int NotificationAttemptCount { get; set; }
        public string PhoneNumber { get; set; }
        public int WaitTimeForEscalation { get; set; }
    }
}
