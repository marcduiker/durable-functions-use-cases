using DurableFunctions.UseCases.FraudDetection.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctions.UseCases
{
    public class StoreAuditRecordActivity
    {
        [FunctionName(nameof(StoreAuditRecordActivity))]
        [return: CosmosDB("%CosmosDBName%", "%CosmosDBCollection%", ConnectionStringSetting = "CosmosDBConnection")]
        public AuditRecord Run(
            [ActivityTrigger] AuditRecord auditRecord)
        {
            return auditRecord; 
        }
    }
}