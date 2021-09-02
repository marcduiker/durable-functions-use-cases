using System;
using System.Threading.Tasks;
using DurableFunctions.UseCases.FraudDetection.Activities;
using DurableFunctions.UseCases.FraudDetection.Builders;
using DurableFunctions.UseCases.FraudDetection.Entities;
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

            (Customer Creditor, Customer Debtor) customers = await GetCustomersForTransactionAsync(context, transaction);

            var auditRecord = AuditRecordBuilder.Create(
                context.NewGuid().ToString(),
                transaction,
                customers.Creditor,
                customers.Debtor);

            var analyzedRecordId = await context.CallActivityAsync<string>(
                nameof(AnalyzeAuditRecordActivity),
                auditRecord);

            // Create an entity based on the analyzed record Id to store the current orchestration Id.
            var entityId = new EntityId(
                nameof(FraudDetectionOrchestratorEntity), 
                analyzedRecordId);
            context.SignalEntity(
                entityId,
                nameof(FraudDetectionOrchestratorEntity.Set),
                context.InstanceId);
            
            var timeOut = TimeSpan.FromMinutes(5);
            var defaultResult = true;
            var isSuspiciousTransaction = await context.WaitForExternalEvent<bool>(
                Constants.FraudResultCompletedEvent,
                timeOut,
                defaultResult);

            auditRecord.IsSuspiciousTransaction = isSuspiciousTransaction;

            await context.CallActivityAsync(
                nameof(StoreAuditRecordActivity),
                auditRecord);
        }

        private async Task<(Customer Creditor, Customer Debtor)> GetCustomersForTransactionAsync(
            IDurableOrchestrationContext context,
            Transaction transaction)
        {
            var creditorTask = context.CallActivityAsync<Customer>(
                nameof(GetCustomerActivity),
                transaction.CreditorBankAccount);

            var debtorTask = context.CallActivityAsync<Customer>(
                nameof(GetCustomerActivity),
                transaction.DebtorBankAccount);

            await Task.WhenAll(creditorTask, debtorTask);
            
            return (creditorTask.Result, debtorTask.Result);
        }
    }
}