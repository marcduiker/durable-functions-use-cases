namespace DurableFunctions.UseCases.NotifySupport
{
    public class SendNotificationOrchestratorResult
    {
        public int Attempt { get; set; }
        public bool CallbackReceived { get; set; }
        public string PhoneNumber { get; set; }
    }
}