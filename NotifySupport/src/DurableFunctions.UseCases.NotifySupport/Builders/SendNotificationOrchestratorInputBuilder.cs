namespace DurableFunctions.UseCases.NotifySupport
{
    public static class SendNotificationOrchestratorInputBuilder
    {
        public static SendNotificationOrchestratorInput Build(
            NotifySupportOrchestratorInput orchestratorInput)
        {
            return new SendNotificationOrchestratorInput 
            {
                MaxNotificationAttempts = orchestratorInput.MaxNotificationAttempts,
                Message = orchestratorInput.Message,
                NotificationAttemptCount = 1,
                SupportContact = orchestratorInput.SupportContacts[orchestratorInput.SupportContactIndex],
                WaitTimeForEscalationInSeconds = orchestratorInput.WaitTimeForEscalationInSeconds
            };
        }
    }
}