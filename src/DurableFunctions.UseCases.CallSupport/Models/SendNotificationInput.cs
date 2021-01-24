namespace DurableFunctions.UseCases
{
    public class SendNotificationInput
    {
        public int Attempt { get; set; }
        public string Message { get; set; }
        public string OrchestratorInstanceId { get; set; }
        public string PhoneNumber { get; set; }
    }
}