namespace DurableFunctions.UseCases.NotifySupport
{
    public static class NotificationOrchestratorInputBuilder
    {
        public static NotificationOrchestratorInput Build(
            NotifySupportOrchestratorInput orchestratorInput,
            int supportContactIndex)
        {
            return new NotificationOrchestratorInput 
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