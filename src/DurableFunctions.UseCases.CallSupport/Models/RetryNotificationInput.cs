namespace DurableFunctions.UseCases
{
    public class RetryNotificationInput
    {
        public string PhoneNumber { get; set; }

        public string  Message { get; set; }

        public int NotificationAttemptCount { get; set; }

        public int WaitTimeForEscalation { get; set; }

        public int MaxNotificationAttempts { get; set; }
    }
}
