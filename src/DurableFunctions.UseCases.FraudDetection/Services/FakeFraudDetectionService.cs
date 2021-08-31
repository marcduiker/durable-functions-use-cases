using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using DurableFunctions.UseCases.FraudDetection.Models;
using DurableFunctions.UseCases.FraudDetection.Entities;
using DurableFunctions.UseCases.FraudDetection.Services;
using System;

namespace DurableFunctions.UseCases.FraudDetection.Clients
{
    public class FakeFraudDetectionService
    {
        private readonly IFraudDetectionService _fraudDetectionService;

        public FakeFraudDetectionService(IFraudDetectionService fraudDetectionService)
        {
            _fraudDetectionService = fraudDetectionService;
        }

        [FunctionName(nameof(FakeFraudDetectionService))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] AuditRecord auditRecord,
            ILogger log)
        {
            // In real life, this would be 3rd party service that analyses the audit record.
            // It will only return an ID that can be used to correlate the result once the 
            // analysis is complete and the webhook is called.
            
            // Here we fake the analysis result and call the webhook immediately.
            var fraudResult = new FraudResult { RecordId = Guid.NewGuid().ToString(), IsSuspiciousTransaction = false };
            await _fraudDetectionService.CallWebhookAsync(fraudResult);

            return new OkObjectResult(fraudResult.RecordId);
        }
    }
}
