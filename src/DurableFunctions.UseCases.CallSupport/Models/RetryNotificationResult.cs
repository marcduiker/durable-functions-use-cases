namespace DurableFunctions.UseCases
{
    public class RetryNotificationResult
    {
        public int Attempt { get; set; }
        public bool CallBackReceived { get; set; }
        public string PhoneNumber { get; set; }
    }
}