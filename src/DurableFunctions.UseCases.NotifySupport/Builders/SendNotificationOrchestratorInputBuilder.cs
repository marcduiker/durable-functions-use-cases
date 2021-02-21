namespace DurableFunctions.UseCases.NotifySupport
{
    public static class SendNotificationOrchestratorInputBuilder
    {
        public static SendNotificationOrchestratorInput Build(
            NotifySupportOrchestratorInput orchestratorInput,
            int supportContactIndex)
        {
            return new SendNotificationOrchestratorInput 
            {
                MaxNotificationAttempts = orchestratorInput.MaxNotificationAttempts,
                Message = orchestratorInput.Message,
                NotificationAttemptCount = 1,
                SupportContact = orchestratorInput.SupportContacts[supportContactIndex],
                WaitTimeForEscalationInSeconds = orchestratorInput.WaitTimeForEscalationInSeconds
            };
        }
    }
}