namespace DurableFunctions.UseCases
{
    public class NotifySupportInput
    {
        public int MaxNotificationAttempts { get; set; }
        public string Message { get; set; }
        public int PhoneNumberIndex { get; set; }
        public string[] PhoneNumbers { get; set; }
        public int WaitTimeForEscalation { get; set; }
    }
}