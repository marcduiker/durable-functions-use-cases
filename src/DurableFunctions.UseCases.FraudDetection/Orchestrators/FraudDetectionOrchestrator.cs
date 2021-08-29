using System;
using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Activities;
using DurableFunctions.UseCases.FraudDetection.Builders;
using DurableFunctions.UseCases.FraudDetection.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace DurableFunctions.UseCases.FraudDetection
{
    public class FraudDetectionOrchestrator
    {
        [FunctionName(nameof(FraudDetectionOrchestrator))]
        public async Task Run(
            [OrchestrationTrigger] IDurableOrchestrationContext context,
            ILogger logger)
        {
            var transaction = context.GetInput<Transaction>();

            var creditor = await context.CallActivityAsync<Customer>(
                nameof(GetCustomerActivity),
                transaction.CreditorBankAccount);

            var debtor = await context.CallActivityAsync<Customer>(
                nameof(GetCustomerActivity),
                transaction.DebtorBankAccount);

            var auditRecord = AuditRecordBuilder.Create(
                transaction,
                creditor,
                debtor);

            var analyzedRecordId = await context.CallActivityAsync<Guid>(
                nameof(AnalyzeAuditRecordActivity),
                auditRecord);

            // TODO store Id
            // TODO wait for event to continue

            await context.CallActivityAsync(
                nameof(StoreAuditRecordActivity),
                auditRecord);
        }
    }
}