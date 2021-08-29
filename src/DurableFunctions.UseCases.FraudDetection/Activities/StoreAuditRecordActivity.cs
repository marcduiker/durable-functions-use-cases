namespace DurableFunctions.UseCases
{
    public class StoreAuditRecordActivity
    {
        [FunctionName(nameof(StoreAuditRecordActivity))]
        [return: CosmosDB()]
        public async Task<AuditRecord> Run(
            [ActivityTrigger] AuditRecord auditRecord,
            ILogger logger)
        {
            return auditRecord; 
        }
    }
}