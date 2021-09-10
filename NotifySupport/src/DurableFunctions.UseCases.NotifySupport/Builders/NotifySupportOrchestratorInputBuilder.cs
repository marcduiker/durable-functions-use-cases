namespace DurableFunctions.UseCases.NotifySupport
{
    public static class NotifySupportOrchestratorInputBuilder
    {
        public static NotifySupportOrchestratorInput Build(
            NotifySupportClientInput clientInput,
            int maxNotificationAttempts,
            int waitTimeForEscalationInSeconds)
        {
            return new NotifySupportOrchestratorInput 
            {
                MaxNotificationAttempts = maxNotificationAttempts,
                Message = clientInput.Message,
                SupportContactIndex = 0,
                // The SupportContacts is *not* set here, they are added later.
                Severity = clientInput.Severity,
                WaitTimeForEscalationInSeconds = waitTimeForEscalationInSeconds
            };
        }
    }
}