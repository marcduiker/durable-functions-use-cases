namespace DurableFunctions.UseCases.NotifySupport
{
    public class NotificationOrchestratorResult
    {
        public int Attempt { get; set; }
        public bool CallBackReceived { get; set; }
        public string PhoneNumber { get; set; }
    }
}