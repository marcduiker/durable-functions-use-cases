namespace DurableFunctions.UseCases
{
    public class NotifySupportInput
    {
        public string Message { get; set; }
        public int MaxNotificationAttempts { get; set; }
        public int PhoneNumberIndex { get; set; }
        public string[] PhoneNumbers { get; set; }
        public int WaitTimeForEscalation { get; set; }
    }
}