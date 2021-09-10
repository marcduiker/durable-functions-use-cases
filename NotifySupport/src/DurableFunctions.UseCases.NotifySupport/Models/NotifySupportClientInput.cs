namespace DurableFunctions.UseCases.NotifySupport
{
    public class NotifySupportClientInput
    {
        public string Message { get; set; }
        public int Severity { get; set; }
    }
}